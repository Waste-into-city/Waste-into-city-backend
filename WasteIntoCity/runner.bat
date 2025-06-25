@echo off
SETLOCAL

:: Проверка, установлен ли Docker
echo Проверка Docker...
docker --version >nul 2>&1
IF %ERRORLEVEL% NEQ 0 (
    echo Docker не найден. Начинаем установку...

    :: Скачиваем установщик Docker Desktop
    echo Скачивание установщика Docker Desktop...
    powershell -Command "Invoke-WebRequest -Uri https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe -OutFile DockerInstaller.exe"

    :: Запускаем установку в тихом режиме
    echo Установка Docker...
    start /wait DockerInstaller.exe install --quiet

    :: Проверка установки
    docker --version >nul 2>&1
    IF %ERRORLEVEL% NEQ 0 (
        echo Ошибка установки Docker. Завершение.
        pause
        exit /b 1
    )

    echo Docker успешно установлен.
) ELSE (
    echo Docker уже установлен.
)

:: Создание и установка HTTPS-сертификата
echo Создание и установка HTTPS-сертификата...
powershell -ExecutionPolicy Bypass -File aspnet-dev-certificate.ps1

:: Запуск docker-compose
echo Запуск docker-compose...
docker-compose up

echo Установка завершена. Приложение доступно по адресу https://localhost:8081
pause