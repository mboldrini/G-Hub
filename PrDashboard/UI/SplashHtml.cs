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
        var PD={pct:0,text:'Initializing...',phase:'loading',login:'',port:5555,err:'',launchFired:false};
        function updateProgress(p,t){PD.pct=p;if(t)PD.text=t;}
        function updatePort(p){PD.port=p;}
        function setReady(l){
          PD.phase='ready';PD.login=l;
          var autoLaunch=localStorage.getItem('prd-auto-launch')!=='0';
          var bgMode=localStorage.getItem('prd-bg')==='1';
          if(autoLaunch&&!bgMode){PD.launchFired=true;wv('launch');}
        }
        function setError(m,fixCmd){PD.phase='error';PD.err=m;PD.fixCmd=fixCmd||'';}
        function setStopped(){PD.phase='stopped';}
        function addLog(){}
        wv('jsReady');
        </script>
        <!-- ② Fontes: non-blocking -->
        <link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" rel="stylesheet" media="print" onload="this.media='all'">
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@100..900&family=JetBrains+Mono:wght@100..900&display=swap" rel="stylesheet" media="print" onload="this.media='all'">
        <!-- ③ Tailwind: bloqueante -->
        <script src="https://cdn.tailwindcss.com"></script>
        <script id="tailwind-config">tailwind.config = { darkMode: "class", theme: { extend: { "colors": {
          "mocha-base":    "#1e1e2e",
          "mocha-mantle":  "#181825",
          "mocha-crust":   "#11111b",
          "mocha-surf":    "#252537",
          "mocha-s0":      "#313244",
          "mocha-s1":      "#45475a",
          "mocha-text":    "#cdd6f4",
          "mocha-sub1":    "#bac2de",
          "mocha-muted":   "#6c7086",
          "mocha-mauve":   "#cba6f7",
          "mocha-blue":    "#89b4fa",
          "mocha-lav":     "#b4befe",
          "mocha-green":   "#a6e3a1",
          "mocha-yellow":  "#f9e2af",
          "mocha-red":     "#f38ba8"
        }, "fontFamily": {
          "sans":  ["Inter", "system-ui", "sans-serif"],
          "mono":  ["JetBrains Mono", "monospace"]
        } } } };</script>
        <style>
        @keyframes shimmer { 0%{transform:translateX(-120%)} 50%{transform:translateX(80%)} 100%{transform:translateX(260%)} }
        .mocha-checkbox { accent-color: #cba6f7 !important; }
        *, *::before, *::after { box-sizing: border-box; }
        html, body { margin:0; padding:0; width:100vw; height:100vh; overflow:hidden; background:#1e1e2e; }
        ::-webkit-scrollbar { display:none; }
        </style>
        </head>
        <body class="font-sans text-mocha-text select-none">

        <!-- ── Titlebar ─────────────────────────────────────────────── -->
        <div id="titlebar"
             class="h-[37px] flex items-center justify-between px-3 bg-mocha-mantle border-b border-mocha-s0"
             style="cursor:default"
             onmousedown="if(!event.target.closest('button')){wv('startDrag')}">
          <div class="flex items-center gap-2">
            <div class="relative flex items-center justify-center">
              <span class="material-symbols-outlined text-mocha-mauve text-[15px]">fork_right</span>
              <span class="absolute -top-0.5 -right-0.5 w-1.5 h-1.5 rounded-full bg-mocha-mauve shadow-[0_0_6px_#cba6f7]"></span>
            </div>
            <span class="font-mono text-[11px] text-mocha-muted font-medium tracking-wide">PrDashboard</span>
            <span class="text-[10px] font-mono text-mocha-muted/40">— GitHub PR Live Monitor</span>
          </div>
          <div class="flex items-center">
            <button onclick="wv('minimize')"
                    class="w-9 h-[37px] flex items-center justify-center text-mocha-muted hover:bg-mocha-s0 hover:text-mocha-text transition-colors"
                    title="Minimize">
              <span class="material-symbols-outlined text-[14px]">minimize</span>
            </button>
            <button onclick="wv('close')"
                    class="w-9 h-[37px] flex items-center justify-center text-mocha-muted hover:bg-mocha-red/20 hover:text-mocha-red transition-colors"
                    title="Close">
              <span class="material-symbols-outlined text-[14px]">close</span>
            </button>
          </div>
        </div>

        <!-- ── Body ─────────────────────────────────────────────────── -->
        <div class="relative p-6 flex flex-col gap-4 overflow-y-auto"
             style="max-height:calc(100vh - 37px)">

          <!-- Shimmer line top -->
          <div class="absolute inset-x-0 top-0 h-[2px] bg-gradient-to-r from-transparent via-mocha-mauve to-transparent pointer-events-none"></div>

          <!-- Ambient glows -->
          <div class="absolute w-96 h-96 rounded-full bg-mocha-mauve/10 blur-3xl pointer-events-none -top-12 -left-12"></div>
          <div class="absolute w-80 h-80 rounded-full bg-mocha-blue/8 blur-3xl pointer-events-none -bottom-10 -right-10"></div>

          <!-- ── Header ── -->
          <div class="flex items-start gap-4 pt-1 relative">
            <div class="relative flex-shrink-0 w-14 h-14 rounded-xl bg-mocha-mantle border border-mocha-s0 shadow-md flex items-center justify-center">
              <div class="absolute inset-0 rounded-xl bg-gradient-to-br from-mocha-mauve/15 via-transparent to-transparent blur-sm"></div>
              <svg class="relative w-8 h-8 drop-shadow-[0_0_10px_rgba(203,166,247,0.5)]" fill="none" viewBox="0 0 24 24">
                <circle class="stroke-mocha-green stroke-[1.8] fill-mocha-mantle" cx="6" cy="6" r="2.5"></circle>
                <circle class="stroke-mocha-green stroke-[1.8] fill-mocha-mantle" cx="6" cy="18" r="2.5"></circle>
                <circle class="stroke-mocha-blue stroke-[1.8] fill-mocha-mantle" cx="18" cy="8" r="2.5"></circle>
                <path class="stroke-mocha-green stroke-[1.8] stroke-linecap-round" d="M6 8.5V15.5"></path>
                <path class="stroke-mocha-blue stroke-[1.8] stroke-linecap-round stroke-linejoin-round" d="M18 10.5V12C18 14.2 16.2 16 14 16H8.5"></path>
                <path class="stroke-mocha-blue stroke-[1.8] stroke-linecap-round stroke-linejoin-round" d="M11 13.5L8.5 16L11 18.5"></path>
              </svg>
              <span class="absolute -top-1 -right-1 flex h-3 w-3">
                <span class="animate-ping absolute inline-flex h-full w-full rounded-full bg-mocha-green opacity-60"></span>
                <span class="relative inline-flex rounded-full h-3 w-3 bg-mocha-green/80 shadow-[0_0_8px_#a6e3a1]"></span>
              </span>
            </div>
            <div class="flex flex-col flex-1 min-w-0 justify-center pt-0.5">
              <div class="flex items-center justify-between gap-2">
                <h1 class="text-[20px] leading-tight font-semibold text-mocha-text tracking-tight">
                  PrDashboard
                </h1>
                <span class="text-[11px] font-mono px-2 py-0.5 rounded bg-mocha-mauve/15 border border-mocha-mauve/35 text-mocha-mauve uppercase tracking-wider font-medium">DAEMON</span>
              </div>
              <p class="text-[12px] text-mocha-muted font-normal mt-0.5 truncate">Local GitHub PR &amp; CI Orchestrator</p>
            </div>
          </div>

          <!-- ── Status Card ── -->
          <div class="flex flex-col gap-2 bg-mocha-mantle border border-mocha-s0 rounded-lg p-3 shadow-sm relative" id="status-card">
            <div class="flex items-center justify-between gap-2">
              <div class="flex items-center gap-2 min-w-0">
                <span class="relative flex h-2 w-2 flex-shrink-0">
                  <span class="status-ping animate-ping absolute inline-flex h-full w-full rounded-full bg-mocha-green opacity-75"></span>
                  <span class="status-ping relative inline-flex rounded-full h-2 w-2 bg-mocha-green/80 shadow-[0_0_6px_#a6e3a1]"></span>
                </span>
                <span class="text-[13px] text-mocha-text font-medium truncate" id="splash-status-text">Initializing...</span>
              </div>
              <div class="flex items-center gap-1.5 flex-shrink-0">
                <span class="font-mono text-[11px] font-semibold text-mocha-mauve transition-colors duration-300 w-9 text-right" id="progress-pct">0%</span>
                <span class="font-mono text-[11px] text-mocha-muted bg-mocha-surf border border-mocha-s0 px-1.5 py-0.5 rounded" id="port-badge">:5555</span>
              </div>
            </div>
            <!-- Progress bar -->
            <div class="w-full h-2 bg-mocha-s0 rounded-full overflow-hidden">
              <div id="progress-bar"
                   class="h-full rounded-full transition-all duration-500 ease-out"
                   style="width:4%;background:linear-gradient(90deg,#7c3aed,#cba6f7);box-shadow:0 0 8px rgba(203,166,247,.4)"></div>
            </div>
            <!-- Step log -->
            <div id="step-log" class="flex flex-col gap-0.5 empty:hidden"></div>
          </div>

          <!-- ── Preferences ── -->
          <div class="flex flex-col gap-3 pt-1 border-t border-mocha-s0 relative">
            <div class="flex items-center justify-between text-[11px] text-mocha-muted font-medium px-0.5">
              <span class="uppercase tracking-wider flex items-center gap-1.5">
                <span class="material-symbols-outlined text-[14px] text-mocha-mauve">tune</span>Startup Preferences
              </span>
              <span class="font-mono text-mocha-muted/80" id="port-label">Port 5555</span>
            </div>
            <div class="flex flex-col gap-1.5">
              <label class="flex items-center justify-between p-2 rounded-lg bg-mocha-mantle border border-mocha-s0 hover:border-mocha-s1 transition-colors cursor-pointer group">
                <div class="flex flex-col pr-3">
                  <span class="text-[13px] text-mocha-text font-medium group-hover:text-mocha-mauve transition-colors">Launch browser automatically</span>
                  <span class="text-[11px] text-mocha-muted" id="browser-url-hint">Navigates to http://localhost:5555 on ready</span>
                </div>
                <input type="checkbox" id="auto-launch-check" checked class="mocha-checkbox h-4 w-4 rounded cursor-pointer">
              </label>
              <label class="flex items-center justify-between p-2 rounded-lg bg-mocha-mantle border border-mocha-s0 hover:border-mocha-s1 transition-colors cursor-pointer group">
                <div class="flex flex-col pr-3">
                  <span class="text-[13px] text-mocha-text font-medium group-hover:text-mocha-mauve transition-colors">Open in background</span>
                  <span class="text-[11px] text-mocha-muted">Silently initialize daemon</span>
                </div>
                <input type="checkbox" id="bg-check" class="mocha-checkbox h-4 w-4 rounded cursor-pointer">
              </label>
              <label class="flex items-center justify-between p-2 rounded-lg bg-mocha-mantle border border-mocha-s0 hover:border-mocha-s1 transition-colors cursor-pointer group">
                <div class="flex flex-col pr-3">
                  <span class="text-[13px] text-mocha-text font-medium group-hover:text-mocha-mauve transition-colors">Minimize to System Tray</span>
                  <span class="text-[11px] text-mocha-muted">Close window sends to tray icon</span>
                </div>
                <input type="checkbox" id="tray-check" checked class="mocha-checkbox h-4 w-4 rounded cursor-pointer">
              </label>
            </div>

            <!-- Action Buttons -->
            <div class="pt-1 flex items-center gap-2">
              <button id="launch-btn" type="button" disabled
                      onclick="if(!this.disabled) wv('launch')"
                      class="flex-1 flex items-center justify-center gap-2 px-4 py-2.5 rounded-lg text-mocha-text font-semibold text-[13px] shadow-md active:scale-[0.99] transition-all"
                      style="background:#252537;border:1px solid #313244;opacity:.55;cursor:not-allowed">
                <span id="launch-icon" class="material-symbols-outlined text-[17px] animate-spin" style="animation-duration:1.2s">progress_activity</span>
                <span id="launch-text">Initializing...</span>
              </button>
              <button id="stop-btn" type="button"
                      onclick="wv('stop')"
                      class="hidden flex items-center justify-center p-2.5 rounded-lg bg-mocha-surf hover:bg-mocha-red/20 border border-mocha-s0 hover:border-mocha-red/40 text-mocha-muted hover:text-mocha-red transition-colors"
                      title="Stop Service">
                <span class="material-symbols-outlined text-[17px]">stop_circle</span>
              </button>
            </div>
          </div>

          <!-- ── Footer ── -->
          <div class="pt-1 flex items-center justify-between text-[11px] text-mocha-muted relative">
            <div class="flex items-center gap-1.5 truncate font-mono">
              <span class="text-mocha-text font-semibold">v1.4.2</span>
              <span class="opacity-40">•</span>
              <span>CoreCLR C# Daemon</span>
              <span class="opacity-40">•</span>
              <span class="text-mocha-mauve truncate" id="footer-status">Initializing...</span>
            </div>
            <button class="flex-shrink-0 text-mocha-muted hover:text-mocha-text p-1 rounded hover:bg-mocha-s0 transition-colors" title="View Runtime Logs" type="button">
              <span class="material-symbols-outlined text-[15px]">terminal</span>
            </button>
          </div>
        </div>

        <script>
        function wv(msg) { try { window.chrome.webview.postMessage(msg); } catch(e) {} }

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
          if (!text) return;
          const log = document.getElementById('step-log');
          const el = document.createElement('div');
          el.className = 'flex items-center gap-1 font-mono text-[10px] leading-4';
          const icons  = { ok: 'check_small', error: 'error_outline', info: 'arrow_right_alt' };
          const colors = { ok: 'text-[#a6e3a1]', error: 'text-[#f38ba8]', info: 'text-[#6c7086]' };
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
          document.getElementById('progress-pct').style.color = '#cba6f7';
          const bar = document.getElementById('progress-bar');
          bar.style.background = 'linear-gradient(90deg,#7c3aed,#cba6f7)';
          bar.style.boxShadow  = '0 0 10px rgba(203,166,247,.5)';
          document.getElementById('footer-status').textContent = 'Server ready';
          document.getElementById('footer-status').style.color = '#cba6f7';

          const btn  = document.getElementById('launch-btn');
          const icon = document.getElementById('launch-icon');
          const text = document.getElementById('launch-text');
          btn.disabled = false;
          btn.style.background  = 'linear-gradient(135deg,#7c3aed,#cba6f7)';
          btn.style.border      = '1px solid rgba(203,166,247,.4)';
          btn.style.opacity     = '1';
          btn.style.cursor      = 'pointer';
          btn.style.boxShadow   = '0 4px 12px rgba(203,166,247,.3)';
          btn.style.color       = '#1e1e2e';
          icon.classList.remove('animate-spin');
          icon.textContent = 'open_in_new';
          text.textContent = 'Launch Dashboard';

          document.getElementById('stop-btn').classList.remove('hidden');
          document.getElementById('stop-btn').classList.add('flex');

          if (!PD.launchFired && autoCheck.checked && !bgCheck.checked) wv('launch');
        }

        function setStopped() {
          const bar = document.getElementById('progress-bar');
          bar.style.background = '#313244';
          bar.style.boxShadow  = 'none';
          document.getElementById('progress-pct').textContent = '--';
          document.getElementById('progress-pct').style.color = '#6c7086';
          document.getElementById('splash-status-text').textContent = 'Service stopped';
          document.getElementById('footer-status').textContent = 'Daemon stopped';
          document.getElementById('footer-status').style.color = '#6c7086';
          document.querySelectorAll('.status-ping').forEach(el => { el.style.animation='none'; el.style.background='#45475a'; el.style.boxShadow='none'; });
          addLog('Service stopped by user', 'info');

          const btn  = document.getElementById('launch-btn');
          const icon = document.getElementById('launch-icon');
          const text = document.getElementById('launch-text');
          btn.disabled = true;
          btn.style.background = '#252537';
          btn.style.border     = '1px solid #313244';
          btn.style.opacity    = '0.55';
          btn.style.cursor     = 'not-allowed';
          btn.style.boxShadow  = 'none';
          btn.style.color      = '#cdd6f4';
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
          bar.style.background = 'linear-gradient(90deg,#4a1a2a,#f38ba8)';
          bar.style.boxShadow  = '0 0 10px rgba(243,139,168,.4)';
          document.getElementById('progress-pct').textContent = 'ERR';
          document.getElementById('progress-pct').style.color = '#f38ba8';
          const statusEl = document.getElementById('splash-status-text');
          statusEl.textContent = msg;
          statusEl.style.color = '#f38ba8';
          if (fixCmd) {
            const fix = document.createElement('div');
            fix.style.cssText = 'font-family:monospace;font-size:11px;color:#6c7086;margin-top:4px';
            fix.textContent = 'Execute: ' + fixCmd;
            statusEl.insertAdjacentElement('afterend', fix);
          }
          document.getElementById('footer-status').textContent = 'Startup error';
          document.getElementById('footer-status').style.color = '#f38ba8';
          document.querySelectorAll('.status-ping').forEach(el => { el.style.animation='none'; el.style.background='#f38ba8'; el.style.boxShadow='0 0 6px rgba(243,139,168,.5)'; });
          addLog(msg, 'error');

          const btn  = document.getElementById('launch-btn');
          const icon = document.getElementById('launch-icon');
          const text = document.getElementById('launch-text');
          btn.disabled = true;
          btn.style.background = 'rgba(243,139,168,.15)';
          btn.style.border     = '1px solid rgba(243,139,168,.35)';
          btn.style.opacity    = '0.7';
          btn.style.cursor     = 'not-allowed';
          btn.style.boxShadow  = 'none';
          btn.style.color      = '#f38ba8';
          icon.classList.remove('animate-spin');
          icon.textContent = 'error';
          text.textContent  = 'Startup failed';
        }

        wv('tray:' + (trayCheck.checked ? '1' : '0'));

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
