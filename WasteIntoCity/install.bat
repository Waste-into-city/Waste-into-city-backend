@echo off
setlocal

rem Получаем текущую директорию, откуда запущен скрипт
set CURRENT_DIR=%~dp0

rem Извлекаем букву диска из пути
for %%i in ("%CURRENT_DIR:~0,2%") do set USB_DRIVE=%%i

if not defined USB_DRIVE (
    echo Флешка не найдена.
    exit /b
)

rem Указываем папку назначения на диске C
set DESTINATION=C:\WIC-Installer

rem Создаём папку, если она не существует
if not exist "%DESTINATION%" (
    mkdir "%DESTINATION%"
)

rem Переносим файлы с флешки
xcopy "%USB_DRIVE%\*.*" "%DESTINATION%\" /S /I /Y

rem Переходим в папку с скриптами
cd /d "%DESTINATION%"

rem Запускаем скрипты (например, скрипты с расширением .bat)
start "" "runner.bat"

endlocal


