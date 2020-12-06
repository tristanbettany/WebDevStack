# WebDevStack

A web development stack using linux docker containers catered towards windows hosts, to prove you dont need a macintosh to develop on. ;)

## Current Stack Information

- nginx - v1.19
- mysql - v8.0
- php - v7.0, v7.1, v7.2, v7.3, v7.4
- mailhog - latest
- node - lts

## Setup

- Point your `REPO_DIR` at a relative path to a folder containing your projects, this could be outside of this repo e.g. `../Projects`
- Create your vhosts to host each of your projects in `containers\nginx\vhosts`
    - Each domain should be a subdomain of `dev.test`, see `welcome.conf` for an example
    - PHP FPM in each vhost should point towards the correct container for the php version you want, see `welcome.conf` for an example
- Change your hosts file in `C:\Windows\System32\drivers\etc` to have your vhosts domains point to `127.0.0.1`
- In powershell type, `.\app.bat build`, this takes a while however you should only have to do it once

## Daily Usage

- Depending on what project you are working on at the time change the `WORKING_DIR` and the `CLI_PHP_CONTAINER` for the php version you need in the `.env` file
    - You DO NOT! need to rebuild or restart the stack after this, it is soley used to direct commands in `app.bat` to the right locations
- `.\app.bat start` to bring up the stack
- `.\app.bat stop` to bring down the stack
- You can access the php containers cli with `.\app.bat bash`
- You might add new projects and not want to re-build, see below on how, its quick and easy
- With a dedicated container for node you can access this by running `.\app.bat bash-node` and install/compile js with npm

## SSH Keys

Most of the time it is conveniant to have your containers match the keys you already have on your host, as containers are disposable and you ideally dont want to create new keys all the time. 
On windows this prooves tricky as it seems it isnt just a case of setting a volume to sync the directories. Permissions of the files are then too open. To workaround this, I have synced each container 
with a volume from the hosts SSH folder to `/ssh` in the container. There is then a script added to the container which copies the files to the right place and chmods them.
This script is ran after build via `docker exec`. You can run the script again if needed using `./app.bat keys`.

## Sending Email

Mailhog is used as an SMTP server. By setting your applications SMTP server to the details below you can send an email to anyone and they will
automatically be caught by mailhog. You can view the emails in the UI at `localhost:8025` 

- SMTP host: `mail`
- SMTP port: `1025`

(TIP: mailhog has an API too which you should be able to use in tests to assert if an email was sent and recieved)

## Adding new vhosts without re-building the stack

- Create your vhost for your your project in `containers\nginx\vhosts`
    - Your domain should be a subdomain of `dev.test`, see `welcome.conf` for an example
    - PHP FPM in your vhost should point towards the correct container for the php version you want, see `welcome.conf` for an example
- Just run `.\app.bat restart`, no need to waste your time rebuilding

## SSL

The ssl for the domain is a self signed certificate for `*.dev.test`, this is why using a subdomain of `dev.test` is needed for your vhosts.
As default the SSL will show a warning in your browser when visiting your projects, to fix this do the following:

- Find the `Manage Certificates` link in your browser settings, this should open a windows Dialog box
- Click the `Trusted Root Certification Authorities` Tab, and click the Import button
- Select the `ssl.cer` file in this repo, next through the wizard and accept any warnings
- Reboot all running browsers

 ## Regenerating SSL certs
 
 If for some reason you need to regenerate them the command is this:
 
```
openssl req -x509 -nodes -days 365 -subj "/C=CA/ST=QC/O=Company, Inc./CN=*.dev.test" \ 
-addext "subjectAltName=DNS:*.dev.test" -newkey rsa:2048 -keyout containers/nginx/ssl/ssl.key \
-out containers/nginx/ssl/ssl.crt
```

Dont forget to also export the new `ssl.cer` file from the browser after doing that and re-import it to `Trusted Root Certification Authorities`