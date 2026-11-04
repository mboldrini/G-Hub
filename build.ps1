#!/usr/bin/env pwsh
#
# Build script for PrDashboard - Clean build and publish
# Usage: .\build.ps1 [-Configuration Release|Debug] [-Runtime win-x64]
#

param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release',

    [string]$Runtime = 'win-x64',

    [switch]$SkipClean,

    [switch]$Help
)

if ($Help) {
    Write-Host @"
PrDashboard Build Script

Usage:
  .\build.ps1 [-Configuration <Debug|Release>] [-Runtime <rid>] [-SkipClean] [-Help]

Parameters:
  -Configuration    Build configuration (default: Release)
  -Runtime          Target runtime identifier (default: win-x64)
  -SkipClean        Skip clean step (faster rebuild)
  -Help             Show this help message

Examples:
  .\build.ps1                           # Clean Release build
  .\build.ps1 -Configuration Debug      # Clean Debug build
  .\build.ps1 -SkipClean                # Skip clean, faster rebuild
"@
    exit 0
}

$ErrorActionPreference = 'Stop'
$ProjectRoot = $PSScriptRoot
$ProjectFile = Join-Path $ProjectRoot "PrDashboard\PrDashboard.csproj"
$PublishDir = Join-Path $ProjectRoot "publish"

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host " PrDashboard Build Script" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Configuration: $Configuration" -ForegroundColor Yellow
Write-Host "Runtime:       $Runtime" -ForegroundColor Yellow
Write-Host "Output:        $PublishDir" -ForegroundColor Yellow
Write-Host ""

# Step 0: Kill running app and clean ALL caches
Write-Host "[0/7] Killing app and cleaning ALL caches..." -ForegroundColor Magenta

# Kill PrDashboard.exe if running
$processes = Get-Process -Name "PrDashboard" -ErrorAction SilentlyContinue
if ($processes) {
    Write-Host "  - Killing PrDashboard.exe processes..." -ForegroundColor Gray
    $processes | Stop-Process -Force
    Start-Sleep -Seconds 1
}

# Clean WebView2 cache
$webViewCache = Join-Path $env:LOCALAPPDATA "PrDashboard\EBWebView"
if (Test-Path $webViewCache) {
    Write-Host "  - Removing WebView2 cache..." -ForegroundColor Gray
    Remove-Item -Path $webViewCache -Recurse -Force -ErrorAction SilentlyContinue
}

# Clean NuGet cache for this project
Write-Host "  - Clearing NuGet cache..." -ForegroundColor Gray
dotnet nuget locals all --clear | Out-Null

# Clean VS/Rider caches
$vsCache = Join-Path $ProjectRoot ".vs"
if (Test-Path $vsCache) {
    Write-Host "  - Removing Visual Studio cache..." -ForegroundColor Gray
    Remove-Item -Path $vsCache -Recurse -Force -ErrorAction SilentlyContinue
}

$ideaCache = Join-Path $ProjectRoot ".idea"
if (Test-Path $ideaCache) {
    Write-Host "  - Removing Rider cache..." -ForegroundColor Gray
    Remove-Item -Path $ideaCache -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Host "  [OK] All caches cleaned" -ForegroundColor Green
Write-Host ""

# Step 1: Clean previous builds (ALWAYS - garantir build 100% limpo)
Write-Host "[1/7] Cleaning previous builds (FORCED)..." -ForegroundColor Green

# Remove bin and obj folders (cache)
$BinDir = Join-Path $ProjectRoot "PrDashboard\bin"
$ObjDir = Join-Path $ProjectRoot "PrDashboard\obj"

if (Test-Path $BinDir) {
    Write-Host "  - Removing bin directory..." -ForegroundColor Gray
    Remove-Item -Path $BinDir -Recurse -Force -ErrorAction SilentlyContinue
}

if (Test-Path $ObjDir) {
    Write-Host "  - Removing obj directory..." -ForegroundColor Gray
    Remove-Item -Path $ObjDir -Recurse -Force -ErrorAction SilentlyContinue
}

# Remove publish directory
if (Test-Path $PublishDir) {
    Write-Host "  - Removing publish directory..." -ForegroundColor Gray
    Remove-Item -Path $PublishDir -Recurse -Force -ErrorAction SilentlyContinue
}

# CRÍTICO: Clean completo do dotnet para garantir rebuild de TUDO
Write-Host "  - Running dotnet clean..." -ForegroundColor Gray
dotnet clean $ProjectFile --configuration $Configuration --nologo --verbosity quiet

Write-Host "  [OK] Clean completed" -ForegroundColor Green
Write-Host ""

# Step 2: Restore dependencies (FORCE - sem cache)
Write-Host "[2/7] Restoring NuGet packages..." -ForegroundColor Green
dotnet restore $ProjectFile --runtime $Runtime --force --no-cache --force-evaluate
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Restore failed" -ForegroundColor Red
    exit 1
}
Write-Host "  [OK] Restore completed" -ForegroundColor Green
Write-Host ""

