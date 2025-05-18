@echo off
start /wait docker-installer.exe install

docker load -i myapp.tar
docker load -i mssql.tar
docker compose up -d

pause
