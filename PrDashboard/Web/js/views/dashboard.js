// ═══════════════════════════════════════════════════════════════
// Dashboard View Module
// Handles rendering and logic for My PRs, Reviews, and Merged tabs
// ═══════════════════════════════════════════════════════════════

// ── State ──────────────────────────────────────────────────────
export let myPrsView = 'repo';
export let _lastMyPrs = [];
export let _lastMrgPrs = [];
export let _lastRevPrs = [];
export let myPrsFilter = null;
export let mySearchText = '', revSearchText = '', mrgSearchText = '';
export let revFilter = null;
export let revAuthorFilter = null;
export let revSidebarPage = 0;
export let mrgSidebarPage = 0;
const SIDEBAR_PAGE_SIZE = 5;

// ── Constants ──────────────────────────────────────────────────
const UCLS = {CiFailing:'u-ci',ChangesRequested:'u-ch',CommentsPending:'u-cp',AwaitingReview:'u-ar',Approved:'u-ap',Normal:''};
const EXPAND_SVG = `<svg class="ti" width="14" height="14"><use href="/icons.svg#ti-chevron-right"/></svg>`;
const SPINNER_SVG = `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" style="animation:spin .7s linear infinite"><path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4M4.93 19.07l2.83-2.83M16.24 7.76l2.83-2.83"/></svg>`;
const MERGE_SVG = `<svg class="ti" width="10" height="10"><use href="/icons.svg#ti-check"/></svg>`;
const _prcData = {};

// ── Imports (from global scope - will be available) ────────────
// t(), e(), b(), bRev(), bRevMeta(), bCI(), bCmt(), bMrg(), bBranch()
// collapsedRepos, repoOrder, sortRepos(), statusChips(), toggleRepo()
// persistUiState(), updateCollapseBtn(), updateRevCollapseBtn()
// hideBotReviews, isBotAuthor(), updateRevHideBotsBtn()

// ── View Control ───────────────────────────────────────────────
export function setMyView(v) {
  myPrsView = v;
  document.getElementById('vbtn-repo')?.classList.toggle('active', v === 'repo');
  document.getElementById('vbtn-status')?.classList.toggle('active', v === 'status');
  renderMyPrs(_lastMyPrs);
  window.persistUiState();
}

export function setMyFilter(urgency) {
  myPrsFilter = urgency;
  renderMyPrs(_lastMyPrs);
  window.persistUiState();
}

export function setMySearch(v) {
  mySearchText = v.toLowerCase();
  renderMyPrs(_lastMyPrs);
}

export function setRevSearch(v) {
  revSearchText = v.toLowerCase();
  renderRevFull(_lastRevPrs);
}

export function setMrgSearch(v) {
  mrgSearchText = v.toLowerCase();
  renderMrgFull(_lastMrgPrs);
}

export function setRevFilter(urgency) {
  revFilter = urgency;
  renderRevFull(_lastRevPrs);
}

export function setRevAuthorFilter(author) {
  revAuthorFilter = author;
  renderRevFull(_lastRevPrs);
}

// ── Helpers ────────────────────────────────────────────────────
function groupDot(list) {
  const u = list.map(p => p.urgency);
  if (u.some(x => x === 'CiFailing' || x === 'ChangesRequested')) return 'var(--red)';
  if (u.some(x => x === 'CommentsPending')) return 'var(--amber)';
  if (u.some(x => x === 'AwaitingReview')) return 'var(--indigo)';
  if (u.some(x => x === 'Approved')) return 'var(--green)';
  return 'var(--muted)';
}

function urgencyColor(u) {
  if (u === 'CiFailing' || u === 'ChangesRequested') return 'var(--red)';
  if (u === 'CommentsPending') return 'var(--amber)';
  if (u === 'AwaitingReview') return 'var(--indigo)';
  if (u === 'Approved') return 'var(--green)';
  return 'var(--muted)';
}

function ageBadgeCls(iso) {
  if (!iso) return 'bx';
  const days = (Date.now() - new Date(iso).getTime()) / 86400000;
  if (days < 1) return 'bg';
  if (days <= 3) return 'by';
  return 'br';
}

