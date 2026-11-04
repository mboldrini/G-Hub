const fs = require('fs');
const path = require('path');

console.log('='.repeat(50));
console.log(' VERIFICAÇÃO DO BUILD COMPILADO');
console.log('='.repeat(50));
console.log('');

let passed = 0;
let failed = 0;

// Verificar se o DLL compilado existe
const dllPath = path.join(__dirname, 'PrDashboard', 'obj', 'Release', 'net8.0-windows', 'win-x64', 'PrDashboard.dll');
if (!fs.existsSync(dllPath)) {
    console.log('❌ DLL não encontrado:', dllPath);
    process.exit(1);
}

const dllContent = fs.readFileSync(dllPath, 'utf8');

// Bug 1: Verificar idioma
console.log('[1/3] Verificando idioma...');
if (dllContent.includes('lang="en-US"')) {
    console.log('  ✅ HTML tem lang="en-US"');
    passed++;
} else {
    console.log('  ❌ HTML NÃO tem lang="en-US"');
    failed++;
}

if (dllContent.includes('lang="pt-BR"')) {
    console.log('  ❌ HTML AINDA tem lang="pt-BR"!');
    failed++;
} else {
    console.log('  ✅ HTML não tem lang="pt-BR"');
    passed++;
}

console.log('');

// Bug 2: Verificar CSS
console.log('[2/3] Verificando CSS do search...');
if (dllContent.includes('toolbar-search') && dllContent.includes('background:var(--surf)')) {
    console.log('  ✅ CSS tem background:var(--surf)');
    passed++;
} else {
    console.log('  ❌ CSS NÃO tem background:var(--surf)');
    failed++;
}

console.log('');

// Bug 3: Verificar paths de áudio
console.log('[3/3] Verificando paths de áudio...');
const audioChecks = [
    { pattern: "playAudio('/done.mp3'", name: '/done.mp3' },
    { pattern: "playAudio('/error.mp3'", name: '/error.mp3' },
    { pattern: "playAudio('/message.mp3'", name: '/message.mp3' }
];

let audioOk = 0;
for (const check of audioChecks) {
    if (dllContent.includes(check.pattern)) {
        console.log(`  ✅ playAudio usa '${check.name}'`);
        audioOk++;
    } else {
        console.log(`  ❌ playAudio NÃO usa '${check.name}'`);
    }
}

if (audioOk === 3) {
    console.log('  ✅ Todos os paths de áudio corretos');
    passed++;
} else {
    console.log('  ❌ Alguns paths de áudio incorretos');
    failed++;
}

// Verificar paths errados
if (dllContent.includes("playAudio('/audio/")) {
    console.log('  ❌ AINDA tem /audio/ paths!');
    failed++;
} else {
    console.log('  ✅ Não tem /audio/ paths');
    passed++;
}

console.log('');

// Verificar MP3 files
console.log('[4/3] Verificando arquivos MP3...');
const mp3Files = ['done.mp3', 'error.mp3', 'message.mp3'];
for (const mp3 of mp3Files) {
    const mp3Path = path.join(__dirname, 'PrDashboard', mp3);
    if (fs.existsSync(mp3Path)) {
        const size = fs.statSync(mp3Path).size;
        if (size > 50) {
            console.log(`  ✅ ${mp3} existe (${size} bytes)`);
            passed++;
        } else {
            console.log(`  ⚠️  ${mp3} existe mas é muito pequeno (${size} bytes)`);
        }
    } else {
        console.log(`  ❌ ${mp3} NÃO existe!`);
        failed++;
    }
}

console.log('');
console.log('='.repeat(50));
console.log(' RESULTADO');
console.log('='.repeat(50));
console.log('');
console.log(`  ✅ Passou: ${passed} checks`);
console.log(`  ❌ Falhou: ${failed} checks`);
console.log('');

if (failed === 0) {
    console.log('🎉 TUDO CORRETO NO BUILD!');
    console.log('');
    console.log('Agora teste manualmente:');
    console.log('1. Abra publish\\PrDashboard.exe');
    console.log('2. Verifique se inicia em INGLÊS');
    console.log('3. Vá para Dashboard e veja se o search tem fundo visível');
    console.log('4. Vá em Settings > Notifications e teste os sons');
    process.exit(0);
} else {
    console.log('⚠️  AINDA TEM PROBLEMAS! Verifique os erros acima.');
    process.exit(1);
}
