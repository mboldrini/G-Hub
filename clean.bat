@echo off
echo =====================================
echo  PrDashboard Clean Script
echo =====================================
echo.

echo [1/4] Killing running app...
taskkill /F /IM PrDashboard.exe >nul 2>&1
if %errorlevel%==0 (echo   - PrDashboard.exe killed) else (echo   - Not running)

echo [2/4] Removing publish directory...
if exist "publish\" (
    rmdir /s /q "publish"
    echo   - publish\ removed
) else (
    echo   - publish\ not found
)

echo [3/4] Removing WebView2 cache...
if exist "%LOCALAPPDATA%\PrDashboard\EBWebView\" (
    rmdir /s /q "%LOCALAPPDATA%\PrDashboard\EBWebView"
    echo   - WebView2 cache removed
) else (
    echo   - WebView2 cache not found
)

echo [4/4] Removing bin and obj...
if exist "PrDashboard\bin\" (
    rmdir /s /q "PrDashboard\bin"
    echo   - PrDashboard\bin\ removed
)
if exist "PrDashboard\obj\" (
    rmdir /s /q "PrDashboard\obj"
    echo   - PrDashboard\obj\ removed
)
if exist "PrDashboard.Tests\bin\" (
    rmdir /s /q "PrDashboard.Tests\bin"
    echo   - PrDashboard.Tests\bin\ removed
)
if exist "PrDashboard.Tests\obj\" (
    rmdir /s /q "PrDashboard.Tests\obj"
    echo   - PrDashboard.Tests\obj\ removed
)

echo.
echo =====================================
echo  Clean complete. Run build.bat to rebuild.
echo =====================================