function renderMd(text) {
  if (!text) return '';
  try {
    return typeof marked !== 'undefined' ? marked.parse(text) : window.e(text).replace(/\n/g, '<br>');
  } catch (_) {
    return window.e(text).replace(/\n/g, '<br>');
  }
}

// ── PR Card Detail Toggle ──────────────────────────────────────
export function togglePrcDetail(btn) {
  const prc = btn.closest('.prc');
  const detail = prc.querySelector('.prc-detail');
  const expanded = btn.classList.contains('expanded');
  if (expanded) {
    detail.style.display = 'none';
    btn.classList.remove('expanded');
    btn.innerHTML = EXPAND_SVG;
    return;
  }
  btn.innerHTML = SPINNER_SVG;
  btn.disabled = true;
  setTimeout(() => {
    if (!detail.dataset.rendered) {
      const data = _prcData[prc.dataset.pk] || {};
      const noDesc = `<em style="color:var(--muted);font-size:12px">${window.t('noDesc')}</em>`;
      const bodyEl = detail.querySelector('.prc-md-desc');
      const cmtEl = detail.querySelector('.prc-md-cmt');
      if (bodyEl) bodyEl.innerHTML = data.body ? renderMd(data.body) : noDesc;
      if (cmtEl) cmtEl.innerHTML = renderComments(data.comments);
      detail.dataset.rendered = '1';
    }
    detail.style.display = 'block';
    btn.innerHTML = EXPAND_SVG;
    btn.disabled = false;
    btn.classList.add('expanded');
  }, 30);
}

function renderComments(cmts) {
  if (!cmts || !cmts.length) return `<em style="color:var(--muted);font-size:12px">${window.t('noCmt')}</em>`;
  const item = c => `<div class="prc-cmt-entry"><div style="display:flex;align-items:center;gap:5px;margin-bottom:3px"><span class="b bx" style="font-size:10px;padding:1px 5px">${window.e(c.author)}</span><span style="font-size:10px;color:var(--muted)">${window.e(c.ago)}</span></div><div>${renderMd(c.body)}</div></div>`;
  if (cmts.length === 1) return item(cmts[0]);
  const n = cmts.length - 1;
  const rest = cmts.slice(1).map(item).join('<div style="height:1px;background:var(--subtle);margin:4px 0"></div>');
  return `${item(cmts[0])}<div class="prc-older-cmts" style="display:none">${rest}</div><button class="prc-more-btn" onclick="toggleMoreCmts(this)">+ ${n} ${window.t('moreCmts')}</button>`;
}

export function toggleMoreCmts(btn) {
  const wrap = btn.previousElementSibling;
  const hidden = wrap.style.display === 'none';
  wrap.style.display = hidden ? 'block' : 'none';
  const n = wrap.querySelectorAll('.prc-cmt-entry').length;
  btn.textContent = hidden ? window.t('hideOlder') : `+ ${n} ${window.t('moreCmts')}`;
}

// ── PR Card Renderers ──────────────────────────────────────────
function renderMyCard(p) {
  const cls = (UCLS[p.urgency] || '') + (p.isDraft ? ' is-draft' : '');
  const _k = `${p.repo}/${p.number}`;
  _prcData[_k] = {body: p.body || '', comments: p.comments || []};
  return `<div class="prc ${cls}" data-pk="${_k}">
    <div style="display:flex;align-items:flex-start;gap:8px">
      <button class="prc-expand-btn" onclick="window.togglePrcDetail(this)">${EXPAND_SVG}</button>
      <div class="prc-row" style="flex:1;min-width:0">
        <div class="prc-left">
          <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${window.e(p.url)}" target="_blank" rel="noopener">${window.e(p.title)}</a></div>
          <div class="prc-meta">${p.isDraft ? window.b('bx', window.t('draft')) : ''}${window.bBranch(p.headRef)}${(p.assignees || []).map(a => `<span class="b bx">→ ${window.e(a)}</span>`).join('')}<span class="b ${ageBadgeCls(p.createdAtIso)}">${window.e(p.ageAgo)}</span></div>
          ${window.bRevMeta(p)}
        </div>
        <div class="prc-right">${window.bRev(p)}${window.bCI(p.ciState, p.failingCheck, p.failingChecks)}${window.bCmt(p)}${window.bMrg(p.mergeState)}</div>
      </div>
    </div>
    <div class="prc-detail">
      <div style="display:grid;grid-template-columns:minmax(0,1fr) minmax(0,1fr);gap:0">
        <div style="padding-right:14px;border-right:1px solid var(--border)"><div class="prc-detail-label">${window.t('prDesc')}</div><div class="prc-detail-body prc-md-desc"></div></div>
        <div style="padding-left:14px"><div class="prc-detail-label">${window.t('lastCmt')}</div><div class="prc-detail-body prc-md-cmt"></div></div>
      </div>
    </div>
  </div>`;
}

