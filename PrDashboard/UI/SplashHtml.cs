namespace PrDashboard.UI;

public static class SplashHtml
{
    public static string Get() => """
        <!DOCTYPE html>
        <html class="dark" lang="en">
        <head>
        <meta charset="utf-8">
        <meta content="width=device-width, initial-scale=1.0" name="viewport">
        <!-- ① wv bridge + stubs: jsReady dispara ANTES de qualquer CDN bloquear -->
        <script>
        function wv(m){try{window.chrome.webview.postMessage(m)}catch(e){}}
        // Estado acumulado enquanto o DOM ainda não está pronto
        var PD={pct:0,text:'Initializing...',phase:'loading',login:'',port:5555,err:'',launchFired:false};
        function updateProgress(p,t){PD.pct=p;if(t)PD.text=t;}
        function updatePort(p){PD.port=p;}
        function setReady(l){
          PD.phase='ready';PD.login=l;
          // Dispara o launch imediatamente, sem esperar o DOM/Tailwind ficar pronto
          var autoLaunch=localStorage.getItem('prd-auto-launch')!=='0';
          var bgMode=localStorage.getItem('prd-bg')==='1';
          if(autoLaunch&&!bgMode){PD.launchFired=true;wv('launch');}
        }
        function setError(m,fixCmd){PD.phase='error';PD.err=m;PD.fixCmd=fixCmd||'';}
        function setStopped(){PD.phase='stopped';}
        function addLog(){}
        wv('jsReady'); // startup pode começar agora
        </script>
        <!-- ② Fontes: non-blocking (não afetam layout) -->
        <link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" rel="stylesheet" media="print" onload="this.media='all'">
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@100..900&family=JetBrains+Mono:wght@100..900&display=swap" rel="stylesheet" media="print" onload="this.media='all'">
        <!-- ③ Tailwind: bloqueante → visual correto; jsReady já foi enviado acima -->
        <script src="https://cdn.tailwindcss.com"></script>
        <script id="tailwind-config">tailwind.config = { darkMode: "class", theme: { extend: { "colors": { "surface-container": "#171f33", "surface-container-lowest": "#060e20", "surface-container-low": "#131b2e", "surface-container-high": "#222a3d", "surface-container-highest": "#2d3449", "surface": "#0b1326", "on-surface": "#dae2fd", "on-surface-variant": "#cbc3d7", "outline": "#958ea0", "error-container": "#93000a", "on-error": "#690005", "error": "#ffb4ab" }, "borderRadius": { "DEFAULT": "0.125rem", "lg": "0.25rem", "xl": "0.5rem", "full": "0.75rem" }, "spacing": { "space-xs": "0.25rem", "space-md": "0.75rem", "space-xl": "1.5rem", "space-sm": "0.5rem", "space-lg": "1rem", "margin": "1rem", "gutter": "0.75rem" }, "fontFamily": { "headline-sm": ["Inter"], "headline-lg": ["Inter"], "body-sm": ["Inter"], "headline-md": ["Inter"], "body-md": ["Inter"], "label-lg": ["JetBrains Mono"], "label-md": ["JetBrains Mono"], "label-sm": ["JetBrains Mono"], "body-lg": ["Inter"] }, "fontSize": { "headline-sm": ["15px", { "lineHeight": "20px", "letterSpacing": "-0.01em", "fontWeight": "600" }], "headline-lg": ["24px", { "lineHeight": "32px", "letterSpacing": "-0.02em", "fontWeight": "600" }], "body-sm": ["12px", { "lineHeight": "16px", "fontWeight": "400" }], "headline-md": ["18px", { "lineHeight": "24px", "letterSpacing": "-0.015em", "fontWeight": "600" }], "body-md": ["13px", { "lineHeight": "18px", "fontWeight": "400" }], "label-lg": ["13px", { "lineHeight": "18px", "letterSpacing": "-0.01em", "fontWeight": "500" }], "label-md": ["11px", { "lineHeight": "14px", "letterSpacing": "0em", "fontWeight": "500" }], "label-sm": ["10px", { "lineHeight": "12px", "letterSpacing": "0.02em", "fontWeight": "500" }], "body-lg": ["14px", { "lineHeight": "20px", "fontWeight": "400" }] } } } };</script>
        <style>
        @keyframes shimmer { 0% { transform: translateX(-120%); } 50% { transform: translateX(80%); } 100% { transform: translateX(260%); } }
        .ghub-accent-checkbox { accent-color: #2ea043 !important; }
        *, *::before, *::after { box-sizing: border-box; }
        html, body { margin: 0; padding: 0; width: 100vw; height: 100vh; overflow: hidden; background: #161b22; }
        ::-webkit-scrollbar { display: none; }
        </style>
        </head>
        <body class="font-body-md text-on-surface select-none">

        <!-- ── Window Titlebar ─────────────────────────────────────────── -->
        <div id="titlebar"
             class="h-[37px] flex items-center justify-between px-3 bg-[#0d1117] border-b border-[#30363d]"
             style="cursor:default"
             onmousedown="if(!event.target.closest('button')){wv('startDrag')}">
          <div class="flex items-center gap-2">
            <div class="relative flex items-center justify-center">
              <span class="material-symbols-outlined text-[#3fb950] text-[15px]">fork_right</span>
              <span class="absolute -top-0.5 -right-0.5 w-1.5 h-1.5 rounded-full bg-[#2ea043] shadow-[0_0_6px_#2ea043]"></span>
            </div>
            <span class="font-label-sm text-[11px] text-[#8b949e] font-medium tracking-wide">PrDashboard</span>
            <span class="text-[10px] font-mono text-[#8b949e]/40">— GitHub PR Live Monitor</span>
          </div>
          <div class="flex items-center">
            <button onclick="wv('minimize')"
                    class="w-9 h-[37px] flex items-center justify-center text-[#8b949e] hover:bg-[#21262d] hover:text-[#f0f6fc] transition-colors"
                    title="Minimize">
              <span class="material-symbols-outlined text-[14px]">minimize</span>
            </button>
            <button onclick="wv('close')"
                    class="w-9 h-[37px] flex items-center justify-center text-[#8b949e] hover:bg-red-900 hover:text-white transition-colors"
                    title="Close">
              <span class="material-symbols-outlined text-[14px]">close</span>
            </button>
          </div>
        </div>

        <!-- ── Card Body ───────────────────────────────────────────────── -->
        <div class="relative p-space-xl flex flex-col gap-space-lg overflow-y-auto"
             style="max-height:calc(100vh - 37px)">

          <!-- Shimmer lines -->
          <div class="absolute inset-x-0 top-0 h-[2px] bg-gradient-to-r from-transparent via-[#2ea043] to-transparent pointer-events-none"></div>
          <div class="absolute inset-x-0 bottom-0 h-[1px] bg-gradient-to-r from-transparent via-[#30363d] to-transparent pointer-events-none"></div>

          <!-- Ambient glows -->
          <div class="absolute w-96 h-96 rounded-full bg-[#238636]/15 blur-3xl pointer-events-none -top-12 -left-12"></div>
          <div class="absolute w-80 h-80 rounded-full bg-[#388bfd]/10 blur-3xl pointer-events-none -bottom-10 -right-10"></div>

          <!-- Header Block -->
          <div class="flex items-start gap-space-lg pt-space-xs relative">
            <div class="relative flex-shrink-0 w-14 h-14 rounded-xl bg-[#0d1117] border border-[#30363d] shadow-md flex items-center justify-center">
              <div class="absolute inset-0 rounded-xl bg-gradient-to-br from-[#238636]/20 via-transparent to-transparent blur-sm"></div>
              <svg class="relative w-8 h-8 drop-shadow-[0_0_10px_rgba(46,160,67,0.6)]" fill="none" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <circle class="stroke-[#3fb950] stroke-[1.8] fill-[#0d1117]" cx="6" cy="6" r="2.5"></circle>
                <circle class="stroke-[#3fb950] stroke-[1.8] fill-[#0d1117]" cx="6" cy="18" r="2.5"></circle>
                <circle class="stroke-[#58a6ff] stroke-[1.8] fill-[#0d1117]" cx="18" cy="8" r="2.5"></circle>
                <path class="stroke-[#3fb950] stroke-[1.8] stroke-linecap-round" d="M6 8.5V15.5"></path>
                <path class="stroke-[#58a6ff] stroke-[1.8] stroke-linecap-round stroke-linejoin-round" d="M18 10.5V12C18 14.2 16.2 16 14 16H8.5"></path>
                <path class="stroke-[#58a6ff] stroke-[1.8] stroke-linecap-round stroke-linejoin-round" d="M11 13.5L8.5 16L11 18.5"></path>
              </svg>
              <span class="absolute -top-1 -right-1 flex h-3 w-3">
                <span class="animate-ping absolute inline-flex h-full w-full rounded-full bg-[#3fb950] opacity-60"></span>
                <span class="relative inline-flex rounded-full h-3 w-3 bg-[#2ea043] shadow-[0_0_8px_#2ea043]"></span>
              </span>
            </div>
            <div class="flex flex-col flex-1 min-w-0 justify-center pt-0.5">
              <div class="flex items-center justify-between gap-space-xs">
                <h1 class="font-headline-lg text-headline-lg text-[#f0f6fc] tracking-tight font-semibold flex items-center gap-space-xs">
                  PrDashboard
                  <span class="text-[11px] font-mono font-normal text-[#8b949e] bg-[#21262d] border border-[#30363d] px-1.5 py-0.5 rounded-full">.NET 8 / gh CLI</span>
                </h1>
                <span class="font-label-sm text-[11px] px-2 py-0.5 rounded bg-[#238636]/20 border border-[#238636]/40 text-[#3fb950] uppercase tracking-wider font-medium">DAEMON</span>
              </div>
              <p class="font-body-sm text-[12px] text-[#8b949e] font-normal mt-0.5 truncate">Local GitHub PR &amp; CI Orchestrator</p>
            </div>
          </div>

          <!-- Status Section -->
          <div class="flex flex-col gap-2 bg-[#0d1117] border border-[#30363d] rounded-lg p-space-md shadow-sm relative" id="status-card">
            <!-- Row 1: dot + texto + % + port -->
            <div class="flex items-center justify-between gap-2">
              <div class="flex items-center gap-space-sm min-w-0">
                <span class="relative flex h-2 w-2 flex-shrink-0">
                  <span class="status-ping animate-ping absolute inline-flex h-full w-full rounded-full bg-[#3fb950] opacity-75"></span>
                  <span class="status-ping relative inline-flex rounded-full h-2 w-2 bg-[#2ea043] shadow-[0_0_6px_#2ea043]"></span>
                </span>
                <span class="font-body-md text-[13px] text-[#e6edf3] font-medium truncate" id="splash-status-text">Initializing...</span>
              </div>
              <div class="flex items-center gap-1.5 flex-shrink-0">
                <span class="font-mono text-[11px] font-semibold text-[#3fb950] transition-colors duration-300 w-9 text-right" id="progress-pct">0%</span>
                <span class="font-label-sm text-[11px] text-[#8b949e] bg-[#21262d] border border-[#30363d] px-1.5 py-0.5 rounded font-mono" id="port-badge">:5555</span>
              </div>
            </div>
            <!-- Row 2: barra de progresso real -->
            <div class="w-full h-2 bg-[#21262d] rounded-full overflow-hidden">
              <div id="progress-bar"
                   class="h-full rounded-full transition-all duration-500 ease-out"
                   style="width:4%;background:linear-gradient(90deg,#238636,#3fb950);box-shadow:0 0 8px rgba(46,160,67,.5)"></div>
            </div>
            <!-- Row 3: log de steps -->
            <div id="step-log" class="flex flex-col gap-0.5 empty:hidden"></div>
          </div>

          <!-- Startup Preferences -->
          <div class="flex flex-col gap-space-sm pt-space-xs border-t border-[#30363d] relative">
            <div class="flex items-center justify-between font-label-sm text-[11px] text-[#8b949e] font-medium px-0.5">
              <span class="uppercase tracking-wider text-[11px] text-[#8b949e] flex items-center gap-1.5">
                <span class="material-symbols-outlined text-[14px] text-[#3fb950]">tune</span>Startup Preferences
              </span>
              <span class="text-[11px] font-mono text-[#8b949e]/80" id="port-label">Port 5555</span>
            </div>
            <div class="flex flex-col gap-1.5">
              <label class="flex items-center justify-between p-space-sm rounded-lg bg-[#0d1117] border border-[#21262d] hover:border-[#30363d] transition-colors cursor-pointer group">
                <div class="flex flex-col pr-space-md">
                  <span class="font-body-md text-[13px] text-[#e6edf3] font-medium group-hover:text-[#3fb950] transition-colors">Launch browser automatically</span>
                  <span class="font-body-sm text-[11px] text-[#8b949e]" id="browser-url-hint">Navigates to http://localhost:5555 on ready</span>
                </div>
                <input type="checkbox" id="auto-launch-check" checked class="ghub-accent-checkbox h-4 w-4 rounded cursor-pointer">
              </label>
              <label class="flex items-center justify-between p-space-sm rounded-lg bg-[#0d1117] border border-[#21262d] hover:border-[#30363d] transition-colors cursor-pointer group">
                <div class="flex flex-col pr-space-md">
                  <span class="font-body-md text-[13px] text-[#e6edf3] font-medium group-hover:text-[#3fb950] transition-colors">Open in background</span>
                  <span class="font-body-sm text-[11px] text-[#8b949e]">Silently initialize daemon</span>
                </div>
                <input type="checkbox" id="bg-check" class="ghub-accent-checkbox h-4 w-4 rounded cursor-pointer">
              </label>
              <label class="flex items-center justify-between p-space-sm rounded-lg bg-[#0d1117] border border-[#21262d] hover:border-[#30363d] transition-colors cursor-pointer group">
                <div class="flex flex-col pr-space-md">
                  <span class="font-body-md text-[13px] text-[#e6edf3] font-medium group-hover:text-[#3fb950] transition-colors">Minimize to System Tray</span>
                  <span class="font-body-sm text-[11px] text-[#8b949e]">Close window sends to tray icon</span>
                </div>
                <input type="checkbox" id="tray-check" checked class="ghub-accent-checkbox h-4 w-4 rounded cursor-pointer">
              </label>
            </div>

            <!-- Action Buttons -->
            <div class="pt-space-xs flex items-center gap-space-sm">
              <button id="launch-btn" type="button" disabled
                      onclick="if(!this.disabled) wv('launch')"
                      class="flex-1 flex items-center justify-center gap-space-sm px-space-lg py-2.5 rounded-lg border border-[rgba(240,246,252,0.1)] text-white font-headline-sm text-[13px] font-semibold shadow-md active:scale-[0.99] transition-all"
                      style="background:#21262d;opacity:.55;cursor:not-allowed">
                <span id="launch-icon" class="material-symbols-outlined text-[17px] animate-spin" style="animation-duration:1.2s">progress_activity</span>
                <span id="launch-text">Initializing...</span>
              </button>
              <button id="stop-btn" type="button"
                      onclick="wv('stop')"
                      class="hidden flex items-center justify-center p-2.5 rounded-lg bg-[#21262d] hover:bg-[#7f1d1d] border border-[#30363d] hover:border-[#f87171] text-[#8b949e] hover:text-[#f87171] transition-colors"
                      title="Stop Service">
                <span class="material-symbols-outlined text-[17px]">stop_circle</span>
              </button>
            </div>
          </div>

          <!-- Footer -->
          <div class="pt-space-xs flex items-center justify-between font-label-sm text-[11px] text-[#8b949e] relative">
            <div class="flex items-center gap-space-xs truncate font-mono">
              <span class="text-[#f0f6fc] font-semibold">v1.4.2</span>
              <span class="opacity-40">•</span>
              <span>CoreCLR C# Daemon</span>
              <span class="opacity-40">•</span>
              <span class="text-[#3fb950] truncate" id="footer-status">Initializing...</span>
            </div>
            <button class="flex-shrink-0 text-[#8b949e] hover:text-[#f0f6fc] p-1 rounded hover:bg-[#21262d] transition-colors" title="View Runtime Logs" type="button">
              <span class="material-symbols-outlined text-[15px]">terminal</span>
            </button>
          </div>
        </div>

        <script>
        function wv(msg) { try { window.chrome.webview.postMessage(msg); } catch(e) {} }

        // Persist checkbox preferences across launches
        const autoCheck = document.getElementById('auto-launch-check');
        const bgCheck   = document.getElementById('bg-check');
        const trayCheck = document.getElementById('tray-check');

        autoCheck.checked = localStorage.getItem('prd-auto-launch') !== '0';
        bgCheck.checked   = localStorage.getItem('prd-bg') === '1';
        trayCheck.checked = localStorage.getItem('prd-tray') !== '0';

        autoCheck.addEventListener('change', () => localStorage.setItem('prd-auto-launch', autoCheck.checked ? '1' : '0'));
        bgCheck.addEventListener('change',   () => localStorage.setItem('prd-bg',          bgCheck.checked   ? '1' : '0'));
        trayCheck.addEventListener('change', () => {
          localStorage.setItem('prd-tray', trayCheck.checked ? '1' : '0');
          wv('tray:' + (trayCheck.checked ? '1' : '0'));
        });

        function updatePort(port) {
          document.getElementById('port-badge').textContent = ':' + port;
          document.getElementById('port-label').textContent = 'Port ' + port;
          document.getElementById('browser-url-hint').textContent = 'Navigates to http://localhost:' + port + ' on ready';
        }

        function addLog(text, state) {
          // state: 'ok' | 'error' | 'info' (default)
          if (!text) return;
          const log = document.getElementById('step-log');
          const el = document.createElement('div');
          el.className = 'flex items-center gap-1 font-mono text-[10px] leading-4';
          const icons  = { ok: 'check_small',     error: 'error_outline', info: 'arrow_right_alt' };
          const colors = { ok: 'text-[#3fb950]',  error: 'text-[#f87171]', info: 'text-[#8b949e]' };
          const s = state || 'info';
          el.innerHTML = `<span class="material-symbols-outlined text-[12px] flex-shrink-0 ${colors[s]}">${icons[s]}</span>`
                       + `<span class="${colors[s]} truncate">${text}</span>`;
          log.appendChild(el);
          while (log.children.length > 4) log.removeChild(log.firstChild);
        }

        function updateProgress(pct, text) {
          const bar   = document.getElementById('progress-bar');
          const pctEl = document.getElementById('progress-pct');
          bar.style.width = pct + '%';
          pctEl.textContent = pct + '%';
          if (text) {
            document.getElementById('splash-status-text').textContent = text;
            addLog(text, 'info');
          }
        }

        function setReady(login) {
          updateProgress(100, 'Authenticated as ' + login);
          addLog('Server ready', 'ok');
          document.getElementById('progress-pct').textContent = '100%';
          document.getElementById('progress-pct').style.color = '#3fb950';
          const bar = document.getElementById('progress-bar');
          bar.style.background = '#238636';
          bar.style.boxShadow  = '0 0 10px rgba(46,160,67,.6)';
          document.getElementById('footer-status').textContent = 'Server ready';

          const btn  = document.getElementById('launch-btn');
          const icon = document.getElementById('launch-icon');
          const text = document.getElementById('launch-text');
          btn.disabled = false;
          btn.style.background = '#238636';
          btn.style.opacity    = '1';
          btn.style.cursor     = 'pointer';
          btn.style.boxShadow  = '0 4px 12px rgba(35,134,54,.3)';
          icon.classList.remove('animate-spin');
          icon.textContent = 'open_in_new';
          text.textContent = 'Launch Dashboard';

          document.getElementById('stop-btn').classList.remove('hidden');
          document.getElementById('stop-btn').classList.add('flex');

          if (!PD.launchFired && autoCheck.checked && !bgCheck.checked) wv('launch');
        }

        function setStopped() {
          const bar = document.getElementById('progress-bar');
          bar.style.background = '#30363d';
          bar.style.boxShadow  = 'none';
          document.getElementById('progress-pct').textContent = '--';
          document.getElementById('progress-pct').style.color = '#8b949e';
          document.getElementById('splash-status-text').textContent = 'Service stopped';
          document.getElementById('footer-status').textContent = 'Daemon stopped';
          document.querySelectorAll('.status-ping').forEach(el => { el.style.animation = 'none'; el.style.background = '#8b949e'; el.style.boxShadow = 'none'; });
          addLog('Service stopped by user', 'info');

          const btn  = document.getElementById('launch-btn');
          const icon = document.getElementById('launch-icon');
          const text = document.getElementById('launch-text');
          btn.disabled = true;
          btn.style.background = '#21262d';
          btn.style.opacity    = '0.55';
          btn.style.cursor     = 'not-allowed';
          btn.style.boxShadow  = 'none';
          icon.classList.remove('animate-spin');
          icon.textContent = 'stop_circle';
          text.textContent = 'Service stopped';

          const stopBtn = document.getElementById('stop-btn');
          stopBtn.classList.add('hidden');
          stopBtn.classList.remove('flex');
        }

        function setError(msg, fixCmd) {
          const bar = document.getElementById('progress-bar');
          bar.style.width      = '100%';
          bar.style.background = 'linear-gradient(90deg,#7f1d1d,#ef4444)';
          bar.style.boxShadow  = '0 0 10px rgba(239,68,68,.5)';
          document.getElementById('progress-pct').textContent = 'ERR';
          document.getElementById('progress-pct').style.color = '#f87171';
          const statusEl = document.getElementById('splash-status-text');
          statusEl.textContent = msg;
          statusEl.style.color = '#f87171';
          if (fixCmd) {
            const fix = document.createElement('div');
            fix.style.cssText = 'font-family:monospace;font-size:11px;color:#8b949e;margin-top:4px';
            fix.textContent = 'Execute: ' + fixCmd;
            statusEl.insertAdjacentElement('afterend', fix);
          }
          document.getElementById('footer-status').textContent = 'Startup error';
          document.querySelectorAll('.status-ping').forEach(el => { el.style.animation = 'none'; el.style.background = '#ef4444'; el.style.boxShadow = '0 0 6px rgba(239,68,68,.6)'; });
          addLog(msg, 'error');

          const btn  = document.getElementById('launch-btn');
          const icon = document.getElementById('launch-icon');
          const text = document.getElementById('launch-text');
          btn.disabled = true;
          btn.style.background = '#7f1d1d';
          btn.style.opacity    = '0.7';
          btn.style.cursor     = 'not-allowed';
          icon.classList.remove('animate-spin');
          icon.textContent = 'error';
          text.textContent  = 'Startup failed';
        }

        // Envia preferência de tray para C#
        wv('tray:' + (trayCheck.checked ? '1' : '0'));

        // Aplica estado acumulado: C# pode ter chamado as funções antes do DOM ficar pronto
        updatePort(PD.port);
        updateProgress(PD.pct, PD.text);
        if (PD.phase === 'ready')        setReady(PD.login);
        else if (PD.phase === 'error')   setError(PD.err, PD.fixCmd);
        else if (PD.phase === 'stopped') setStopped();
        </script>
        </body>
        </html>
        """;
}
