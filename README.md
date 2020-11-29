# WebDevStack

A web development stack using linux docker containers catered towards windows hosts, to prove you dont need a macintosh to develop on. ;)

## Setup

- Point your `REPO_DIR` at a relative path to a folder containing your projects, this could be outside of this repo e.g. `../Projects`
- Create your vhosts to host each of your projects in `containers\nginx\vhosts`
    - Each domain should be a subdomain of `dev.test`, see `welcome.conf` for an example
    - PHP FPM in each vhost should point towards the correct container for the php version you want, see `welcome.conf` for an example
- Change your hosts file in `C:\Windows\System32\drivers\etc` to have your vhosts domains point to `127.0.0.1`
- In powershell type, `.\app.bat build`, this takes a while however you should only have to do it once

## Daily Usage

- Depending on what project you are working on at the time change the `WORKING_DIR` and the `CLI_PHP_CONTAINER` for the phjp version you need in the `.env` file
    - You DO NOT! need to rebuild the stack after this, you only need to do `.\app.bat restart`
- `.\app.bat start` to bring up the stack
- `.\app.bat stop` to bring down the stack
- You can access the php containers cli with `.\app.bat bash`

## Adding new vhosts after building

- Create your vhost for your your project in `containers\nginx\vhosts`
    - Your domain should be a subdomain of `dev.test`, see `welcome.conf` for an example
    - PHP FPM in your vhost should point towards the correct container for the php version you want, see `welcome.conf` for an example

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
openssl req -x509 -nodes -days 365 -subj "/C=CA/ST=QC/O=Company, Inc./CN=*.dev.test" -addext "subjectAltName=DNS:*.dev.test" -newkey rsa:2048 -keyout containers/nginx/ssl/ssl.key -out containers/nginx/ssl/ssl.crt
```

Dont forget to also export the new `ssl.cer` file from the browser after doing that and re-import it to `Trusted Root Certification Authorities`