function renderRevCard(p) {
  const cls = (UCLS[p.urgency] || '') + (p.isDraft ? ' is-draft' : '');
  const _k = `${p.repo}/${p.number}`;
  _prcData[_k] = {body: p.body || '', comments: p.comments || []};
  return `<div class="prc ${cls}" data-pk="${_k}">
    <div style="display:flex;align-items:flex-start;gap:8px">
      <button class="prc-expand-btn" onclick="window.togglePrcDetail(this)">${EXPAND_SVG}</button>
      <div class="prc-row" style="flex:1;min-width:0">
        <div class="prc-left">
          <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${window.e(p.url)}" target="_blank" rel="noopener">${window.e(p.title)}</a></div>
          <div class="prc-meta">${window.b('bx', p.author)}${p.isDraft ? window.b('bx', window.t('draft')) : ''}${window.bBranch(p.headRef)}<span class="b ${ageBadgeCls(p.createdAtIso)}">${window.e(p.ageAgo)}</span></div>
        </div>
        <div class="prc-right">${window.bCI(p.ciState, p.failingCheck, p.failingChecks)}${window.bCmt(p)}${window.bMrg(p.mergeState)}</div>
      </div>
    </div>
    <div class="prc-detail">
      <div style="display:grid;grid-template-columns:minmax(0,1fr) minmax(0,1fr);gap:0">
        <div style="padding-right:14px;border-right:1px solid var(--border)"><div class="prc-detail-label">${window.t('prDesc')}</div><div class="prc-detail-body prc-md-desc"></div></div>
        <div style="padding-left:14px"><div class="prc-detail-label">${window.t('lastCmt')}</div><div class="prc-detail-body prc-md-cmt"></div></div>
      </div>
    </div>
  </div>`;
}

function renderMyCardWithRepo(p) {
  const cls = (UCLS[p.urgency] || '') + (p.isDraft ? ' is-draft' : '');
  const _k = `${p.repo}/${p.number}`;
  _prcData[_k] = {body: p.body || '', comments: p.comments || []};
  return `<div class="prc ${cls}" data-pk="${_k}">
    <div style="display:flex;align-items:flex-start;gap:8px">
      <button class="prc-expand-btn" onclick="window.togglePrcDetail(this)">${EXPAND_SVG}</button>
      <div class="prc-row" style="flex:1;min-width:0">
        <div class="prc-left">
          <div class="prc-trow"><span class="prc-num">#${p.number}</span><a class="prc-link" href="${window.e(p.url)}" target="_blank" rel="noopener">${window.e(p.title)}</a></div>
          <div class="prc-meta"><span class="branch">${window.e(p.repo)}</span>${p.isDraft ? window.b('bx', window.t('draft')) : ''}${window.bBranch(p.headRef)}<span>${window.e(p.ageAgo)}</span></div>
          ${window.bRevMeta(p)}
        </div>
        <div class="prc-right">${window.bRev(p)}${window.bCI(p.ciState, p.failingCheck, p.failingChecks)}${window.bCmt(p)}${window.bMrg(p.mergeState)}</div>
      </div>
    </div>
    <div class="prc-detail">
      <div style="display:grid;grid-template-columns:minmax(0,1fr) minmax(0,1fr);gap:0">
        <div style="padding-right:14px;border-right:1px solid var(--border)"><div class="prc-detail-label">${window.t('prDesc')}</div><div class="prc-detail-body prc-md-desc"></div></div>
        <div style="padding-left:14px"><div class="prc-detail-label">${window.t('lastCmt')}</div><div class="prc-detail-body prc-md-cmt"></div></div>
      </div>
    </div>
  </div>`;
}

