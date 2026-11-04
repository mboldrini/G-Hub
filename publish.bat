@echo off
REM Ultra-simple publish script - single dotnet command

setlocal

echo Building and publishing PrDashboard...
echo.

REM Clean output
if exist publish rmdir /s /q publish

REM Publish (includes restore and build)
dotnet publish PrDashboard\PrDashboard.csproj ^
    -c Release ^
    -r win-x64 ^
    -o publish ^
    --self-contained true ^
    -p:PublishSingleFile=true ^
    -p:IncludeNativeLibrariesForSelfExtract=true ^
    -p:EnableCompressionInSingleFile=true ^
    -p:DebugType=None ^
    -p:DebugSymbols=false ^
    /clp:NoSummary

if %ERRORLEVEL% neq 0 (
    echo.
    echo [ERROR] Build failed!
    pause
    exit /b 1
)

echo.
echo =====================================
echo Build completed successfully!
echo =====================================
echo.
echo Output: publish\PrDashboard.exe
echo.
echo To run:
echo   cd publish
echo   PrDashboard.exe
echo.

endlocal
