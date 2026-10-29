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
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&family=JetBrains+Mono:wght@400;500;600&display=swap" rel="stylesheet">
        <link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" rel="stylesheet">
        <script src="https://cdn.jsdelivr.net/npm/canvas-confetti@1.9.3/dist/confetti.browser.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/marked@14/marked.min.js"></script>
        <script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
        <script>
          tailwind.config={darkMode:'class',theme:{extend:{fontFamily:{
            sans:['Inter','system-ui','-apple-system','sans-serif'],
            mono:['JetBrains Mono','ui-monospace','SFMono-Regular','monospace']
          }}}}
        </script>
        <style>
        /* ── Design tokens ─────────────────────────────────────────── */
        :root{
          --bg:#f8fafc;--surf:#ffffff;--subtle:#f1f5f9;--border:#e2e8f0;--text:#0f172a;--muted:#64748b;--hover:#f0f4f8;
          --red:#dc2626;--red-bg:#fef2f2;--red-bd:#fecaca;
          --amber:#b45309;--amber-bg:#fffbeb;--amber-bd:#fde68a;
          --green:#059669;--green-bg:#f0fdf4;--green-bd:#a7f3d0;
          --indigo:#4f46e5;--indigo-bg:#eef2ff;--indigo-bd:#c7d2fe;
          --violet:#7c3aed;--violet-bg:#faf5ff;--violet-bd:#e9d5ff;
          --gray-bg:#f1f5f9;--gray-text:#64748b;
          --header-bg:rgba(248,250,252,.92);
        }
        .dark{
          --bg:#1e1e2e;--surf:#252537;--subtle:#181825;--border:#313244;--text:#cdd6f4;--muted:#6c7086;--hover:#2a2a3d;
          --red:#f38ba8;--red-bg:rgba(243,139,168,.15);--red-bd:rgba(243,139,168,.35);
          --amber:#f9e2af;--amber-bg:rgba(249,226,175,.15);--amber-bd:rgba(249,226,175,.35);
          --green:#a6e3a1;--green-bg:rgba(166,227,161,.15);--green-bd:rgba(166,227,161,.35);
          --indigo:#cba6f7;--indigo-bg:rgba(203,166,247,.15);--indigo-bd:rgba(203,166,247,.35);
          --violet:#b4befe;--violet-bg:rgba(180,190,254,.15);--violet-bd:rgba(180,190,254,.35);
          --gray-bg:#313244;--gray-text:#6c7086;
          --header-bg:rgba(30,30,46,.92);
        }
        *{box-sizing:border-box;margin:0;padding:0}
        body{font-family:'Inter',system-ui,-apple-system,sans-serif;-webkit-font-smoothing:antialiased;background:var(--bg);color:var(--text);min-height:100vh;font-size:14px}

        ::-webkit-scrollbar{width:4px;height:4px}
        ::-webkit-scrollbar-track{background:transparent}
        ::-webkit-scrollbar-thumb{background:var(--border);border-radius:9999px}
        ::-webkit-scrollbar-thumb:hover{background:var(--muted)}

        .content-grid{display:grid;grid-template-columns:1fr 280px;gap:20px;align-items:start}
        .content-grid.sidebar-closed{grid-template-columns:1fr}
        .content-grid.sidebar-closed .c-sidebar{display:none}
        @media(max-width:520px){.content-grid{grid-template-columns:1fr !important}.c-sidebar{display:none !important}}

        .rgroup{background:var(--surf);border:1px solid var(--border);border-radius:14px;overflow:hidden;margin-bottom:12px;box-shadow:0 2px 12px rgba(0,0,0,.25)}
        .rgroup-hdr{background:var(--hover);border-bottom:1px solid var(--border);padding:11px 18px;display:flex;align-items:center;justify-content:space-between;cursor:pointer;user-select:none;transition:background .12s}
        .rgroup-hdr:hover{background:var(--border)}
        .rgroup-name{font-family:'JetBrains Mono',ui-monospace,monospace;font-size:12px;font-weight:600;color:var(--text)}
        .rgroup-sub{font-family:'JetBrains Mono',ui-monospace,monospace;font-size:11px;color:var(--muted)}
        .rg-chevron{transition:transform .2s ease;color:var(--muted);flex-shrink:0}

        .prc{padding:12px 18px;border-left:3px solid transparent;transition:background .12s}
        .prc:not(:last-child){border-bottom:1px solid var(--border)}
        .prc:hover{background:var(--hover)}
        .prc.u-ci,.prc.u-ch{border-left-color:var(--red)}
        .prc.u-cp{border-left-color:var(--amber)}
        .prc.u-ar{border-left-color:var(--indigo)}
        .prc.u-ap{border-left-color:var(--green)}
        .prc.is-draft{opacity:.6}
        .prc-row{display:flex;align-items:flex-start;justify-content:space-between;gap:12px}
        .prc-left{flex:1;min-width:0}
        .prc-trow{display:flex;align-items:baseline;gap:6px;flex-wrap:wrap;margin-bottom:5px}
        .prc-num{font-family:'JetBrains Mono',ui-monospace,monospace;font-size:11px;font-weight:600;color:var(--indigo);flex-shrink:0}
        .prc-link{font-size:13px;font-weight:500;color:var(--text);text-decoration:none;line-height:1.35}
        .prc-link:hover{color:var(--indigo)}
        .prc-meta{display:flex;align-items:center;gap:5px;flex-wrap:wrap;font-size:11px;color:var(--muted)}
        .prc-right{display:flex;flex-wrap:wrap;align-items:center;justify-content:flex-end;gap:4px;flex-shrink:0;padding-top:2px}
        .prc-expand-btn{display:flex;align-items:center;justify-content:center;width:22px;height:22px;border-radius:6px;background:none;border:none;cursor:pointer;color:var(--muted);transition:all .15s;flex-shrink:0;align-self:center}
        .prc-expand-btn:hover{color:var(--text);background:var(--hover)}
        .prc-expand-btn svg{transition:transform .2s}
        .prc-expand-btn.expanded svg{transform:rotate(90deg)}
        .prc-detail{border-top:1px solid var(--subtle);margin-top:10px;padding-top:10px;display:none}
        .prc-detail-label{font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--muted);margin-bottom:6px}
        .prc-detail-body{font-size:12px;color:var(--text);word-break:break-word;max-height:360px;overflow-y:auto;line-height:1.55}
        .prc-detail-body p{margin-bottom:6px}.prc-detail-body p:last-child{margin-bottom:0}
        .prc-detail-body h1,.prc-detail-body h2,.prc-detail-body h3{font-size:12px;font-weight:600;margin-bottom:4px;color:var(--text)}
        .prc-detail-body code{font-family:'JetBrains Mono',monospace;background:var(--subtle);padding:1px 4px;border-radius:3px;font-size:11px}
        .prc-detail-body pre{background:var(--subtle);padding:8px 10px;border-radius:6px;overflow-x:auto;font-size:11px;margin-bottom:6px;border:1px solid var(--border)}
        .prc-detail-body pre code{background:none;padding:0}
        .prc-detail-body a{color:var(--indigo);text-decoration:none}.prc-detail-body a:hover{text-decoration:underline}
        .prc-detail-body ul,.prc-detail-body ol{padding-left:16px;margin-bottom:6px}
        .prc-detail-body li{margin-bottom:2px}
        .prc-detail-body blockquote{border-left:2px solid var(--border);padding-left:8px;color:var(--muted);margin-bottom:6px}
        .prc-detail-body hr{border:none;border-top:1px solid var(--border);margin:8px 0}
        .prc-detail-body img{max-width:100%;border-radius:4px}
        .prc-cmt-entry{margin-bottom:6px;padding-bottom:6px}.prc-cmt-entry:last-child{margin-bottom:0;padding-bottom:0}
        .prc-more-btn{margin-top:6px;font-size:11px;color:var(--indigo);background:none;border:none;cursor:pointer;padding:2px 0;font-family:inherit;display:block;line-height:1.4}
        .prc-more-btn:hover{text-decoration:underline}

        .b{display:inline-flex;align-items:center;gap:3px;padding:2px 7px;border-radius:6px;font-size:11px;font-weight:500;white-space:nowrap;border:1px solid transparent}
        .br{background:var(--red-bg);color:var(--red);border-color:var(--red-bd)}
        .by{background:var(--amber-bg);color:var(--amber);border-color:var(--amber-bd)}
        .bg{background:var(--green-bg);color:var(--green);border-color:var(--green-bd)}
        .bi{background:var(--indigo-bg);color:var(--indigo);border-color:var(--indigo-bd)}
        .bv{background:var(--violet-bg);color:var(--violet);border-color:var(--violet-bd)}
        .bx{background:var(--gray-bg);color:var(--gray-text);border-color:var(--border)}
        .branch{display:inline-flex;align-items:center;gap:3px;font-family:'JetBrains Mono',ui-monospace,monospace;font-size:10px;padding:2px 6px;border-radius:5px;background:var(--subtle);color:var(--muted);border:1px solid var(--border)}

        .mcard{background:var(--surf);border:1px solid var(--border);border-radius:14px;padding:16px 18px;transition:border-color .15s,background .15s;box-shadow:0 2px 12px rgba(0,0,0,.2);position:relative;overflow:hidden}
        .mcard:hover{background:var(--hover);border-color:var(--muted)}
        .mlabel{font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.08em;color:var(--muted);margin-bottom:8px;display:flex;align-items:center;justify-content:space-between}
        .mval{font-size:26px;font-weight:700;letter-spacing:-.03em;color:var(--text);line-height:1.1}
        .msub{font-size:11px;font-family:'JetBrains Mono',ui-monospace,monospace;color:var(--muted);margin-left:6px}
        .mprog{width:100%;height:3px;border-radius:9999px;background:var(--subtle);margin-top:14px;overflow:hidden}
        .mprog-bar{height:100%;border-radius:9999px;transition:width .5s ease}

        .tab-btn{padding:6px 12px;border-radius:9px;font-size:13px;font-weight:500;color:var(--muted);background:none;border:none;cursor:pointer;transition:all .15s;white-space:nowrap;display:inline-flex;align-items:center;gap:6px;font-family:inherit}
        .tab-btn:hover{color:var(--text);background:var(--subtle)}
        .tab-btn.active{color:var(--text);background:var(--hover);box-shadow:0 0 0 1px var(--border)}
        .tab-panel{display:none}
        .tab-panel.active{display:block}
        .tcnt{font-size:10px;font-family:'JetBrains Mono',monospace;padding:1px 6px;border-radius:9999px;border:1px solid var(--border);background:var(--subtle);color:var(--muted)}
        .tcnt-amber{border-color:rgba(251,191,36,.4);background:rgba(251,191,36,.1);color:var(--amber)}
        .tcnt-indigo{border-color:var(--indigo-bd);background:var(--indigo-bg);color:var(--indigo)}

        .sc{background:var(--surf);border:1px solid var(--border);border-radius:12px;padding:12px;margin-bottom:14px;box-shadow:0 2px 12px rgba(0,0,0,.18)}
        .sc-hdr{display:flex;align-items:center;justify-content:space-between;padding-bottom:8px;border-bottom:1px solid var(--subtle);margin-bottom:10px}
        .sc-title{font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--text);display:flex;align-items:center;gap:6px}
        .srev{padding:8px 10px;border-radius:8px;border:1px solid var(--border);margin-bottom:6px;transition:background .12s}
        .srev:last-child{margin-bottom:0}
        .srev:hover{background:var(--hover)}
        .srev-repo{font-family:'JetBrains Mono',monospace;font-size:10px;color:var(--muted);margin-bottom:3px}
        .srev-link{font-size:12px;font-weight:500;color:var(--text);text-decoration:none;display:block;margin-bottom:5px;line-height:1.3}
        .srev-link:hover{color:var(--indigo)}
        .smerge-item{display:flex;align-items:flex-start;gap:8px;padding:5px 0;border-bottom:1px solid var(--subtle)}
        .smerge-item:last-child{border-bottom:none}
        .smerge-icon{width:18px;height:18px;border-radius:9999px;background:var(--green-bg);border:1px solid var(--green-bd);color:var(--green);display:flex;align-items:center;justify-content:center;flex-shrink:0;margin-top:2px}
        .smerge-link{font-size:12px;font-weight:500;color:var(--text);text-decoration:none;display:block;line-height:1.3}
        .smerge-link:hover{color:var(--indigo)}
        .smerge-sub{font-size:11px;color:var(--muted);margin-top:2px}

        .ebanner{background:var(--red-bg);border:1px solid var(--red-bd);border-radius:10px;padding:10px 16px;margin-bottom:16px;color:var(--red);display:none;font-size:13px}
        .empty{color:var(--muted);text-align:center;padding:40px;font-style:italic;font-size:13px}

        #nstack{position:fixed;top:80px;right:16px;z-index:1000;display:flex;flex-direction:column;gap:8px;max-width:320px;pointer-events:none}
        .notif{background:var(--surf);border:1px solid var(--border);border-radius:12px;padding:12px 36px 12px 14px;box-shadow:0 6px 28px rgba(0,0,0,.45);animation:slideIn .2s ease;pointer-events:all;position:relative}
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

        .stoggle{display:inline-flex;align-items:center;gap:5px;font-size:12px;font-weight:500;color:var(--muted);background:none;border:1px solid var(--border);border-radius:8px;padding:4px 10px;cursor:pointer;transition:all .15s}
        .stoggle:hover{color:var(--text);border-color:var(--muted)}
        .view-seg{display:inline-flex;border:1px solid var(--border);border-radius:8px;overflow:hidden;background:var(--subtle)}
        .view-btn{padding:4px 10px;font-size:11px;font-weight:500;border:none;background:none;color:var(--muted);cursor:pointer;transition:background .12s,color .12s;font-family:inherit}
        .view-btn.active{background:var(--hover);color:var(--text)}
        .fchip{display:inline-flex;align-items:center;gap:5px;padding:4px 10px;border-radius:8px;font-size:11px;font-weight:500;border:1px solid var(--border);background:none;color:var(--muted);cursor:pointer;transition:border-color .12s,color .12s;font-family:inherit}
        .fchip:hover{border-color:var(--muted);color:var(--text)}
        .tab-toolbar{background:var(--surf);border:1px solid var(--border);border-radius:12px;margin-bottom:16px;box-shadow:0 2px 8px rgba(0,0,0,.18);overflow:hidden;transition:border-color .15s}
        .tab-toolbar:focus-within{border-color:var(--indigo)}
        .toolbar-search{width:100%;padding:9px 12px;border:none;background:var(--subtle) !important;color:var(--text) !important;font-size:13px;font-family:inherit;outline:none;display:block;-webkit-appearance:none;appearance:none;forced-color-adjust:none;caret-color:var(--text)}
        .toolbar-search::placeholder{color:var(--muted)}
        .toolbar-search::-webkit-search-decoration,.toolbar-search::-webkit-search-cancel-button{-webkit-appearance:none}
        .toolbar-divider{height:1px;background:var(--border)}
        .toolbar-controls{display:flex;align-items:center;gap:8px;padding:8px 12px;flex-wrap:wrap}

        @media(max-width:640px){.metrics-grid{grid-template-columns:repeat(2,1fr) !important}}
        @keyframes ping{0%{transform:scale(1);opacity:.7}75%,100%{transform:scale(2);opacity:0}}
        @keyframes spin{from{transform:rotate(0deg)}to{transform:rotate(360deg)}}

        .npref-btn{display:inline-flex;align-items:center;padding:2px 9px;border-radius:9999px;font-size:11px;font-weight:500;border:1px solid var(--border);background:var(--subtle);color:var(--muted);cursor:pointer;transition:all .15s;font-family:inherit;opacity:.5}
        .npref-btn.on{border-color:var(--green-bd);background:var(--green-bg);color:var(--green);opacity:1}
        .dd-test-btn{display:inline-flex;align-items:center;gap:4px;padding:3px 10px;border-radius:9999px;font-size:11px;font-weight:500;border:1px solid var(--border);background:var(--subtle);color:var(--muted);cursor:pointer;font-family:inherit;transition:all .15s}
        .dd-test-btn:hover{border-color:var(--indigo);color:var(--indigo);background:var(--indigo-bg)}

        .sdot{display:inline-block;border-radius:9999px;flex-shrink:0}
        </style>
        </head>
        <body>
        <div id="nstack"></div>

        <!-- ── BACKFILL MODAL ─────────────────────────────────────── -->
        <div id="backfill-overlay" onclick="if(event.target===this)closeBackfillModal()" style="display:none;position:fixed;inset:0;z-index:500;background:rgba(0,0,0,.6);align-items:center;justify-content:center">
          <div style="background:var(--surf);border:1px solid var(--border);border-radius:14px;padding:24px;width:460px;max-width:calc(100vw - 32px);box-shadow:0 8px 40px rgba(0,0,0,.4)">
            <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:20px">
              <span style="font-size:15px;font-weight:600;color:var(--text)">📅 <span data-i18n="backfillTitle">Histórico do mês</span></span>
              <button onclick="closeBackfillModal()" style="background:none;border:none;color:var(--muted);font-size:20px;cursor:pointer;padding:0 4px;line-height:1;border-radius:4px" onmouseover="this.style.color='var(--text)'" onmouseout="this.style.color='var(--muted)'">×</button>
            </div>
            <div style="margin-bottom:16px">
              <label style="font-size:11px;font-weight:700;text-transform:uppercase;letter-spacing:.06em;color:var(--muted);display:block;margin-bottom:6px" data-i18n="backfillMonth">Mês</label>
              <select id="backfill-month" onchange="resetBackfillState()" style="width:100%;padding:7px 10px;border-radius:6px;border:1px solid var(--border);background:var(--subtle);color:var(--text);font-size:13px;font-family:inherit"></select>
            </div>
            <div id="backfill-progress-wrap" style="display:none;margin-bottom:14px">
              <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:5px">
                <span style="font-size:11px;color:var(--muted)" data-i18n="backfillProgress">Progresso</span>
                <span id="backfill-pct" style="font-size:11px;font-family:monospace;color:var(--muted)"></span>
              </div>
              <div style="height:6px;border-radius:9999px;background:var(--subtle);overflow:hidden">
                <div id="backfill-bar" style="height:100%;border-radius:9999px;background:var(--indigo);width:0%;transition:width .3s ease"></div>
              </div>
            </div>
            <div id="backfill-log" style="display:none;background:var(--subtle);border:1px solid var(--border);border-radius:8px;padding:10px 12px;max-height:180px;overflow-y:auto;font-size:11px;font-family:monospace;color:var(--text);margin-bottom:16px"></div>
            <div style="display:flex;gap:8px;justify-content:flex-end">
              <button id="backfill-start-btn" onclick="startBackfill()" style="padding:7px 20px;border-radius:6px;border:1px solid var(--indigo-bd);background:var(--indigo-bg);color:var(--indigo);font-size:13px;font-weight:500;cursor:pointer;font-family:inherit;transition:all .12s" data-i18n="backfillStart">Iniciar</button>
              <button onclick="closeBackfillModal()" style="padding:7px 16px;border-radius:6px;border:1px solid var(--border);background:none;color:var(--muted);font-size:13px;cursor:pointer;font-family:inherit" data-i18n="backfillClose">Fechar</button>
            </div>
          </div>
        </div>

        <!-- ── HEADER ─────────────────────────────────────────────── -->
        <header style="position:sticky;top:0;z-index:40;background:var(--header-bg);backdrop-filter:blur(18px);-webkit-backdrop-filter:blur(18px);border-bottom:1px solid var(--border)">
          <div style="max-width:1280px;margin:0 auto;height:64px;padding:0 24px;display:flex;align-items:center;justify-content:space-between;gap:12px;flex-wrap:wrap">
            <div style="display:flex;align-items:center;gap:14px">
              <!-- Logo -->
              <div style="display:flex;align-items:center;gap:8px">
                <img src="/favicon.svg" width="22" height="22" style="border-radius:5px;flex-shrink:0" alt="">
                <span style="font-size:16px;font-weight:700;letter-spacing:-.02em;color:var(--text)">PrDashboard</span>
              </div>
            </div>
            <div style="display:flex;align-items:center;gap:6px;flex-wrap:wrap">
              <!-- Sync countdown -->
              <div style="display:inline-flex;align-items:center;gap:6px;padding:4px 10px;border-radius:8px;background:var(--subtle);border:1px solid var(--border);font-size:12px;color:var(--muted)">
                <span id="rlabel" style="font-family:'JetBrains Mono',monospace;font-variant-numeric:tabular-nums;min-width:28px">—</span>
              </div>
              <!-- Refresh -->
              <button onclick="manual()" style="display:inline-flex;align-items:center;gap:6px;padding:5px 12px;border-radius:8px;font-size:12px;font-weight:500;color:var(--muted);background:var(--subtle);border:1px solid var(--border);cursor:pointer;transition:all .15s" onmouseover="this.style.borderColor='var(--indigo)';this.style.color='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)';this.style.color='var(--muted)'">
                <svg id="ricon" width="13" height="13" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24">
                  <path d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15"/>
                </svg>
                <span data-i18n="refresh">Atualizar</span>
              </button>
              <div style="width:1px;height:16px;background:var(--border)"></div>
              <!-- Language toggle -->
              <button onclick="toggleLanguage()" id="lang-btn" title="Toggle language" style="display:inline-flex;align-items:center;justify-content:center;padding:5px 10px;border-radius:8px;background:var(--subtle);border:1px solid var(--border);cursor:pointer;font-size:11px;font-weight:700;font-family:'JetBrains Mono',monospace;color:var(--muted);transition:all .15s;letter-spacing:.05em" onmouseover="this.style.borderColor='var(--indigo)';this.style.color='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)';this.style.color='var(--muted)'">
                <span id="lang-display">PT</span>
              </button>
              <!-- Theme toggle -->
              <button onclick="toggleTheme()" title="Toggle theme" style="display:inline-flex;align-items:center;justify-content:center;padding:5px;border-radius:8px;background:var(--subtle);border:1px solid var(--border);cursor:pointer;transition:all .15s" onmouseover="this.style.borderColor='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)'">
                <svg id="icon-sun" width="16" height="16" fill="none" stroke="#f59e0b" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24" style="display:none"><circle cx="12" cy="12" r="5"/><path d="M12 1v2m0 18v2M4.22 4.22l1.42 1.42m12.72 12.72l1.42 1.42M1 12h2m18 0h2M4.22 19.78l1.42-1.42M18.36 5.64l1.42-1.42"/></svg>
                <svg id="icon-moon" width="16" height="16" fill="var(--indigo)" viewBox="0 0 24 24"><path d="M12 3a9 9 0 109 9c0-.46-.04-.92-.1-1.36a5.389 5.389 0 01-4.4 2.26 5.403 5.403 0 01-5.4-5.4c0-1.81.89-3.42 2.26-4.4-.44-.06-.9-.1-1.36-.1z"/></svg>
              </button>
              <!-- User menu -->
              <div style="position:relative;display:inline-block" id="user-menu-wrap">
                <div id="upill" onclick="toggleUserMenu()" title="" style="width:32px;height:32px;border-radius:9999px;background:linear-gradient(135deg,var(--indigo),var(--violet));color:var(--bg);display:flex;align-items:center;justify-content:center;font-weight:700;font-size:11px;flex-shrink:0;cursor:pointer;box-shadow:0 0 0 2px var(--bg),0 0 12px rgba(212,187,255,.25)">—</div>
                <div id="user-dd" style="display:none;position:absolute;right:0;top:calc(100% + 8px);background:var(--surf);border:1px solid var(--border);border-radius:14px;min-width:278px;box-shadow:0 8px 40px rgba(0,0,0,.55);z-index:200;overflow:hidden">
                  <!-- Identity -->
                  <div style="padding:12px 14px;display:flex;align-items:center;gap:10px;border-bottom:1px solid var(--border)">
                    <div id="user-dd-avatar" style="width:36px;height:36px;border-radius:9999px;background:linear-gradient(135deg,var(--indigo),var(--violet));display:flex;align-items:center;justify-content:center;font-weight:700;font-size:13px;color:var(--bg);flex-shrink:0;box-shadow:0 0 10px rgba(203,166,247,.2)">—</div>
                    <div style="min-width:0">
                      <div id="user-dd-name" style="font-size:13px;font-weight:600;color:var(--text);white-space:nowrap;overflow:hidden;text-overflow:ellipsis">—</div>
                      <div style="font-size:11px;color:var(--muted);margin-top:1px">GitHub · Connected</div>
                    </div>
                  </div>
                  <!-- Notifications config -->
                  <div style="padding:8px 0;border-bottom:1px solid var(--border)">
                    <div style="padding:4px 14px 6px;font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--muted)" data-i18n="menuConfigNotifs">Notificações</div>
                    <div style="display:flex;align-items:center;gap:6px;padding:3px 14px">
                      <span style="flex:1;font-size:12px;color:var(--text)">🔔 <span data-i18n="notifComment">Novo comentário</span></span>
                      <button id="npref-comment-visual" class="npref-btn" onclick="toggleNotifPref('comment','visual')"><span data-i18n="notifVisual">Aviso</span></button>
                      <button id="npref-comment-audio" class="npref-btn" onclick="toggleNotifPref('comment','audio')"><span data-i18n="notifAudio">Som</span></button>
                    </div>
                    <div style="display:flex;align-items:center;gap:6px;padding:3px 14px">
                      <span style="flex:1;font-size:12px;color:var(--text)">⚠️ <span data-i18n="notifCi">CI com erro</span></span>
                      <button id="npref-ci-visual" class="npref-btn" onclick="toggleNotifPref('ci','visual')"><span data-i18n="notifVisual">Aviso</span></button>
                      <button id="npref-ci-audio" class="npref-btn" onclick="toggleNotifPref('ci','audio')"><span data-i18n="notifAudio">Som</span></button>
                    </div>
                    <div style="display:flex;align-items:center;gap:6px;padding:3px 14px">
                      <span style="flex:1;font-size:12px;color:var(--text)">🎉 <span data-i18n="notifMerged">PR mergeado</span></span>
                      <button id="npref-merged-visual" class="npref-btn" onclick="toggleNotifPref('merged','visual')"><span data-i18n="notifVisual">Aviso</span></button>
                      <button id="npref-merged-audio" class="npref-btn" onclick="toggleNotifPref('merged','audio')"><span data-i18n="notifAudio">Som</span></button>
                    </div>
                    <div style="display:flex;align-items:center;gap:6px;padding:3px 14px">
                      <span style="flex:1;font-size:12px;color:var(--text)">✅ <span data-i18n="notifApproved">PR aprovado</span></span>
                      <button id="npref-approved-visual" class="npref-btn" onclick="toggleNotifPref('approved','visual')"><span data-i18n="notifVisual">Aviso</span></button>
                      <button id="npref-approved-audio" class="npref-btn" onclick="toggleNotifPref('approved','audio')"><span data-i18n="notifAudio">Som</span></button>
                    </div>
                    <div style="display:flex;align-items:center;gap:6px;padding:3px 14px">
                      <span style="flex:1;font-size:12px;color:var(--text)">🔵 <span data-i18n="notifReady">Pronto p/ review</span></span>
                      <button id="npref-ready-visual" class="npref-btn" onclick="toggleNotifPref('ready','visual')"><span data-i18n="notifVisual">Aviso</span></button>
                      <button id="npref-ready-audio" class="npref-btn" onclick="toggleNotifPref('ready','audio')"><span data-i18n="notifAudio">Som</span></button>
                    </div>
                  </div>
                  <!-- Test notifications -->
                  <div style="padding:8px 0;border-bottom:1px solid var(--border)">
                    <div style="padding:4px 14px 6px;font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--muted)" data-i18n="menuTestNotifs">Testar</div>
                    <div style="padding:2px 14px 4px;display:flex;flex-wrap:wrap;gap:5px">
                      <button onclick="testNotif('comment');closeUserMenu()" class="dd-test-btn">🔔 <span data-i18n="testComment">Comentário</span></button>
                      <button onclick="testNotif('ci');closeUserMenu()" class="dd-test-btn">⚠️ <span data-i18n="testCi">CI</span></button>
                      <button onclick="testNotif('merged');closeUserMenu()" class="dd-test-btn">🎉 <span data-i18n="testMerged">Merge</span></button>
                      <button onclick="testNotif('approved');closeUserMenu()" class="dd-test-btn">✅ <span data-i18n="testApproved">Aprovado</span></button>
                      <button onclick="testNotif('ready');closeUserMenu()" class="dd-test-btn">🔵 <span data-i18n="testReady">Draft</span></button>
                    </div>
                  </div>
                  <!-- Settings: interval + backfill -->
                  <div style="padding:10px 14px 12px">
                    <div style="font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--muted);margin-bottom:8px" data-i18n="menuInterval">Intervalo de atualização</div>
                    <div style="display:flex;align-items:center;gap:8px">
                      <input id="interval-input" type="number" min="30" max="3600" value="300" style="width:64px;padding:4px 8px;border-radius:6px;border:1px solid var(--border);background:var(--bg);color:var(--text);font-size:13px;font-family:'JetBrains Mono',monospace">
                      <span style="font-size:12px;color:var(--muted)" data-i18n="menuSecs">seg</span>
                      <button onclick="saveInterval()" style="margin-left:auto;padding:4px 12px;border-radius:6px;border:1px solid var(--indigo-bd);background:var(--indigo-bg);color:var(--indigo);font-size:11px;font-weight:600;cursor:pointer;font-family:inherit;transition:all .12s" data-i18n="menuSave">Salvar</button>
                    </div>
                    <div style="height:1px;background:var(--border);margin:10px 0 8px"></div>
                    <button onclick="openBackfillModal();closeUserMenu()" style="display:flex;align-items:center;gap:7px;padding:4px 0;font-size:12px;color:var(--muted);background:none;border:none;cursor:pointer;font-family:inherit;transition:color .12s" onmouseover="this.style.color='var(--text)'" onmouseout="this.style.color='var(--muted)'">
                      <svg width="13" height="13" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><rect x="3" y="4" width="18" height="18" rx="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>
                      <span data-i18n="menuBackfill">Histórico do mês</span>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </header>

        <main style="max-width:1280px;margin:0 auto;padding:28px 24px">
          <div class="ebanner" id="err"></div>
          <div class="empty" id="loading" data-i18n="loading">Carregando...</div>

          <div id="content" style="display:none">
            <!-- ── METRICS ── -->
            <div style="display:grid;grid-template-columns:repeat(4,1fr);gap:14px;margin-bottom:28px" class="metrics-grid">

              <div class="mcard">
                <div style="position:absolute;right:-20px;bottom:-20px;width:80px;height:80px;border-radius:9999px;background:var(--green-bg);filter:blur(20px);pointer-events:none"></div>
                <div class="mlabel">
                  <span style="display:flex;align-items:center;gap:6px"><span class="sdot" style="width:7px;height:7px;background:var(--green);box-shadow:0 0 6px var(--green)"></span><span data-i18n="metricsMyPrs">Meus PRs Abertos</span></span>
                  <svg width="15" height="15" fill="none" stroke="var(--green)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/><polyline points="22 4 12 14.01 9 11.01"/></svg>
                </div>
                <div style="display:flex;align-items:baseline;gap:6px;margin-top:4px">
                  <span class="mval" id="m-open">—</span><span class="msub" id="m-open-sub"></span>
                </div>
                <div class="mprog"><div class="mprog-bar" id="m-open-bar" style="background:var(--green);width:0%"></div></div>
              </div>

              <div class="mcard">
                <div style="position:absolute;right:-20px;bottom:-20px;width:80px;height:80px;border-radius:9999px;background:var(--red-bg);filter:blur(20px);pointer-events:none"></div>
                <div class="mlabel">
                  <span style="display:flex;align-items:center;gap:6px"><span class="sdot" style="width:7px;height:7px;background:var(--red);box-shadow:0 0 6px var(--red)"></span><span data-i18n="metricsCi">CI com Falha</span></span>
                  <svg width="15" height="15" fill="none" stroke="var(--red)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><circle cx="12" cy="12" r="10"/><line x1="15" y1="9" x2="9" y2="15"/><line x1="9" y1="9" x2="15" y2="15"/></svg>
                </div>
                <div style="display:flex;align-items:baseline;gap:6px;margin-top:4px">
                  <span class="mval" id="m-ci" style="color:var(--red)">—</span><span class="msub" id="m-ci-sub" style="color:var(--red)"></span>
                </div>
                <div class="mprog"><div class="mprog-bar" id="m-ci-bar" style="background:var(--red);width:0%"></div></div>
              </div>

              <div class="mcard">
                <div style="position:absolute;right:-20px;bottom:-20px;width:80px;height:80px;border-radius:9999px;background:var(--amber-bg);filter:blur(20px);pointer-events:none"></div>
                <div class="mlabel">
                  <span style="display:flex;align-items:center;gap:6px"><span class="sdot" style="width:7px;height:7px;background:var(--amber);box-shadow:0 0 6px var(--amber)"></span><span data-i18n="metricsReviews">Reviews a Mim</span></span>
                  <svg width="15" height="15" fill="none" stroke="var(--amber)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"/></svg>
                </div>
                <div style="display:flex;align-items:baseline;gap:6px;margin-top:4px">
                  <span class="mval" id="m-rev" style="color:var(--amber)">—</span><span class="msub" id="m-rev-sub" style="color:var(--amber)"></span>
                </div>
                <div class="mprog"><div class="mprog-bar" id="m-rev-bar" style="background:var(--amber);width:0%"></div></div>
              </div>

              <div class="mcard">
                <div style="position:absolute;right:-20px;bottom:-20px;width:80px;height:80px;border-radius:9999px;background:var(--indigo-bg);filter:blur(20px);pointer-events:none"></div>
                <div class="mlabel">
                  <span style="display:flex;align-items:center;gap:6px"><span class="sdot" style="width:7px;height:7px;background:var(--indigo);box-shadow:0 0 6px var(--indigo)"></span><span data-i18n="metricsMerged">Mergeados Recentes</span></span>
                  <svg width="15" height="15" fill="none" stroke="var(--indigo)" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M8 5v8a2 2 0 002 2h8m-2 2l2-2-2-2"/><circle cx="6" cy="18" r="2"/><circle cx="6" cy="6" r="2"/></svg>
                </div>
                <div style="display:flex;align-items:baseline;gap:6px;margin-top:4px">
                  <span class="mval" id="m-mrg" style="color:var(--indigo)">—</span><span class="msub" id="m-mrg-sub"></span>
                </div>
                <div class="mprog"><div class="mprog-bar" id="m-mrg-bar" style="background:var(--indigo);width:0%"></div></div>
              </div>

            </div>

            <!-- ── TABS ── -->
            <div style="background:var(--surf);border:1px solid var(--border);border-radius:14px;padding:14px 18px;margin-bottom:24px;box-shadow:0 2px 12px rgba(0,0,0,.2)">
              <div style="display:flex;align-items:center;justify-content:space-between;gap:8px;flex-wrap:wrap">
                <nav style="display:flex;gap:2px">
                  <button class="tab-btn" data-tab="my" onclick="setTab('my')">
                    <span data-i18n="tabMyPrs">Meus PRs</span> <span id="tc-my" class="tcnt tcnt-indigo">0</span>
                  </button>
                  <button class="tab-btn" data-tab="rev" onclick="setTab('rev')">
                    <span data-i18n="tabReviews">Reviews Solicitados</span> <span id="tc-rev" class="tcnt tcnt-amber">0</span>
                  </button>
                  <button class="tab-btn" data-tab="mrg" onclick="setTab('mrg')">
                    <span data-i18n="tabMerged">Mergeados</span> <span id="tc-mrg" class="tcnt">0</span>
                  </button>
                  <button class="tab-btn" data-tab="notif" onclick="setTab('notif')">
                    <span data-i18n="tabNotif">Notificações</span>
                    <span id="tc-notif" class="tcnt">0</span>
                  </button>
                </nav>
                <div>
                  <button class="stoggle" id="stoggle-btn" onclick="toggleSidebar()">
                    <svg width="12" height="12" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M4 6h16M4 12h10M4 18h7"/></svg>
                    <span id="stoggle-label" data-i18n="closeSidebar">Fechar painel</span>
                  </button>
                </div>
              </div>
            </div>

            <!-- ── TAB: MEUS PRs ── -->
            <div id="tab-my" class="tab-panel">
              <div class="tab-toolbar">
                <input id="my-search" type="search" class="toolbar-search" placeholder="" data-i18n-ph="searchPlaceholder" oninput="setMySearch(this.value)">
                <div class="toolbar-divider"></div>
                <div class="toolbar-controls">
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
              <div class="tab-toolbar">
                <input id="rev-search" type="search" class="toolbar-search" placeholder="" data-i18n-ph="searchPlaceholder" oninput="setRevSearch(this.value)">
                <div class="toolbar-divider"></div>
                <div class="toolbar-controls">
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
              </div>
              <div id="s-rev-filters" style="margin-bottom:10px"></div>
              <div id="s-rev-full"></div>
            </div>
            <div id="tab-mrg" class="tab-panel">
              <div style="margin-bottom:12px">
                <input id="mrg-search" type="search" placeholder="" data-i18n-ph="searchPlaceholder"
                  oninput="setMrgSearch(this.value)"
                  style="width:100%;padding:8px 12px;border-radius:10px;border:1px solid var(--border);background:var(--subtle);color:var(--text);font-size:13px;font-family:inherit;outline:none;transition:border-color .15s" onfocus="this.style.borderColor='var(--indigo)'" onblur="this.style.borderColor='var(--border)'">
              </div>
              <div id="s-mrg-full"></div>
            </div>
            <div id="tab-notif" class="tab-panel">
              <div style="display:flex;align-items:center;justify-content:space-between;margin-bottom:16px">
                <span style="font-size:11px;color:var(--muted)" data-i18n="notifHistoryTitle">Eventos recentes nesta sessão</span>
                <button onclick="clearNotifHistory()" style="font-size:11px;padding:3px 10px;border-radius:6px;border:1px solid var(--border);background:none;color:var(--muted);cursor:pointer;font-family:inherit" data-i18n="clearHistory">Limpar</button>
              </div>
              <div id="s-notif-list"></div>
            </div>
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
            testComment:'Comentário', testCi:'CI', testMerged:'Merge', testApproved:'Aprovado', testReady:'Draft',
            menuTestNotifs:'Testar', menuConfigNotifs:'Notificações',
            menuInterval:'Intervalo de atualização', menuSecs:'seg', menuSave:'Salvar',
            notifComment:'Novo comentário', notifCi:'CI com erro', notifMerged:'PR mergeado', notifApproved:'PR aprovado', notifReady:'PR pronto p/ review',
            notifVisual:'Aviso', notifAudio:'Som',
            hideBots:'Ocultar bots',
            menuBackfill:'Histórico do mês',
            backfillTitle:'Histórico do mês',backfillMonth:'Mês',backfillProgress:'Progresso',backfillStart:'Iniciar',backfillClose:'Fechar',backfillRunning:'Executando...',backfillDone:'Concluído ✅',backfillRetry:'Tentar novamente',
            searchPlaceholder:'Filtrar por título ou repo...',
            tabNotif:'Notificações',
            notifHistoryTitle:'Eventos recentes nesta sessão',
            notifHistoryEmpty:'Nenhuma notificação ainda.',
            clearHistory:'Limpar',
            prDesc:'Descrição', lastCmt:'Comentários', noDesc:'Sem descrição', noCmt:'Sem comentários',
            moreCmts:'mais', hideOlder:'Recolher',
            filterByStatus:'Urgência', filterByAuthor:'Por Autor',
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
            testComment:'Comment', testCi:'CI', testMerged:'Merge', testApproved:'Approved', testReady:'Draft',
            menuTestNotifs:'Test', menuConfigNotifs:'Notifications',
            menuInterval:'Update interval', menuSecs:'sec', menuSave:'Save',
            notifComment:'New comment', notifCi:'CI error', notifMerged:'PR merged', notifApproved:'PR approved', notifReady:'PR ready for review',
            notifVisual:'Alert', notifAudio:'Sound',
            hideBots:'Hide bots',
            menuBackfill:'Month history',
            backfillTitle:'Month history',backfillMonth:'Month',backfillProgress:'Progress',backfillStart:'Start',backfillClose:'Close',backfillRunning:'Running...',backfillDone:'Done ✅',backfillRetry:'Retry',
            searchPlaceholder:'Filter by title or repo...',
            tabNotif:'Notifications',
            notifHistoryTitle:'Recent events this session',
            notifHistoryEmpty:'No notifications yet.',
            clearHistory:'Clear',
            prDesc:'Description', lastCmt:'Comments', noDesc:'No description', noCmt:'No comments',
            moreCmts:'more', hideOlder:'Collapse',
            filterByStatus:'Urgency', filterByAuthor:'By Author',
          }
        };
        let currentLang=localStorage.getItem('prd-lang')||'pt-BR';
        function t(key,...args){const v=I18N[currentLang]?.[key]??I18N['pt-BR'][key]??key;return typeof v==='function'?v(...args):v}
        function applyI18n(){
          document.querySelectorAll('[data-i18n]').forEach(el=>{el.textContent=t(el.dataset.i18n)});
          document.querySelectorAll('[data-i18n-ph]').forEach(el=>{el.placeholder=t(el.dataset.i18nPh)});
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
          if(tab==='notif')renderNotifHistory();
        }
        setTab('my');

        // ── Notification + audio ───────────────────────────────────────
        let ivSecs=300,cd=0,tmr=null,prevState={},notifInited=false,settingsInitialized=false;
        let notifPrefs={commentVisual:true,commentAudio:true,ciVisual:true,ciAudio:true,mergedVisual:true,mergedAudio:true,approvedVisual:true,approvedAudio:true,readyVisual:true,readyAudio:true};
        let hideBotReviews=false;
        let prevMyNums=new Set(),prevMergedNums=new Set();
        function ck(p){return p.commentState+'|'+p.lastCommentBy+'|'+(p.lastCommentAt||p.lastCommentAgo||'')}
        function logNotifToServer(type, pr) {
          fetch('/api/notify-log', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({type, repo: pr.repo, prNumber: pr.number, prTitle: pr.title})
          }).catch(() => {});
        }
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
              if(merged){if(notifPrefs.mergedVisual)showMergedNotif(merged);if(notifPrefs.mergedAudio)playAudio('/audio/done.mp3',1);logNotifToServer('merged',merged)}
            }
          }
          for(const p of prs){
            const prev=prevState[p.number]||{};const k=ck(p);
            if(p.commentState==='New'&&k!==prev.ck){if(notifPrefs.commentVisual)showNotif('comment',p);if(notifPrefs.commentAudio)playAudio('/audio/message.mp3',1);logNotifToServer('comment',p)}
            if(p.ciState==='Failing'&&!prev.cif){if(notifPrefs.ciVisual)showNotif('ci',p);if(notifPrefs.ciAudio)playAudio('/audio/error.mp3',3);logNotifToServer('ci',p)}
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
          pushHistory(type,pr);
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
          pushHistory('merged',pr);
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
        function pushHistory(type,pr){
          const icons={comment:'💬',ci:'🔴',merged:'🎉',approved:'✅',ready:'🔵'};
          notifHistory.unshift({type,icon:icons[type]||'•',ts:new Date().toLocaleTimeString(),repo:pr.repo,number:pr.number,title:pr.title,url:pr.url});
          if(notifHistory.length>200)notifHistory.length=200;
          document.getElementById('tc-notif').textContent=notifHistory.length;
          if(currentTab==='notif')renderNotifHistory();
        }
        function renderNotifHistory(){
          const el=document.getElementById('s-notif-list');
          if(!el)return;
          if(!notifHistory.length){el.innerHTML=`<div class="empty" data-i18n="notifHistoryEmpty">${t('notifHistoryEmpty')}</div>`;return;}
          el.innerHTML=notifHistory.map(h=>`
            <div class="mcard" style="margin-bottom:8px;display:flex;align-items:flex-start;gap:10px;padding:10px 14px">
              <span style="font-size:16px;flex-shrink:0">${h.icon}</span>
              <div style="flex:1;min-width:0">
                <div style="font-size:11px;color:var(--muted);margin-bottom:2px">${e(h.ts)} · ${e(h.repo)} · #${h.number}</div>
                <a href="${e(h.url)}" target="_blank" rel="noopener" style="font-size:13px;font-weight:500;color:var(--text);text-decoration:none">${e(h.title)}</a>
              </div>
            </div>`).join('');
        }
        function clearNotifHistory(){notifHistory.length=0;document.getElementById('tc-notif').textContent='0';renderNotifHistory();}
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
        function bCI(state,fail,failingChecks){
          if(state==='Passing')return b('bg',t('ciPassing'));
          if(state==='Failing'){const label='✗ '+(fail||'CI');return failingChecks&&failingChecks.length>1?`<span class="b br" title="${failingChecks.join(', ')}">${e(label)}</span>`:b('br',label)}
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
        let _lastMrgPrs=[];
        const notifHistory=[];
        let myPrsFilter=null;
        let mySearchText='', revSearchText='', mrgSearchText='';
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
        function setMySearch(v){mySearchText=v.toLowerCase();renderMyPrs(_lastMyPrs);}
        function setRevSearch(v){revSearchText=v.toLowerCase();renderRevFull(_lastRevPrs);}
        function setMrgSearch(v){mrgSearchText=v.toLowerCase();renderMrgFull(_lastMrgPrs);}
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
        function ageBadgeCls(iso){
          if(!iso)return 'bx';
          const days=(Date.now()-new Date(iso).getTime())/86400000;
          if(days<1)return 'bg';
          if(days<=3)return 'by';
          return 'br';
        }

        // ── PR card renderers ──────────────────────────────────────────
        function renderMd(text){
          if(!text)return '';
          try{return typeof marked!=='undefined'?marked.parse(text):e(text).replace(/\n/g,'<br>')}
          catch(_){return e(text).replace(/\n/g,'<br>')}
        }
        function togglePrcDetail(btn){
          const prc=btn.closest('.prc');
          const detail=prc.querySelector('.prc-detail');
          const expanded=btn.classList.contains('expanded');
          if(expanded){detail.style.display='none';btn.classList.remove('expanded');btn.innerHTML=EXPAND_SVG;return;}
          btn.innerHTML=SPINNER_SVG;btn.disabled=true;
          setTimeout(()=>{
            if(!detail.dataset.rendered){
              const data=_prcData[prc.dataset.pk]||{};
              const noDesc=`<em style="color:var(--muted);font-size:12px">${t('noDesc')}</em>`;
              const bodyEl=detail.querySelector('.prc-md-desc');
              const cmtEl=detail.querySelector('.prc-md-cmt');
              if(bodyEl)bodyEl.innerHTML=data.body?renderMd(data.body):noDesc;
              if(cmtEl)cmtEl.innerHTML=renderComments(data.comments);
              detail.dataset.rendered='1';
            }
            detail.style.display='block';btn.innerHTML=EXPAND_SVG;btn.disabled=false;btn.classList.add('expanded');
          },30);
        }
        const EXPAND_SVG=`<svg width="14" height="14" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M9 6l6 6-6 6"/></svg>`;
        const SPINNER_SVG=`<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" style="animation:spin .7s linear infinite"><path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4M4.93 19.07l2.83-2.83M16.24 7.76l2.83-2.83"/></svg>`;
        const _prcData={};

        function renderComments(cmts){
          if(!cmts||!cmts.length)return`<em style="color:var(--muted);font-size:12px">${t('noCmt')}</em>`;
          const item=c=>`<div class="prc-cmt-entry"><div style="display:flex;align-items:center;gap:5px;margin-bottom:3px"><span class="b bx" style="font-size:10px;padding:1px 5px">${e(c.author)}</span><span style="font-size:10px;color:var(--muted)">${e(c.ago)}</span></div><div>${renderMd(c.body)}</div></div>`;
          if(cmts.length===1)return item(cmts[0]);
          const n=cmts.length-1;
          const rest=cmts.slice(1).map(item).join('<div style="height:1px;background:var(--subtle);margin:4px 0"></div>');
          return`${item(cmts[0])}<div class="prc-older-cmts" style="display:none">${rest}</div><button class="prc-more-btn" onclick="toggleMoreCmts(this)">+ ${n} ${t('moreCmts')}</button>`;
        }
        function toggleMoreCmts(btn){
          const wrap=btn.previousElementSibling;
          const hidden=wrap.style.display==='none';
          wrap.style.display=hidden?'block':'none';
          const n=wrap.querySelectorAll('.prc-cmt-entry').length;
          btn.textContent=hidden?t('hideOlder'):`+ ${n} ${t('moreCmts')}`;
        }

        function renderMyCard(p){
          const cls=(UCLS[p.urgency]||'')+(p.isDraft?' is-draft':'');
          const _k=`${p.repo}/${p.number}`;_prcData[_k]={body:p.body||'',comments:p.comments||[]};
          return `<div class="prc ${cls}" data-pk="${_k}">
            <div style="display:flex;align-items:flex-start;gap:8px">
              <button class="prc-expand-btn" onclick="togglePrcDetail(this)">${EXPAND_SVG}</button>
              <div class="prc-row" style="flex:1;min-width:0">
                <div class="prc-left">
                  <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
                  <div class="prc-meta">${p.isDraft?b('bx',t('draft')):''}${bBranch(p.headRef)}${(p.assignees||[]).map(a=>`<span class="b bx">→ ${e(a)}</span>`).join('')}<span class="b ${ageBadgeCls(p.createdAtIso)}">${e(p.ageAgo)}</span></div>
                  ${bRevMeta(p)}
                </div>
                <div class="prc-right">${bRev(p)}${bCI(p.ciState,p.failingCheck,p.failingChecks)}${bCmt(p)}${bMrg(p.mergeState)}</div>
              </div>
            </div>
            <div class="prc-detail">
              <div style="display:grid;grid-template-columns:minmax(0,1fr) minmax(0,1fr);gap:0">
                <div style="padding-right:14px;border-right:1px solid var(--border)"><div class="prc-detail-label">${t('prDesc')}</div><div class="prc-detail-body prc-md-desc"></div></div>
                <div style="padding-left:14px"><div class="prc-detail-label">${t('lastCmt')}</div><div class="prc-detail-body prc-md-cmt"></div></div>
              </div>
            </div>
          </div>`;
        }

        function renderRevCard(p){
          const cls=(UCLS[p.urgency]||'')+(p.isDraft?' is-draft':'');
          const _k=`${p.repo}/${p.number}`;_prcData[_k]={body:p.body||'',comments:p.comments||[]};
          return `<div class="prc ${cls}" data-pk="${_k}">
            <div style="display:flex;align-items:flex-start;gap:8px">
              <button class="prc-expand-btn" onclick="togglePrcDetail(this)">${EXPAND_SVG}</button>
              <div class="prc-row" style="flex:1;min-width:0">
                <div class="prc-left">
                  <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
                  <div class="prc-meta">${b('bx',p.author)}${p.isDraft?b('bx',t('draft')):''}${bBranch(p.headRef)}<span class="b ${ageBadgeCls(p.createdAtIso)}">${e(p.ageAgo)}</span></div>
                </div>
                <div class="prc-right">${bCI(p.ciState,p.failingCheck,p.failingChecks)}${bCmt(p)}${bMrg(p.mergeState)}</div>
              </div>
            </div>
            <div class="prc-detail">
              <div style="display:grid;grid-template-columns:minmax(0,1fr) minmax(0,1fr);gap:0">
                <div style="padding-right:14px;border-right:1px solid var(--border)"><div class="prc-detail-label">${t('prDesc')}</div><div class="prc-detail-body prc-md-desc"></div></div>
                <div style="padding-left:14px"><div class="prc-detail-label">${t('lastCmt')}</div><div class="prc-detail-body prc-md-cmt"></div></div>
              </div>
            </div>
          </div>`;
        }

        function renderMyCardWithRepo(p){
          const cls=(UCLS[p.urgency]||'')+(p.isDraft?' is-draft':'');
          const _k=`${p.repo}/${p.number}`;_prcData[_k]={body:p.body||'',comments:p.comments||[]};
          return `<div class="prc ${cls}" data-pk="${_k}">
            <div style="display:flex;align-items:flex-start;gap:8px">
              <button class="prc-expand-btn" onclick="togglePrcDetail(this)">${EXPAND_SVG}</button>
              <div class="prc-row" style="flex:1;min-width:0">
                <div class="prc-left">
                  <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${e(p.url)}" target="_blank" rel="noopener">${e(p.title)}</a></div>
                  <div class="prc-meta"><span class="branch">${e(p.repo)}</span>${p.isDraft?b('bx',t('draft')):''}${bBranch(p.headRef)}<span>${e(p.ageAgo)}</span></div>
                  ${bRevMeta(p)}
                </div>
                <div class="prc-right">${bRev(p)}${bCI(p.ciState,p.failingCheck,p.failingChecks)}${bCmt(p)}${bMrg(p.mergeState)}</div>
              </div>
            </div>
            <div class="prc-detail">
              <div style="display:grid;grid-template-columns:minmax(0,1fr) minmax(0,1fr);gap:0">
                <div style="padding-right:14px;border-right:1px solid var(--border)"><div class="prc-detail-label">${t('prDesc')}</div><div class="prc-detail-body prc-md-desc"></div></div>
                <div style="padding-left:14px"><div class="prc-detail-label">${t('lastCmt')}</div><div class="prc-detail-body prc-md-cmt"></div></div>
              </div>
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
          const filtered2=mySearchText?filtered.filter(p=>p.title.toLowerCase().includes(mySearchText)||p.repo.toLowerCase().includes(mySearchText)):filtered;
          if(myPrsView==='status'){renderMyPrsByStatus(filtered2);updateCollapseBtn();return}
          const s=document.getElementById('s-my');
          if(!filtered2.length){s.innerHTML=`<div class="empty">${t('emptyMyPrs')}</div>`;updateCollapseBtn();return}
          const groups={};for(const p of filtered2)(groups[p.repo]=groups[p.repo]||[]).push(p);
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
            <div class="prc-meta">${bCI(p.ciState,p.failingCheck,p.failingChecks)}${bMrg(p.mergeState)}<span>${e(p.ageAgo)}</span></div>
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
          const FLABEL=`font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--muted);margin-bottom:5px`;
          if(existing.length){
            const all=revFilter===null;
            const chips=[
              `<button class="fchip" style="${all?'border-color:var(--muted);color:var(--text)':''}" onclick="setRevFilter(null)">${t('filterAll')} <span class="tcnt">${visible.length}</span></button>`,
              ...existing.map(u=>{
                const active=revFilter===u;const col=urgencyColor(u);
                return `<button class="fchip" style="${active?`border-color:${col};color:${col}`:''}" onclick="setRevFilter('${u}')">${t('st'+u)} <span class="tcnt">${cnt[u]}</span></button>`;
              })
            ];
            html+=`<div><div style="${FLABEL}">${t('filterByStatus')}</div><div style="display:flex;gap:6px;flex-wrap:wrap">${chips.join('')}</div></div>`;
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
            html+=(html?'<div style="height:10px"></div>':'')+`<div><div style="${FLABEL}">${t('filterByAuthor')}</div><div style="display:flex;gap:6px;flex-wrap:wrap">${achips.join('')}</div></div>`;
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
          const filtered2=revSearchText?filtered.filter(p=>p.title.toLowerCase().includes(revSearchText)||p.repo.toLowerCase().includes(revSearchText)):filtered;
          const s=document.getElementById('s-rev-full');
          if(!filtered2.length){s.innerHTML=`<div class="empty">${t('emptyReviews')}</div>`;updateRevCollapseBtn();return}
          const groups={};for(const p of filtered2)(groups[p.repo]=groups[p.repo]||[]).push(p);
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
          _lastMrgPrs=prs;
          const filtered=mrgSearchText?prs.filter(p=>p.title.toLowerCase().includes(mrgSearchText)||p.repo.toLowerCase().includes(mrgSearchText)):prs;
          const s=document.getElementById('s-mrg-full');
          if(!filtered.length){s.innerHTML=`<div class="empty">${t('emptyMerged')}</div>`;return}
          const rows=filtered.map(p=>`<div class="prc">
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
        function setBar(id,pct){const el=document.getElementById(id);if(el)el.style.width=Math.min(100,Math.max(0,pct))+'%'}
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
          // progress bars
          const tot=Math.max(my.length,1);
          setBar('m-open-bar',my.length>0?Math.max(10,Math.round((my.length-attn)/tot*100)):0);
          setBar('m-ci-bar',Math.round(ci/tot*100));
          setBar('m-rev-bar',Math.min(100,rev.length*5));
          setBar('m-mrg-bar',Math.min(100,mrg.length*10));
        }

        // ── Main render ────────────────────────────────────────────────
        function render(data){
          if(data.isLoading)return;

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
            const ddav=document.getElementById('user-dd-avatar');if(ddav)ddav.textContent=initials;
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
          try{
            const r=await fetch('/api/prs');if(!r.ok)throw new Error('HTTP '+r.status);
            render(await r.json());
          }catch(ex){
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

        // ── Backfill modal ─────────────────────────────────────────────
        let _bfJobId=null,_bfPollTmr=null;
        function buildMonthOptions(){
          const sel=document.getElementById('backfill-month');
          if(!sel||sel.options.length>0)return;
          const now=new Date();
          for(let i=0;i<=13;i++){
            const d=new Date(now.getFullYear(),now.getMonth()-i,1);
            const val=`${d.getFullYear()}-${String(d.getMonth()+1).padStart(2,'0')}`;
            const lbl=d.toLocaleDateString(currentLang,{month:'long',year:'numeric'});
            const opt=document.createElement('option');
            opt.value=val;opt.textContent=lbl.charAt(0).toUpperCase()+lbl.slice(1);
            sel.appendChild(opt);
          }
        }
        function resetBackfillState(){
          clearTimeout(_bfPollTmr);_bfJobId=null;
          document.getElementById('backfill-progress-wrap').style.display='none';
          document.getElementById('backfill-log').style.display='none';
          document.getElementById('backfill-log').innerHTML='';
          const bar=document.getElementById('backfill-bar');if(bar)bar.style.width='0%';
          const pct=document.getElementById('backfill-pct');if(pct)pct.textContent='';
          const btn=document.getElementById('backfill-start-btn');if(btn){btn.disabled=false;btn.textContent=t('backfillStart');}
        }
        function openBackfillModal(){
          buildMonthOptions();
          const ov=document.getElementById('backfill-overlay');if(!ov)return;
          _bfJobId=null;clearTimeout(_bfPollTmr);
          document.getElementById('backfill-progress-wrap').style.display='none';
          document.getElementById('backfill-log').style.display='none';
          document.getElementById('backfill-log').innerHTML='';
          const bar=document.getElementById('backfill-bar');if(bar)bar.style.width='0%';
          const pct=document.getElementById('backfill-pct');if(pct)pct.textContent='';
          const btn=document.getElementById('backfill-start-btn');if(btn){btn.disabled=false;btn.textContent=t('backfillStart');}
          ov.style.display='flex';
        }
        function closeBackfillModal(){
          clearTimeout(_bfPollTmr);_bfJobId=null;
          const ov=document.getElementById('backfill-overlay');if(ov)ov.style.display='none';
        }
        async function startBackfill(){
          const month=document.getElementById('backfill-month')?.value;if(!month)return;
          const btn=document.getElementById('backfill-start-btn');
          btn.disabled=true;btn.textContent=t('backfillRunning');
          document.getElementById('backfill-progress-wrap').style.display='';
          document.getElementById('backfill-log').style.display='';
          document.getElementById('backfill-log').innerHTML='';
          try{
            const r=await fetch('/api/backfill/start',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({month})});
            if(!r.ok){const txt=await r.text();bfLog('❌ '+txt);btn.disabled=false;btn.textContent=t('backfillRetry');return;}
            const {jobId}=await r.json();_bfJobId=jobId;
            pollBackfill(jobId);
          }catch(ex){bfLog('❌ '+ex.message);btn.disabled=false;btn.textContent=t('backfillRetry');}
        }
        function bfLog(text){
          const log=document.getElementById('backfill-log');if(!log)return;
          const div=document.createElement('div');
          div.style.cssText='padding:2px 0;border-bottom:1px solid rgba(128,128,128,.15)';
          div.textContent=text;log.appendChild(div);log.scrollTop=log.scrollHeight;
        }
        function pollBackfill(jobId){
          if(_bfJobId!==jobId)return;
          fetch('/api/backfill/status/'+jobId).then(r=>r.json()).then(data=>{
            const log=document.getElementById('backfill-log');
            if(log){const cur=log.children.length;for(let i=cur;i<data.lines.length;i++)bfLog(data.lines[i]);}
            const bar=document.getElementById('backfill-bar');
            const pct=document.getElementById('backfill-pct');
            if(data.total>0){
              const p=Math.round(data.current/data.total*100);
              if(bar)bar.style.width=p+'%';
              if(pct)pct.textContent=data.current+' / '+data.total;
            }else if(data.done){if(bar)bar.style.width='100%';if(pct)pct.textContent='—';}
            if(data.done){
              const btn=document.getElementById('backfill-start-btn');
              if(btn)btn.textContent=t('backfillDone');
            }else if(_bfJobId===jobId){_bfPollTmr=setTimeout(()=>pollBackfill(jobId),600);}
          }).catch(()=>{if(_bfJobId===jobId)_bfPollTmr=setTimeout(()=>pollBackfill(jobId),1000);});
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