// ── Filters ────────────────────────────────────────────────────
export function renderStatusFilters(prs) {
  const el = document.getElementById('s-my-filters');
  if (!el) return;
  const cnt = {};
  for (const p of prs) cnt[p.urgency] = (cnt[p.urgency] || 0) + 1;
  const ORDER = ['CiFailing', 'ChangesRequested', 'CommentsPending', 'AwaitingReview', 'Approved', 'Normal'];
  const existing = ORDER.filter(u => cnt[u]);
  if (!existing.length) {
    el.innerHTML = '';
    return;
  }
  const all = myPrsFilter === null;
  const chips = [
    `<button class="fchip" style="${all ? 'border-color:var(--muted);color:var(--text)' : ''}" onclick="window.setMyFilter(null)">${window.t('filterAll')} <span class="tcnt">${prs.length}</span></button>`,
    ...existing.map(u => {
      const active = myPrsFilter === u;
      const col = urgencyColor(u);
      const style = active ? `border-color:${col};color:${col}` : '';
      return `<button class="fchip" style="${style}" onclick="window.setMyFilter('${u}')">${window.t('st' + u)} <span class="tcnt">${cnt[u]}</span></button>`;
    })
  ];
  el.innerHTML = `<div style="display:flex;gap:6px;flex-wrap:wrap">${chips.join('')}</div>`;
}

function renderRevFilters(visible) {
  const el = document.getElementById('s-rev-filters');
  if (!el) return;
  let html = '';
  const cnt = {};
  for (const p of visible) cnt[p.urgency] = (cnt[p.urgency] || 0) + 1;
  const ORDER = ['CiFailing', 'ChangesRequested', 'CommentsPending', 'AwaitingReview', 'Approved', 'Normal'];
  const existing = ORDER.filter(u => cnt[u]);
  const FLABEL = `font-size:10px;font-weight:700;text-transform:uppercase;letter-spacing:.07em;color:var(--muted);margin-bottom:5px`;
  if (existing.length) {
    const all = revFilter === null;
    const chips = [
      `<button class="fchip" style="${all ? 'border-color:var(--muted);color:var(--text)' : ''}" onclick="window.setRevFilter(null)">${window.t('filterAll')} <span class="tcnt">${visible.length}</span></button>`,
      ...existing.map(u => {
        const active = revFilter === u;
        const col = urgencyColor(u);
        return `<button class="fchip" style="${active ? `border-color:${col};color:${col}` : ''}" onclick="window.setRevFilter('${u}')">${window.t('st' + u)} <span class="tcnt">${cnt[u]}</span></button>`;
      })
    ];
    html += `<div><div style="${FLABEL}">${window.t('filterByStatus')}</div><div style="display:flex;gap:6px;flex-wrap:wrap">${chips.join('')}</div></div>`;
  }
  const acnt = {};
  for (const p of visible) if (p.author) acnt[p.author] = (acnt[p.author] || 0) + 1;
  const authors = Object.keys(acnt).sort((a, b) => acnt[b] - acnt[a]);
  if (authors.length >= 2) {
    const allA = revAuthorFilter === null;
    const achips = [
      `<button class="fchip" style="${allA ? 'border-color:var(--muted);color:var(--text)' : ''}" onclick="window.setRevAuthorFilter(null)">${window.t('filterAll')} <span class="tcnt">${visible.length}</span></button>`,
      ...authors.map(a => {
        const active = revAuthorFilter === a;
        return `<button class="fchip" style="${active ? 'border-color:var(--indigo);color:var(--indigo)' : ''}" onclick="window.setRevAuthorFilter('${a}')">${window.e(a)} <span class="tcnt">${acnt[a]}</span></button>`;
      })
    ];
    html += (html ? '<div style="height:10px"></div>' : '') + `<div><div style="${FLABEL}">${window.t('filterByAuthor')}</div><div style="display:flex;gap:6px;flex-wrap:wrap">${achips.join('')}</div></div>`;
  }
  el.innerHTML = html;
}

