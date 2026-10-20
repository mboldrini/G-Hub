namespace PrDashboard.Web;

public static class WebAssets
{
    public const string Html = """
        <!DOCTYPE html>
        <html class="dark" lang="pt-BR">
        <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>PrDashboard</title>
        <link rel="icon" type="image/svg+xml" href="/favicon.svg">
        <link rel="preconnect" href="https://fonts.googleapis.com">
        <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap" rel="stylesheet">
        <script src="https://cdn.jsdelivr.net/npm/canvas-confetti@1.9.3/dist/confetti.browser.min.js"></script>
        <script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
        <script>
          tailwind.config={darkMode:'class',theme:{extend:{fontFamily:{
            sans:['Inter','system-ui','-apple-system','sans-serif'],
            mono:['ui-monospace','SFMono-Regular','Menlo','Monaco','Consolas','monospace']
          }}}}
        </script>
        <style>
        /* ── Design tokens ─────────────────────────────────────────── */
        :root{
          --bg:#f8fafc;--surf:#ffffff;--subtle:#f1f5f9;--border:#e2e8f0;--text:#0f172a;--muted:#64748b;--hover:#f8fafc;
          --red:#dc2626;--red-bg:#fef2f2;--red-bd:#fecaca;
          --amber:#b45309;--amber-bg:#fffbeb;--amber-bd:#fde68a;
          --green:#059669;--green-bg:#f0fdf4;--green-bd:#a7f3d0;
          --indigo:#4f46e5;--indigo-bg:#eef2ff;--indigo-bd:#c7d2fe;
          --violet:#7c3aed;--violet-bg:#faf5ff;--violet-bd:#e9d5ff;
          --gray-bg:#f1f5f9;--gray-text:#64748b;
        }
        .dark{
          --bg:#0d1117;--surf:#161b22;--subtle:#21262d;--border:#30363d;--text:#f0f6fc;--muted:#8b949e;--hover:#1c2128;
          --red:#f87171;--red-bg:rgba(248,113,113,.1);--red-bd:rgba(248,113,113,.3);
          --amber:#fbbf24;--amber-bg:rgba(245,158,11,.1);--amber-bd:rgba(245,158,11,.3);
          --green:#4ade80;--green-bg:rgba(74,222,128,.1);--green-bd:rgba(74,222,128,.3);
          --indigo:#818cf8;--indigo-bg:rgba(129,140,248,.1);--indigo-bd:rgba(129,140,248,.3);
          --violet:#c084fc;--violet-bg:rgba(192,132,252,.1);--violet-bd:rgba(192,132,252,.3);
          --gray-bg:#21262d;--gray-text:#8b949e;
        }
        *{box-sizing:border-box;margin:0;padding:0}
        body{font-family:'Inter',system-ui,-apple-system,sans-serif;-webkit-font-smoothing:antialiased;background:var(--bg);color:var(--text);min-height:100vh;font-size:14px}

        ::-webkit-scrollbar{width:6px;height:6px}
        ::-webkit-scrollbar-track{background:var(--bg)}
        ::-webkit-scrollbar-thumb{background:var(--border);border-radius:9999px}
        ::-webkit-scrollbar-thumb:hover{background:var(--muted)}

        .content-grid{display:grid;grid-template-columns:1fr 300px;gap:24px;align-items:start}
        .content-grid.sidebar-closed{grid-template-columns:1fr}
        .content-grid.sidebar-closed .c-sidebar{display:none}
        @media(max-width:520px){.content-grid{grid-template-columns:1fr !important}.c-sidebar{display:none !important}}

        .rgroup{background:var(--surf);border:1px solid var(--border);border-radius:12px;overflow:hidden;margin-bottom:16px;box-shadow:0 1px 3px rgba(0,0,0,.06)}
        .rgroup-hdr{background:var(--subtle);border-bottom:1px solid var(--border);padding:10px 16px;display:flex;align-items:center;justify-content:space-between;cursor:pointer;user-select:none;transition:background .12s}
        .rgroup-hdr:hover{background:var(--border)}
        .rgroup-name{font-family:ui-monospace,monospace;font-size:12px;font-weight:600;color:var(--text)}
        .rgroup-sub{font-family:ui-monospace,monospace;font-size:11px;color:var(--muted)}
        .rg-chevron{transition:transform .2s ease;color:var(--muted);flex-shrink:0}

        .prc{padding:14px 16px;border-left:4px solid transparent;transition:background .12s}
        .prc:not(:last-child){border-bottom:1px solid var(--subtle)}
        .prc:hover{background:var(--hover)}
        .prc.u-ci,.prc.u-ch{border-left-color:var(--red)}
        .prc.u-cp{border-left-color:var(--amber)}
        .prc.u-ar{border-left-color:var(--indigo)}
        .prc.u-ap{border-left-color:var(--green)}
        .prc.is-draft{opacity:.6}
        .prc-row{display:flex;align-items:flex-start;justify-content:space-between;gap:12px}
        .prc-left{flex:1;min-width:0}
        .prc-trow{display:flex;align-items:baseline;gap:6px;flex-wrap:wrap;margin-bottom:6px}
        .prc-num{font-family:ui-monospace,monospace;font-size:11px;font-weight:600;color:var(--muted);flex-shrink:0}
        .prc-link{font-size:13px;font-weight:600;color:var(--text);text-decoration:none;line-height:1.35}
        .prc-link:hover{color:var(--indigo)}
        .prc-meta{display:flex;align-items:center;gap:5px;flex-wrap:wrap;font-size:11px;color:var(--muted)}
        .prc-right{display:flex;flex-direction:column;align-items:flex-end;gap:4px;flex-shrink:0;padding-top:2px}

        .b{display:inline-flex;align-items:center;gap:4px;padding:2px 8px;border-radius:6px;font-size:11px;font-weight:500;white-space:nowrap;border:1px solid transparent}
        .br{background:var(--red-bg);color:var(--red);border-color:var(--red-bd)}
        .by{background:var(--amber-bg);color:var(--amber);border-color:var(--amber-bd)}
        .bg{background:var(--green-bg);color:var(--green);border-color:var(--green-bd)}
        .bi{background:var(--indigo-bg);color:var(--indigo);border-color:var(--indigo-bd)}
        .bv{background:var(--violet-bg);color:var(--violet);border-color:var(--violet-bd)}
        .bx{background:var(--gray-bg);color:var(--gray-text);border-color:var(--border)}
        .branch{display:inline-flex;align-items:center;gap:4px;font-family:ui-monospace,monospace;font-size:10px;padding:2px 7px;border-radius:5px;background:var(--subtle);color:var(--text);border:1px solid var(--border)}

        .mcard{background:var(--surf);border:1px solid var(--border);border-radius:12px;padding:14px 16px;transition:border-color .15s;box-shadow:0 1px 3px rgba(0,0,0,.04)}
        .mcard:hover{border-color:var(--muted)}
        .mlabel{font-size:12px;color:var(--muted);margin-bottom:6px;display:flex;align-items:center;justify-content:space-between}
        .mval{font-size:22px;font-weight:600;letter-spacing:-.02em;color:var(--text)}
        .msub{font-size:11px;font-family:ui-monospace,monospace;color:var(--muted);margin-left:6px}

        .tab-btn{padding:0 4px 12px;border-bottom:2px solid transparent;font-size:13px;font-weight:500;color:var(--muted);background:none;border-top:none;border-left:none;border-right:none;cursor:pointer;transition:color .15s,border-color .15s;white-space:nowrap;display:inline-flex;align-items:center;gap:6px}
        .tab-btn:hover{color:var(--text)}
        .tab-btn.active{color:var(--text);border-bottom-color:#4ade80}
        .tab-panel{display:none}
        .tab-panel.active{display:block}
        .tcnt{font-size:11px;font-family:monospace;padding:1px 7px;border-radius:9999px;border:1px solid var(--border);background:var(--subtle);color:var(--muted)}
        .tcnt-amber{border-color:rgba(245,158,11,.4);background:rgba(245,158,11,.1);color:var(--amber)}

        .sc{background:var(--surf);border:1px solid var(--border);border-radius:12px;padding:14px;margin-bottom:16px;box-shadow:0 1px 3px rgba(0,0,0,.04)}
        .sc-hdr{display:flex;align-items:center;justify-content:space-between;padding-bottom:10px;border-bottom:1px solid var(--subtle);margin-bottom:12px}
        .sc-title{font-size:11px;font-weight:700;text-transform:uppercase;letter-spacing:.06em;color:var(--text);display:flex;align-items:center;gap:6px}
        .srev{padding:8px 10px;border-radius:8px;border:1px solid var(--border);margin-bottom:8px;transition:background .12s}
        .srev:last-child{margin-bottom:0}
        .srev:hover{background:var(--hover)}
        .srev-repo{font-family:ui-monospace,monospace;font-size:11px;color:var(--muted);margin-bottom:3px}
        .srev-link{font-size:12px;font-weight:500;color:var(--text);text-decoration:none;display:block;margin-bottom:6px;line-height:1.3}
        .srev-link:hover{color:var(--indigo)}
        .smerge-item{display:flex;align-items:flex-start;gap:8px;padding:6px 0;border-bottom:1px solid var(--subtle)}
        .smerge-item:last-child{border-bottom:none}
        .smerge-icon{width:18px;height:18px;border-radius:9999px;background:var(--indigo-bg);border:1px solid var(--indigo-bd);color:var(--indigo);display:flex;align-items:center;justify-content:center;flex-shrink:0;margin-top:2px}
        .smerge-link{font-size:12px;font-weight:500;color:var(--text);text-decoration:none;display:block;line-height:1.3}
        .smerge-link:hover{color:var(--indigo)}
        .smerge-sub{font-size:11px;color:var(--muted);margin-top:2px}

        .ebanner{background:var(--red-bg);border:1px solid var(--red-bd);border-radius:8px;padding:10px 16px;margin-bottom:16px;color:var(--red);display:none;font-size:13px}
        .empty{color:var(--muted);text-align:center;padding:40px;font-style:italic;font-size:13px}

        #nstack{position:fixed;top:16px;right:16px;z-index:1000;display:flex;flex-direction:column;gap:8px;max-width:320px;pointer-events:none}
        .notif{background:var(--surf);border:1px solid var(--border);border-radius:10px;padding:12px 36px 12px 14px;box-shadow:0 4px 20px rgba(0,0,0,.25);animation:slideIn .2s ease;pointer-events:all;position:relative}
        .notif.comment{border-left:3px solid var(--amber)}
        .notif.ci{border-left:3px solid var(--red)}
        .notif.merged{border-left:3px solid var(--green)}
        .notif.approved{border-left:3px solid var(--green)}
        .notif.ready{border-left:3px solid var(--indigo)}
        .nclose{position:absolute;top:6px;right:8px;cursor:pointer;color:var(--muted);background:none;border:none;font-size:18px;line-height:1;padding:2px}
        .nclose:hover{color:var(--text)}
        .ntitle{font-weight:600;font-size:13px;margin-bottom:3px;color:var(--text)}
        .nbody{font-size:12px;color:var(--muted)}
        @keyframes slideIn{from{transform:translateX(24px);opacity:0}to{transform:none;opacity:1}}

        .stoggle{display:inline-flex;align-items:center;gap:5px;font-size:12px;font-weight:500;color:var(--muted);background:none;border:1px solid var(--border);border-radius:6px;padding:3px 10px;cursor:pointer;transition:all .15s}
        .stoggle:hover{color:var(--text);border-color:var(--muted)}
        .view-seg{display:inline-flex;border:1px solid var(--border);border-radius:6px;overflow:hidden}
        .view-btn{padding:3px 10px;font-size:11px;font-weight:500;border:none;background:none;color:var(--muted);cursor:pointer;transition:background .12s,color .12s;font-family:inherit}
        .view-btn.active{background:var(--subtle);color:var(--text)}
        .fchip{display:inline-flex;align-items:center;gap:5px;padding:4px 10px;border-radius:6px;font-size:11px;font-weight:500;border:1px solid var(--border);background:none;color:var(--muted);cursor:pointer;transition:border-color .12s,color .12s;font-family:inherit}
        .fchip:hover{border-color:var(--muted);color:var(--text)}

        @media(max-width:640px){.metrics-grid{grid-template-columns:repeat(2,1fr) !important}}
        @keyframes ping{0%{transform:scale(1);opacity:.7}75%,100%{transform:scale(2);opacity:0}}
        @keyframes spin{from{transform:rotate(0deg)}to{transform:rotate(360deg)}}

        .npref-btn{display:inline-flex;align-items:center;gap:5px;padding:3px 10px;border-radius:9999px;font-size:11px;font-weight:500;border:1px solid var(--border);background:var(--subtle);color:var(--muted);cursor:pointer;transition:all .15s;font-family:inherit;opacity:.55}
        .npref-btn.on{border-color:var(--green-bd);background:var(--green-bg);color:var(--green);opacity:1}
        </style>
        </head>
        <body>
        <div id="nstack"></div>

        <!-- ── HEADER ─────────────────────────────────────────────── -->
        <header style="position:sticky;top:0;z-index:40;background:var(--surf);backdrop-filter:blur(12px);-webkit-backdrop-filter:blur(12px);border-bottom:1px solid var(--border);padding:10px 24px">
          <div style="max-width:1280px;margin:0 auto;display:flex;align-items:center;justify-content:space-between;gap:12px;flex-wrap:wrap">
            <div style="display:flex;align-items:center;gap:10px">
              <img src="/favicon.svg" width="28" height="28" style="border-radius:6px;flex-shrink:0" alt="">
              <span style="font-size:15px;font-weight:600;letter-spacing:-.015em;color:var(--text)">PrDashboard</span>
              <span style="font-size:11px;font-family:monospace;padding:2px 8px;border-radius:9999px;border:1px solid var(--border);background:var(--subtle);color:var(--muted)">.NET 8 / gh CLI</span>
            </div>
            <div style="display:flex;align-items:center;gap:8px;flex-wrap:wrap">
              <!-- Sync indicator -->
              <div style="display:inline-flex;align-items:center;gap:8px;padding:5px 10px;border-radius:6px;background:var(--subtle);border:1px solid var(--border);font-size:12px;color:var(--muted)">
                <span style="position:relative;width:8px;height:8px;flex-shrink:0">
                  <span id="dot-ping" style="position:absolute;inset:0;border-radius:9999px;background:#10b981;animation:ping 1.2s cubic-bezier(0,0,.2,1) infinite;opacity:.7"></span>
                  <span id="dot-core" style="position:relative;display:block;width:8px;height:8px;border-radius:9999px;background:#10b981"></span>
                </span>
                <span id="rlabel" style="font-variant-numeric:tabular-nums">—</span>
              </div>
              <!-- Refresh -->
              <button onclick="manual()" style="display:inline-flex;align-items:center;gap:6px;padding:5px 12px;border-radius:6px;font-size:12px;font-weight:500;color:var(--text);background:var(--subtle);border:1px solid var(--border);cursor:pointer;transition:border-color .15s" onmouseover="this.style.borderColor='var(--muted)'" onmouseout="this.style.borderColor='var(--border)'">
                <svg id="ricon" width="14" height="14" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24">
                  <path d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15"/>
                </svg>
                <span data-i18n="refresh">Atualizar</span>
              </button>
              <div style="width:1px;height:16px;background:var(--border)"></div>
              <!-- Language toggle -->
              <button onclick="toggleLanguage()" id="lang-btn" title="Toggle language" style="display:inline-flex;align-items:center;justify-content:center;padding:4px 8px;border-radius:6px;background:var(--subtle);border:1px solid var(--border);cursor:pointer;font-size:11px;font-weight:600;font-family:monospace;color:var(--muted);transition:border-color .15s;letter-spacing:.04em" onmouseover="this.style.borderColor='var(--muted)'" onmouseout="this.style.borderColor='var(--border)'">
                <span id="lang-display">PT</span>
              </button>
              <!-- Theme toggle -->
              <button onclick="toggleTheme()" title="Toggle theme" style="display:inline-flex;align-items:center;justify-content:center;padding:5px;border-radius:6px;background:var(--subtle);border:1px solid var(--border);cursor:pointer;transition:border-color .15s" onmouseover="this.style.borderColor='var(--muted)'" onmouseout="this.style.borderColor='var(--border)'">
                <svg id="icon-sun" width="16" height="16" fill="none" stroke="#f59e0b" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24" style="display:none"><circle cx="12" cy="12" r="5"/><path d="M12 1v2m0 18v2M4.22 4.22l1.42 1.42m12.72 12.72l1.42 1.42M1 12h2m18 0h2M4.22 19.78l1.42-1.42M18.36 5.64l1.42-1.42"/></svg>
                <svg id="icon-moon" width="16" height="16" fill="#818cf8" viewBox="0 0 24 24"><path d="M12 3a9 9 0 109 9c0-.46-.04-.92-.1-1.36a5.389 5.389 0 01-4.4 2.26 5.403 5.403 0 01-5.4-5.4c0-1.81.89-3.42 2.26-4.4-.44-.06-.9-.1-1.36-.1z"/></svg>
              </button>
              <!-- User menu -->
              <div style="position:relative;display:inline-block" id="user-menu-wrap">
                <div id="upill" onclick="toggleUserMenu()" title="" style="width:28px;height:28px;border-radius:9999px;background:linear-gradient(135deg,#4f46e5,#0ea5e9);color:#fff;display:flex;align-items:center;justify-content:center;font-weight:700;font-size:11px;flex-shrink:0;cursor:pointer;box-shadow:0 0 0 2px var(--surf)">—</div>
                <div id="user-dd" style="display:none;position:absolute;right:0;top:calc(100% + 8px);background:var(--surf);border:1px solid var(--border);border-radius:10px;min-width:240px;box-shadow:0 4px 20px rgba(0,0,0,.3);z-index:200;overflow:hidden">
                  <div style="padding:10px 14px 8px;border-bottom:1px solid var(--border)">
                    <div style="font-size:10px;color:var(--muted);margin-bottom:2px;text-transform:uppercase;letter-spacing:.05em">GitHub</div>
                    <div id="user-dd-name" style="font-size:13px;font-weight:600;color:var(--text)">—</div>
                  </div>
                  <div style="padding:4px 0;border-bottom:1px solid var(--border)">
                    <div style="padding:6px 14px 3px;font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.06em;color:var(--muted)" data-i18n="menuTestNotifs">Testar notificações</div>
                    <button onclick="testNotif('comment');closeUserMenu()" style="display:flex;align-items:center;gap:8px;width:100%;padding:7px 14px;font-size:12px;color:var(--text);background:none;border:none;cursor:pointer;font-family:inherit;transition:background .12s;text-align:left" onmouseover="this.style.background='var(--subtle)'" onmouseout="this.style.background='none'">🔔 <span data-i18n="testComment">Novo comentário</span></button>
                    <button onclick="testNotif('ci');closeUserMenu()" style="display:flex;align-items:center;gap:8px;width:100%;padding:7px 14px;font-size:12px;color:var(--text);background:none;border:none;cursor:pointer;font-family:inherit;transition:background .12s;text-align:left" onmouseover="this.style.background='var(--subtle)'" onmouseout="this.style.background='none'">⚠️ <span data-i18n="testCi">CI com erro</span></button>
                    <button onclick="testNotif('merged');closeUserMenu()" style="display:flex;align-items:center;gap:8px;width:100%;padding:7px 14px;font-size:12px;color:var(--text);background:none;border:none;cursor:pointer;font-family:inherit;transition:background .12s;text-align:left" onmouseover="this.style.background='var(--subtle)'" onmouseout="this.style.background='none'">🎉 <span data-i18n="testMerged">PR mergeado</span></button>
                    <button onclick="testNotif('approved');closeUserMenu()" style="display:flex;align-items:center;gap:8px;width:100%;padding:7px 14px;font-size:12px;color:var(--text);background:none;border:none;cursor:pointer;font-family:inherit;transition:background .12s;text-align:left" onmouseover="this.style.background='var(--subtle)'" onmouseout="this.style.background='none'">✅ <span data-i18n="testApproved">PR aprovado</span></button>
                    <button onclick="testNotif('ready');closeUserMenu()" style="display:flex;align-items:center;gap:8px;width:100%;padding:7px 14px;font-size:12px;color:var(--text);background:none;border:none;cursor:pointer;font-family:inherit;transition:background .12s;text-align:left" onmouseover="this.style.background='var(--subtle)'" onmouseout="this.style.background='none'">🔵 <span data-i18n="testReady">PR saiu de Draft</span></button>
                  </div>
                  <div style="padding:6px 0;border-bottom:1px solid var(--border)">
                    <div style="padding:4px 14px 6px;font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.06em;color:var(--muted)" data-i18n="menuConfigNotifs">Notificações</div>
                    <div style="padding:4px 14px 8px">
                      <div style="font-size:12px;font-weight:500;color:var(--text);margin-bottom:5px">🔔 <span data-i18n="notifComment">Novo comentário</span></div>
                      <div style="display:flex;gap:6px">
                        <button id="npref-comment-visual" class="npref-btn" onclick="toggleNotifPref('comment','visual')"><span data-i18n="notifVisual">👁 Aviso</span></button>
                        <button id="npref-comment-audio" class="npref-btn" onclick="toggleNotifPref('comment','audio')"><span data-i18n="notifAudio">🔊 Som</span></button>
                      </div>
                    </div>
                    <div style="padding:4px 14px 8px">
                      <div style="font-size:12px;font-weight:500;color:var(--text);margin-bottom:5px">⚠️ <span data-i18n="notifCi">CI com erro</span></div>
                      <div style="display:flex;gap:6px">
                        <button id="npref-ci-visual" class="npref-btn" onclick="toggleNotifPref('ci','visual')"><span data-i18n="notifVisual">👁 Aviso</span></button>
                        <button id="npref-ci-audio" class="npref-btn" onclick="toggleNotifPref('ci','audio')"><span data-i18n="notifAudio">🔊 Som</span></button>
                      </div>
                    </div>
                    <div style="padding:4px 14px 4px">
                      <div style="font-size:12px;font-weight:500;color:var(--text);margin-bottom:5px">🎉 <span data-i18n="notifMerged">PR mergeado</span></div>
                      <div style="display:flex;gap:6px">
                        <button id="npref-merged-visual" class="npref-btn" onclick="toggleNotifPref('merged','visual')"><span data-i18n="notifVisual">👁 Aviso</span></button>
                        <button id="npref-merged-audio" class="npref-btn" onclick="toggleNotifPref('merged','audio')"><span data-i18n="notifAudio">🔊 Som</span></button>
                      </div>
                    </div>
                    <div style="padding:4px 14px 8px">
                      <div style="font-size:12px;font-weight:500;color:var(--text);margin-bottom:5px">✅ <span data-i18n="notifApproved">PR aprovado</span></div>
                      <div style="display:flex;gap:6px">
                        <button id="npref-approved-visual" class="npref-btn" onclick="toggleNotifPref('approved','visual')"><span data-i18n="notifVisual">👁 Aviso</span></button>
                        <button id="npref-approved-audio" class="npref-btn" onclick="toggleNotifPref('approved','audio')"><span data-i18n="notifAudio">🔊 Som</span></button>
                      </div>
                    </div>
                    <div style="padding:4px 14px 4px">
                      <div style="font-size:12px;font-weight:500;color:var(--text);margin-bottom:5px">🔵 <span data-i18n="notifReady">PR pronto p/ review</span></div>
                      <div style="display:flex;gap:6px">
                        <button id="npref-ready-visual" class="npref-btn" onclick="toggleNotifPref('ready','visual')"><span data-i18n="notifVisual">👁 Aviso</span></button>
                        <button id="npref-ready-audio" class="npref-btn" onclick="toggleNotifPref('ready','audio')"><span data-i18n="notifAudio">🔊 Som</span></button>
                      </div>
                    </div>
                  </div>
                  <div style="padding:10px 14px 12px">
                    <div style="font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.06em;color:var(--muted);margin-bottom:8px" data-i18n="menuInterval">Intervalo de atualização</div>
                    <div style="display:flex;align-items:center;gap:6px;margin-bottom:8px">
                      <input id="interval-input" type="number" min="30" max="3600" value="300" style="width:70px;padding:4px 8px;border-radius:5px;border:1px solid var(--border);background:var(--subtle);color:var(--text);font-size:13px;font-family:inherit">
                      <span style="font-size:12px;color:var(--muted)" data-i18n="menuSecs">seg</span>
                    </div>
                    <button onclick="saveInterval()" style="width:100%;padding:6px 0;border-radius:6px;border:1px solid var(--indigo-bd);background:var(--indigo-bg);color:var(--indigo);font-size:12px;font-weight:500;cursor:pointer;font-family:inherit;transition:all .12s" data-i18n="menuSave">Salvar</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </header>

        <main style="max-width:1280px;margin:0 auto;padding:24px">
          <div class="ebanner" id="err"></div>
          <div class="empty" id="loading" data-i18n="loading">Carregando...</div>

          <div id="content" style="display:none">
            <!-- ── METRICS ── -->
            <div style="display:grid;grid-template-columns:repeat(4,1fr);gap:12px;margin-bottom:24px" class="metrics-grid">
              <div class="mcard">
                <div class="mlabel"><span data-i18n="metricsMyPrs">Meus PRs Abertos</span> <span style="width:7px;height:7px;border-radius:9999px;background:#10b981;display:inline-block"></span></div>
                <div><span class="mval" id="m-open">—</span><span class="msub" id="m-open-sub"></span></div>
              </div>
              <div class="mcard">
                <div class="mlabel"><span data-i18n="metricsCi">CI com Falha</span> <span style="width:7px;height:7px;border-radius:9999px;background:#f87171;display:inline-block"></span></div>
                <div><span class="mval" id="m-ci" style="color:var(--red)">—</span><span class="msub" id="m-ci-sub" style="color:var(--red)"></span></div>
              </div>
              <div class="mcard">
                <div class="mlabel"><span data-i18n="metricsReviews">Reviews a Mim</span> <span style="width:7px;height:7px;border-radius:9999px;background:#fbbf24;display:inline-block"></span></div>
                <div><span class="mval" id="m-rev" style="color:var(--amber)">—</span><span class="msub" id="m-rev-sub" style="color:var(--amber)"></span></div>
              </div>
              <div class="mcard">
                <div class="mlabel"><span data-i18n="metricsMerged">Mergeados Recentes</span> <span style="width:7px;height:7px;border-radius:9999px;background:#818cf8;display:inline-block"></span></div>
                <div><span class="mval" id="m-mrg">—</span><span class="msub" id="m-mrg-sub"></span></div>
              </div>
            </div>

            <!-- ── TABS ── -->
            <div style="border-bottom:1px solid var(--border);display:flex;align-items:flex-end;justify-content:space-between;margin-bottom:24px;gap:8px;flex-wrap:wrap">
              <nav style="display:flex;gap:8px">
                <button class="tab-btn" data-tab="my" onclick="setTab('my')">
                  <span data-i18n="tabMyPrs">Meus PRs</span> <span id="tc-my" class="tcnt">0</span>
                </button>
                <button class="tab-btn" data-tab="rev" onclick="setTab('rev')">
                  <span data-i18n="tabReviews">Reviews Solicitados</span> <span id="tc-rev" class="tcnt tcnt-amber">0</span>
                </button>
                <button class="tab-btn" data-tab="mrg" onclick="setTab('mrg')">
                  <span data-i18n="tabMerged">Mergeados</span> <span id="tc-mrg" class="tcnt">0</span>
                </button>
              </nav>
              <div style="padding-bottom:12px">
                <button class="stoggle" id="stoggle-btn" onclick="toggleSidebar()">
                  <svg width="13" height="13" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M4 6h16M4 12h10M4 18h7"/></svg>
                  <span id="stoggle-label" data-i18n="closeSidebar">Fechar painel</span>
                </button>
              </div>
            </div>

            <!-- ── TAB: MEUS PRs ── -->
            <div id="tab-my" class="tab-panel">
              <div style="display:flex;align-items:center;gap:8px;margin-bottom:16px">
                <span style="font-size:11px;color:var(--muted)" data-i18n="groupBy">Agrupar por</span>
                <div class="view-seg">
                  <button class="view-btn active" id="vbtn-repo" onclick="setMyView('repo')" data-i18n="groupByRepo">Repositório</button>
                  <button class="view-btn" id="vbtn-status" onclick="setMyView('status')" data-i18n="groupByStatus">Status</button>
                </div>
                <div style="width:1px;height:16px;background:var(--border)"></div>
                <div class="view-seg">
                  <button class="view-btn" id="collapse-all-btn" onclick="toggleAllRepos()" style="display:inline-flex;align-items:center;gap:4px">
                    <svg class="icon-collapse" width="11" height="11" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M5 12l7-7 7 7M5 19l7-7 7 7"/></svg>
                    <svg class="icon-expand" width="11" height="11" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24" style="display:none"><path d="M5 5l7 7 7-7M5 12l7 7 7-7"/></svg>
                    <span id="collapse-all-label">Recolher todos</span>
                  </button>
                </div>
              </div>
              <div id="s-my-filters" style="margin-bottom:16px"></div>
              <div class="content-grid" id="main-grid">
                <section id="s-my"></section>
                <aside class="c-sidebar">
                  <div id="s-rev-side"></div>
                  <div id="s-mrg-side"></div>
                  <div style="border-radius:10px;border:1px dashed var(--border);padding:10px 14px;font-size:11px;color:var(--muted);display:flex;align-items:center;justify-content:space-between">
                    <span style="font-family:monospace;color:var(--text)">gh pr checkout &lt;n&gt;</span>
                    <span data-i18n="quickShortcut">atalho rápido</span>
                  </div>
                </aside>
              </div>
            </div>

            <div id="tab-rev" class="tab-panel">
              <div style="display:flex;align-items:center;gap:8px;margin-bottom:10px;flex-wrap:wrap">
                <button id="rev-hide-bots-btn" class="fchip" onclick="toggleRevHideBots()"><span data-i18n="hideBots">Ocultar bots</span></button>
                <div style="width:1px;height:16px;background:var(--border)"></div>
                <div class="view-seg">
                  <button id="rev-collapse-all-btn" class="view-btn" onclick="toggleAllRevRepos()" style="display:inline-flex;align-items:center;gap:4px">
                    <svg class="rev-icon-collapse" width="11" height="11" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M5 12l7-7 7 7M5 19l7-7 7 7"/></svg>
                    <svg class="rev-icon-expand" width="11" height="11" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24" style="display:none"><path d="M5 5l7 7 7-7M5 12l7 7 7-7"/></svg>
                    <span id="rev-collapse-all-label" data-i18n="collapseAll">Recolher todos</span>
                  </button>
                </div>
              </div>
              <div id="s-rev-filters" style="margin-bottom:10px"></div>
              <div id="s-rev-full"></div>
            </div>
            <div id="tab-mrg" class="tab-panel"><div id="s-mrg-full"></div></div>
          </div>
        </main>

        <script>
        // ── i18n ───────────────────────────────────────────────────────
        const I18N={
          'pt-BR':{
            refresh:'Atualizar',
            metricsMyPrs:'Meus PRs Abertos', metricsCi:'CI com Falha', metricsReviews:'Reviews a Mim', metricsMerged:'Mergeados Recentes',
            metricsOk:'ok', metricsBlocker:'bloqueador', metricsBlockers:'bloqueadores',
            metricsPending:'pendente', metricsPendings:'pendentes', metricsRecent:'recentes',
            metricsNeedAction:n=>`${n} em atenção`,
            tabMyPrs:'Meus PRs', tabReviews:'Reviews Solicitados', tabMerged:'Mergeados',
            closeSidebar:'Fechar painel', openSidebar:'Abrir painel',
            draft:'rascunho', noReview:'sem review',
            changesRequested:'✗ mudanças pedidas', awaitingApproval:'⏳ aguardando approval',
            conflict:'⚡ conflito', outdated:'↓ desatualizado',
            ciPassing:'✓ CI', ciPending:'⏳ CI',
            groupNeedAction:(total,attn)=>`${total} PR${total>1?'s':''}, ${attn} em atenção`,
            groupNormal:n=>`${n} PR${n>1?'s':''}`,
            sidebarReviews:'Reviews a Mim', sidebarMerged:'Mergeados',
            pendingCount:n=>`${n} pendentes`, recentCount:n=>`${n} recentes`,
            tookDuration:d=>`levou ${d}`,
            emptyMyPrs:'Nenhum PR aberto.', emptyReviews:'Nenhum review solicitado.', emptyMerged:'Nenhum PR mergeado recentemente.',
            quickShortcut:'atalho rápido', loading:'Carregando...',
            filterAll:'Todos',
            groupBy:'Agrupar por', groupByRepo:'Repositório', groupByStatus:'Status',
            stCiFailing:'CI com Falha', stChangesRequested:'Mudanças Pedidas', stCommentsPending:'Novos Comentários', stAwaitingReview:'Aguardando Review', stApproved:'Aprovado', stNormal:'Sem Ação',
            collapseAll:'Recolher todos', expandAll:'Expandir todos',
            testComment:'Novo comentário', testCi:'CI com erro', testMerged:'PR mergeado', testApproved:'PR aprovado', testReady:'PR saiu de Draft',
            menuTestNotifs:'Testar notificações', menuConfigNotifs:'Notificações',
            menuInterval:'Intervalo de atualização', menuSecs:'seg', menuSave:'Salvar',
            notifComment:'Novo comentário', notifCi:'CI com erro', notifMerged:'PR mergeado', notifApproved:'PR aprovado', notifReady:'PR pronto p/ review',
            notifVisual:'👁 Aviso', notifAudio:'🔊 Som',
            hideBots:'Ocultar bots',
          },
          'en-US':{
            refresh:'Refresh',
            metricsMyPrs:'My Open PRs', metricsCi:'CI Failing', metricsReviews:'Reviews for Me', metricsMerged:'Recently Merged',
            metricsOk:'ok', metricsBlocker:'blocker', metricsBlockers:'blockers',
            metricsPending:'pending', metricsPendings:'pending', metricsRecent:'recent',
            metricsNeedAction:n=>`${n} need attention`,
            tabMyPrs:'My PRs', tabReviews:'Review Requests', tabMerged:'Merged',
            closeSidebar:'Close panel', openSidebar:'Open panel',
            draft:'draft', noReview:'no review',
            changesRequested:'✗ changes requested', awaitingApproval:'⏳ waiting for approval',
            conflict:'⚡ conflict', outdated:'↓ outdated',
            ciPassing:'✓ CI', ciPending:'⏳ CI',
            groupNeedAction:(total,attn)=>`${total} PR${total>1?'s':''}, ${attn} need attention`,
            groupNormal:n=>`${n} PR${n>1?'s':''}`,
            sidebarReviews:'Reviews for Me', sidebarMerged:'Merged',
            pendingCount:n=>`${n} pending`, recentCount:n=>`${n} recent`,
            tookDuration:d=>`took ${d}`,
            emptyMyPrs:'No open PRs.', emptyReviews:'No review requests.', emptyMerged:'No recently merged PRs.',
            quickShortcut:'quick shortcut', loading:'Loading...',
            filterAll:'All',
            groupBy:'Group by', groupByRepo:'Repository', groupByStatus:'Status',
            stCiFailing:'CI Failing', stChangesRequested:'Changes Requested', stCommentsPending:'New Comments', stAwaitingReview:'Awaiting Review', stApproved:'Approved', stNormal:'No Action',
            collapseAll:'Collapse all', expandAll:'Expand all',
            testComment:'New comment', testCi:'CI error', testMerged:'PR merged', testApproved:'PR approved', testReady:'PR left Draft',
            menuTestNotifs:'Test notifications', menuConfigNotifs:'Notifications',
            menuInterval:'Update interval', menuSecs:'sec', menuSave:'Save',
            notifComment:'New comment', notifCi:'CI error', notifMerged:'PR merged', notifApproved:'PR approved', notifReady:'PR ready for review',
            notifVisual:'👁 Alert', notifAudio:'🔊 Sound',
            hideBots:'Hide bots',
          }
        };
        let currentLang=localStorage.getItem('prd-lang')||'pt-BR';
        function t(key,...args){const v=I18N[currentLang]?.[key]??I18N['pt-BR'][key]??key;return typeof v==='function'?v(...args):v}
        function applyI18n(){
          document.querySelectorAll('[data-i18n]').forEach(el=>{el.textContent=t(el.dataset.i18n)});
          document.getElementById('lang-display').textContent=currentLang==='pt-BR'?'PT':'EN';
          document.documentElement.lang=currentLang==='pt-BR'?'pt-BR':'en-US';
          const lbl=document.getElementById('stoggle-label');
          if(lbl)lbl.textContent=sidebarOpen?t('closeSidebar'):t('openSidebar');
          updateCollapseBtn();
          updateRevCollapseBtn();
        }

        // ── Theme ──────────────────────────────────────────────────────
        function applyTheme(th){
          const d=document.documentElement;
          if(th==='light'){d.classList.remove('dark');document.getElementById('icon-sun').style.display='block';document.getElementById('icon-moon').style.display='none'}
          else{d.classList.add('dark');document.getElementById('icon-sun').style.display='none';document.getElementById('icon-moon').style.display='block'}
        }
        async function toggleTheme(){
          const th=document.documentElement.classList.contains('dark')?'light':'dark';
          applyTheme(th);
          await saveSettings({theme:th});
        }
        applyTheme(localStorage.getItem('prd-theme')||'dark');

        // ── Language ───────────────────────────────────────────────────
        async function toggleLanguage(){
          const l=currentLang==='pt-BR'?'en-US':'pt-BR';
          currentLang=l;
          localStorage.setItem('prd-lang',l);
          applyI18n();
          await saveSettings({language:l});
        }

        // ── Settings API ───────────────────────────────────────────────
        async function saveSettings(patch){
          const body={collapsedRepos:[...collapsedRepos],myPrsFilter:myPrsFilter??'',myPrsView,sidebarOpen,repoOrder,...patch};
          try{
            const r=await fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(body)});
            if(!r.ok)return;
            render(await r.json());
          }catch{}
        }
        function persistUiState(){
          fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({collapsedRepos:[...collapsedRepos],myPrsFilter:myPrsFilter??'',myPrsView,sidebarOpen,repoOrder})}).catch(()=>{});
        }

        // ── Sidebar ────────────────────────────────────────────────────
        let sidebarOpen=localStorage.getItem('prd-sidebar')!=='0';
        let repoOrder=[];
        function applySidebar(){
          const g=document.getElementById('main-grid');if(!g)return;
          const lbl=document.getElementById('stoggle-label');
          if(sidebarOpen){g.classList.remove('sidebar-closed');if(lbl)lbl.textContent=t('closeSidebar')}
          else{g.classList.add('sidebar-closed');if(lbl)lbl.textContent=t('openSidebar')}
        }
        function toggleSidebar(){sidebarOpen=!sidebarOpen;localStorage.setItem('prd-sidebar',sidebarOpen?'1':'0');applySidebar();persistUiState()}

        // ── Tabs ───────────────────────────────────────────────────────
        let currentTab='my';
        function setTab(tab){
          currentTab=tab;
          document.querySelectorAll('.tab-panel').forEach(p=>p.classList.remove('active'));
          document.getElementById('tab-'+tab).classList.add('active');
          document.querySelectorAll('.tab-btn').forEach(b=>{b.classList.toggle('active',b.dataset.tab===tab)});
          document.getElementById('stoggle-btn').style.display=(tab==='my')?'inline-flex':'none';
        }
        setTab('my');

        // ── Notification + audio ───────────────────────────────────────
        let ivSecs=300,cd=0,tmr=null,prevState={},notifInited=false,settingsInitialized=false;
        let notifPrefs={commentVisual:true,commentAudio:true,ciVisual:true,ciAudio:true,mergedVisual:true,mergedAudio:true,approvedVisual:true,approvedAudio:true,readyVisual:true,readyAudio:true};
        let hideBotReviews=false;
        let prevMyNums=new Set(),prevMergedNums=new Set();
        function ck(p){return p.commentState+'|'+p.lastCommentBy+'|'+(p.lastCommentAt||p.lastCommentAgo||'')}
        function detectAndNotify(data){
          const prs=data.myPrs||[];const mrg=data.mergedPrs||[];
          if(!notifInited){
            for(const p of prs)prevState[p.number]={ck:ck(p),cif:p.ciState==='Failing',rv:p.reviewState,dr:p.isDraft};
            prevMyNums=new Set(prs.map(p=>p.number));
            prevMergedNums=new Set(mrg.map(p=>p.number));
            notifInited=true;return;
          }
          const curMergedNums=new Set(mrg.map(p=>p.number));
          for(const num of prevMyNums){
            if(curMergedNums.has(num)&&!prevMergedNums.has(num)){
              const merged=mrg.find(p=>p.number===num);
              if(merged){if(notifPrefs.mergedVisual)showMergedNotif(merged);if(notifPrefs.mergedAudio)playAudio('/audio/done.mp3',1)}
            }
          }
          for(const p of prs){
            const prev=prevState[p.number]||{};const k=ck(p);
            if(p.commentState==='New'&&k!==prev.ck){if(notifPrefs.commentVisual)showNotif('comment',p);if(notifPrefs.commentAudio)playAudio('/audio/message.mp3',1)}
            if(p.ciState==='Failing'&&!prev.cif){if(notifPrefs.ciVisual)showNotif('ci',p);if(notifPrefs.ciAudio)playAudio('/audio/error.mp3',3)}
            if(p.reviewState==='Approved'&&prev.rv&&prev.rv!=='Approved'){if(notifPrefs.approvedVisual)showApprovedNotif(p);if(notifPrefs.approvedAudio)playAudio('/audio/done.mp3',1)}
            if(!p.isDraft&&prev.dr===true){if(notifPrefs.readyVisual)showNotif('ready',p);if(notifPrefs.readyAudio)playAudio('/audio/done.mp3',1)}
            prevState[p.number]={ck:k,cif:p.ciState==='Failing',rv:p.reviewState,dr:p.isDraft};
          }
          prevMyNums=new Set(prs.map(p=>p.number));
          prevMergedNums=curMergedNums;
        }
        async function playAudio(src,n){for(let i=0;i<n;i++)await new Promise(r=>{const a=new Audio(src);a.onended=r;a.onerror=r;a.play().catch(r)})}
        function showNotif(type,pr){
          const d=document.createElement('div');d.className='notif '+type;
          const msg=type==='comment'?`💬 ${currentLang==='en-US'?'New comment on':'Novo comentário em'} #${pr.number}`:`🔴 ${currentLang==='en-US'?'CI failed on':'CI com erro em'} #${pr.number}`;
          d.innerHTML=`<button class="nclose" onclick="this.parentNode.remove()">×</button><div class="ntitle">${e(msg)}</div><div class="nbody">${e(pr.repo)} · ${e(pr.title)}</div>`;
          document.getElementById('nstack').prepend(d);
          setTimeout(()=>{if(d.parentNode)d.remove()},8000);
        }
        function launchConfetti(){
          if(typeof confetti==='undefined')return;
          confetti({particleCount:120,spread:70,origin:{y:0.6}});
          setTimeout(()=>confetti({particleCount:60,spread:100,origin:{x:.2,y:.55}}),200);
          setTimeout(()=>confetti({particleCount:60,spread:100,origin:{x:.8,y:.55}}),400);
        }
        function showMergedNotif(pr){
          const d=document.createElement('div');d.className='notif merged';
          const msg=`🎉 ${currentLang==='en-US'?'Merged!':'Mergeado!'} #${pr.number}`;
          d.innerHTML=`<button class="nclose" onclick="this.parentNode.remove()">×</button><div class="ntitle" style="color:var(--green)">${e(msg)}</div><div class="nbody">${e(pr.repo)} · ${e(pr.title)}</div>`;
          document.getElementById('nstack').prepend(d);
          launchConfetti();
          setTimeout(()=>{if(d.parentNode)d.remove()},12000);
        }
        function showApprovedNotif(pr){
          const d=document.createElement('div');d.className='notif approved';
          const msg=`✅ ${currentLang==='en-US'?'Approved!':'Aprovado!'} #${pr.number} ${currentLang==='en-US'?'by':'por'} ${e(pr.reviewedBy||'?')}`;
          d.innerHTML=`<button class="nclose" onclick="this.parentNode.remove()">×</button><div class="ntitle" style="color:var(--green)">${msg}</div><div class="nbody">${e(pr.repo)} · ${e(pr.title)}</div>`;
          document.getElementById('nstack').prepend(d);
          setTimeout(()=>{if(d.parentNode)d.remove()},10000);
        }
        function testNotif(type){
          if(type==='merged'){if(notifPrefs.mergedAudio)playAudio('/audio/done.mp3',1);if(notifPrefs.mergedVisual)showMergedNotif({number:42,title:'PR de teste',repo:'repo/test'})}
          else if(type==='comment'){if(notifPrefs.commentAudio)playAudio('/audio/message.mp3',1);if(notifPrefs.commentVisual)showNotif(type,{number:0,title:'PR de teste',repo:'repo/test'})}
          else if(type==='approved'){if(notifPrefs.approvedAudio)playAudio('/audio/done.mp3',1);if(notifPrefs.approvedVisual)showApprovedNotif({number:42,title:'PR de teste',repo:'repo/test',reviewedBy:'colega'})}
          else if(type==='ready'){if(notifPrefs.readyAudio)playAudio('/audio/done.mp3',1);if(notifPrefs.readyVisual)showNotif('ready',{number:0,title:'PR de teste',repo:'repo/test'})}
          else{if(notifPrefs.ciAudio)playAudio('/audio/error.mp3',1);if(notifPrefs.ciVisual)showNotif(type,{number:0,title:'PR de teste',repo:'repo/test'})}
        }
        function updateNotifPrefButtons(){
          const pairs=[['npref-comment-visual',notifPrefs.commentVisual],['npref-comment-audio',notifPrefs.commentAudio],['npref-ci-visual',notifPrefs.ciVisual],['npref-ci-audio',notifPrefs.ciAudio],['npref-merged-visual',notifPrefs.mergedVisual],['npref-merged-audio',notifPrefs.mergedAudio],['npref-approved-visual',notifPrefs.approvedVisual],['npref-approved-audio',notifPrefs.approvedAudio],['npref-ready-visual',notifPrefs.readyVisual],['npref-ready-audio',notifPrefs.readyAudio]];
          for(const[id,on]of pairs){const el=document.getElementById(id);if(el)el.classList.toggle('on',on)}
        }
        function isBotAuthor(a){
          const l=(a||'').toLowerCase();
          return l.endsWith('[bot]')||l.endsWith('-bot')||l.endsWith('_bot')||['dependabot','renovate','github-actions','snyk-bot','codecov-commenter'].includes(l);
        }
        function toggleRevHideBots(){
          hideBotReviews=!hideBotReviews;
          updateRevHideBotsBtn();
          fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({hideBotReviews})}).catch(()=>{});
          renderRevFull(_lastRevPrs);
          renderRevSidebar(_lastRevPrs);
        }
        function updateRevHideBotsBtn(){
          const btn=document.getElementById('rev-hide-bots-btn');
          if(!btn)return;
          btn.style.borderColor=hideBotReviews?'var(--muted)':'';
          btn.style.color=hideBotReviews?'var(--text)':'';
        }
        function toggleNotifPref(type,kind){
          const key=type+(kind==='visual'?'Visual':'Audio');
          notifPrefs[key]=!notifPrefs[key];
          updateNotifPrefButtons();
          const sk='notif'+type.charAt(0).toUpperCase()+type.slice(1)+(kind==='visual'?'Visual':'Audio');
          const body={};body[sk]=notifPrefs[key];
          fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(body)}).catch(()=>{});
        }
        function saveInterval(){
          const v=parseInt(document.getElementById('interval-input')?.value||'300');
          if(isNaN(v)||v<30)return;
          fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({intervalSeconds:v})}).catch(()=>{});
          ivSecs=v;cd=v;
        }
        function toggleUserMenu(){const dd=document.getElementById('user-dd');dd.style.display=dd.style.display==='none'?'block':'none'}
        function closeUserMenu(){const dd=document.getElementById('user-dd');if(dd)dd.style.display='none'}
        document.addEventListener('click',e=>{const w=document.getElementById('user-menu-wrap');if(w&&!w.contains(e.target))closeUserMenu()})

        // ── Repo ordering ──────────────────────────────────────────────
        function sortRepos(names){
          return [...names].sort((a,b)=>{
            const ia=repoOrder.indexOf(a),ib=repoOrder.indexOf(b);
            if(ia>=0&&ib>=0)return ia-ib;
            if(ia>=0)return -1;
            if(ib>=0)return 1;
            return a.localeCompare(b);
          });
        }
        function reorderBtns(repo,idx,total){
          const r=JSON.stringify(repo).replace(/"/g,'&quot;');
          const bs=`background:none;border:none;cursor:pointer;color:var(--muted);padding:1px 3px;border-radius:3px;line-height:1;font-size:9px;opacity:.3;transition:opacity .12s`;
          const up=idx>0?`<button onclick="event.stopPropagation();moveRepo(${r},-1)" style="${bs}" onmouseover="this.style.opacity='1'" onmouseout="this.style.opacity='.3'">▲</button>`:`<span style="display:inline-block;width:15px"></span>`;
          const dn=idx<total-1?`<button onclick="event.stopPropagation();moveRepo(${r},1)" style="${bs}" onmouseover="this.style.opacity='1'" onmouseout="this.style.opacity='.3'">▼</button>`:`<span style="display:inline-block;width:15px"></span>`;
          return `<div style="display:flex;flex-direction:column;gap:1px;margin-left:6px">${up}${dn}</div>`;
        }
        function moveRepo(repo,dir){
          const groups=[...document.querySelectorAll('#s-my .rgroup')];
          const repos=groups.map(g=>g.dataset.repo).filter(r=>r&&!r.startsWith('__s__'));
          const idx=repos.indexOf(repo);
          if(idx<0)return;
          const ni=idx+dir;
          if(ni<0||ni>=repos.length)return;
          [repos[idx],repos[ni]]=[repos[ni],repos[idx]];
          repoOrder=repos;
          renderMyPrs(_lastMyPrs);
          persistUiState();
        }
        function cleanupRepoOrder(data){
          const valid=new Set((data.myPrs||[]).map(p=>p.repo));
          const next=repoOrder.filter(r=>valid.has(r));
          if(next.length!==repoOrder.length){repoOrder=next;persistUiState()}
        }

        // ── Badge helpers ──────────────────────────────────────────────
        const UCLS={CiFailing:'u-ci',ChangesRequested:'u-ch',CommentsPending:'u-cp',AwaitingReview:'u-ar',Approved:'u-ap',Normal:''};
        function e(s){return(s||'').replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;')}
        function b(cls,txt){return txt?`<span class="b ${cls}">${e(txt)}</span>`:''}
        function bRev(p){
          if(p.reviewState==='Approved')return b('bg',`✓ ${p.reviewedBy}${p.reviewedAgo?' · '+p.reviewedAgo:''}`);
          if(p.reviewState==='ChangesRequested')return b('br',t('changesRequested'));
          if(p.reviewState==='Pending')return '';
          return b('bx',t('noReview'));
        }
        function bRevMeta(p){
          if(p.reviewState!=='Pending')return '';
          const rv=(p.pendingReviewers||[]).join(', ');
          return `<div class="prc-meta" style="margin-top:4px">${b('bv',t('awaitingApproval'))}${rv?b('bx',rv):''}</div>`;
        }
        function bCI(state,fail){
          if(state==='Passing')return b('bg',t('ciPassing'));
          if(state==='Failing')return b('br','✗ '+(fail||'CI'));
          if(state==='Pending')return b('by',t('ciPending'));
          return '';
        }
        function bCmt(p){
          if(p.commentState==='New'){const t2=p.openThreads>0?` · ${p.openThreads}t`:'';return b('by',`💬 ${p.lastCommentBy} ${p.lastCommentAgo}${t2}`)}
          if(p.commentState==='HasComments'||p.openThreads>0)return b('bx',`💬${p.openThreads>0?' '+p.openThreads+'t':''}`);
          return '';
        }
        function bMrg(s){
          if(s==='Conflict')return b('br',t('conflict'));
          if(s==='BehindBase')return b('by',t('outdated'));
          return '';
        }
        function bBranch(ref){return ref?`<span class="branch">⎇ ${e(ref)}</span>`:''}

        const MERGE_SVG=`<svg width="10" height="10" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M5 13l4 4L19 7"/></svg>`;

        // ── Repo collapse ─────────────────────────────────────────────
        const collapsedRepos=new Set(JSON.parse(localStorage.getItem('prd-collapsed')||'[]'));
        function toggleRepo(hdr){
          const rg=hdr.closest('.rgroup');
          const repo=rg.dataset.repo;
          const body=rg.querySelector('.rgroup-body');
          const chv=hdr.querySelector('.rg-chevron');
          if(collapsedRepos.has(repo)){
            collapsedRepos.delete(repo);
            body.style.display='';
            if(chv)chv.style.transform='';
          }else{
            collapsedRepos.add(repo);
            body.style.display='none';
            if(chv)chv.style.transform='rotate(-90deg)';
          }
          localStorage.setItem('prd-collapsed',JSON.stringify([...collapsedRepos]));
          persistUiState();
        }
        function statusChips(list){
          const cnt={};
          for(const p of list)cnt[p.urgency]=(cnt[p.urgency]||0)+1;
          const out=[];
          if(cnt.CiFailing)out.push(`<span class="b br">✗ CI${cnt.CiFailing>1?' ×'+cnt.CiFailing:''}</span>`);
          if(cnt.ChangesRequested)out.push(`<span class="b br">✗ rev${cnt.ChangesRequested>1?' ×'+cnt.ChangesRequested:''}</span>`);
          if(cnt.CommentsPending)out.push(`<span class="b by">💬${cnt.CommentsPending>1?' ×'+cnt.CommentsPending:''}</span>`);
          if(cnt.AwaitingReview)out.push(`<span class="b bi">⏳${cnt.AwaitingReview>1?' ×'+cnt.AwaitingReview:''}</span>`);
          if(cnt.Approved)out.push(`<span class="b bg">✓${cnt.Approved>1?' ×'+cnt.Approved:''}</span>`);
          return out.join('');
        }

        // ── Collapse all / expand all ─────────────────────────────────
        function toggleAllRepos(){
          const groups=[...document.querySelectorAll('#s-my .rgroup')];
          if(!groups.length)return;
          const repos=groups.map(g=>g.dataset.repo).filter(Boolean);
          const allCollapsed=repos.length>0&&repos.every(r=>collapsedRepos.has(r));
          if(allCollapsed){
            repos.forEach(r=>collapsedRepos.delete(r));
            groups.forEach(g=>{const b=g.querySelector('.rgroup-body');const c=g.querySelector('.rg-chevron');if(b)b.style.display='';if(c)c.style.transform=''});
          }else{
            repos.forEach(r=>collapsedRepos.add(r));
            groups.forEach(g=>{const b=g.querySelector('.rgroup-body');const c=g.querySelector('.rg-chevron');if(b)b.style.display='none';if(c)c.style.transform='rotate(-90deg)'});
          }
          localStorage.setItem('prd-collapsed',JSON.stringify([...collapsedRepos]));
          persistUiState();
          updateCollapseBtn();
        }
        function updateCollapseBtn(){
          const btn=document.getElementById('collapse-all-btn');
          if(!btn)return;
          const groups=[...document.querySelectorAll('#s-my .rgroup')];
          const repos=groups.map(g=>g.dataset.repo).filter(Boolean);
          const allCollapsed=repos.length>0&&repos.every(r=>collapsedRepos.has(r));
          const lbl=document.getElementById('collapse-all-label');
          const ic=btn.querySelector('.icon-collapse');
          const ie=btn.querySelector('.icon-expand');
          if(lbl)lbl.textContent=allCollapsed?t('expandAll'):t('collapseAll');
          if(ic)ic.style.display=allCollapsed?'none':'';
          if(ie)ie.style.display=allCollapsed?'':'none';
        }
        function cleanupCollapsedRepos(data){
          const valid=new Set([...(data.myPrs||[]).map(p=>p.repo),...(data.reviewPrs||[]).map(p=>p.repo)]);
          let changed=false;
          for(const r of[...collapsedRepos]){if(!r.startsWith('__s__')&&!valid.has(r)){collapsedRepos.delete(r);changed=true}}
          if(changed)persistUiState();
        }

        // ── My PRs view + filter ──────────────────────────────────────
        let myPrsView=localStorage.getItem('prd-my-view')||'repo';
        let _lastMyPrs=[];
        let myPrsFilter=null;
        function setMyView(v){
          myPrsView=v;
          localStorage.setItem('prd-my-view',v);
          document.getElementById('vbtn-repo')?.classList.toggle('active',v==='repo');
          document.getElementById('vbtn-status')?.classList.toggle('active',v==='status');
          renderMyPrs(_lastMyPrs);
          persistUiState();
        }
        function setMyFilter(urgency){
          myPrsFilter=urgency;
          renderMyPrs(_lastMyPrs);
          persistUiState();
        }
        function renderStatusFilters(prs){
          const el=document.getElementById('s-my-filters');
          if(!el)return;
          const cnt={};for(const p of prs)cnt[p.urgency]=(cnt[p.urgency]||0)+1;
          const ORDER=['CiFailing','ChangesRequested','CommentsPending','AwaitingReview','Approved','Normal'];
          const existing=ORDER.filter(u=>cnt[u]);
          if(!existing.length){el.innerHTML='';return}
          const all=myPrsFilter===null;
          const chips=[
            `<button class="fchip" style="${all?'border-color:var(--muted);color:var(--text)':''}" onclick="setMyFilter(null)">${t('filterAll')} <span class="tcnt">${prs.length}</span></button>`,
            ...existing.map(u=>{
              const active=myPrsFilter===u;
              const col=urgencyColor(u);
              const style=active?`border-color:${col};color:${col}`:'';
              return `<button class="fchip" style="${style}" onclick="setMyFilter('${u}')">${t('st'+u)} <span class="tcnt">${cnt[u]}</span></button>`;
            })
          ];
          el.innerHTML=`<div style="display:flex;gap:6px;flex-wrap:wrap">${chips.join('')}</div>`;
        }

        // ── Group dot ─────────────────────────────────────────────────
        function groupDot(list){
          const u=list.map(p=>p.urgency);
          if(u.some(x=>x==='CiFailing'||x==='ChangesRequested'))return 'var(--red)';
          if(u.some(x=>x==='CommentsPending'))return 'var(--amber)';
          if(u.some(x=>x==='AwaitingReview'))return 'var(--indigo)';
          if(u.some(x=>x==='Approved'))return 'var(--green)';
          return 'var(--muted)';
        }
        function urgencyColor(u){
          if(u==='CiFailing'||u==='ChangesRequested')return 'var(--red)';
          if(u==='CommentsPending')return 'var(--amber)';
          if(u==='AwaitingReview')return 'var(--indigo)';
          if(u==='Approved')return 'var(--green)';
          return 'var(--muted)';
        }

        // ── PR card renderers ──────────────────────────────────────────
        function renderMyCard(p){
          const cls=(UCLS[p.urgency]||'')+(p.isDraft?' is-draft':'');
          return `<div class="prc ${cls}">
            <div class="prc-row">
              <div class="prc-left">
                <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
                <div class="prc-meta">${p.isDraft?b('bx',t('draft')):''}${bBranch(p.headRef)}<span>${e(p.ageAgo)}</span></div>
                ${bRevMeta(p)}
              </div>
              <div class="prc-right">${bRev(p)}${bCI(p.ciState,p.failingCheck)}${bCmt(p)}${bMrg(p.mergeState)}</div>
            </div>
          </div>`;
        }

        function renderRevCard(p){
          const cls=(UCLS[p.urgency]||'')+(p.isDraft?' is-draft':'');
          return `<div class="prc ${cls}">
            <div class="prc-row">
              <div class="prc-left">
                <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
                <div class="prc-meta">${b('bx',p.author)}${p.isDraft?b('bx',t('draft')):''}${bBranch(p.headRef)}<span>${e(p.ageAgo)}</span></div>
              </div>
              <div class="prc-right">${bCI(p.ciState,p.failingCheck)}${bCmt(p)}${bMrg(p.mergeState)}</div>
            </div>
          </div>`;
        }

        function renderMyCardWithRepo(p){
          const cls=(UCLS[p.urgency]||'')+(p.isDraft?' is-draft':'');
          return `<div class="prc ${cls}">
            <div class="prc-row">
              <div class="prc-left">
                <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
                <div class="prc-meta"><span class="branch">${e(p.repo)}</span>${p.isDraft?b('bx',t('draft')):''}${bBranch(p.headRef)}<span>${e(p.ageAgo)}</span></div>
                ${bRevMeta(p)}
              </div>
              <div class="prc-right">${bRev(p)}${bCI(p.ciState,p.failingCheck)}${bCmt(p)}${bMrg(p.mergeState)}</div>
            </div>
          </div>`;
        }

        function renderMyPrsByStatus(prs){
          const s=document.getElementById('s-my');
          if(!prs.length){s.innerHTML=`<div class="empty">${t('emptyMyPrs')}</div>`;return}
          const ORDER=['CiFailing','ChangesRequested','CommentsPending','AwaitingReview','Approved','Normal'];
          const groups={};for(const p of prs)(groups[p.urgency]=groups[p.urgency]||[]).push(p);
          const CHV=`<svg class="rg-chevron" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg>`;
          s.innerHTML=ORDER.filter(u=>groups[u]?.length).map(urgency=>{
            const list=groups[urgency];
            const col=urgencyColor(urgency);
            const key='__s__'+urgency;
            const collapsed=collapsedRepos.has(key);
            const chv=`<svg class="rg-chevron" style="${collapsed?'transform:rotate(-90deg)':''}" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg>`;
            return `<div class="rgroup" data-repo="${key}"><div class="rgroup-hdr" onclick="toggleRepo(this)"><div style="display:flex;align-items:center;gap:6px">${chv}<span style="width:8px;height:8px;border-radius:9999px;background:${col};display:inline-block;flex-shrink:0"></span><span class="rgroup-name" style="color:${col}">${t('st'+urgency)}</span><span class="tcnt">${list.length}</span></div></div><div class="rgroup-body"${collapsed?' style="display:none"':''}>${list.map(renderMyCardWithRepo).join('')}</div></div>`;
          }).join('');
        }

        // ── Section renderers ──────────────────────────────────────────
        function renderMyPrs(prs){
          _lastMyPrs=prs;
          renderStatusFilters(prs);
          const filtered=myPrsFilter?prs.filter(p=>p.urgency===myPrsFilter):prs;
          if(myPrsView==='status'){renderMyPrsByStatus(filtered);updateCollapseBtn();return}
          const s=document.getElementById('s-my');
          if(!filtered.length){s.innerHTML=`<div class="empty">${t('emptyMyPrs')}</div>`;updateCollapseBtn();return}
          const groups={};for(const p of filtered)(groups[p.repo]=groups[p.repo]||[]).push(p);
          const sortedRepos=sortRepos(Object.keys(groups));
          s.innerHTML=sortedRepos.map((repo,idx)=>{
            const list=groups[repo];const col=collapsedRepos.has(repo);
            return `<div class="rgroup" data-repo="${e(repo)}"><div class="rgroup-hdr" onclick="toggleRepo(this)"><div style="display:flex;align-items:center;gap:6px"><svg class="rg-chevron" style="${col?'transform:rotate(-90deg)':''}" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg><span style="width:8px;height:8px;border-radius:9999px;background:${groupDot(list)};display:inline-block;flex-shrink:0"></span><span class="rgroup-name">${e(repo)}</span><span class="tcnt">${list.length}</span></div><div style="display:flex;align-items:center;gap:4px">${statusChips(list)}${reorderBtns(repo,idx,sortedRepos.length)}</div></div><div class="rgroup-body"${col?' style="display:none"':''}>${list.map(renderMyCard).join('')}</div></div>`;
          }).join('');
          updateCollapseBtn();
        }

        function renderRevSidebar(prs){
          const s=document.getElementById('s-rev-side');
          const visible=hideBotReviews?prs.filter(p=>!isBotAuthor(p.author)):prs;
          if(!visible.length){s.innerHTML='';return}
          const items=visible.map(p=>`<div class="srev">
            <div class="srev-repo">${e(p.repo)} · #${p.number}</div>
            <a class="srev-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a>
            <div class="prc-meta">${bCI(p.ciState,p.failingCheck)}${bMrg(p.mergeState)}<span>${e(p.ageAgo)}</span></div>
          </div>`).join('');
          s.innerHTML=`<div class="sc"><div class="sc-hdr"><div class="sc-title"><span style="width:7px;height:7px;border-radius:9999px;background:var(--amber);display:inline-block"></span>${t('sidebarReviews')}</div><span class="b by">${t('pendingCount',visible.length)}</span></div>${items}</div>`;
        }

        // ── Reviews tab view + filter ─────────────────────────────────
        let _lastRevPrs=[];
        let revFilter=null;
        let revAuthorFilter=null;
        function setRevFilter(urgency){
          revFilter=urgency;
          renderRevFull(_lastRevPrs);
        }
        function setRevAuthorFilter(author){
          revAuthorFilter=author;
          renderRevFull(_lastRevPrs);
        }
        function renderRevFilters(visible){
          const el=document.getElementById('s-rev-filters');
          if(!el)return;
          let html='';
          const cnt={};for(const p of visible)cnt[p.urgency]=(cnt[p.urgency]||0)+1;
          const ORDER=['CiFailing','ChangesRequested','CommentsPending','AwaitingReview','Approved','Normal'];
          const existing=ORDER.filter(u=>cnt[u]);
          if(existing.length){
            const all=revFilter===null;
            const chips=[
              `<button class="fchip" style="${all?'border-color:var(--muted);color:var(--text)':''}" onclick="setRevFilter(null)">${t('filterAll')} <span class="tcnt">${visible.length}</span></button>`,
              ...existing.map(u=>{
                const active=revFilter===u;const col=urgencyColor(u);
                return `<button class="fchip" style="${active?`border-color:${col};color:${col}`:''}" onclick="setRevFilter('${u}')">${t('st'+u)} <span class="tcnt">${cnt[u]}</span></button>`;
              })
            ];
            html+=`<div style="display:flex;gap:6px;flex-wrap:wrap">${chips.join('')}</div>`;
          }
          const acnt={};for(const p of visible)if(p.author)acnt[p.author]=(acnt[p.author]||0)+1;
          const authors=Object.keys(acnt).sort((a,b)=>acnt[b]-acnt[a]);
          if(authors.length>=2){
            const allA=revAuthorFilter===null;
            const achips=[
              `<button class="fchip" style="${allA?'border-color:var(--muted);color:var(--text)':''}" onclick="setRevAuthorFilter(null)">${t('filterAll')} <span class="tcnt">${visible.length}</span></button>`,
              ...authors.map(a=>{
                const active=revAuthorFilter===a;
                return `<button class="fchip" style="${active?'border-color:var(--indigo);color:var(--indigo)':''}" onclick="setRevAuthorFilter('${a}')">${e(a)} <span class="tcnt">${acnt[a]}</span></button>`;
              })
            ];
            html+=(html?'<div style="height:6px"></div>':'')+`<div style="display:flex;gap:6px;flex-wrap:wrap">${achips.join('')}</div>`;
          }
          el.innerHTML=html;
        }
        function toggleAllRevRepos(){
          const groups=[...document.querySelectorAll('#s-rev-full .rgroup')];
          if(!groups.length)return;
          const repos=groups.map(g=>g.dataset.repo).filter(Boolean);
          const allCollapsed=repos.length>0&&repos.every(r=>collapsedRepos.has(r));
          if(allCollapsed){
            repos.forEach(r=>collapsedRepos.delete(r));
            groups.forEach(g=>{const b=g.querySelector('.rgroup-body');const c=g.querySelector('.rg-chevron');if(b)b.style.display='';if(c)c.style.transform=''});
          }else{
            repos.forEach(r=>collapsedRepos.add(r));
            groups.forEach(g=>{const b=g.querySelector('.rgroup-body');const c=g.querySelector('.rg-chevron');if(b)b.style.display='none';if(c)c.style.transform='rotate(-90deg)'});
          }
          localStorage.setItem('prd-collapsed',JSON.stringify([...collapsedRepos]));
          persistUiState();
          updateRevCollapseBtn();
        }
        function updateRevCollapseBtn(){
          const btn=document.getElementById('rev-collapse-all-btn');
          if(!btn)return;
          const groups=[...document.querySelectorAll('#s-rev-full .rgroup')];
          const repos=groups.map(g=>g.dataset.repo).filter(Boolean);
          const allCollapsed=repos.length>0&&repos.every(r=>collapsedRepos.has(r));
          const lbl=document.getElementById('rev-collapse-all-label');
          const ic=btn.querySelector('.rev-icon-collapse');
          const ie=btn.querySelector('.rev-icon-expand');
          if(lbl)lbl.textContent=allCollapsed?t('expandAll'):t('collapseAll');
          if(ic)ic.style.display=allCollapsed?'none':'';
          if(ie)ie.style.display=allCollapsed?'':'none';
        }

        function renderRevFull(prs){
          _lastRevPrs=prs;
          const visible=hideBotReviews?prs.filter(p=>!isBotAuthor(p.author)):prs;
          renderRevFilters(visible);
          updateRevHideBotsBtn();
          const filtered=visible.filter(p=>(!revFilter||p.urgency===revFilter)&&(!revAuthorFilter||p.author===revAuthorFilter));
          const s=document.getElementById('s-rev-full');
          if(!filtered.length){s.innerHTML=`<div class="empty">${t('emptyReviews')}</div>`;updateRevCollapseBtn();return}
          const groups={};for(const p of filtered)(groups[p.repo]=groups[p.repo]||[]).push(p);
          s.innerHTML=Object.entries(groups).sort(([a],[b])=>a.localeCompare(b)).map(([repo,list])=>{
            const col=collapsedRepos.has(repo);
            return `<div class="rgroup" data-repo="${e(repo)}"><div class="rgroup-hdr" onclick="toggleRepo(this)"><div style="display:flex;align-items:center;gap:6px"><svg class="rg-chevron" style="${col?'transform:rotate(-90deg)':''}" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg><span style="width:8px;height:8px;border-radius:9999px;background:${groupDot(list)};display:inline-block;flex-shrink:0"></span><span class="rgroup-name">${e(repo)}</span><span class="tcnt">${list.length}</span></div><div style="display:flex;align-items:center;gap:4px">${statusChips(list)}</div></div><div class="rgroup-body"${col?' style="display:none"':''}>${list.map(renderRevCard).join('')}</div></div>`;
          }).join('');
          updateRevCollapseBtn();
        }

        function renderMrgSidebar(prs){
          const s=document.getElementById('s-mrg-side');
          if(!prs.length){s.innerHTML='';return}
          const items=prs.slice(0,6).map(p=>`<div class="smerge-item">
            <div class="smerge-icon">${MERGE_SVG}</div>
            <div><a class="smerge-link" href="${e(p.url)}" target="_blank" rel="noopener">#${p.number} ${e(p.title)}</a><div class="smerge-sub">${e(p.repo)} · ${e(p.mergedAgo)}</div></div>
          </div>`).join('');
          s.innerHTML=`<div class="sc"><div class="sc-hdr"><div class="sc-title"><span style="width:7px;height:7px;border-radius:9999px;background:var(--indigo);display:inline-block"></span>${t('sidebarMerged')}</div><span style="font-size:11px;font-family:monospace;color:var(--muted)">${t('recentCount',prs.length)}</span></div>${items}</div>`;
        }

        function renderMrgFull(prs){
          const s=document.getElementById('s-mrg-full');
          if(!prs.length){s.innerHTML=`<div class="empty">${t('emptyMerged')}</div>`;return}
          const rows=prs.map(p=>`<div class="prc">
            <div class="prc-row">
              <div class="prc-left">
                <div class="prc-trow"><span style="font-size:11px;font-family:monospace;color:var(--muted)">${e(p.repo)}</span><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
              </div>
              <div class="prc-right" style="flex-direction:row;align-items:center;gap:8px">${b('bx',p.mergedAgo)}<span style="font-size:11px;color:var(--muted)">${t('tookDuration',p.duration)}</span></div>
            </div>
          </div>`).join('');
          s.innerHTML=`<div class="rgroup">${rows}</div>`;
        }

        // ── Metrics ────────────────────────────────────────────────────
        function updateMetrics(data){
          const my=data.myPrs||[];const rev=data.reviewPrs||[];const mrg=data.mergedPrs||[];
          const ci=my.filter(p=>p.ciState==='Failing').length;
          const attn=my.filter(p=>p.needsAction).length;
          document.getElementById('m-open').textContent=my.length;
          document.getElementById('m-open-sub').textContent=attn?t('metricsNeedAction',attn):t('metricsOk');
          document.getElementById('m-ci').textContent=ci;
          document.getElementById('m-ci-sub').textContent=ci===1?t('metricsBlocker'):t('metricsBlockers');
          document.getElementById('m-rev').textContent=rev.length;
          document.getElementById('m-rev-sub').textContent=rev.length===1?t('metricsPending'):t('metricsPendings');
          document.getElementById('m-mrg').textContent=mrg.length;
          document.getElementById('m-mrg-sub').textContent=t('metricsRecent');
          document.getElementById('tc-my').textContent=my.length;
          document.getElementById('tc-rev').textContent=rev.length;
          document.getElementById('tc-mrg').textContent=mrg.length;
        }

        // ── Main render ────────────────────────────────────────────────
        function render(data){
          const dp=document.getElementById('dot-ping');const dc=document.getElementById('dot-core');
          if(data.isLoading){dp.style.background='#f59e0b';dc.style.background='#f59e0b';return}
          dp.style.background='';dc.style.background='';

          // Sync theme + lang from server (authoritative)
          if(data.theme){localStorage.setItem('prd-theme',data.theme);applyTheme(data.theme)}
          if(data.language&&data.language!==currentLang){currentLang=data.language;localStorage.setItem('prd-lang',data.language)}
          applyI18n();
          if(!settingsInitialized){
            settingsInitialized=true;
            if(Array.isArray(data.collapsedRepos)){collapsedRepos.clear();data.collapsedRepos.forEach(r=>collapsedRepos.add(r))}
            if(data.myPrsView==='repo'||data.myPrsView==='status'){myPrsView=data.myPrsView;document.getElementById('vbtn-repo')?.classList.toggle('active',myPrsView==='repo');document.getElementById('vbtn-status')?.classList.toggle('active',myPrsView==='status')}
            myPrsFilter=data.myPrsFilter||null;
            if(data.sidebarOpen!==undefined)sidebarOpen=!!data.sidebarOpen;
            if(Array.isArray(data.repoOrder))repoOrder=data.repoOrder;
            if(data.notifCommentVisual!==undefined)notifPrefs.commentVisual=!!data.notifCommentVisual;
            if(data.notifCommentAudio!==undefined)notifPrefs.commentAudio=!!data.notifCommentAudio;
            if(data.notifCiVisual!==undefined)notifPrefs.ciVisual=!!data.notifCiVisual;
            if(data.notifCiAudio!==undefined)notifPrefs.ciAudio=!!data.notifCiAudio;
            if(data.notifMergedVisual!==undefined)notifPrefs.mergedVisual=!!data.notifMergedVisual;
            if(data.notifMergedAudio!==undefined)notifPrefs.mergedAudio=!!data.notifMergedAudio;
            if(data.notifApprovedVisual!==undefined)notifPrefs.approvedVisual=data.notifApprovedVisual;
            if(data.notifApprovedAudio!==undefined)notifPrefs.approvedAudio=data.notifApprovedAudio;
            if(data.notifReadyVisual!==undefined)notifPrefs.readyVisual=data.notifReadyVisual;
            if(data.notifReadyAudio!==undefined)notifPrefs.readyAudio=data.notifReadyAudio;
            if(data.hideBotReviews!==undefined)hideBotReviews=!!data.hideBotReviews;
            const inp=document.getElementById('interval-input');if(inp)inp.value=data.intervalSeconds||300;
            updateNotifPrefButtons();
            updateRevHideBotsBtn();
          }

          document.getElementById('loading').style.display='none';
          document.getElementById('content').style.display='';
          const pill=document.getElementById('upill');
          if(data.login){
            pill.title=data.login;
            const initials=data.login.replace(/-/g,' ').split(' ').map(w=>w[0]?.toUpperCase()||'').join('').slice(0,2)||'—';
            pill.textContent=initials;
            const ddname=document.getElementById('user-dd-name');if(ddname)ddname.textContent=data.login;
          }
          const err=document.getElementById('err');
          if(data.error){err.style.display='';err.textContent='Erro: '+data.error}else{err.style.display='none'}
          detectAndNotify(data);
          updateMetrics(data);
          renderMyPrs(data.myPrs||[]);
          renderRevSidebar(data.reviewPrs||[]);
          renderRevFull(data.reviewPrs||[]);
          renderMrgSidebar(data.mergedPrs||[]);
          renderMrgFull(data.mergedPrs||[]);
          cleanupCollapsedRepos(data);
          cleanupRepoOrder(data);
          applySidebar();
          ivSecs=data.intervalSeconds||300;
          startCd();
        }

        // ── Countdown + polling ────────────────────────────────────────
        function startCd(){clearTimeout(tmr);cd=ivSecs;tick()}
        function tick(){
          const l=document.getElementById('rlabel');
          if(cd>0){l.textContent=`${cd}s`;cd--;tmr=setTimeout(tick,1000)}
          else{l.textContent=currentLang==='en-US'?'updating...':'atualizando...';go()}
        }
        async function go(){
          const dp=document.getElementById('dot-ping');const dc=document.getElementById('dot-core');
          try{
            const r=await fetch('/api/prs');if(!r.ok)throw new Error('HTTP '+r.status);
            dp.style.background='';dc.style.background='';
            render(await r.json());
          }catch(ex){
            dp.style.background='#f87171';dc.style.background='#f87171';
            const err=document.getElementById('err');err.style.display='';err.textContent=(currentLang==='en-US'?'Connection failed: ':'Falha na conexão: ')+ex.message;
            startCd();
          }
        }
        async function manual(){
          clearTimeout(tmr);
          document.getElementById('rlabel').textContent=currentLang==='en-US'?'updating...':'atualizando...';
          const icon=document.getElementById('ricon');icon.style.animation='spin .7s linear';
          try{
            const r=await fetch('/api/refresh',{method:'POST'});if(!r.ok)throw new Error('HTTP '+r.status);
            render(await r.json());
          }catch(ex){
            const err=document.getElementById('err');err.style.display='';err.textContent=(currentLang==='en-US'?'Error: ':'Falha: ')+ex.message;go();
          }finally{setTimeout(()=>{icon.style.animation=''},750)}
        }

        // Init i18n immediately with cached lang
        applyI18n();
        updateNotifPrefButtons();
        document.getElementById('vbtn-repo')?.classList.toggle('active',myPrsView==='repo');
        document.getElementById('vbtn-status')?.classList.toggle('active',myPrsView==='status');
        go();
        </script>
        </body>
        </html>
        """;
}
