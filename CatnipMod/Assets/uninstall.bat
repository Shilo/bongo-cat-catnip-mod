@echo off
cd /d "%~dp0"
echo Uninstalling Bongo Cat Catnip Mod...
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
        echo Bongo Cat is currently running. Closing it to ensure a clean uninstallation...
        taskkill /F /IM BongoCat.exe >NUL 2>&1
        timeout /t 1 /nobreak >NUL
        echo.
    )
)

echo Removing Catnip Mod files from: %GAME_DIR%
echo.

set "ERRORS=0"

if exist "%GAME_DIR%\winhttp.dll" (
    del /F /Q "%GAME_DIR%\winhttp.dll" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to remove winhttp.dll
        set /a ERRORS+=1
    ) else if exist "%GAME_DIR%\winhttp.dll" (
        echo [ERROR] Failed to remove winhttp.dll
        set /a ERRORS+=1
    ) else (
        echo [OK] Removed winhttp.dll
    )
) else (
    echo [INFO] winhttp.dll not found ^(may already be removed^)
)

if exist "%GAME_DIR%\doorstop_config.ini" (
    del /F /Q "%GAME_DIR%\doorstop_config.ini" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to remove doorstop_config.ini
        set /a ERRORS+=1
    ) else if exist "%GAME_DIR%\doorstop_config.ini" (
        echo [ERROR] Failed to remove doorstop_config.ini
        set /a ERRORS+=1
    ) else (
        echo [OK] Removed doorstop_config.ini
    )
) else (
    echo [INFO] doorstop_config.ini not found ^(may already be removed^)
)

if exist "%GAME_DIR%\CatnipMod" (
    rmdir /S /Q "%GAME_DIR%\CatnipMod" >nul 2>&1
    if errorlevel 1 (
        echo [ERROR] Failed to remove CatnipMod folder
        set /a ERRORS+=1
    ) else if exist "%GAME_DIR%\CatnipMod" (
        echo [ERROR] Failed to remove CatnipMod folder
        set /a ERRORS+=1
    ) else (
        echo [OK] Removed CatnipMod folder
    )
) else (
    echo [INFO] CatnipMod folder not found ^(may already be removed^)
)

echo.
if %ERRORS% equ 0 (
    echo Uninstallation complete.
) else (
    echo Uninstallation failed with %ERRORS% error^(s^)!
    echo Please check the errors above and try again.
)
echo.
pause
