@echo off
REM Quick build launcher for PrDashboard
REM Kills app, cleans ALL caches, and builds fresh

echo ========================================
echo  PrDashboard - Clean Build
echo ========================================
echo.
echo This will:
echo  - Kill PrDashboard.exe if running
echo  - Clean WebView2 cache
echo  - Clean NuGet cache
echo  - Clean bin/obj/publish folders
echo  - Build 100%% fresh
echo.

powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1" %*

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo  Ready to run!
    echo ========================================
    echo.
    echo The app was completely rebuilt.
    echo All caches were cleared.
    echo.
    echo Press any key to launch the app...
    pause >nul
    start "" "%~dp0publish\PrDashboard.exe"
)