// ── Renderers ──────────────────────────────────────────────────
function renderMyPrsByStatus(prs) {
  const s = document.getElementById('s-my');
  if (!prs.length) {
    s.innerHTML = `<div class="empty">${window.t('emptyMyPrs')}</div>`;
    return;
  }
  const ORDER = ['CiFailing', 'ChangesRequested', 'CommentsPending', 'AwaitingReview', 'Approved', 'Normal'];
  const groups = {};
  for (const p of prs) (groups[p.urgency] = groups[p.urgency] || []).push(p);
  s.innerHTML = ORDER.filter(u => groups[u]?.length).map(urgency => {
    const list = groups[urgency];
    const col = urgencyColor(urgency);
    const key = '__s__' + urgency;
    const collapsed = window.collapsedRepos.has(key);
    const chv = `<svg class="rg-chevron" style="${collapsed ? 'transform:rotate(-90deg)' : ''}" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg>`;
    return `<div class="rgroup" data-repo="${key}"><div class="rgroup-hdr" onclick="window.toggleRepo(this)"><div style="display:flex;align-items:center;gap:6px">${chv}<span style="width:8px;height:8px;border-radius:9999px;background:${col};display:inline-block;flex-shrink:0"></span><span class="rgroup-name" style="color:${col}">${window.t('st' + urgency)}</span><span class="tcnt">${list.length}</span></div></div><div class="rgroup-body"${collapsed ? ' style="display:none"' : ''}>${list.map(renderMyCardWithRepo).join('')}</div></div>`;
  }).join('');
}

// Re-render functions using cached data
export function reRenderMyPrs() {
  renderMyPrs(_lastMyPrs);
}

export function reRenderRevPrs() {
  renderRevSidebar(_lastRevPrs);
  renderRevFull(_lastRevPrs);
}

export function renderMyPrs(prs) {
  _lastMyPrs = prs;
  renderStatusFilters(prs);
  const filtered = myPrsFilter ? prs.filter(p => p.urgency === myPrsFilter) : prs;
  const filtered2 = mySearchText ? filtered.filter(p => p.title.toLowerCase().includes(mySearchText) || p.repo.toLowerCase().includes(mySearchText)) : filtered;
  if (myPrsView === 'status') {
    renderMyPrsByStatus(filtered2);
    window.updateCollapseBtn();
    return;
  }
  const s = document.getElementById('s-my');
  if (!filtered2.length) {
    s.innerHTML = `<div class="empty">${window.t('emptyMyPrs')}</div>`;
    window.updateCollapseBtn();
    return;
  }
  const groups = {};
  for (const p of filtered2) (groups[p.repo] = groups[p.repo] || []).push(p);
  const sortedRepos = window.sortRepos(Object.keys(groups));
  s.innerHTML = sortedRepos.map((repo, idx) => {
    const list = groups[repo];
    const col = window.collapsedRepos.has(repo);
    return `<div class="rgroup" data-repo="${window.e(repo)}"><div class="rgroup-hdr" onclick="window.toggleRepo(this)"><div style="display:flex;align-items:center;gap:6px"><svg class="rg-chevron" style="${col ? 'transform:rotate(-90deg)' : ''}" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg><span style="width:8px;height:8px;border-radius:9999px;background:${groupDot(list)};display:inline-block;flex-shrink:0"></span><span class="rgroup-name">${window.e(repo)}</span><span class="tcnt">${list.length}</span></div><div style="display:flex;align-items:center;gap:4px">${window.statusChips(list)}${window.reorderBtns(repo, idx, sortedRepos.length)}</div></div><div class="rgroup-body"${col ? ' style="display:none"' : ''}>${list.map(renderMyCard).join('')}</div></div>`;
  }).join('');
  window.updateCollapseBtn();
}

export function nextRevPage() {
  const visible = window.hideBotReviews ? _lastRevPrs.filter(p => !window.isBotAuthor(p.author)) : _lastRevPrs;
  const maxPage = Math.max(0, Math.ceil(visible.length / SIDEBAR_PAGE_SIZE) - 1);
  if (revSidebarPage < maxPage) {
    revSidebarPage++;
    renderRevSidebar(_lastRevPrs);
  }
}

export function prevRevPage() {
  if (revSidebarPage > 0) {
    revSidebarPage--;
    renderRevSidebar(_lastRevPrs);
  }
}

