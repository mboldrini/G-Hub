@echo off
REM Quick launcher for PrDashboard from publish directory

if not exist "publish\PrDashboard.exe" (
    echo Error: PrDashboard.exe not found in publish directory
    echo.
    echo Please build first:
    echo   build.bat
    echo.
    pause
    exit /b 1
)

echo Starting PrDashboard...
cd publish
start PrDashboard.exe
cd ..
