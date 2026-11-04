#!/usr/bin/env pwsh
#
# Script de verificação dos 3 bugs corrigidos
#

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host " Verificação dos 3 Bugs Corrigidos" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

$Passed = 0
$Failed = 0

# Bug 1: Idioma padrão
Write-Host "[1/3] Verificando idioma padrão..." -ForegroundColor Yellow

$indexHtml = Get-Content "PrDashboard\Web\index.html" -Raw
if ($indexHtml -match 'lang="en-US"') {
    Write-Host "  ✅ HTML tem lang='en-US'" -ForegroundColor Green
    $Passed++
} else {
    Write-Host "  ❌ HTML NÃO tem lang='en-US'" -ForegroundColor Red
    $Failed++
}

if ($indexHtml -match 'lang="pt-BR"') {
    Write-Host "  ❌ HTML AINDA tem lang='pt-BR'!" -ForegroundColor Red
    $Failed++
} else {
    Write-Host "  ✅ HTML não tem lang='pt-BR'" -ForegroundColor Green
    $Passed++
}

$appJs = Get-Content "PrDashboard\Web\app.js" -Raw
if ($appJs -match "currentLang='en-US'") {
    Write-Host "  ✅ app.js tem currentLang='en-US'" -ForegroundColor Green
    $Passed++
} else {
    Write-Host "  ❌ app.js NÃO tem currentLang='en-US'" -ForegroundColor Red
    $Failed++
}

Write-Host ""

# Bug 2: Background do search
Write-Host "[2/3] Verificando background do search..." -ForegroundColor Yellow

$appCss = Get-Content "PrDashboard\Web\app.css" -Raw
if ($appCss -match '\.toolbar-search\{[^}]*background:var\(--surf\)') {
    Write-Host "  ✅ CSS tem background:var(--surf)" -ForegroundColor Green
    $Passed++
} else {
    Write-Host "  ❌ CSS NÃO tem background:var(--surf)" -ForegroundColor Red

    # Verificar qual background está sendo usado
    if ($appCss -match '\.toolbar-search\{[^}]*background:var\(--([a-z]+)\)') {
        Write-Host "  ℹ️  CSS usa: var(--$($Matches[1]))" -ForegroundColor Yellow
    }
    $Failed++
}

Write-Host ""

# Bug 3: Caminhos de áudio
Write-Host "[3/3] Verificando caminhos de áudio..." -ForegroundColor Yellow

$audioPathsCorrect = 0
$audioPathsWrong = 0

if ($appJs -match "playAudio\('/done\.mp3'") {
    Write-Host "  ✅ playAudio usa '/done.mp3'" -ForegroundColor Green
    $audioPathsCorrect++
} else {
    Write-Host "  ❌ playAudio NÃO usa '/done.mp3'" -ForegroundColor Red
    $audioPathsWrong++
}

if ($appJs -match "playAudio\('/error\.mp3'") {
    Write-Host "  ✅ playAudio usa '/error.mp3'" -ForegroundColor Green
    $audioPathsCorrect++
} else {
    Write-Host "  ❌ playAudio NÃO usa '/error.mp3'" -ForegroundColor Red
    $audioPathsWrong++
}

if ($appJs -match "playAudio\('/message\.mp3'") {
    Write-Host "  ✅ playAudio usa '/message.mp3'" -ForegroundColor Green
    $audioPathsCorrect++
} else {
    Write-Host "  ❌ playAudio NÃO usa '/message.mp3'" -ForegroundColor Red
    $audioPathsWrong++
}

# Verificar se ainda tem paths errados
if ($appJs -match "playAudio\('/audio/") {
    Write-Host "  ❌ AINDA tem '/audio/' paths!" -ForegroundColor Red
    $audioPathsWrong++
}

if ($audioPathsCorrect -eq 3 -and $audioPathsWrong -eq 0) {
    Write-Host "  ✅ Todos os paths de áudio corretos" -ForegroundColor Green
    $Passed++
} else {
    Write-Host "  ❌ Alguns paths de áudio incorretos" -ForegroundColor Red
    $Failed++
}

# Verificar arquivos MP3
Write-Host ""
Write-Host "  Verificando arquivos MP3..." -ForegroundColor Yellow
$mp3Files = @("PrDashboard\done.mp3", "PrDashboard\error.mp3", "PrDashboard\message.mp3")
foreach ($mp3 in $mp3Files) {
    if (Test-Path $mp3) {
        $size = (Get-Item $mp3).Length
        if ($size -gt 50) {
            Write-Host "    ✅ $mp3 existe ($size bytes)" -ForegroundColor Green
            $Passed++
        } else {
            Write-Host "    ⚠️  $mp3 existe mas é muito pequeno ($size bytes)" -ForegroundColor Yellow
            $Failed++
        }
    } else {
        Write-Host "    ❌ $mp3 NÃO existe!" -ForegroundColor Red
        $Failed++
    }
}

Write-Host ""
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host " Resultado da Verificação" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "  ✅ Passou: $Passed checks" -ForegroundColor Green
Write-Host "  ❌ Falhou: $Failed checks" -ForegroundColor Red
Write-Host ""

if ($Failed -eq 0) {
    Write-Host "🎉 TUDO CORRETO! Agora gere o build." -ForegroundColor Green
    exit 0
} else {
    Write-Host "⚠️  AINDA TEM PROBLEMAS! Verifique os erros acima." -ForegroundColor Red
    exit 1
}