export function renderRevSidebar(prs) {
  const s = document.getElementById('s-rev-side');
  const visible = window.hideBotReviews ? prs.filter(p => !window.isBotAuthor(p.author)) : prs;
  if (!visible.length) {
    s.innerHTML = '';
    revSidebarPage = 0;
    return;
  }
  const totalPages = Math.ceil(visible.length / SIDEBAR_PAGE_SIZE);
  if (revSidebarPage >= totalPages) revSidebarPage = Math.max(0, totalPages - 1);

  const start = revSidebarPage * SIDEBAR_PAGE_SIZE;
  const end = start + SIDEBAR_PAGE_SIZE;
  const page = visible.slice(start, end);

  const items = page.map(p => `<div class="srev">
    <div class="srev-repo">${window.e(p.repo)} · #${p.number}</div>
    <a class="srev-link" href="${window.e(p.url)}" target="_blank" rel="noopener">${window.e(p.title)}</a>
    <div class="prc-meta">${window.bCI(p.ciState, p.failingCheck, p.failingChecks)}${window.bMrg(p.mergeState)}<span>${window.e(p.ageAgo)}</span></div>
  </div>`).join('');

  const pagination = totalPages > 1 ? `<div style="display:flex;align-items:center;justify-content:space-between;padding:8px 0;border-top:1px solid var(--border);margin-top:8px">
    <button onclick="window.prevRevPage()" ${revSidebarPage === 0 ? 'disabled' : ''} style="padding:4px 10px;border-radius:4px;border:1px solid var(--border);background:var(--surf);color:var(--text);cursor:pointer;font-size:11px;font-family:inherit" ${revSidebarPage === 0 ? 'style="opacity:0.5;cursor:not-allowed"' : ''}>← Anterior</button>
    <span style="font-size:11px;color:var(--muted)">${revSidebarPage + 1} / ${totalPages}</span>
    <button onclick="window.nextRevPage()" ${revSidebarPage === totalPages - 1 ? 'disabled' : ''} style="padding:4px 10px;border-radius:4px;border:1px solid var(--border);background:var(--surf);color:var(--text);cursor:pointer;font-size:11px;font-family:inherit" ${revSidebarPage === totalPages - 1 ? 'style="opacity:0.5;cursor:not-allowed"' : ''}>Próximo →</button>
  </div>` : '';

  s.innerHTML = `<div class="sc"><div class="sc-hdr"><div class="sc-title"><span style="width:7px;height:7px;border-radius:9999px;background:var(--amber);display:inline-block"></span>${window.t('sidebarReviews')}</div><span class="b by">${window.t('pendingCount', visible.length)}</span></div>${items}${pagination}</div>`;
}

export function toggleAllRevRepos() {
  const groups = [...document.querySelectorAll('#s-rev-full .rgroup')];
  if (!groups.length) return;
  const repos = groups.map(g => g.dataset.repo).filter(Boolean);
  const allCollapsed = repos.length > 0 && repos.every(r => window.collapsedRepos.has(r));
  if (allCollapsed) {
    repos.forEach(r => window.collapsedRepos.delete(r));
    groups.forEach(g => {
      const b = g.querySelector('.rgroup-body');
      const c = g.querySelector('.rg-chevron');
      if (b) b.style.display = '';
      if (c) c.style.transform = '';
    });
  } else {
    repos.forEach(r => window.collapsedRepos.add(r));
    groups.forEach(g => {
      const b = g.querySelector('.rgroup-body');
      const c = g.querySelector('.rg-chevron');
      if (b) b.style.display = 'none';
      if (c) c.style.transform = 'rotate(-90deg)';
    });
  }
  localStorage.setItem('prd-collapsed', JSON.stringify([...window.collapsedRepos]));
  window.persistUiState();
  window.updateRevCollapseBtn();
}

export function updateRevCollapseBtn() {
  const btn = document.getElementById('rev-collapse-all-btn');
  if (!btn) return;
  const groups = [...document.querySelectorAll('#s-rev-full .rgroup')];
  const repos = groups.map(g => g.dataset.repo).filter(Boolean);
  const allCollapsed = repos.length > 0 && repos.every(r => window.collapsedRepos.has(r));
  const lbl = document.getElementById('rev-collapse-all-label');
  const ic = btn.querySelector('.rev-icon-collapse');
  const ie = btn.querySelector('.rev-icon-expand');
  if (lbl) lbl.textContent = allCollapsed ? window.t('expandAll') : window.t('collapseAll');
  if (ic) ic.style.display = allCollapsed ? 'none' : '';
  if (ie) ie.style.display = allCollapsed ? '' : 'none';
}

