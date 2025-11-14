@echo off
cd /d "%~dp0"
echo Installing Bongo Cat Catnip mod...
echo.

set "GAME_DIR=C:\Program Files (x86)\Steam\steamapps\common\BongoCat"

if not exist "%GAME_DIR%\BongoCat.exe" (
    echo Error: Bongo Cat not found at default location.
    echo Please edit this file and set GAME_DIR to your Bongo Cat installation path.
    pause
    exit /b 1
)

if exist "%GAME_DIR%\winhttp.dll" (
    tasklist /FI "IMAGENAME eq BongoCat.exe" /NH 2>NUL | findstr /R "[0-9]" >NUL
    if not errorlevel 1 (
        echo Bongo Cat is currently running. Closing it to ensure a clean re-installation...
        taskkill /F /IM BongoCat.exe >NUL 2>&1
        timeout /t 1 /nobreak >NUL
        echo.
    )
)

echo Copying Catnip mod files to: %GAME_DIR%
echo.

set "ERROR=0"

if exist "BongoCat" (
    xcopy /E /I /Y "BongoCat" "%GAME_DIR%" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to copy mod files
        set "ERROR=1"
    ) else (
        echo [OK] Mod files copied successfully
    )
) else (
    echo [ERROR] BongoCat folder not found!
    set "ERROR=1"
)

echo.
if %ERROR% equ 0 (
    echo Installation complete.
    echo Launch Bongo Cat through Steam to verify the mod is working.
) else (
    echo Installation failed!
    echo Please check the errors above and try again.
)
echo.
pause

