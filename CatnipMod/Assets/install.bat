@echo off
cd /d "%~dp0"
echo Installing Bongo Cat Catnip Mod...
echo.

set "GAME_DIR=C:\Program Files (x86)\Steam\steamapps\common\BongoCat"

if not exist "%GAME_DIR%\BongoCat.exe" (
    echo Error: Bongo Cat not found at default location.
    echo Please edit this file and set GAME_DIR to your Bongo Cat installation path.
    pause
    exit /b 1
)

if exist "%GAME_DIR%\winhttp.dll" (
    tasklist /FI "IMAGENAME eq BongoCat.exe" /NH 2>NUL | findstr /R "BongoCat.exe" >NUL
    if not errorlevel 1 (
        echo Bongo Cat is currently running. Closing it to ensure a clean re-installation...
        taskkill /F /IM BongoCat.exe >NUL 2>&1
        timeout /t 1 /nobreak >NUL
        echo.
    )
)

echo Copying Catnip Mod files to: %GAME_DIR%
echo.

set "ERRORS=0"

if exist "winhttp.dll" (
    copy /Y "winhttp.dll" "%GAME_DIR%\" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to copy winhttp.dll
        set /a ERRORS+=1
    ) else if not exist "%GAME_DIR%\winhttp.dll" (
        echo [ERROR] Failed to copy winhttp.dll
        set /a ERRORS+=1
    ) else (
        echo [OK] winhttp.dll
    )
) else (
    echo [ERROR] winhttp.dll not found!
    set /a ERRORS+=1
)

if exist "doorstop_config.ini" (
    copy /Y "doorstop_config.ini" "%GAME_DIR%\" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to copy doorstop_config.ini
        set /a ERRORS+=1
    ) else if not exist "%GAME_DIR%\doorstop_config.ini" (
        echo [ERROR] Failed to copy doorstop_config.ini
        set /a ERRORS+=1
    ) else (
        echo [OK] doorstop_config.ini
    )
) else (
    echo [ERROR] doorstop_config.ini not found!
    set /a ERRORS+=1
)

if exist "CatnipMod" (
    xcopy /E /I /Y "CatnipMod" "%GAME_DIR%\CatnipMod" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to copy CatnipMod folder
        set /a ERRORS+=1
    ) else if not exist "%GAME_DIR%\CatnipMod" (
        echo [ERROR] Failed to copy CatnipMod folder
        set /a ERRORS+=1
    ) else (
        echo [OK] CatnipMod folder
    )
) else (
    echo [ERROR] CatnipMod folder not found!
    set /a ERRORS+=1
)

echo.
if %ERRORS% equ 0 (
    echo Installation complete.
    echo Launch Bongo Cat through Steam to verify the mod is working.
) else (
    echo Installation failed with %ERRORS% error^(s^)!
    echo Please check the errors above and try again.
)
echo.
pause

