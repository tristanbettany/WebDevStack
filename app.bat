@ECHO OFF

FOR /f "delims== tokens=1,2" %%G IN (.env) DO SET %%G=%%H

CALL :CASE_%1
IF ERRORLEVEL 1 CALL :DEFAULT_CASE

ECHO Done!
EXIT /B

:CASE_remove
    ECHO Removing...
    docker-compose down -v
    GOTO END_CASE

:CASE_rebuild
    ECHO Removing...
    docker-compose down -v
    ECHO Building...
    docker-compose up -d --build
    GOTO CASE_keys
    GOTO END_CASE

:CASE_build
    ECHO Building...
    docker-compose up -d --build
    GOTO CASE_keys
    GOTO END_CASE

:CASE_keys
    docker container exec -w /root php70 ./keys.sh 
    docker container exec -w /root php71 ./keys.sh 
    docker container exec -w /root php72 ./keys.sh 
    docker container exec -w /root php73 ./keys.sh 
    docker container exec -w /root php74 ./keys.sh 
    docker container exec -w /root node ./keys.sh 
    GOTO END_CASE

:CASE_restart
    ECHO Stopping...
    docker-compose down
    ECHO Starting...
    docker-compose up -d
    GOTO CASE_keys
    GOTO END_CASE

:CASE_stop
    ECHO Stopping...
    docker-compose down
    GOTO END_CASE

:CASE_start
    ECHO Starting...
    docker-compose up -d
    GOTO CASE_keys
    GOTO END_CASE

:CASE_install
    ECHO Removing temporary application files...
    @RD /S /Q "vendor"
    ECHO Installing composer dependencies...
    docker container exec -w %WORKING_DIR% %CLI_PHP_CONTAINER% composer install
    GOTO END_CASE

:CASE_update
    ECHO Updating composer dependencies...
    docker container exec -w %WORKING_DIR% %CLI_PHP_CONTAINER% composer update
    GOTO END_CASE

:CASE_dump
    ECHO Dumping autoload...
    docker container exec -w %WORKING_DIR% %CLI_PHP_CONTAINER% composer dump-autoload
    GOTO END_CASE

:CASE_laravel-key
    ECHO Generating Key...
    docker container exec -w %WORKING_DIR% %CLI_PHP_CONTAINER% php artisan key:generate
    GOTO END_CASE

:CASE_web-stop
    ECHO Stopping Nginx...
    docker-compose stop nginx
    GOTO END_CASE

:CASE_web-start
    ECHO Starting Nginx...
    docker-compose start nginx
    GOTO END_CASE

:CASE_web-restart
    ECHO Re-starting Nginx...
    docker-compose stop nginx
    docker-compose start nginx
    GOTO END_CASE

:CASE_bash
    docker container exec -it -w %WORKING_DIR% %CLI_PHP_CONTAINER% bash
    GOTO END_CASE

:CASE_bash-node
    docker container exec -it -w %WORKING_DIR% %CLI_NODE_CONTAINER% bash
    GOTO END_CASE

:DEFAULT_CASE
    ECHO Unknown function %1
    GOTO END_CASE
:END_CASE
    GOTO :EOF