export function renderRevFull(prs) {
  _lastRevPrs = prs;
  const visible = window.hideBotReviews ? prs.filter(p => !window.isBotAuthor(p.author)) : prs;
  renderRevFilters(visible);
  window.updateRevHideBotsBtn();
  const filtered = visible.filter(p => (!revFilter || p.urgency === revFilter) && (!revAuthorFilter || p.author === revAuthorFilter));
  const filtered2 = revSearchText ? filtered.filter(p => p.title.toLowerCase().includes(revSearchText) || p.repo.toLowerCase().includes(revSearchText)) : filtered;
  const s = document.getElementById('s-rev-full');
  if (!filtered2.length) {
    s.innerHTML = `<div class="empty">${window.t('emptyReviews')}</div>`;
    updateRevCollapseBtn();
    return;
  }
  const groups = {};
  for (const p of filtered2) (groups[p.repo] = groups[p.repo] || []).push(p);
  s.innerHTML = Object.entries(groups).sort(([a], [b]) => a.localeCompare(b)).map(([repo, list]) => {
    const col = window.collapsedRepos.has(repo);
    return `<div class="rgroup" data-repo="${window.e(repo)}"><div class="rgroup-hdr" onclick="window.toggleRepo(this)"><div style="display:flex;align-items:center;gap:6px"><svg class="rg-chevron" style="${col ? 'transform:rotate(-90deg)' : ''}" width="12" height="12" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round" viewBox="0 0 24 24"><path d="M6 9l6 6 6-6"/></svg><span style="width:8px;height:8px;border-radius:9999px;background:${groupDot(list)};display:inline-block;flex-shrink:0"></span><span class="rgroup-name">${window.e(repo)}</span><span class="tcnt">${list.length}</span></div><div style="display:flex;align-items:center;gap:4px">${window.statusChips(list)}</div></div><div class="rgroup-body"${col ? ' style="display:none"' : ''}>${list.map(renderRevCard).join('')}</div></div>`;
  }).join('');
  updateRevCollapseBtn();
}

export function nextMrgPage() {
  const maxPage = Math.max(0, Math.ceil(_lastMrgPrs.length / SIDEBAR_PAGE_SIZE) - 1);
  if (mrgSidebarPage < maxPage) {
    mrgSidebarPage++;
    renderMrgSidebar(_lastMrgPrs);
  }
}

export function prevMrgPage() {
  if (mrgSidebarPage > 0) {
    mrgSidebarPage--;
    renderMrgSidebar(_lastMrgPrs);
  }
}

export function renderMrgSidebar(prs) {
  const s = document.getElementById('s-mrg-side');
  if (!prs.length) {
    s.innerHTML = '';
    mrgSidebarPage = 0;
    return;
  }
  const totalPages = Math.ceil(prs.length / SIDEBAR_PAGE_SIZE);
  if (mrgSidebarPage >= totalPages) mrgSidebarPage = Math.max(0, totalPages - 1);

  const start = mrgSidebarPage * SIDEBAR_PAGE_SIZE;
  const end = start + SIDEBAR_PAGE_SIZE;
  const page = prs.slice(start, end);

  const items = page.map(p => `<div class="smerge-item">
    <div class="smerge-icon">${MERGE_SVG}</div>
    <div><a class="smerge-link" href="${window.e(p.url)}" target="_blank" rel="noopener">#${p.number} ${window.e(p.title)}</a><div class="smerge-sub">${window.e(p.repo)} · ${window.e(p.mergedAgo)}</div></div>
  </div>`).join('');

  const pagination = totalPages > 1 ? `<div style="display:flex;align-items:center;justify-content:space-between;padding:8px 0;border-top:1px solid var(--border);margin-top:8px">
    <button onclick="window.prevMrgPage()" ${mrgSidebarPage === 0 ? 'disabled' : ''} style="padding:4px 10px;border-radius:4px;border:1px solid var(--border);background:var(--surf);color:var(--text);cursor:pointer;font-size:11px;font-family:inherit" ${mrgSidebarPage === 0 ? 'style="opacity:0.5;cursor:not-allowed"' : ''}>← Anterior</button>
    <span style="font-size:11px;color:var(--muted)">${mrgSidebarPage + 1} / ${totalPages}</span>
    <button onclick="window.nextMrgPage()" ${mrgSidebarPage === totalPages - 1 ? 'disabled' : ''} style="padding:4px 10px;border-radius:4px;border:1px solid var(--border);background:var(--surf);color:var(--text);cursor:pointer;font-size:11px;font-family:inherit" ${mrgSidebarPage === totalPages - 1 ? 'style="opacity:0.5;cursor:not-allowed"' : ''}>Próximo →</button>
  </div>` : '';

  s.innerHTML = `<div class="sc"><div class="sc-hdr"><div class="sc-title"><span style="width:7px;height:7px;border-radius:9999px;background:var(--indigo);display:inline-block"></span>${window.t('sidebarMerged')}</div><span style="font-size:11px;font-family:monospace;color:var(--muted)">${window.t('recentCount', prs.length)}</span></div>${items}${pagination}</div>`;
}

