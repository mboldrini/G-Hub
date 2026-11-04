@echo off
REM Simple build script for PrDashboard
REM Alternative to build.ps1 for quick builds

setlocal

set PROJECT=PrDashboard\PrDashboard.csproj
set OUTPUT=publish
set CONFIGURATION=Release
set RUNTIME=win-x64

echo =====================================
echo  PrDashboard Simple Build
echo =====================================
echo.

REM Clean previous build
if exist "%OUTPUT%" (
    echo [1/4] Cleaning previous build...
    rmdir /s /q "%OUTPUT%" 2>nul
    echo   [OK] Clean completed
) else (
    echo [1/4] No previous build to clean
)
echo.

REM Clean cache
echo [2/4] Cleaning build cache...
dotnet clean "%PROJECT%" --configuration %CONFIGURATION% --nologo
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Clean failed
    exit /b 1
)
echo   [OK] Cache cleaned
echo.

REM Restore
echo [3/4] Restoring packages...
dotnet restore "%PROJECT%" --runtime %RUNTIME% --force --no-cache
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Restore failed
    exit /b 1
)
echo   [OK] Restore completed
echo.

REM Publish
echo [4/4] Publishing application...
dotnet publish "%PROJECT%" ^
    --configuration %CONFIGURATION% ^
    --runtime %RUNTIME% ^
    --output "%OUTPUT%" ^
    --nologo ^
    --self-contained true ^
    -p:PublishSingleFile=true ^
    -p:IncludeNativeLibrariesForSelfExtract=true ^
    -p:EnableCompressionInSingleFile=true ^
    -p:DebugType=None ^
    -p:DebugSymbols=false

if %ERRORLEVEL% neq 0 (
    echo [ERROR] Publish failed
    exit /b 1
)
echo   [OK] Publish completed
echo.

REM Summary
echo =====================================
echo  Build completed successfully!
echo =====================================
echo.
echo Output: %OUTPUT%\PrDashboard.exe
echo.

if exist "%OUTPUT%\PrDashboard.exe" (
    echo To run:
    echo   cd %OUTPUT%
    echo   PrDashboard.exe
    echo.
) else (
    echo [ERROR] Executable not found in output directory
    exit /b 1
)

endlocal
