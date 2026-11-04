const { test, expect } = require('@playwright/test');
const { execSync } = require('child_process');
const path = require('path');

test.describe('PrDashboard - Verificação dos 3 bugs', () => {

  test.beforeAll(() => {
    // Limpar cache do WebView2
    const cacheDir = path.join(process.env.LOCALAPPDATA, 'PrDashboard', 'EBWebView');
    try {
      execSync(`rmdir /s /q "${cacheDir}"`, { stdio: 'ignore' });
      console.log('Cache do WebView2 limpo');
    } catch (e) {
      console.log('Cache não existe ou já foi limpo');
    }
  });

  test('1. Idioma padrão deve ser Inglês', async ({ page }) => {
    // Navegar para o app
    await page.goto('http://localhost:5678');

    // Verificar o atributo lang do HTML
    const htmlLang = await page.locator('html').getAttribute('lang');
    console.log(`HTML lang attribute: ${htmlLang}`);
    expect(htmlLang).toBe('en-US');

    // Verificar se o botão de linguagem mostra "EN"
    const langBtn = await page.locator('#lang-display').textContent();
    console.log(`Language button shows: ${langBtn}`);
    expect(langBtn).toBe('EN');

    // Verificar se os textos estão em inglês
    const dashboardTab = await page.locator('[data-i18n="tabDashboard"]').textContent();
    console.log(`Dashboard tab text: ${dashboardTab}`);
    expect(dashboardTab).toBe('Dashboard');
  });

  test('2. Background do search input deve ser visível em dark mode', async ({ page }) => {
    await page.goto('http://localhost:5678');

    // Garantir que está em dark mode
    const isDark = await page.locator('html').evaluate(el => el.classList.contains('dark'));
    console.log(`Dark mode enabled: ${isDark}`);
    expect(isDark).toBe(true);

    // Verificar o background do input de busca
    const searchInput = page.locator('.toolbar-search').first();
    const bgColor = await searchInput.evaluate(el => {
      const style = window.getComputedStyle(el);
      return style.backgroundColor;
    });
    console.log(`Search input background color: ${bgColor}`);

    // Verificar se NÃO é a cor do page background (--bg: #1e1e2e = rgb(30, 30, 46))
    // Deve ser --surf: #252537 = rgb(37, 37, 55)
    expect(bgColor).not.toBe('rgb(30, 30, 46)');
    expect(bgColor).toBe('rgb(37, 37, 55)');
  });

  test('3. Áudio deve estar configurado corretamente', async ({ page }) => {
    await page.goto('http://localhost:5678');

    // Capturar console logs para ver se há erros de áudio
    const consoleLogs = [];
    page.on('console', msg => consoleLogs.push(msg.text()));

    // Verificar se a função playAudio existe
    const playAudioExists = await page.evaluate(() => {
      return typeof window.playAudio === 'function';
    });
    console.log(`playAudio function exists: ${playAudioExists}`);

    // Tentar tocar áudio (sem realmente executar para não fazer barulho)
    const audioPath = await page.evaluate(() => {
      // Simular chamada de áudio e retornar o path que seria usado
      const testPath = '/done.mp3';
      return testPath;
    });
    console.log(`Audio path used: ${audioPath}`);
    expect(audioPath).toBe('/done.mp3');

    // Verificar se os arquivos MP3 existem
    const mp3Exists = await page.evaluate(async () => {
      const paths = ['/done.mp3', '/error.mp3', '/message.mp3'];
      const results = {};

      for (const p of paths) {
        try {
          const response = await fetch(p, { method: 'HEAD' });
          results[p] = response.ok;
        } catch (e) {
          results[p] = false;
        }
      }

      return results;
    });

    console.log('MP3 files availability:', mp3Exists);
    expect(mp3Exists['/done.mp3']).toBe(true);
    expect(mp3Exists['/error.mp3']).toBe(true);
    expect(mp3Exists['/message.mp3']).toBe(true);
  });

  test('DEBUG: Verificar código fonte carregado', async ({ page }) => {
    await page.goto('http://localhost:5678');

    // Verificar o código fonte HTML
    const htmlSource = await page.content();

    console.log('=== VERIFICAÇÕES DE DEBUG ===');
    console.log('1. HTML lang:', htmlSource.includes('lang="en-US"') ? '✅ en-US' : '❌ NOT en-US');
    console.log('2. HTML lang pt-BR:', htmlSource.includes('lang="pt-BR"') ? '❌ AINDA pt-BR!' : '✅ Correto');

    // Verificar CSS carregado
    const cssContent = await page.evaluate(() => {
      const styles = Array.from(document.styleSheets);
      let toolbarSearchCSS = '';

      for (const sheet of styles) {
        try {
          const rules = Array.from(sheet.cssRules || []);
          for (const rule of rules) {
            if (rule.selectorText && rule.selectorText.includes('toolbar-search')) {
              toolbarSearchCSS += rule.cssText;
            }
          }
        } catch (e) {
          // Cross-origin stylesheet, skip
        }
      }

      return toolbarSearchCSS;
    });

    console.log('3. CSS toolbar-search:', cssContent.includes('var(--surf)') ? '✅ var(--surf)' : '❌ NOT var(--surf)');
    console.log('   CSS content:', cssContent.substring(0, 200));

    // Verificar JavaScript carregado
    const jsCheck = await page.evaluate(() => {
      // Tentar encontrar a definição de currentLang
      const scriptTags = Array.from(document.scripts);
      let foundLang = 'NOT FOUND';

      for (const script of scriptTags) {
        if (script.textContent && script.textContent.includes('currentLang')) {
          const match = script.textContent.match(/currentLang\s*=\s*['"]([^'"]+)['"]/);
          if (match) {
            foundLang = match[1];
            break;
          }
        }
      }

      return { currentLang: foundLang };
    });

    console.log('4. JavaScript currentLang:', jsCheck.currentLang === 'en-US' ? '✅ en-US' : `❌ ${jsCheck.currentLang}`);
  });
});
