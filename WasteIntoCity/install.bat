@echo off
SETLOCAL

echo Проверка Docker...
powershell -ExecutionPolicy Bypass -File check-docker.ps1
IF %ERRORLEVEL% NEQ 0 (
    echo Установка Docker требуется. Завершение.
    pause
    exit /b 1
)

echo Создание и установка HTTPS-сертификата...
powershell -ExecutionPolicy Bypass -File aspnet-dev-certificate.ps1

echo Запуск docker-compose...
docker-compose up -d --build

echo Установка завершена. Приложение доступно по адресу https://localhost:8081
pause