export function renderMrgFull(prs) {
  _lastMrgPrs = prs;
  const filtered = mrgSearchText ? prs.filter(p => p.title.toLowerCase().includes(mrgSearchText) || p.repo.toLowerCase().includes(mrgSearchText)) : prs;
  const s = document.getElementById('s-mrg-full');
  if (!filtered.length) {
    s.innerHTML = `<div class="empty">${window.t('emptyMerged')}</div>`;
    return;
  }
  const rows = filtered.map(p => `<div class="prc">
    <div class="prc-row">
      <div class="prc-left">
        <div class="prc-trow"><span style="font-size:11px;font-family:monospace;color:var(--muted)">${window.e(p.repo)}</span><span class="prc-num">#${p.number}</span><a class="prc-link" href="${window.e(p.url)}" target="_blank" rel="noopener">${window.e(p.title)}</a></div>
      </div>
      <div class="prc-right" style="flex-direction:row;align-items:center;gap:8px">${window.b('bx', p.mergedAgo)}<span style="font-size:11px;color:var(--muted)">${window.t('tookDuration', p.duration)}</span></div>
    </div>
  </div>`).join('');
  s.innerHTML = `<div class="rgroup">${rows}</div>`;
}

// ── Metrics ────────────────────────────────────────────────────
function setBar(id, pct) {
  const el = document.getElementById(id);
  if (el) el.style.width = Math.min(100, Math.max(0, pct)) + '%';
}

export function updateMetrics(data) {
  const my = data.myPrs || [];
  const rev = data.reviewPrs || [];
  const mrg = data.mergedPrs || [];
  const ci = my.filter(p => p.ciState === 'Failing').length;
  const attn = my.filter(p => p.needsAction).length;
  document.getElementById('m-open').textContent = my.length;
  document.getElementById('m-open-sub').textContent = attn ? window.t('metricsNeedAction', attn) : window.t('metricsOk');
  document.getElementById('m-ci').textContent = ci;
  document.getElementById('m-ci-sub').textContent = ci === 1 ? window.t('metricsBlocker') : window.t('metricsBlockers');
  document.getElementById('m-rev').textContent = rev.length;
  document.getElementById('m-rev-sub').textContent = rev.length === 1 ? window.t('metricsPending') : window.t('metricsPendings');
  document.getElementById('m-mrg').textContent = mrg.length;
  document.getElementById('m-mrg-sub').textContent = window.t('metricsRecent');
  document.getElementById('tc-my').textContent = my.length;
  document.getElementById('tc-rev').textContent = rev.length;
  document.getElementById('tc-mrg').textContent = mrg.length;
  const tot = Math.max(my.length, 1);
  setBar('m-open-bar', my.length > 0 ? Math.max(10, Math.round((my.length - attn) / tot * 100)) : 0);
  setBar('m-ci-bar', Math.round(ci / tot * 100));
  setBar('m-rev-bar', Math.min(100, rev.length * 5));
  setBar('m-mrg-bar', Math.min(100, mrg.length * 10));
}