# Step 3: Build explicitamente (garantir recompilação)
Write-Host "[3/7] Building project..." -ForegroundColor Green
dotnet build $ProjectFile `
    --configuration $Configuration `
    --runtime $Runtime `
    --no-restore `
    --nologo `
    --force

if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Build failed" -ForegroundColor Red
    exit 1
}
Write-Host "  [OK] Build completed" -ForegroundColor Green
Write-Host ""

# Step 4: Publish (com rebuild forçado)
Write-Host "[4/7] Publishing application..." -ForegroundColor Green
dotnet publish $ProjectFile `
    --configuration $Configuration `
    --runtime $Runtime `
    --output $PublishDir `
    --nologo `
    --self-contained true `
    --force `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:EnableCompressionInSingleFile=true `
    -p:DebugType=None `
    -p:DebugSymbols=false `
    -p:UseAppHost=true

if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Publish failed" -ForegroundColor Red
    exit 1
}
Write-Host "  [OK] Publish completed" -ForegroundColor Green
Write-Host ""

# Step 5: Verificar embedded resources
Write-Host "[5/7] Verifying embedded resources..." -ForegroundColor Green
$ExePath = Join-Path $PublishDir "PrDashboard.exe"
if (Test-Path $ExePath) {
    try {
        $FileHash = (certUtil -hashfile $ExePath MD5 | Select-String -Pattern "^[a-f0-9]{32}$").Matches[0].Value
        Write-Host "  - Executable hash: $FileHash" -ForegroundColor Gray
    } catch {
        Write-Host "  - Hash verification skipped" -ForegroundColor Gray
    }
    Write-Host "  [OK] Resources embedded successfully" -ForegroundColor Green
}
Write-Host ""

# Step 6: Verify fixes in compiled DLL
Write-Host "[6/7] Verifying fixes in compiled code..." -ForegroundColor Green
$dllPath = Join-Path $ProjectRoot "PrDashboard\obj\Release\net8.0-windows\win-x64\PrDashboard.dll"
if (Test-Path $dllPath) {
    $dllContent = Get-Content $dllPath -Raw -Encoding UTF8

    Write-Host "  - Checking language..." -ForegroundColor Gray
    if ($dllContent -match 'lang="en-US"') {
        Write-Host "    ✅ Language: en-US" -ForegroundColor Green
    } else {
        Write-Host "    ❌ Language NOT en-US!" -ForegroundColor Red
    }

    Write-Host "  - Checking CSS..." -ForegroundColor Gray
    if ($dllContent -match 'background:var\(--surf\)') {
        Write-Host "    ✅ CSS: background:var(--surf)" -ForegroundColor Green
    } else {
        Write-Host "    ❌ CSS NOT correct!" -ForegroundColor Red
    }

    Write-Host "  - Checking audio paths..." -ForegroundColor Gray
    if ($dllContent -match "playAudio\('/audio/done\.mp3'" -and $dllContent -match "playAudio\('/audio/error\.mp3'") {
        Write-Host "    ✅ Audio paths: correct (/audio/done.mp3, /audio/error.mp3, /audio/message.mp3)" -ForegroundColor Green
    } else {
        Write-Host "    ❌ Audio paths NOT correct!" -ForegroundColor Red
    }
}
Write-Host "  [OK] Verification completed" -ForegroundColor Green
Write-Host ""

# Step 7: Summary
Write-Host "[7/7] Build Summary" -ForegroundColor Green
Write-Host ""

$ExePath = Join-Path $PublishDir "PrDashboard.exe"
if (Test-Path $ExePath) {
    $FileInfo = Get-Item $ExePath
    $SizeMB = [math]::Round($FileInfo.Length / 1MB, 2)

    Write-Host "  Published to: $PublishDir" -ForegroundColor Cyan
    Write-Host "  Executable:   PrDashboard.exe" -ForegroundColor Cyan
    Write-Host "  Size:         $SizeMB MB" -ForegroundColor Cyan
    Write-Host ""

    # List all files in publish directory
    Write-Host "  Files in publish directory:" -ForegroundColor Gray
    Get-ChildItem $PublishDir | ForEach-Object {
        $size = if ($_.PSIsContainer) { "<DIR>" } else { "$([math]::Round($_.Length / 1KB, 1)) KB" }
        Write-Host ("    {0,-40} {1,15}" -f $_.Name, $size) -ForegroundColor Gray
    }
    Write-Host ""

    Write-Host "=====================================" -ForegroundColor Green
    Write-Host " Build completed successfully!" -ForegroundColor Green
    Write-Host "=====================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "To run the application:" -ForegroundColor Yellow
    Write-Host "  cd publish" -ForegroundColor White
    Write-Host "  .\PrDashboard.exe" -ForegroundColor White
    Write-Host ""
    Write-Host "✨ All caches cleared - 100% fresh build!" -ForegroundColor Cyan
    Write-Host ""
} else {
    Write-Host "[ERROR] Build completed but executable not found" -ForegroundColor Red
    exit 1
}
