// ── i18n ───────────────────────────────────────────────────────
const I18N={
  'pt-BR':{
    refresh:'Atualizar',
    metricsMyPrs:'Meus PRs Abertos', metricsCi:'CI com Falha', metricsReviews:'Reviews a Mim', metricsMerged:'Mergeados Recentes',
    metricsOk:'ok', metricsBlocker:'bloqueador', metricsBlockers:'bloqueadores',
    metricsPending:'pendente', metricsPendings:'pendentes', metricsRecent:'recentes',
    metricsNeedAction:n=>`${n} em atenção`,
    tabDashboard:'Dashboard', tabMyPrs:'Meus PRs', tabReviews:'Reviews Solicitados', tabMerged:'Mergeados', tabWorktrees:'Worktrees & Repos', tabStats:'Estatísticas',
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
    menuInterval:'Intervalo de atualização', menuSecs:'seg', menuSave:'Salvar', menuDname:'Nome no relatório PDF',
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
    stFilterRepo:'Repositório', stFilterMonth:'Mês', stFilterWeek:'Semana', stFilterDate:'Período',
    stShowPRTable:'Tabela de PRs', stClearFilters:'Limpar filtros', stClearPR:'limpar',
    stEmptyTitle:'Nenhum dado encontrado', stEmptySub:'Rode o backfill para gerar os JSONs',
    stStatTotal:'PRs abertos', stStatMerged:'PRs mergeados', stStatCycle:'Tempo médio de ciclo', stStatRepos:'Repos tocados',
    stChartRepos:'Entregas por repositório', stChartCadence:'Cadência por mês', stChartStatus:'Status de entrega', stChartTimeline:'Timeline por mês', stChartReposMes:'Distribuição por mês',
    stAllRepos:'Todos os repositórios', stAllMonths:'Todos os meses', stAllWeeks:'Todas as semanas',
    stReposPl:'repositórios', stMonthsPl:'meses', stWeeksPl:'semanas',
    stAllFilter:'Todos', stAllReposLabel:'Todos os repos',
    stColPR:'PR / Ref', stColDate:'Data', stColTitle:'Título',
    stNoEntries:'Nenhuma entrada para este filtro.',
    stEntries:n=>`${n} entrada${n===1?'':'s'}`,
    stShowingOf:(a,b)=>`Mostrando ${a} de ${b}`,
    wtFilterRepo:'REPOSITÓRIO', wtFilterStatus:'STATUS DO REPO', wtFilterSearch:'BUSCAR RAMO / REPO',
    wtAllRepos:'Todos os repositórios (14)', wtAllStates:'Todos os estados',
    wtOutdated:'Desatualizados (5)', wtWithWorktrees:'Com Worktrees (4)',
    wtWithChanges:'Com alterações locais (2)', wtInConflict:'Em conflito (1)',
    wtSearchPlaceholder:'Filtrar por nome ou branch...', wtWorktreeView:'Worktree View',
    wtClearFilters:'Limpar filtros', wtExportReport:'Exportar Relatório',
    wtKpiIndexed:'Repositórios Indexados', wtKpiWorktrees:'Worktrees Ativas em Disco',
    wtKpiPendingUpdates:'Repositórios com Atualizações Pendentes', wtKpiDiskSpace:'Espaço Ocupado pelas Pastas',
    wtLabelRoot:'RAIZ:', wtLabelDestination:'DESTINO:', wtChangeFolder:'Alterar pasta',
    wtSelected:'2 selecionados', wtUpdateSelected:'Atualizar Selecionados para main (2)',
    wtCheckRemote:'Verificar status remoto',
    wtUnderConstruction:'Em construção', wtUnderConstructionDesc:'A visualização de repositórios e worktrees será implementada em breve.',
    wtFooterSynced:'14 repositórios sincronizados', wtFooterSpace:'Espaço ocupado por worktrees: 3.8 GB',
    wtFooterEngine:'SSD Hub Engine: 0.14ms',
    wtPromptRoot:'Digite o caminho da pasta raiz:', wtPromptWorktrees:'Digite o caminho da pasta de worktrees:',
    wtPathUpdated:'Caminho {type} atualizado: {path}', wtPathTypeRoot:'raiz', wtPathTypeWorktrees:'destino',
    menuWorktrees:'Configurar Pastas', menuRootFolder:'Pasta raiz dos repositórios', menuRootFolderDesc:'Definir localização raiz',
    menuWorktreesFolder:'Pasta de worktrees', menuWorktreesFolderDesc:'Definir destino das worktrees',
    wtErrorConfigTitle:'Configuração Incompleta', wtConfigNow:'Configurar Agora',
    wtErrorRootPath:'A pasta raiz dos repositórios não está configurada.',
    wtErrorWorktreesPath:'A pasta de destino das worktrees não está configurada.',
    wtErrorRootPathNotFound:'A pasta raiz configurada não existe no sistema.',
    wtErrorWorktreesPathNotFound:'A pasta de worktrees configurada não existe no sistema.',
    wtBatchSelected:'selecionados',
    wtBatchUpdate:'Atualizar Selecionados',
    wtBatchDeselect:'Deselecionar',
    wtStatRepos:'Repositórios Indexados',
    wtStatWorktrees:'Worktrees Ativas em Disco',
    wtStatUpdates:'Repositórios com Atualizações Pendentes',
    wtStatDisk:'Espaço Ocupado pelas Pastas',
    wtUpdate:'Atualizar',
    wtCreateWorktree:'+ Worktree',
    wtOpenVSCode:'VS Code',
    wtWorktreesLinked:'worktrees',
    wtCreateFrom:'Criar worktree a partir de branch...',
    wtOpen:'Abrir',
    wtTerminal:'Terminal',
    wtDelete:'Excluir',
    wtNoWorktrees:'Nenhuma worktree vinculada',
    wtModalCreateTitle:'Criar Nova Worktree',
    wtModalName:'Nome da Worktree',
    wtModalBranch:'Branch',
    wtModalCreate:'Criar Worktree',
    wtModalCancel:'Cancelar',
    wtToastCreated:'Worktree criada com sucesso!',
    wtToastDeleted:'Worktree excluída com sucesso!',
    wtToastUpdated:'atualizado com sucesso!',
    wtToastError:'Erro ao executar operação',
    wtConfirmDelete:'Tem certeza que deseja excluir a worktree?',
    wtConfirmDeleteWarning:'Esta ação não pode ser desfeita.',
    wtStatusUpdated:'Atualizado',
    wtStatusBehind:'commits atrás',
    wtStatusAhead:'à frente de origin',
    wtStatusConflict:'Conflito / Divergente',
    wtStatusClean:'Limpo',
    wtStatusUncommitted:'arquivo(s) não commitado(s)',
    wtWorktreeCount:'worktrees ativas',
    wtWorktreeCount_one:'worktree ativa',
    wtSyncStatus:'Git Daemon Local:',
    wtSyncTime:'sync',
    settingsTitle:'Configurações',
    settingsSubtitle:'Ajuste suas preferências',
    settingsDisplayName:'Nome de Exibição',
    settingsDisplayNameDesc:'Personalizar seu nome no dashboard',
    wtSettingsRoot:'Pasta Raiz dos Repositórios',
    wtSettingsRootDesc:'Local onde seus repositórios estão',
    wtSettingsWorktrees:'Pasta de Destino das Worktrees',
    wtSettingsWorktreesDesc:'Onde criar novas worktrees',
    menuSectionDashboard:'Dashboard',
    menuSectionStats:'Estatísticas',
    menuSectionWorktrees:'Repos & Worktrees',
    menuNoSettings:'Sem configurações disponíveis',
    menuWtPolling:'Intervalo de atualização',
  },
  'en-US':{
    refresh:'Refresh',
    metricsMyPrs:'My Open PRs', metricsCi:'CI Failing', metricsReviews:'Reviews for Me', metricsMerged:'Recently Merged',
    metricsOk:'ok', metricsBlocker:'blocker', metricsBlockers:'blockers',
    metricsPending:'pending', metricsPendings:'pending', metricsRecent:'recent',
    metricsNeedAction:n=>`${n} need attention`,
    tabDashboard:'Dashboard', tabMyPrs:'My PRs', tabReviews:'Review Requests', tabMerged:'Merged', tabWorktrees:'Worktrees & Repos', tabStats:'Statistics',
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
    menuInterval:'Update interval', menuSecs:'sec', menuSave:'Save', menuDname:'PDF report name',
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
    stFilterRepo:'Repository', stFilterMonth:'Month', stFilterWeek:'Week', stFilterDate:'Date range',
    stShowPRTable:'PR Table', stClearFilters:'Clear filters', stClearPR:'clear',
    stEmptyTitle:'No data found', stEmptySub:'Run backfill to generate the JSONs',
    stStatTotal:'PRs opened', stStatMerged:'Merged PRs', stStatCycle:'Avg cycle time', stStatRepos:'Repos touched',
    stChartRepos:'Deliveries by repository', stChartCadence:'Delivery cadence by month', stChartStatus:'Delivery status', stChartTimeline:'Timeline by month', stChartReposMes:'Repo distribution by month',
    stAllRepos:'All repositories', stAllMonths:'All months', stAllWeeks:'All weeks',
    stReposPl:'repositories', stMonthsPl:'months', stWeeksPl:'weeks',
    stAllFilter:'All', stAllReposLabel:'All repos',
    stColPR:'PR / Ref', stColDate:'Date', stColTitle:'Title',
    stNoEntries:'No entries for this filter.',
    stEntries:n=>`${n} entr${n===1?'y':'ies'}`,
    stShowingOf:(a,b)=>`Showing ${a} of ${b}`,
    wtFilterRepo:'REPOSITORY', wtFilterStatus:'REPO STATUS', wtFilterSearch:'SEARCH BRANCH / REPO',
    wtAllRepos:'All repositories (14)', wtAllStates:'All states',
    wtOutdated:'Outdated (5)', wtWithWorktrees:'With Worktrees (4)',
    wtWithChanges:'With local changes (2)', wtInConflict:'In conflict (1)',
    wtSearchPlaceholder:'Filter by name or branch...', wtWorktreeView:'Worktree View',
    wtClearFilters:'Clear filters', wtExportReport:'Export Report',
    wtKpiIndexed:'Indexed Repositories', wtKpiWorktrees:'Active Worktrees on Disk',
    wtKpiPendingUpdates:'Repositories with Pending Updates', wtKpiDiskSpace:'Space Used by Folders',
    wtLabelRoot:'ROOT:', wtLabelDestination:'DESTINATION:', wtChangeFolder:'Change folder',
    wtSelected:'2 selected', wtUpdateSelected:'Update Selected to main (2)',
    wtCheckRemote:'Check remote status',
    wtUnderConstruction:'Under construction', wtUnderConstructionDesc:'Repository and worktree visualization will be implemented soon.',
    wtFooterSynced:'14 synchronized repositories', wtFooterSpace:'Space used by worktrees: 3.8 GB',
    wtFooterEngine:'SSD Hub Engine: 0.14ms',
    wtPromptRoot:'Enter the root folder path:', wtPromptWorktrees:'Enter the worktrees folder path:',
    wtPathUpdated:'Path {type} updated: {path}', wtPathTypeRoot:'root', wtPathTypeWorktrees:'destination',
    menuWorktrees:'Configure Folders', menuRootFolder:'Root repository folder', menuRootFolderDesc:'Set root location',
    menuWorktreesFolder:'Worktrees folder', menuWorktreesFolderDesc:'Set worktrees destination',
    wtErrorConfigTitle:'Incomplete Configuration', wtConfigNow:'Configure Now',
    wtErrorRootPath:'The root repositories folder is not configured.',
    wtErrorWorktreesPath:'The worktrees destination folder is not configured.',
    wtErrorRootPathNotFound:'The configured root folder does not exist on the system.',
    wtErrorWorktreesPathNotFound:'The configured worktrees folder does not exist on the system.',
    wtBatchSelected:'selected',
    wtBatchUpdate:'Update Selected',
    wtBatchDeselect:'Deselect',
    wtStatRepos:'Indexed Repositories',
    wtStatWorktrees:'Active Worktrees on Disk',
    wtStatUpdates:'Repositories with Pending Updates',
    wtStatDisk:'Space Used by Folders',
    wtUpdate:'Update',
    wtCreateWorktree:'+ Worktree',
    wtOpenVSCode:'VS Code',
    wtWorktreesLinked:'worktrees',
    wtCreateFrom:'Create worktree from branch...',
    wtOpen:'Open',
    wtTerminal:'Terminal',
    wtDelete:'Delete',
    wtNoWorktrees:'No additional worktrees',
    wtModalCreateTitle:'Create New Worktree',
    wtModalName:'Worktree Name',
    wtModalBranch:'Branch',
    wtModalCreate:'Create Worktree',
    wtModalCancel:'Cancel',
    wtToastCreated:'Worktree created successfully!',
    wtToastDeleted:'Worktree deleted successfully!',
    wtToastUpdated:'updated successfully!',
    wtToastError:'Error executing operation',
    wtConfirmDelete:'Are you sure you want to delete this worktree?',
    wtConfirmDeleteWarning:'This action cannot be undone.',
    wtStatusUpdated:'Updated',
    wtStatusBehind:'commits behind',
    wtStatusAhead:'ahead of origin',
    wtStatusConflict:'Conflict / Diverged',
    wtStatusClean:'Clean',
    wtStatusUncommitted:'uncommitted file(s)',
    wtWorktreeCount:'active worktrees',
    wtWorktreeCount_one:'active worktree',
    wtSyncStatus:'Local Git Daemon:',
    wtSyncTime:'sync',
    settingsTitle:'Settings',
    settingsSubtitle:'Adjust your preferences',
    settingsDisplayName:'Display Name',
    settingsDisplayNameDesc:'Customize your dashboard name',
    wtSettingsRoot:'Root Folder for Repositories',
    wtSettingsRootDesc:'Where your repositories are located',
    wtSettingsWorktrees:'Worktrees Destination Folder',
    wtSettingsWorktreesDesc:'Where to create new worktrees',
    menuSectionDashboard:'Dashboard',
    menuSectionStats:'Statistics',
    menuSectionWorktrees:'Repos & Worktrees',
    menuNoSettings:'No settings available',
    menuWtPolling:'Update interval',
  }
};
let currentLang='en-US';  // Default to English
let ghLogin='';
function t(key,...args){const v=I18N[currentLang]?.[key]??I18N['pt-BR'][key]??key;return typeof v==='function'?v(...args):v}
function applyI18n(){
  document.querySelectorAll('[data-i18n]').forEach(el=>{el.textContent=t(el.dataset.i18n)});
  document.querySelectorAll('[data-i18n-ph]').forEach(el=>{el.placeholder=t(el.dataset.i18nPh)});
  document.querySelectorAll('[data-i18n-title]').forEach(el=>{el.title=t(el.dataset.i18nTitle)});
  st_syncI18n();if(st_initialized)st_render();
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
  if(st_initialized)st_applyChartTheme(th==='light');
}
async function toggleTheme(){
  const th=document.documentElement.classList.contains('dark')?'light':'dark';
  applyTheme(th);
  await saveSettings({theme:th});
}
applyTheme('dark');

// ── Language ───────────────────────────────────────────────────
async function toggleLanguage(){
  const l=currentLang==='pt-BR'?'en-US':'pt-BR';
  currentLang=l;
  
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
let sidebarOpen=true;
let repoOrder=[];
function applySidebar(){
  const g=document.getElementById('main-grid');if(!g)return;
  const lbl=document.getElementById('stoggle-label');
  if(sidebarOpen){g.classList.remove('sidebar-closed');if(lbl)lbl.textContent=t('closeSidebar')}
  else{g.classList.add('sidebar-closed');if(lbl)lbl.textContent=t('openSidebar')}
}
function toggleSidebar(){sidebarOpen=!sidebarOpen;applySidebar();persistUiState()}

// ── View switcher (Dashboard / Estatísticas) ───────────────────
var currentView='dashboard';
var dataLoaded=false;
var st_initialized=false;
function switchView(v){
  currentView=v;
  document.getElementById('view-btn-dashboard').classList.toggle('active',v==='dashboard');
  document.getElementById('view-btn-worktrees').classList.toggle('active',v==='worktrees');
  document.getElementById('view-btn-stats').classList.toggle('active',v==='stats');
  const content=document.getElementById('content');
  const statsDiv=document.getElementById('view-stats');
  const worktreesDiv=document.getElementById('view-worktrees');
  const globalLoading=document.getElementById('loading');

  if(v==='dashboard'){
    stopWorktreesPolling();
    if(statsDiv)statsDiv.style.display='none';
    if(worktreesDiv)worktreesDiv.style.display='none';
    if(content&&dataLoaded){
      content.style.display='block';
      if(globalLoading)globalLoading.style.display='none';
    }
  } else if(v==='worktrees'){
    stopWorktreesPolling();
    if(content)content.style.display='none';
    if(statsDiv)statsDiv.style.display='none';
    if(worktreesDiv)worktreesDiv.style.display='block';
    // IMPORTANTE: Esconder loading global ANTES de carregar
    if(globalLoading)globalLoading.style.display='none';
    loadWorktreesView();
  } else {
    stopWorktreesPolling();
    if(content)content.style.display='none';
    if(worktreesDiv)worktreesDiv.style.display='none';
    if(statsDiv){
      statsDiv.style.display='block';
      // IMPORTANTE: Esconder loading global nas stats também
      if(globalLoading)globalLoading.style.display='none';
      if(!st_initialized){st_initialized=true;st_syncI18n();buildCharts();loadData();}
    }
  }
}

// ── Tabs ───────────────────────────────────────────────────────
let currentTab='my';
function setTab(tab){
  currentTab=tab;
  document.querySelectorAll('.tab-panel').forEach(p=>p.classList.remove('active'));
  document.getElementById('tab-'+tab).classList.add('active');
  document.querySelectorAll('.tab-btn[data-tab]').forEach(b=>{b.classList.toggle('active',b.dataset.tab===tab)});
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
async function playAudio(src,n){
  for(let i=0;i<n;i++){
    if(i>0)await new Promise(r=>setTimeout(r,300));
    const a=new Audio(src);
    await new Promise(r=>{a.onended=r;a.onerror=r;a.play().catch(r);setTimeout(r,5000);});
  }
}
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

// Simple toast notification helper (global)
function pushNotif(message, type = 'info') {
  const d = document.createElement('div');
  d.className = 'notif ' + type;
  d.innerHTML = `<button class="nclose" onclick="this.parentNode.remove()">×</button><div class="ntitle">${e(message)}</div>`;
  document.getElementById('nstack').prepend(d);
  setTimeout(() => { if (d.parentNode) d.remove() }, 5000);
}

function saveInterval(){
  const v=parseInt(document.getElementById('interval-input')?.value||'300');
  if(isNaN(v)||v<30)return;
  fetch('/api/settings',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({intervalSeconds:v})}).catch(()=>{});
  ivSecs=v;cd=v;
}
function toggleUserMenu(){
  const dd=document.getElementById('user-dd');
  const opening=dd.style.display==='none';
  dd.style.display=opening?'block':'none';
  if(opening){
    const inp=document.getElementById('dname-input');
    if(inp)inp.value=ghLogin||'';

    // Atualizar intervalo de polling
    const wtPollingInput=document.getElementById('wt-polling-input');
    if(wtPollingInput)wtPollingInput.value=worktreesPollingRate/1000;

    // Atualizar paths configurados no dropdown
    updateWorktreesPathsDisplay();
  }
}
function closeUserMenu(){const dd=document.getElementById('user-dd');if(dd)dd.style.display='none'}
document.addEventListener('click',e=>{const w=document.getElementById('user-menu-wrap');if(w&&!w.contains(e.target))closeUserMenu()})

function updateWorktreesPathsDisplay(){
  const config=loadWorktreesConfig();

  const rootDisplay=document.getElementById('wt-root-path-display');
  const worktreesDisplay=document.getElementById('wt-worktrees-path-display');

  if(rootDisplay){
    if(config.rootPath&&config.rootPath!=='C:\\'){
      rootDisplay.textContent=config.rootPath;
      rootDisplay.style.color='var(--text)';
    }else{
      rootDisplay.textContent=t('wtSettingsRootDesc');
      rootDisplay.style.color='var(--muted)';
    }
  }

  if(worktreesDisplay){
    if(config.worktreesPath&&config.worktreesPath!=='C:\\'){
      worktreesDisplay.textContent=config.worktreesPath;
      worktreesDisplay.style.color='var(--text)';
    }else{
      worktreesDisplay.textContent=t('wtSettingsWorktreesDesc');
      worktreesDisplay.style.color='var(--muted)';
    }
  }
}

window.saveWtPolling=function(){
  const input=document.getElementById('wt-polling-input');
  if(!input)return;

  const seconds=parseInt(input.value,10);
  if(isNaN(seconds)||seconds<60||seconds>3600){
    alert(currentLang==='pt-BR'?'Intervalo deve ser entre 60 e 3600 segundos':'Interval must be between 60 and 3600 seconds');
    return;
  }

  saveWorktreesPollingConfig(seconds);

  const msg=currentLang==='pt-BR'?`Atualização automática a cada ${seconds}s (${Math.floor(seconds/60)}min)`:`Auto-refresh every ${seconds}s (${Math.floor(seconds/60)}min)`;
  pushNotif(`✅ ${msg}`,'info');

  console.log(`[WORKTREES] Polling configurado para ${seconds}s (${Math.floor(seconds/60)}min)`);
}

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

const MERGE_SVG=`<svg class="ti" width="10" height="10"><use href="/icons.svg#ti-check"/></svg>`;

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
let myPrsView='repo';
let _lastMyPrs=[];
let _lastMrgPrs=[];
const notifHistory=[];
let myPrsFilter=null;
let mySearchText='', revSearchText='', mrgSearchText='';
function setMyView(v){
  myPrsView=v;
  
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
const EXPAND_SVG=`<svg class="ti" width="14" height="14"><use href="/icons.svg#ti-chevron-right"/></svg>`;
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
  const CHV=`<svg class="ti rg-chevron" width="12" height="12"><use href="/icons.svg#ti-chevron-down"/></svg>`;
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
  if(data.theme){applyTheme(data.theme)}
  if(data.language&&data.language!==currentLang){currentLang=data.language;}
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
  dataLoaded=true;
  if(currentView==='dashboard'){
    const contentEl=document.getElementById('content');
    if(contentEl)contentEl.style.display='block';
  }
  else if(currentView==='stats'&&st_initialized){
    document.getElementById('view-stats').style.display='block';
  }
  else if(currentView==='worktrees'){
    document.getElementById('view-worktrees').style.display='block';
  }
  const pill=document.getElementById('upill');
  if(data.login) ghLogin=data.login;
  if(data.displayName) ghLogin=data.displayName;
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

// ── Stats view ─────────────────────────────────────────────────
Chart.defaults.color='#6c7086';
Chart.defaults.borderColor='#313244';
Chart.defaults.font.size=11;
Chart.defaults.font.family="system-ui,-apple-system,'Segoe UI',sans-serif";
const PALETTE=['#cba6f7','#89b4fa','#94e2d5','#f9e2af','#fab387','#a6e3a1','#f38ba8','#89dceb','#b4befe','#eba0ac'];
const C={mauve:'#cba6f7',blue:'#89b4fa',teal:'#94e2d5',yellow:'#f9e2af',peach:'#fab387',green:'#a6e3a1',red:'#f38ba8',sky:'#89dceb',gray:'#585b70'};
const MONTH_NAMES=['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
const MONTH_ORDER={Jan:1,Feb:2,Mar:3,Apr:4,May:5,Jun:6,Jul:7,Aug:8,Sep:9,Oct:10,Nov:11,Dec:12};
const MONTH_PALETTE=[C.peach,C.sky,C.teal,C.blue,C.mauve,C.green,C.yellow,C.red];
const STATUS_COLORS={merged:C.green,closed:C.gray,open:C.blue,draft:C.yellow,blocked:C.peach,na:'#45475a'};
const STATUS_LBL={merged:'Merged',closed:'Closed',open:'Open',draft:'Draft',blocked:'Conflict',na:'N/A'};
const STATUS_CLS={merged:'badge-merged',closed:'badge-na',open:'badge-open',draft:'badge-draft',blocked:'badge-blocked',na:'badge-na'};
const STATUS_TOOLTIP={merged:'PR merged',closed:'Closed without merge',open:'Open — pending review',draft:'Draft — not ready',blocked:'Merge conflict',na:'Internal task'};
let DATA=[],MONTHS=[],REPOS=[],REPO_COLORS={};
let fRepos=[],fMonths=[],fWeeks=[],showPRTable=true,prSearch='';
let fDateFrom='',fDateTo='';
const MS_DEFAULTS={repo:'All repositories',month:'All months',week:'All weeks'};
const MS_PLURAL={repo:'repositories',month:'months',week:'weeks'};
function shortRepo(repo){return repo.includes('/')?repo.split('/').pop():repo;}
function monthLabel(iso){const d=new Date(iso);return`${MONTH_NAMES[d.getUTCMonth()]}/${String(d.getUTCFullYear()).slice(-2)}`;}
function weekOf(iso){return Math.ceil(new Date(iso).getUTCDate()/7);}
function durationDays(from,to){if(!from||!to)return null;return Math.max(0,Math.round((new Date(to)-new Date(from))/86400000));}
function transformState(state){
  const nullDate=d=>!d||d.startsWith('0001');
  const entries=[];
  for(const pr of(state.entries||[])){
    const dateIso=!nullDate(pr.mergedAt)?pr.mergedAt:!nullDate(pr.closedAt)?pr.closedAt:pr.createdAt;
    if(!dateIso)continue;
    entries.push({date:dateIso.slice(0,10),month:monthLabel(dateIso),week:weekOf(dateIso),repos:[shortRepo(pr.repo)],pr:`#${pr.number}`,url:pr.url||null,title:pr.title||'',body:pr.body||'',author:pr.author||'',status:pr.status,summary:'',meta:[],durationDays:durationDays(pr.createdAt,!nullDate(pr.mergedAt)?pr.mergedAt:null),fullRepo:pr.repo});
  }
  return entries.sort((a,b)=>a.date.localeCompare(b.date));
}
async function loadData(){
  try{
    const r=await fetch('/api/stats/logs');
    if(!r.ok)throw new Error('HTTP '+r.status);
    const state=await r.json();
    DATA=transformState(state);
    const fc=state._meta?.files??0;
    const tt=state._meta?.readAt?new Date(state._meta.readAt).toLocaleTimeString('en-US',{hour:'2-digit',minute:'2-digit',second:'2-digit',hour12:true}):'';
    const sb=document.getElementById('status-bar');
    if(sb){sb.textContent=`${fc} file${fc!==1?'s':''} · ${tt}`;sb.style.color=fc>0?'#a6e3a1':'#f38ba8';}
    syncFilterOptions();
    if(DATA.length===0){document.getElementById('empty-state').style.display='';document.getElementById('main-content').style.display='none';}
    else{document.getElementById('empty-state').style.display='none';document.getElementById('main-content').style.display='';st_render();}
  }catch(err){const sb=document.getElementById('status-bar');if(sb){sb.textContent='Error: '+err.message;sb.style.color='#f38ba8';}}
}
function syncFilterOptions(){
  REPOS=[...new Set(DATA.flatMap(d=>d.repos))].sort();
  REPO_COLORS={};
  REPOS.forEach((r,i)=>REPO_COLORS[r]=PALETTE[i%PALETTE.length]);
  MONTHS=[...new Set(DATA.map(d=>d.month))].sort((a,b)=>{
    const p=m=>{const[mn,yr]=m.split('/');return parseInt('20'+yr)*100+(MONTH_ORDER[mn]||0);};
    return p(a)-p(b);
  });
  fRepos=fRepos.filter(r=>REPOS.includes(r));
  fMonths=fMonths.filter(m=>MONTHS.includes(m));
  document.getElementById('ms-panel-repo').innerHTML=REPOS.map(r=>'<div class="ms-item'+(fRepos.includes(r)?' sel':'')+'" data-val="'+r+'" onclick="toggleMSItem(\'repo\',\''+r+'\');event.stopPropagation()"><span class="ms-check"></span>'+r+'</div>').join('');
  document.getElementById('ms-panel-month').innerHTML=[...MONTHS].reverse().map(m=>'<div class="ms-item'+(fMonths.includes(m)?' sel':'')+'" data-val="'+m+'" onclick="toggleMSItem(\'month\',\''+m+'\');event.stopPropagation()"><span class="ms-check"></span>'+m+'</div>').join('');
  updateMSBtn('repo');updateMSBtn('month');syncWeekOptions();
}
function getFiltered(){
  return DATA.filter(d=>{
    if(fRepos.length&&!d.repos.some(r=>fRepos.includes(r)))return false;
    if(fMonths.length&&!fMonths.includes(d.month))return false;
    if(fWeeks.length&&!fWeeks.includes(d.week))return false;
    if(fDateFrom&&d.date<fDateFrom)return false;
    if(fDateTo&&d.date>fDateTo)return false;
    return true;
  });
}
function onFilterChange(){
  fDateFrom=document.getElementById('f-date-from').value;
  fDateTo=document.getElementById('f-date-to').value;
  showPRTable=document.getElementById('f-show-prs').checked;
  prSort={col:null,dir:'asc'};prTF={status:'all',repo:'all'};prPage=0;
  st_render();
}
function resetFilters(){
  fRepos=[];fMonths=[];fWeeks=[];fDateFrom='';fDateTo='';
  document.getElementById('f-date-from').value='';document.getElementById('f-date-to').value='';
  prSort={col:null,dir:'asc'};prTF={status:'all',repo:'all'};prPage=0;
  syncFilterOptions();st_render();
}
function syncWeekOptions(){
  const wrap=document.getElementById('ms-week');
  const group=document.getElementById('fg-week');
  if(fMonths.length!==1){wrap.className='ms-wrap ms-disabled';document.getElementById('ms-panel-week').innerHTML='';fWeeks=[];updateMSBtn('week');if(group)group.style.display='none';return;}
  wrap.className='ms-wrap';if(group)group.style.display='';
  const weeks=[...new Set(DATA.filter(d=>d.month===fMonths[0]).map(d=>d.week))].sort((a,b)=>a-b);
  fWeeks=fWeeks.filter(w=>weeks.includes(w));
  document.getElementById('ms-panel-week').innerHTML=weeks.map(w=>{const d1=(w-1)*7+1,d2=Math.min(w*7,31);return'<div class="ms-item'+(fWeeks.includes(w)?' sel':'')+'" data-val="'+w+'" onclick="toggleMSItem(\'week\','+w+');event.stopPropagation()"><span class="ms-check"></span>Week '+w+' ('+d1+'–'+d2+')</div>';}).join('');
  updateMSBtn('week');
}
function updateStats(){
  const f=getFiltered(),merged=f.filter(d=>d.status==='merged').length,repos=new Set(f.flatMap(d=>d.repos)).size;
  const withDur=f.filter(d=>d.status==='merged'&&d.durationDays!=null);
  const cycle=withDur.length?Math.round(withDur.reduce((s,d)=>s+d.durationDays,0)/withDur.length):null;
  document.getElementById('s-entries').textContent=f.length;
  document.getElementById('s-merged').textContent=merged;
  document.getElementById('s-open').textContent=cycle!=null?cycle+'d':'—';
  document.getElementById('s-repos').textContent=repos;
}
function renderReposMes(f,months){
  const activeRepos=REPOS.filter(r=>f.some(d=>d.repos.includes(r)));
  if(months.length<=1){
    ch.reposMes.data.labels=activeRepos;
    ch.reposMes.data.datasets=[{data:activeRepos.map(r=>f.filter(d=>d.repos.includes(r)).length),backgroundColor:activeRepos.map(r=>REPO_COLORS[r]??C.gray),borderWidth:0,borderRadius:4}];
    ch.reposMes.options.scales.x.stacked=false;ch.reposMes.options.scales.y.stacked=false;ch.reposMes.options.scales.y.max=undefined;
    ch.reposMes.options.scales.y.ticks={color:Chart.defaults.color,stepSize:1,callback:v=>Number.isInteger(v)?(v%2===0?v:''):null};
    ch.reposMes.options.plugins.legend.display=false;
  }else{
    ch.reposMes.data.labels=months;
    ch.reposMes.data.datasets=activeRepos.map(r=>({label:r,data:months.map(m=>f.filter(d=>d.month===m&&d.repos.includes(r)).length),backgroundColor:REPO_COLORS[r]??C.gray,borderWidth:0,borderRadius:2}));
    ch.reposMes.options.scales.x.stacked=false;ch.reposMes.options.scales.y.stacked=false;ch.reposMes.options.scales.y.max=undefined;
    ch.reposMes.options.scales.y.ticks={color:Chart.defaults.color,stepSize:1,callback:v=>Number.isInteger(v)?(v%2===0?v:''):null};
    ch.reposMes.options.plugins.legend.display=true;
  }
  ch.reposMes.update();
}
const ch={};
const barValuePlugin={id:'barValues',afterDatasetsDraw(chart){const{ctx,data}=chart;const dark=document.documentElement.classList.contains('dark');chart.getDatasetMeta(0).data.forEach((bar,i)=>{const value=data.datasets[0].data[i];if(!value)return;ctx.save();ctx.fillStyle=dark?'#cdd6f4':'#1e293b';ctx.font='bold 11px sans-serif';ctx.textAlign='center';ctx.textBaseline='bottom';ctx.fillText(value,bar.x,bar.y-4);ctx.restore();})}};
function OPT(extra={}){const t=Chart.defaults.color,g=Chart.defaults.borderColor;return{responsive:true,maintainAspectRatio:false,plugins:{legend:{display:false}},scales:{x:{grid:{color:g},ticks:{color:t}},y:{grid:{color:g},ticks:{color:t}}},...extra};}
function buildCharts(){
  const t=Chart.defaults.color,g=Chart.defaults.borderColor;
  const leg=document.documentElement.classList.contains('dark')?'#a6adc8':'#475569';
  const barTxt=document.documentElement.classList.contains('dark')?'#cdd6f4':'#1e293b';
  ch.cadencia=new Chart(document.getElementById('cadencia'),{type:'bar',data:{labels:[],datasets:[{data:[],backgroundColor:[],borderWidth:0,borderRadius:4}]},options:OPT(),plugins:[barValuePlugin]});
  const statusPlugin={id:'pct',afterDatasetsDraw(chart){const{ctx,data}=chart;const total=data.datasets[0].data.reduce((a,b)=>a+b,0);if(!total)return;chart.getDatasetMeta(0).data.forEach((arc,i)=>{const val=data.datasets[0].data[i];const pct=Math.round(val/total*100);if(pct<6)return;const mid=(arc.startAngle+arc.endAngle)/2;const r=(arc.innerRadius+arc.outerRadius)/2;ctx.save();ctx.fillStyle='#1e1e2e';ctx.font='bold 11px system-ui,sans-serif';ctx.textAlign='center';ctx.textBaseline='middle';ctx.fillText(pct+'%',arc.x+Math.cos(mid)*r,arc.y+Math.sin(mid)*r);ctx.restore();})}};
  ch.status=new Chart(document.getElementById('status'),{plugins:[statusPlugin],type:'doughnut',data:{labels:[],datasets:[{data:[],backgroundColor:[],borderWidth:0,hoverOffset:8}]},options:{responsive:true,maintainAspectRatio:false,cutout:'62%',plugins:{legend:{display:true,position:'right',labels:{boxWidth:10,padding:14,color:leg,font:{size:11}}}}}});
  ch.repos=new Chart(document.getElementById('repos'),{type:'bar',data:{labels:[],datasets:[{data:[],backgroundColor:[],borderRadius:4,borderWidth:0}]},options:{...OPT(),indexAxis:'y',scales:{...OPT().scales,x:{...OPT().scales.x,ticks:{color:t,stepSize:1,callback:v=>Number.isInteger(v)?(v%2===0?v:''):null}}}},plugins:[{id:'reposBarValues',afterDatasetsDraw(chart){const{ctx,data}=chart;chart.getDatasetMeta(0).data.forEach((bar,i)=>{const value=data.datasets[0].data[i];if(!value)return;ctx.save();ctx.fillStyle=barTxt;ctx.font='bold 11px sans-serif';ctx.textAlign='left';ctx.textBaseline='middle';ctx.fillText(value,bar.x+4,bar.y);ctx.restore();})}}]});
  ch.timeline=new Chart(document.getElementById('timeline'),{type:'bar',data:{labels:[],datasets:[]},options:{responsive:true,maintainAspectRatio:false,plugins:{legend:{display:true,position:'bottom',labels:{boxWidth:10,padding:12,color:leg,font:{size:11}}}},scales:{x:{stacked:true,grid:{color:g},ticks:{color:t}},y:{stacked:true,grid:{color:g},ticks:{color:t,stepSize:1}}}}});
  ch.reposMes=new Chart(document.getElementById('reposMes'),{type:'bar',data:{labels:[],datasets:[]},options:{responsive:true,maintainAspectRatio:false,plugins:{legend:{display:true,position:'bottom',labels:{boxWidth:10,padding:16,color:leg,font:{size:11}}}},scales:{x:{stacked:true,grid:{color:g},ticks:{color:t}},y:{stacked:true,grid:{color:g},ticks:{color:t,stepSize:1,callback:v=>Number.isInteger(v)?v:''},grace:1}}},plugins:[{id:'groupedBarValues',afterDatasetsDraw(chart){const{ctx}=chart;chart.data.datasets.forEach((dataset,i)=>{chart.getDatasetMeta(i).data.forEach((bar,j)=>{const value=dataset.data[j];if(!value)return;ctx.save();ctx.fillStyle=barTxt;ctx.font='bold 10px sans-serif';ctx.textAlign='center';ctx.textBaseline='bottom';ctx.fillText(value,bar.x,bar.y-3);ctx.restore();})})}}]});
}
function updateCharts(){
  const f=getFiltered(),singleRepo=fRepos.length===1;
  document.getElementById('card-repos').classList.toggle('hidden',singleRepo);
  document.getElementById('card-reposMes').classList.toggle('hidden',singleRepo);
  document.getElementById('card-timeline').classList.toggle('hidden',!singleRepo);
  document.getElementById('charts-grid').style.gridTemplateColumns='';
  const months=fMonths.length?MONTHS.filter(m=>fMonths.includes(m)):MONTHS.filter(m=>f.some(d=>d.month===m));
  if(singleRepo){const activeStatuses=Object.keys(STATUS_COLORS).filter(s=>f.some(d=>d.status===s));ch.timeline.data.labels=months;ch.timeline.data.datasets=activeStatuses.map(s=>({label:STATUS_LBL[s],data:months.map(m=>f.filter(d=>d.month===m&&d.status===s).length),backgroundColor:STATUS_COLORS[s],borderWidth:0,borderRadius:2})).filter(ds=>ds.data.some(v=>v>0));ch.timeline.update();}
  ch.cadencia.data.labels=months;ch.cadencia.data.datasets[0].data=months.map(m=>f.filter(d=>d.month===m).length);ch.cadencia.data.datasets[0].backgroundColor=months.map((_,i)=>MONTH_PALETTE[i%MONTH_PALETTE.length]);ch.cadencia.update();
  const activeStatuses=Object.keys(STATUS_COLORS).filter(s=>f.some(d=>d.status===s));ch.status.data.labels=activeStatuses.map(s=>STATUS_LBL[s]);ch.status.data.datasets[0].data=activeStatuses.map(s=>f.filter(d=>d.status===s).length);ch.status.data.datasets[0].backgroundColor=activeStatuses.map(s=>STATUS_COLORS[s]);ch.status.update();
  if(!singleRepo){const withData=REPOS.map(r=>({r,c:f.filter(d=>d.repos.includes(r)).length})).filter(x=>x.c>0).sort((a,b)=>b.c-a.c);ch.repos.data.labels=withData.map(x=>x.r);ch.repos.data.datasets[0].data=withData.map(x=>x.c);ch.repos.data.datasets[0].backgroundColor=withData.map(x=>REPO_COLORS[x.r]??C.gray);ch.repos.options.scales.x.suggestedMax=(Math.max(0,...withData.map(x=>x.c)))+1;ch.repos.update();renderReposMes(f,months);}
}
let prSort={col:null,dir:'asc'},prTF={status:'all',repo:'all'};
const PR_PAGE_SIZE=20;
let prPage=0;
function goToPRPage(p){prPage=p;updatePRSection();}
function setPRSort(col){if(prSort.col===col)prSort.dir=prSort.dir==='asc'?'desc':'asc';else{prSort.col=col;prSort.dir='asc';}prPage=0;updatePRSection();}
function setPRFilter(key,val){prTF[key]=val;prPage=0;updatePRSection();}
function resetPRFilters(){prSort={col:null,dir:'asc'};prTF={status:'all',repo:'all'};prPage=0;updatePRSection();}
function onPRSearch(){prSearch=document.getElementById('pr-search').value;prPage=0;updatePRSection();}
function st_syncI18n(){
  MS_DEFAULTS.repo=t('stAllRepos');MS_DEFAULTS.month=t('stAllMonths');MS_DEFAULTS.week=t('stAllWeeks');
  MS_PLURAL.repo=t('stReposPl');MS_PLURAL.month=t('stMonthsPl');MS_PLURAL.week=t('stWeeksPl');
}
function st_applyChartTheme(light){
  Chart.defaults.color=light?'#64748b':'#6c7086';
  Chart.defaults.borderColor=light?'#e2e8f0':'#313244';
  Object.values(ch).forEach(c=>{try{c.destroy();}catch(e){}});
  Object.keys(ch).forEach(k=>delete ch[k]);
  buildCharts();st_render();
}
function togglePRExpand(id){const row=document.getElementById(id);const hint=document.querySelector('[data-expand="'+id+'"]');if(!row)return;const open=row.style.display!=='none';row.style.display=open?'none':'';if(hint)hint.classList.toggle('open',!open);}
function st_renderMd(md){
  if(!md)return'';
  const esc=s=>s.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;');
  function inline(s){s=s.replace(/\*\*(.+?)\*\*/g,'<strong style="color:#cdd6f4">$1</strong>');s=s.replace(/\*([^*\n]+)\*/g,'<em>$1</em>');s=s.replace(/`([^`\n]+)`/g,'<code style="background:#313244;padding:1px 4px;border-radius:3px;font-family:monospace;font-size:0.71rem;color:#f38ba8">$1</code>');return s;}
  const lines=md.split('\n');let out='',inCode=false,codeBuf='';
  for(const line of lines){if(line.startsWith('```')){if(inCode){out+='<pre style="background:#1e1e2e;border:1px solid #313244;padding:6px 10px;border-radius:4px;font-size:0.71rem;white-space:pre-wrap;margin:4px 0;color:#cdd6f4;overflow-wrap:break-word">'+codeBuf+'</pre>';codeBuf='';inCode=false;}else{inCode=true;}continue;}if(inCode){codeBuf+=esc(line)+'\n';continue;}if(!line.trim()){out+='<div style="height:0.35rem"></div>';continue;}let l=esc(line);const hm=l.match(/^(#{1,6}) /);if(hm){const lvl=hm[1].length;const sz=['0.85rem','0.8rem','0.77rem','0.75rem','0.75rem','0.75rem'][lvl-1];const cl=['#cba6f7','#cba6f7','#89b4fa','#89b4fa','#a6adc8','#a6adc8'][lvl-1];const mg=lvl<=2?'10px':'6px';out+=`<div style="font-weight:700;color:${cl};font-size:${sz};margin:${mg} 0 3px">${inline(l.replace(/^#+\s/,''))}</div>`;continue;}if(l.startsWith('- [ ] ')||l.startsWith('* [ ] ')){out+=`<div style="display:flex;align-items:center;gap:6px;margin:2px 0;color:#a6adc8"><input type="checkbox" disabled> ${inline(l.slice(6))}</div>`;continue;}if(/^[-*] \[[xX]\] /.test(l)){out+=`<div style="display:flex;align-items:center;gap:6px;margin:2px 0;color:#585b70"><input type="checkbox" checked disabled> <span style="text-decoration:line-through">${inline(l.slice(6))}</span></div>`;continue;}if(l.startsWith('- ')||l.startsWith('* ')){out+=`<div style="padding-left:14px;margin:2px 0;color:#a6adc8">• ${inline(l.slice(2))}</div>`;continue;}out+=`<div style="margin:2px 0;color:#a6adc8">${inline(l)}</div>`;}
  return out;
}
function updatePRSection(){
  const section=document.getElementById('pr-section');
  const shouldShow=fRepos.length>0||fMonths.length>0||showPRTable;
  if(!shouldShow){section.style.display='none';return;}
  section.style.display='';
  const all=getFiltered().sort((a,b)=>a.date.localeCompare(b.date));
  const repoLabel=fRepos.length===1?fRepos[0]:fRepos.length>1?fRepos.join(', '):t('stAllReposLabel');
  const suffix=[fMonths.length===1&&fMonths[0],fMonths.length>1&&(fMonths.length+' months'),fWeeks.length>0&&('Week '+fWeeks.join('+'))].filter(Boolean).join(' · ');
  document.getElementById('pr-section-title').textContent='PRs — '+repoLabel+(suffix?' · '+suffix:'');
  const hasActiveFilter=prSort.col!==null||prTF.status!=='all'||prTF.repo!=='all';
  document.getElementById('pr-clear-btn').style.display=hasActiveFilter?'':'none';
  const statusOpts=[...new Set(all.map(d=>d.status))];
  const repoOpts=[...new Set(all.flatMap(d=>d.repos))].filter(r=>!fRepos.includes(r));
  function arrow(key){if(prSort.col!==key)return'<span class="sort-icon">↕</span>';return`<span class="sort-icon active">${prSort.dir==='asc'?'↑':'↓'}</span>`;}
  function fsel(key,opts,labelFn){const v=prTF[key];const ops=opts.map(o=>`<option value="${o}"${v==o?' selected':''}>${labelFn(o)}</option>`).join('');return`<select class="pr-filter-sel" onchange="setPRFilter('${key}',this.value)" onclick="event.stopPropagation()"><option value="all">${t('stAllFilter')}</option>${ops}</select>`;}
  const COLS=[{key:'pr',label:t('stColPR'),filter:null},{key:'repo',label:'Repo',filter:repoOpts.length?()=>fsel('repo',repoOpts,r=>r):null},{key:'date',label:t('stColDate'),filter:null},{key:'status',label:'Status',filter:()=>fsel('status',statusOpts,s=>STATUS_LBL[s])},{key:'title',label:t('stColTitle'),filter:null}];
  document.getElementById('pr-thead').innerHTML='<tr>'+COLS.map(c=>`<th class="sortable" onclick="setPRSort('${c.key}')">${c.label}${arrow(c.key)}</th>`).join('')+'</tr>'+'<tr class="filter-row">'+COLS.map(c=>`<th>${c.filter?c.filter():''}</th>`).join('')+'</tr>';
  let data=all.filter(d=>{if(prTF.status!=='all'&&d.status!==prTF.status)return false;if(prTF.repo!=='all'&&!d.repos.includes(prTF.repo))return false;return true;});
  if(prSort.col){data=[...data].sort((a,b)=>{let va,vb;switch(prSort.col){case'pr':va=a.pr;vb=b.pr;break;case'repo':va=a.repos[0]??'';vb=b.repos[0]??'';break;case'date':va=a.date;vb=b.date;break;case'status':va=a.status;vb=b.status;break;case'title':va=a.title.toLowerCase();vb=b.title.toLowerCase();break;default:return 0;}const cmp=va<vb?-1:va>vb?1:0;return prSort.dir==='asc'?cmp:-cmp;});}
  const srch=prSearch.trim().toLowerCase();if(srch)data=data.filter(d=>d.title.toLowerCase().includes(srch));
  const countEl=document.getElementById('pr-count');
  countEl.textContent=data.length<all.length?t('stShowingOf',data.length,all.length):t('stEntries',all.length);
  const tbody=document.getElementById('pr-tbody');
  if(data.length===0){tbody.innerHTML=`<tr><td colspan="5" style="text-align:center;color:#585b70;padding:2rem 0">${t('stNoEntries')}</td></tr>`;document.getElementById('pr-pagination').innerHTML='';return;}
  const totalPages=Math.ceil(data.length/PR_PAGE_SIZE);
  if(prPage>=totalPages)prPage=Math.max(0,totalPages-1);
  const pageData=data.slice(prPage*PR_PAGE_SIZE,(prPage+1)*PR_PAGE_SIZE);
  const openEids=new Set([...document.querySelectorAll('.pr-expand-row')].filter(r=>r.style.display!=='none').map(r=>r.id));
  tbody.innerHTML=pageData.map((d,i)=>{
    const gi=prPage*PR_PAGE_SIZE+i;
    const prCell=d.url?`<a href="${d.url}" target="_blank" class="pr-ref" onclick="event.stopPropagation()">${d.pr}</a>`:`<span class="pr-ref pr-ref-nolink" title="No link">${d.pr}</span>`;
    const[y,mm,dd]=d.date.split('-');const dateStr=`${mm}/${dd}/${y.slice(2)}`;const eid='pex-'+gi;const hasBody=!!d.body;
    const expandRow=hasBody?`<tr id="${eid}" class="pr-expand-row" style="display:none"><td colspan="5"><div class="pr-expand-body">${st_renderMd(d.body)}</div></td></tr>`:'';
    return`<tr class="${hasBody?'pr-has-body':''}" ${hasBody?`onclick="togglePRExpand('${eid}')"`:''}><td>${prCell}</td><td style="white-space:nowrap;color:#6c7086;font-size:0.72rem">${d.repos.join(' + ')}</td><td style="white-space:nowrap;color:#6c7086;font-size:0.72rem">${dateStr}</td><td><span class="badge ${STATUS_CLS[d.status]||'badge-na'}" title="${STATUS_TOOLTIP[d.status]||''}">${STATUS_LBL[d.status]||d.status}</span></td><td><div class="pr-title">${d.title}${hasBody?`<span class="pr-expand-hint" data-expand="${eid}">▾</span>`:''}</div>${d.summary?`<div class="pr-summary">${d.summary}</div>`:''}</td></tr>${expandRow}`;
  }).join('');
  openEids.forEach(id=>{const row=document.getElementById(id);if(row){row.style.display='';const hint=document.querySelector('[data-expand="'+id+'"]');if(hint)hint.classList.add('open');}});
  const pag=document.getElementById('pr-pagination');
  if(totalPages<=1){pag.innerHTML='';return;}
  const s=prPage*PR_PAGE_SIZE+1,e=Math.min((prPage+1)*PR_PAGE_SIZE,data.length);
  let ph=`<span class="pr-page-info">${t('stShowingOf',s+'-'+e,data.length)}</span>`;
  ph+=`<button class="pr-page-btn" onclick="goToPRPage(${prPage-1})" ${prPage===0?'disabled':''}>‹</button>`;
  for(let i=0;i<totalPages;i++){
    if(totalPages<=7||i===0||i===totalPages-1||Math.abs(i-prPage)<=1){ph+=`<button class="pr-page-btn${i===prPage?' active':''}" onclick="goToPRPage(${i})">${i+1}</button>`;}
    else if(Math.abs(i-prPage)===2){ph+=`<span class="pr-page-info">…</span>`;}
  }
  ph+=`<button class="pr-page-btn" onclick="goToPRPage(${prPage+1})" ${prPage===totalPages-1?'disabled':''}>›</button>`;
  pag.innerHTML=ph;
}
function st_render(){updateStats();updateCharts();updatePRSection();}
function captureChart(id){try{const src=document.getElementById(id);if(!src||!src.width)return null;const tmp=document.createElement('canvas');tmp.width=src.width;tmp.height=src.height;const ctx=tmp.getContext('2d');ctx.fillStyle='#ffffff';ctx.fillRect(0,0,tmp.width,tmp.height);ctx.drawImage(src,0,0);return tmp.toDataURL('image/png');}catch(err){return null;}}
function toggleMS(key,ev){ev.stopPropagation();const panel=document.getElementById('ms-panel-'+key);const isOpen=panel.style.display!=='none';document.querySelectorAll('.ms-panel').forEach(p=>p.style.display='none');document.getElementById('export-menu').style.display='none';if(!isOpen)panel.style.display='';}
function toggleMSItem(key,val){const arr=key==='repo'?fRepos:key==='month'?fMonths:fWeeks;const idx=arr.indexOf(val);if(idx===-1)arr.push(val);else arr.splice(idx,1);updateMSBtn(key);document.querySelectorAll('#ms-panel-'+key+' .ms-item').forEach(item=>{item.classList.toggle('sel',arr.includes(item.dataset.val)||arr.includes(Number(item.dataset.val)));});if(key!=='week'){fWeeks=[];syncWeekOptions();}prSort={col:null,dir:'asc'};prTF={status:'all',repo:'all'};prPage=0;st_render();}
function updateMSBtn(key){const arr=key==='repo'?fRepos:key==='month'?fMonths:fWeeks;const el=document.querySelector('#ms-'+key+' .ms-label');if(!el)return;el.textContent=arr.length===0?MS_DEFAULTS[key]:arr.length===1?String(arr[0]):arr.length+' '+MS_PLURAL[key];}
function toggleExportMenu(ev){ev.stopPropagation();const m=document.getElementById('export-menu');m.style.display=m.style.display==='none'?'':'none';}
async function saveDname(){const inp=document.getElementById('dname-input');if(!inp)return;const val=inp.value.trim();await saveSettings({displayName:val||null});if(val)ghLogin=val;closeUserMenu();}
document.addEventListener('click',()=>{document.querySelectorAll('.ms-panel').forEach(p=>p.style.display='none');const m=document.getElementById('export-menu');if(m)m.style.display='none';});
function buildReportData(){const f=getFiltered().sort((a,b)=>a.date.localeCompare(b.date));const filterDesc=[fRepos.length&&(fRepos.length===1?'Repo: '+fRepos[0]:fRepos.length+' repos'),fMonths.length&&(fMonths.length===1?fMonths[0]:fMonths.length+' months'),fWeeks.length&&('Week '+fWeeks.join('+')),((fDateFrom||fDateTo)&&((fDateFrom||'…')+' → '+(fDateTo||'…')))].filter(Boolean).join(' · ')||'All data';const byStatus={};for(const d of f)byStatus[d.status]=(byStatus[d.status]||0)+1;const repos=[...new Set(f.flatMap(d=>d.repos))].sort();const months=MONTHS.filter(m=>f.some(d=>d.month===m));const byMonth={};for(const m of months)byMonth[m]=f.filter(d=>d.month===m);return{f,filterDesc,byStatus,repos,byMonth,months};}
function monthFull(m){const[mn,yr]=m.split('/');const N={Jan:'January',Feb:'February',Mar:'March',Apr:'April',May:'May',Jun:'June',Jul:'July',Aug:'August',Sep:'September',Oct:'October',Nov:'November',Dec:'December'};return(N[mn]||mn)+' 20'+yr;}
function slugM(m){return'sec-'+m.replace('/','/-');}
function printBadge(status){const s={merged:'background:#dcfce7;color:#166534',open:'background:#dbeafe;color:#1d4ed8',closed:'background:#f3f4f6;color:#374151',draft:'background:#fef3c7;color:#92400e',blocked:'background:#ffedd5;color:#9a3412'}[status]||'background:#f3f4f6;color:#6b7280';return'<span style="'+s+';padding:1px 7px;border-radius:3px;font-size:8.5pt;font-weight:700;white-space:nowrap">'+(STATUS_LBL[status]||status)+'</span>';}
function generateReportHTML(){
  const{f,filterDesc,byStatus,repos,byMonth,months}=buildReportData();
  const _nd=new Date();const now=MONTH_NAMES[_nd.getMonth()]+' '+_nd.getDate()+', '+_nd.getFullYear();
  const period=months.length>=2?monthFull(months[0])+' – '+monthFull(months[months.length-1]):months.length===1?monthFull(months[0]):'All data';
  const coverSub=fRepos.length===1?fRepos[0]:fRepos.length>1?fRepos.join(', '):null;
  const totalMerged=byStatus.merged||0,totalOpen=byStatus.open||0,totalClosed=byStatus.closed||0;
  const authorCounts={};f.forEach(d=>{if(d.author)authorCounts[d.author]=(authorCounts[d.author]||0)+1;});
  const topAuthor=Object.keys(authorCounts).sort((a,b)=>authorCounts[b]-authorCounts[a])[0]||null;
  const peakMonth=months.length>1?months.reduce((a,b)=>byMonth[a].length>=byMonth[b].length?a:b):null;
  const narrative=f.length+' PR'+(f.length!==1?'s':'')+' delivered in '+period+' ('+totalMerged+' merged) across '+repos.length+' repositor'+(repos.length!==1?'ies':'y')+'.'+(peakMonth?' '+monthFull(peakMonth)+' had the highest volume with '+byMonth[peakMonth].length+' deliveries.':'');
  function TH(label,align){return'<th style="text-align:'+(align||'left')+';padding:5pt 7pt;font-size:7.5pt;color:#6b7280;font-weight:600;text-transform:uppercase;letter-spacing:.05em;background:#f8fafc;border-bottom:1.5px solid #e2e8f0">'+label+'</th>';}
  function escHtml(s){return(s||'').replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;');}
  function mdToHtml(md){if(!md)return'';const lines=md.split('\n');let out='',inCode=false,codeBuf='';for(const line of lines){if(line.startsWith('```')){if(inCode){out+='<pre style="background:#f1f5f9;padding:6pt 8pt;border-radius:3pt;font-family:monospace;font-size:7.5pt;white-space:pre-wrap;margin:4pt 0 6pt;overflow-wrap:break-word">'+codeBuf+'</pre>';codeBuf='';inCode=false;}else{inCode=true;}continue;}if(inCode){codeBuf+=escHtml(line)+'\n';continue;}if(!line.trim()){out+='<br>';continue;}let l=escHtml(line);if(l.startsWith('## ')){out+='<p style="font-weight:700;color:#1e3a5f;margin:7pt 0 2pt;font-size:9.5pt">'+l.slice(3)+'</p>';continue;}if(l.startsWith('### ')){out+='<p style="font-weight:600;color:#374151;margin:5pt 0 2pt;font-size:9pt">'+l.slice(4)+'</p>';continue;}if(l.startsWith('- ')||l.startsWith('* ')){l='<span style="display:block;padding-left:10pt;margin:1pt 0">• '+l.slice(2)+'</span>';}else{l='<span style="display:block;margin:1pt 0">'+l+'</span>';}l=l.replace(/\*\*(.+?)\*\*/g,'<strong>$1</strong>');l=l.replace(/`([^`\n]+)`/g,'<code style="background:#f1f5f9;padding:1pt 3pt;border-radius:2pt;font-family:monospace;font-size:8pt">$1</code>');out+=l;}return out;}
  const repoRows=repos.map(function(r){const rp=f.filter(d=>d.repos.includes(r));const rm=rp.filter(d=>d.status==='merged').length;const ro=rp.filter(d=>d.status==='open').length;return'<tr>'+'<td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;font-weight:500">'+r+'</td>'+'<td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right">'+rp.length+'</td>'+'<td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right;color:#16a34a;font-weight:600">'+rm+'</td>'+'<td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right;color:#1d4ed8">'+ro+'</td>'+'</tr>';}).join('');
  const PDF_PAL=[C.blue,C.peach,C.teal,C.mauve,C.green,C.yellow,C.red,C.sky,C.gray];
  function pdfCanvas(w,h,chartCfg){const tmp=document.createElement('canvas');tmp.width=w;tmp.height=h;const tc=new Chart(tmp,chartCfg);const bg=document.createElement('canvas');bg.width=w;bg.height=h;const ctx=bg.getContext('2d');ctx.fillStyle='#ffffff';ctx.fillRect(0,0,w,h);ctx.drawImage(tmp,0,0);tc.destroy();return bg.toDataURL('image/png');}
  const pdfBarValuePlugin={id:'pdfBarValues',afterDatasetsDraw(chart){const{ctx,data}=chart;chart.getDatasetMeta(0).data.forEach((bar,i)=>{const value=data.datasets[0].data[i];if(!value)return;ctx.save();ctx.fillStyle='#374151';ctx.font='bold 11px sans-serif';ctx.textAlign='center';ctx.textBaseline='bottom';ctx.fillText(value,bar.x,bar.y-4);ctx.restore();})}};
  const isCadencia=pdfCanvas(900,180,{type:'bar',data:{labels:months,datasets:[{data:months.map(m=>f.filter(d=>d.month===m).length),backgroundColor:months.map((_,i)=>PDF_PAL[i%PDF_PAL.length]),borderWidth:0,borderRadius:4}]},options:{responsive:false,animation:false,plugins:{legend:{display:false}},scales:{x:{grid:{color:'#e2e8f0'},ticks:{color:'#374151'}},y:{grid:{color:'#e2e8f0'},ticks:{color:'#374151',stepSize:1}}}},plugins:[pdfBarValuePlugin]});
  const isStatus=captureChart('status'),isSingle=fRepos.length===1,isTimeline=captureChart('timeline'),isRepos=captureChart('repos');
  const activeRepos=repos.filter(r=>f.some(d=>d.repos.includes(r)));
  const groupedBarImg=(function(){if(isSingle||!activeRepos.length)return null;const singleMonth=months.length===1;const datasets=singleMonth?[{label:months[0],data:activeRepos.map(r=>f.filter(d=>d.repos.includes(r)).length),backgroundColor:activeRepos.map(r=>REPO_COLORS[r]??C.gray),borderWidth:0,borderRadius:3}]:months.map(function(m,i){return{label:m,data:activeRepos.map(r=>f.filter(d=>d.month===m&&d.repos.includes(r)).length),backgroundColor:PDF_PAL[i%PDF_PAL.length],borderWidth:0,borderRadius:3};});return pdfCanvas(900,Math.max(180,activeRepos.length*30+70),{type:'bar',data:{labels:activeRepos,datasets},options:{indexAxis:'y',responsive:false,animation:false,plugins:{legend:{display:!singleMonth,position:'top',labels:{boxWidth:10,padding:12,color:'#374151',font:{size:11}}}},scales:{x:{grid:{color:'#e2e8f0'},ticks:{color:'#6b7280',stepSize:1}},y:{grid:{color:'#e2e8f0'},ticks:{color:'#374151',font:{size:10}}}}}});})();
  function chartBox(title,dataUrl,mb){if(!dataUrl)return'';return'<div style="margin-bottom:'+(mb||'.55cm')+'"><p style="font-size:7.5pt;font-weight:600;text-transform:uppercase;letter-spacing:.07em;color:#6b7280;margin-bottom:4pt">'+title+'</p><div style="background:#f8fafc;border-radius:5px;padding:8px;border:1px solid #e2e8f0"><img src="'+dataUrl+'" style="width:100%;max-height:130px;object-fit:contain;display:block"></div></div>';}
  const chartsSection='<section id="charts-section" style="page-break-before:always"><h2 style="font-size:15pt;color:#1e3a5f;border-bottom:2px solid #1e3a5f;padding-bottom:5pt;margin:0 0 .7cm">Overview Charts</h2>'+chartBox('Delivery Cadence by Month',isCadencia)+chartBox(isSingle?'Timeline by Month':'Deliveries by Repository',isSingle?isTimeline:isRepos)+(!isSingle?chartBox('Deliveries by Repository by Month',groupedBarImg):'')+chartBox('PR Status',isStatus,'0')+'</section>';
  const TD_NUM='padding:4pt 8pt;border-bottom:1px solid #f1f5f9;text-align:right;font-size:9pt;';
  const deliveriesSection='<section id="deliveries" style="page-break-before:always"><h2 style="font-size:15pt;color:#1e3a5f;border-bottom:2px solid #1e3a5f;padding-bottom:5pt;margin:0 0 4pt">Deliveries by Repository</h2><p style="color:#6b7280;font-size:9.5pt;margin:0 0 14pt">PRs per month per repository</p><table style="width:100%;border-collapse:collapse;table-layout:fixed"><thead><tr><th style="text-align:left;padding:5pt 8pt;font-size:8pt;color:#6b7280;font-weight:600;text-transform:uppercase;letter-spacing:.05em;background:#f8fafc;border-bottom:1.5px solid #e2e8f0">Repository</th>'+months.map(m=>'<th style="text-align:right;padding:5pt 8pt;font-size:8pt;color:#6b7280;font-weight:600;text-transform:uppercase;letter-spacing:.05em;background:#f8fafc;border-bottom:1.5px solid #e2e8f0;width:60pt">'+m+'</th>').join('')+'<th style="text-align:right;padding:5pt 8pt;font-size:8pt;color:#6b7280;font-weight:600;text-transform:uppercase;letter-spacing:.05em;background:#f8fafc;border-bottom:1.5px solid #e2e8f0;width:46pt">Total</th></tr></thead><tbody>'+repos.map(function(r){const rowTotal=f.filter(d=>d.repos.includes(r)).length;return'<tr><td style="padding:4pt 8pt;border-bottom:1px solid #f1f5f9;font-weight:500;font-size:9.5pt;overflow:hidden;text-overflow:ellipsis;white-space:nowrap">'+r+'</td>'+months.map(function(m){const v=f.filter(d=>d.month===m&&d.repos.includes(r)).length;return'<td style="'+TD_NUM+(v===0?'color:#cbd5e1':'color:#374151')+'">'+(v||'—')+'</td>';}).join('')+'<td style="'+TD_NUM+'font-weight:700;color:#1e3a5f">'+rowTotal+'</td></tr>';}).join('')+'<tr style="background:#f8fafc"><td style="padding:5pt 8pt;font-size:8.5pt;font-weight:700;color:#6b7280;text-transform:uppercase;letter-spacing:.04em">Total</td>'+months.map(function(m){const v=f.filter(d=>d.month===m).length;return'<td style="padding:5pt 8pt;text-align:right;font-weight:700;color:#1e3a5f;font-size:9pt">'+v+'</td>';}).join('')+'<td style="padding:5pt 8pt;text-align:right;font-weight:700;color:#1e3a5f;font-size:9pt">'+f.length+'</td></tr></tbody></table></section>';
  function descId(d){return'desc-'+d.fullRepo.replace(/[^a-z0-9]/gi,'-')+'-'+d.pr.slice(1);}
  function fmtDate(isoDate){const[y,mm,dd]=isoDate.split('-');return MONTH_NAMES[parseInt(mm)-1]+' '+parseInt(dd)+', '+y;}
  const sections=months.map(function(m){const prs=byMonth[m];const mc=prs.filter(d=>d.status==='merged').length;const oc=prs.filter(d=>d.status==='open').length;const summary=[mc&&(mc+' merged'),oc&&(oc+' open')].filter(Boolean).join(' · ');const rows=prs.map(function(d){const pr=d.url?'<a href="'+d.url+'" style="color:#1e40af;font-family:monospace;font-size:9pt;font-weight:600">'+d.pr+'</a>':'<span style="font-family:monospace;font-size:9pt;color:#6b7280">'+d.pr+'</span>';const descLink=d.body?'<a href="#'+descId(d)+'" style="display:inline-block;background:#eff6ff;color:#1e40af;border:1px solid #bfdbfe;padding:2pt 7pt;border-radius:99pt;font-size:7.5pt;font-weight:600;text-decoration:none;white-space:nowrap;letter-spacing:.02em">↓ Full description</a>':'';return'<tr><td style="padding:4pt 7pt;border-bottom:1px solid #f1f5f9;font-size:9pt;color:#374151;white-space:nowrap">'+d.repos.join('+')+'</td><td style="padding:4pt 7pt;border-bottom:1px solid #f1f5f9;white-space:nowrap">'+pr+'</td><td style="padding:4pt 7pt;border-bottom:1px solid #f1f5f9;font-size:9.5pt">'+d.title+'</td><td style="padding:4pt 7pt;border-bottom:1px solid #f1f5f9;white-space:nowrap">'+printBadge(d.status)+'</td><td style="padding:4pt 7pt;border-bottom:1px solid #f1f5f9;text-align:center;white-space:nowrap">'+descLink+'</td></tr>';}).join('');return'<section id="'+slugM(m)+'" style="page-break-before:always"><h2 style="font-size:15pt;color:#1e3a5f;border-bottom:2px solid #1e3a5f;padding-bottom:5pt;margin:0 0 4pt">'+monthFull(m)+'</h2>'+(summary?'<p style="color:#6b7280;font-size:9.5pt;margin:0 0 12pt">'+summary+' · '+prs.length+' total</p>':'')+'<table style="width:100%;border-collapse:collapse"><thead><tr>'+TH('Repo')+TH('PR')+TH('Title')+TH('Status')+TH('Description','center')+'</tr></thead><tbody>'+rows+'</tbody></table></section>';}).join('');
  const descriptionsSection=(function(){const hasBodies=months.some(m=>byMonth[m].some(d=>d.body));if(!hasBodies)return'';return'<section id="pr-descriptions" style="page-break-before:always"><h2 style="font-size:15pt;color:#1e3a5f;border-bottom:2px solid #1e3a5f;padding-bottom:5pt;margin:0 0 1cm">PR Descriptions</h2>'+months.map(function(m){const prs=byMonth[m].filter(d=>d.body);if(!prs.length)return'';return'<div style="margin-bottom:1.2cm"><h3 style="font-size:11pt;color:#374151;border-bottom:1px solid #e2e8f0;padding-bottom:4pt;margin:0 0 .7cm">'+monthFull(m)+'</h3>'+prs.map(function(d){const date=fmtDate(d.date);const prRef=d.url?'<a href="'+d.url+'" style="color:#1e40af;font-family:monospace;font-size:8.5pt;font-weight:600;text-decoration:none">'+d.pr+'</a>':'<span style="font-family:monospace;font-size:8.5pt;color:#6b7280">'+d.pr+'</span>';return'<div id="'+descId(d)+'" style="margin-bottom:.9cm;padding:.6cm;background:#fafafa;border-left:3px solid #1e3a5f;border-radius:0 4pt 4pt 0"><div style="display:flex;align-items:baseline;gap:.5cm;margin-bottom:.4cm;flex-wrap:wrap">'+prRef+'<span style="font-size:10pt;font-weight:600;color:#1e293b;flex:1">'+escHtml(d.title)+'</span><span style="font-size:8pt;color:#9ca3af;white-space:nowrap">'+escHtml(d.repos.join('+'))+' · '+date+'</span></div><div style="font-size:8.5pt;color:#374151;line-height:1.65">'+mdToHtml(d.body)+'</div></div>';}).join('')+'</div>';}).join('')+'</section>';})();
  return'<!DOCTYPE html><html lang="en"><head><meta charset="UTF-8"><title>Evolution Report — Hyland</title><style>@page{size:A4;margin:2cm 2.2cm}@page :first{margin:0}*{box-sizing:border-box;margin:0;padding:0}body{font-family:"Segoe UI",-apple-system,BlinkMacSystemFont,sans-serif;font-size:11pt;color:#1e293b;line-height:1.55}@media print{a[href^="http"]::after{content:none!important}.pdf-cover{-webkit-print-color-adjust:exact!important;print-color-adjust:exact!important}}</style></head><body><div class="pdf-cover" style="height:100vh;display:flex;flex-direction:column;justify-content:space-between;padding:3cm 2.5cm;background:linear-gradient(150deg,#1e3a5f 0%,#0f172a 100%);color:#fff;page-break-after:always"><div><h1 style="font-size:30pt;font-weight:700;line-height:1.2;margin-bottom:.4cm">Development<br>Activity Report</h1><p style="font-size:13pt;color:#cbd5e1">'+period+'</p>'+(coverSub?'<p style="font-size:11pt;color:#93c5fd;margin-top:.3cm">'+coverSub+'</p>':'')+'</div><div style="border-top:1px solid rgba(255,255,255,.18);padding-top:.55cm"><div style="display:flex;align-items:center;gap:.25cm;margin-bottom:.2cm"><svg height="14" width="14" viewBox="0 0 16 16" fill="#fff" style="flex-shrink:0"><path d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0016 8c0-4.42-3.58-8-8-8z"/></svg><span style="font-size:12pt;font-weight:700;color:#fff">'+escHtml(ghLogin||'')+'</span></div><div style="font-size:8pt;color:rgba(255,255,255,.6)">Generated '+now+(filterDesc!=='All data'?'&ensp;&middot;&ensp;'+filterDesc:'')+'</div></div></div><section id="summary" style="page-break-before:always"><h2 style="font-size:15pt;color:#1e3a5f;border-bottom:2px solid #1e3a5f;padding-bottom:5pt;margin-bottom:.6cm">Summary</h2><p style="font-size:10.5pt;color:#374151;line-height:1.65;margin-bottom:1cm;padding:.6cm .8cm;background:#f0f7ff;border-left:3px solid #1e3a5f;border-radius:0 4pt 4pt 0">'+narrative+'</p><table style="width:100%;border-collapse:collapse;margin-bottom:1.2cm"><thead><tr>'+TH('Metric')+TH('Value','right')+'</tr></thead><tbody><tr><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;font-weight:500">Total PRs</td><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right">'+f.length+'</td></tr><tr><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;font-weight:500">Merged</td><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right;color:#16a34a;font-weight:600">'+totalMerged+'</td></tr><tr><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;font-weight:500">Open</td><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right;color:#1d4ed8;font-weight:600">'+totalOpen+'</td></tr><tr><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;font-weight:500">Closed (no merge)</td><td style="padding:5pt 7pt;border-bottom:1px solid #f1f5f9;text-align:right">'+totalClosed+'</td></tr><tr><td style="padding:5pt 7pt;font-weight:500">Repositories</td><td style="padding:5pt 7pt;text-align:right">'+repos.length+'</td></tr></tbody></table><h3 style="font-size:11pt;color:#374151;margin-bottom:.5cm">Activity by Repository</h3><table style="width:100%;border-collapse:collapse"><thead><tr>'+TH('Repository')+TH('Total','right')+TH('Merged','right')+TH('Open','right')+'</tr></thead><tbody>'+repoRows+'</tbody></table><div style="margin-top:1.2cm;border-top:1px solid #e2e8f0;padding-top:.6cm"><p style="font-size:8pt;font-weight:700;color:#9ca3af;text-transform:uppercase;letter-spacing:.07em;margin-bottom:.4cm">Jump to Section</p><div style="display:flex;flex-wrap:wrap;gap:.3cm .8cm"><a href="#charts-section" style="color:#1e40af;font-size:9.5pt;text-decoration:none;font-weight:500">Overview Charts</a><a href="#deliveries" style="color:#1e40af;font-size:9.5pt;text-decoration:none;font-weight:500">Deliveries by Repository</a>'+months.map(m=>'<a href="#'+slugM(m)+'" style="color:#1e40af;font-size:9.5pt;text-decoration:none;font-weight:500">'+monthFull(m)+'</a>').join('')+'<a href="#pr-descriptions" style="color:#1e40af;font-size:9.5pt;text-decoration:none;font-weight:500">PR Descriptions</a></div></div></section>'+chartsSection+deliveriesSection+sections+descriptionsSection+'</body></html>';
}
function exportPDF(){document.getElementById('export-menu').style.display='none';const win=window.open('','_blank');win.document.write(generateReportHTML());win.document.close();win.addEventListener('load',function(){win.print();});}
function exportCSV(){
  document.getElementById('export-menu').style.display='none';
  const{f,filterDesc}=buildReportData();
  const rows=[['Date','Repo','PR','Title','Status','Lead Time (days)','Author']];
  f.forEach(d=>{
    rows.push([
      d.date,
      d.repos.join('+'),
      d.pr,
      '"'+d.title.replace(/"/g,'""')+'"',
      d.status,
      d.durationDays!=null?d.durationDays:'',
      d.author||''
    ]);
  });
  const csv=rows.map(r=>r.join(',')).join('\n');
  const a=document.createElement('a');
  a.href='data:text/csv;charset=utf-8,'+encodeURIComponent(csv);
  const slug=filterDesc.replace(/[^a-z0-9]+/gi,'-').toLowerCase();
  a.download='prs-'+slug+'.csv';
  a.click();
}
function exportMD(){
  document.getElementById('export-menu').style.display='none';
  const{f,filterDesc,byStatus,repos,byMonth,months}=buildReportData();
  const now=new Date().toLocaleDateString('en-US');
  const period=months.length>=2?monthFull(months[0])+' – '+monthFull(months[months.length-1]):months.length===1?monthFull(months[0]):filterDesc;
  const totalMerged=byStatus.merged||0,totalOpen=byStatus.open||0,totalClosed=byStatus.closed||0;
  const withDur=f.filter(d=>d.status==='merged'&&d.durationDays!=null);
  const avgDays=withDur.length?Math.round(withDur.reduce((s,d)=>s+d.durationDays,0)/withDur.length)+'d':'—';
  const toc=['- [Summary](#summary)'].concat(months.map(m=>'- ['+monthFull(m)+'](#'+slugM(m)+')')).join('\n');
  const repoTable=['| Repository | PRs | Merged | Open |','|------------|----:|-------:|-----:|'].concat(repos.map(function(r){const rp=f.filter(d=>d.repos.includes(r));return'| '+r+' | '+rp.length+' | '+rp.filter(d=>d.status==='merged').length+' | '+rp.filter(d=>d.status==='open').length+' |';})).join('\n');
  const monthSections=months.map(function(m){const prs=byMonth[m];const mc=prs.filter(d=>d.status==='merged').length;const oc=prs.filter(d=>d.status==='open').length;const rows=prs.map(function(d){const parts=d.date.split('-');const date=parts[1]+'/'+parts[2]+'/'+parts[0].slice(2);const pr=d.url?'['+d.pr+']('+d.url+')':d.pr;const dur=d.durationDays!=null?(d.durationDays===0?'same day':d.durationDays+'d'):'—';return'| '+date+' | '+d.repos.join('+')+' | '+pr+' | '+d.title.replace(/\|/g,'\\|')+' | '+(STATUS_LBL[d.status]||d.status)+' | '+dur+' |';}).join('\n');const summary=[mc&&(mc+' merged'),oc&&(oc+' open'),prs.length+' total'].filter(Boolean).join(' · ');return'## '+monthFull(m)+'\n\n'+'*'+summary+'*\n\n'+'| Date | Repo | PR | Title | Status | Lead Time |\n'+'|------|------|----|-------|--------|-----------|\n'+rows;}).join('\n\n---\n\n');
  const md='# Development Activity Report — Hyland\n\n'+'**Period:** '+period+'  \n'+'**Generated:** '+now+(filterDesc!=='All data'?'  \n**Filter:** '+filterDesc:'')+'\n\n'+'---\n\n## Table of Contents\n\n'+toc+'\n\n'+'---\n\n## Summary\n\n'+'| Metric | Value |\n|--------|------:|\n'+'| Total PRs | '+f.length+' |\n'+'| Merged | '+totalMerged+' |\n'+'| Open | '+totalOpen+' |\n'+'| Closed (no merge) | '+totalClosed+' |\n'+'| Avg lead time | '+avgDays+' |\n'+'| Repos | '+repos.length+' |\n\n'+'### Activity by Repository\n\n'+repoTable+'\n\n'+'---\n\n'+monthSections;
  const blob=new Blob([md],{type:'text/markdown'});const url=URL.createObjectURL(blob);const a=document.createElement('a');a.href=url;a.download='evolution-report-'+new Date().toISOString().slice(0,10)+'.md';a.click();URL.revokeObjectURL(url);
}

// ── Worktrees Configuration Management ─────────────────────────
function loadWorktreesConfig(){
  const defaultConfig={
    rootPath:'C:\\',
    worktreesPath:'C:\\'
  };
  const stored=localStorage.getItem('worktreesConfig');
  return stored?JSON.parse(stored):defaultConfig;
}
function saveWorktreesConfig(config){
  localStorage.setItem('worktreesConfig',JSON.stringify(config));
  syncWorktreesConfigToBackend(config);
}
async function syncWorktreesConfigToBackend(config){
  try{
    const response=await fetch('/api/settings',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({worktreesConfig:config})
    });
    if(!response.ok){
      console.warn('Failed to sync worktrees config to backend:',response.statusText);
    }
  }catch(err){
    console.warn('Failed to sync worktrees config to backend:',err);
  }
}
async function loadWorktreesConfigFromBackend(){
  try{
    const response=await fetch('/api/config/worktrees');
    if(response.ok){
      const config=await response.json();
      localStorage.setItem('worktreesConfig',JSON.stringify(config));
      return config;
    }
  }catch(err){
    console.warn('Failed to load worktrees config from backend:',err);
  }
  return loadWorktreesConfig();
}
function updateWorktreesUI(){
  const config=loadWorktreesConfig();
  const rootInfo=document.getElementById('wt-root-path-info');
  const worktreesInfo=document.getElementById('wt-worktrees-path-info');
  if(rootInfo)rootInfo.textContent=config.rootPath;
  if(worktreesInfo)worktreesInfo.textContent=config.worktreesPath;
}

// Make functions globally available - opens native Windows folder dialog
window.selectRootPath=async function(){
  const config=loadWorktreesConfig();
  const description=currentLang==='en-US'?'Select root folder':'Selecione a pasta raiz';

  try{
    const response=await fetch('/api/select-folder',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({initialPath:config.rootPath,description:description})
    });

    if(response.ok){
      const result=await response.json();
      if(result.path){
        config.rootPath=result.path;
        saveWorktreesConfig(config);
        updateWorktreesUI();
        updateWorktreesPathsDisplay();
        const typeText=currentLang==='en-US'?'root':'raiz';
        pushNotif(`📁 Caminho ${typeText} atualizado: ${config.rootPath}`,'info');
      }
    }
  }catch(err){
    console.error('Error selecting folder:',err);
    pushNotif(currentLang==='en-US'?'❌ Error selecting folder':'❌ Erro ao selecionar pasta','error');
  }
}

window.selectWorktreesPath=async function(){
  const config=loadWorktreesConfig();
  const description=currentLang==='en-US'?'Select worktrees folder':'Selecione a pasta de worktrees';

  try{
    const response=await fetch('/api/select-folder',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({initialPath:config.worktreesPath,description:description})
    });

    if(response.ok){
      const result=await response.json();
      if(result.path){
        config.worktreesPath=result.path;
        saveWorktreesConfig(config);
        updateWorktreesUI();
        updateWorktreesPathsDisplay();
        const typeText=currentLang==='en-US'?'destination':'destino';
        pushNotif(`📁 Caminho ${typeText} atualizado: ${config.worktreesPath}`,'info');
      }
    }
  }catch(err){
    console.error('Error selecting folder:',err);
    pushNotif(currentLang==='en-US'?'❌ Error selecting folder':'❌ Erro ao selecionar pasta','error');
  }
}

// ── Worktrees data state ───────────────────────────────────────
let worktreesData={
  repositories:[],
  stats:{},
  selectedRepos:new Set(),
  lastUpdate:null,
  cacheExpiry:60000 // 1 minuto de cache
};

// Configuração de polling (padrão: 5 minutos)
let worktreesPollingInterval=null;
let worktreesPollingRate=300000; // 5 min default

// Loading states para cada seção
let worktreesSectionLoading={
  stats:false,
  repositories:false,
  diskSpace:false
};

// Carregar config de polling do localStorage
function loadWorktreesPollingConfig(){
  const stored=localStorage.getItem('worktreesPollingRate');
  if(stored){
    worktreesPollingRate=parseInt(stored,10);
  }
}

function saveWorktreesPollingConfig(rateInSeconds){
  worktreesPollingRate=rateInSeconds*1000;
  localStorage.setItem('worktreesPollingRate',worktreesPollingRate.toString());
  // Reiniciar polling se já estiver ativo
  if(worktreesPollingInterval){
    stopWorktreesPolling();
    startWorktreesPolling();
  }
}

loadWorktreesPollingConfig();

// ── Worktrees multi-selection functions ────────────────────────
function toggleRepoSelection(repoName){
  if(worktreesData.selectedRepos.has(repoName)){
    worktreesData.selectedRepos.delete(repoName);
  }else{
    worktreesData.selectedRepos.add(repoName);
  }
  updateBatchActionsBar();
  renderWorktreesList();
}

function updateBatchActionsBar(){
  const bar=document.getElementById('wt-batch-actions');
  if(!bar)return;
  const count=worktreesData.selectedRepos.size;

  if(count===0){
    bar.classList.add('hidden');
    bar.style.display='none';
    return;
  }

  bar.classList.remove('hidden');
  bar.style.display='flex';
  bar.innerHTML=`
    <div style="display:flex;align-items:center;gap:10px">
      <span style="font-size:13px;font-weight:600;color:var(--text)">${count} ${t('wtBatchSelected')}</span>
    </div>
    <div style="display:flex;align-items:center;gap:10px">
      <button onclick="updateSelectedRepos()" style="padding:8px 16px;border-radius:10px;background:rgba(180,155,250,0.15);border:1px solid rgba(180,155,250,0.3);color:var(--indigo);font-size:13px;font-weight:600;cursor:pointer;transition:all .15s;display:inline-flex;align-items:center;gap:6px" onmouseover="this.style.background='rgba(180,155,250,0.25)'" onmouseout="this.style.background='rgba(180,155,250,0.15)'">
        <svg class="ti" width="14" height="14" style="stroke:currentColor"><use href="/icons.svg#ti-refresh"/></svg>
        ${t('wtBatchUpdate')}
      </button>
      <button onclick="clearSelection()" style="padding:8px 16px;border-radius:10px;background:var(--subtle);border:1px solid var(--border);color:var(--muted);font-size:13px;font-weight:600;cursor:pointer;transition:all .15s" onmouseover="this.style.borderColor='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)'">${t('wtBatchDeselect')}</button>
    </div>
  `;
}

function clearSelection(){
  worktreesData.selectedRepos.clear();
  updateBatchActionsBar();
  renderWorktreesList();
}

async function updateSelectedRepos(){
  const selected=Array.from(worktreesData.selectedRepos);

  for(const repoName of selected){
    const repo=worktreesData.repositories.find(r=>r.name===repoName);
    if(!repo)continue;

    try{
      const result=await fetch('/api/worktrees/update',{
        method:'POST',
        headers:{'Content-Type':'application/json'},
        body:JSON.stringify({repoPath:repo.path})
      }).then(r=>r.json());

      if(result.success){
        console.log(`Updated ${repoName}`);
      }
    }catch(err){
      console.error(`Error updating ${repoName}:`,err);
    }
  }

  // Recarregar dados
  await loadWorktreesData();
}

let worktreesFilters={
  search:'',
  status:'all', // 'all', 'outdated', 'with-worktrees', 'uncommitted', 'conflict'
  selectedRepo:'all'
};

// ── Worktrees validation and data loading ──────────────────────
async function loadWorktreesView(){
  try{
    // IMPORTANTE: Esconder o loading global que bloqueia tudo
    const globalLoading=document.getElementById('loading');
    if(globalLoading){
      globalLoading.style.display='none';
      console.log('[WORKTREES] Loading global escondido');
    }

    const validation=await fetch('/api/config/worktrees/validate').then(r=>r.json());
    if(!validation.isValid){
      console.error('[WORKTREES] Config invalid:',validation.errors);
      showWorktreesConfigError(validation.errors);
      return;
    }
    hideWorktreesConfigError();
    initWorktreesFilters();

    // Tentar carregar do cache primeiro
    console.log('[WORKTREES] Tentando carregar cache...');
    const cacheLoaded=await loadWorktreesFromCache();

    // Carregar dados atualizados em background
    console.log('[WORKTREES] Carregando dados atualizados...');
    await loadWorktreesData(!cacheLoaded);

    startWorktreesPolling();
  }catch(err){
    console.error('[WORKTREES] Error loading worktrees view:',err);
    showToast('Erro ao carregar visualização de worktrees','error');
  }
}

async function loadWorktreesFromCache(){
  try{
    console.log('[WORKTREES] Buscando cache em /api/worktrees/cache...');
    const response=await fetch('/api/worktrees/cache');
    const result=await response.json();

    console.log('[WORKTREES] Resposta do cache:',result);

    if(result.cached&&result.data){
      worktreesData.repositories=result.data.repositories||[];
      worktreesData.stats=result.data.stats||{};
      worktreesData.lastUpdate=result.data.lastUpdate||null;

      console.log('[WORKTREES] Cache encontrado!',{
        repos:worktreesData.repositories.length,
        stats:worktreesData.stats
      });

      // Renderizar dados do cache
      updateWorktreesStats(worktreesData.stats);
      renderWorktreesList();

      console.log('[WORKTREES] ✅ Cache carregado e renderizado com sucesso');
      return true;
    }else{
      console.log('[WORKTREES] Cache não encontrado ou vazio');
    }
  }catch(err){
    console.error('[WORKTREES] ❌ Erro ao carregar cache:',err);
  }
  return false;
}

async function saveWorktreesCache(){
  try{
    const cacheData={
      repositories:worktreesData.repositories,
      stats:worktreesData.stats,
      lastUpdate:worktreesData.lastUpdate,
      generatedAt:new Date().toISOString()
    };

    console.log('[WORKTREES] Salvando cache...',{
      repos:cacheData.repositories.length,
      stats:cacheData.stats
    });

    const response=await fetch('/api/worktrees/cache',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify(cacheData)
    });

    const result=await response.json();

    if(result.success){
      console.log('[WORKTREES] ✅ Cache salvo com sucesso em out/worktrees-cache.json');
    }else{
      console.error('[WORKTREES] ❌ Erro ao salvar cache:',result.error);
    }
  }catch(err){
    console.error('[WORKTREES] ❌ Falha ao salvar cache:',err);
  }
}

async function loadWorktreesData(showLoading=true){
  // Verificar cache em memória
  const now=Date.now();
  if(!showLoading&&worktreesData.lastUpdate&&(now-worktreesData.lastUpdate)<worktreesData.cacheExpiry){
    renderWorktreesList();
    return;
  }

  try{
    const validation=await fetch('/api/config/worktrees/validate').then(r=>r.json());
    if(!validation.isValid){
      showWorktreesConfigError(validation.errors);
      return;
    }

    // Mostrar loading localizado apenas se necessário
    if(showLoading){
      setWorktreesLoading('stats',true);
    }

    // Carregar stats e atualizar IMEDIATAMENTE
    const stats=await fetch('/api/worktrees/stats').then(r=>r.json());
    worktreesData.stats=stats;
    updateWorktreesStats(stats);
    setWorktreesLoading('stats',false);

    // Carregar repositórios
    setWorktreesLoading('repositories',true);
    const repos=await fetch('/api/worktrees/repositories').then(r=>r.json());
    worktreesData.repositories=repos.repositories;

    // Renderizar lista vazia (com esqueletos/placeholders se necessário)
    const container=document.getElementById('wt-repos-list');
    if(container&&showLoading)container.innerHTML='';

    // Carregar worktrees de cada repo EM PARALELO (máximo 5 simultâneos)
    const BATCH_SIZE=5;

    for(let i=0;i<worktreesData.repositories.length;i+=BATCH_SIZE){
      const batch=worktreesData.repositories.slice(i,i+BATCH_SIZE);

      await Promise.all(batch.map(async repo=>{
        try{
          // Carregar worktrees e status em paralelo
          const [worktreesResp,statusData]=await Promise.all([
            fetch(`/api/worktrees/list?repoPath=${encodeURIComponent(repo.path)}`).then(r=>r.json()),
            fetch(`/api/worktrees/status?repoPath=${encodeURIComponent(repo.path)}`).then(r=>r.json())
          ]);

          repo.worktrees=worktreesResp.worktrees||[];
          repo.status=statusData;
          repo._loaded=true; // Marcar como carregado

          // Atualizar APENAS este card (não re-renderizar tudo)
          updateSingleRepoCard(repo);

        }catch(err){
          console.error('[WORKTREES] Error loading',repo.name,':',err);
          repo.worktrees=[];
          repo.status={ahead:0,behind:0,hasUncommitted:false,isClean:true};
          repo._loaded=true;
          updateSingleRepoCard(repo);
        }
      }));
    }

    // Atualizar timestamp do cache
    worktreesData.lastUpdate=Date.now();
    setWorktreesLoading('repositories',false);

    // Salvar cache persistente
    await saveWorktreesCache();

  }catch(err){
    console.error('[WORKTREES] Error:',err);
    setWorktreesLoading('stats',false);
    setWorktreesLoading('repositories',false);
    showToast(currentLang==='pt-BR'?'Erro ao carregar dados dos repositórios.':'Error loading repository data.','error');
  }
}

function setWorktreesLoading(section,isLoading){
  worktreesSectionLoading[section]=isLoading;

  const loadingSpinner='<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" style="animation:spin .7s linear infinite"><path d="M12 2v4M12 18v4M4.93 4.93l2.83 2.83M16.24 16.24l2.83 2.83M2 12h4M18 12h4M4.93 19.07l2.83-2.83M16.24 7.76l2.83-2.83"/></svg>';

  if(section==='stats'){
    const repos=document.getElementById('wt-stat-repos');
    const worktrees=document.getElementById('wt-stat-worktrees');
    const updates=document.getElementById('wt-stat-updates');
    const disk=document.getElementById('wt-stat-disk');

    if(isLoading){
      if(repos)repos.innerHTML=loadingSpinner;
      if(worktrees)worktrees.innerHTML=loadingSpinner;
      if(updates)updates.innerHTML=loadingSpinner;
      if(disk)disk.innerHTML=loadingSpinner;
    }
  }else if(section==='repositories'){
    const container=document.getElementById('wt-repos-list');
    if(isLoading&&container&&!container.querySelector('.wt-loading-placeholder')){
      // Não adicionar placeholder se já houver dados do cache
      if(worktreesData.repositories.length===0){
        container.innerHTML=`<div class="wt-loading-placeholder" style="display:flex;align-items:center;justify-content:center;padding:40px;color:var(--muted);font-size:14px;gap:10px">${loadingSpinner} Carregando repositórios...</div>`;
      }
    }else if(!isLoading&&container){
      const placeholder=container.querySelector('.wt-loading-placeholder');
      if(placeholder)placeholder.remove();
    }
  }
}

function updateWorktreesStats(stats){
  const repos=document.getElementById('wt-stat-repos');
  const worktrees=document.getElementById('wt-stat-worktrees');
  const updates=document.getElementById('wt-stat-updates');
  const disk=document.getElementById('wt-stat-disk');

  if(repos)repos.textContent=stats.totalRepos||0;
  if(worktrees)worktrees.textContent=stats.totalWorktrees||0;
  if(updates)updates.textContent=stats.reposWithUpdates||0;
  if(disk)disk.textContent=stats.diskSpace||'0 B';
}

function renderWorktreesList(){
  // Use applyWorktreesFilters to render with current filters
  applyWorktreesFilters();
}

// Atualizar apenas um card específico (incremental)
function updateSingleRepoCard(repo){
  const container=document.getElementById('wt-repos-list');
  if(!container)return;

  // Verificar se card já existe
  let existingCard=container.querySelector(`[data-repo-name="${repo.name}"]`);

  const newCard=createRepoCard(repo);

  if(existingCard){
    // Substituir card existente
    existingCard.replaceWith(newCard);
  }else{
    // Adicionar novo card
    container.appendChild(newCard);
  }
}

function createRepoCard(repo){
  const isSelected=worktreesData.selectedRepos.has(repo.name);

  const card=document.createElement('div');
  card.style.cssText='background:var(--surf);border-radius:14px;border:1px solid var(--border);overflow:hidden;box-shadow:0 2px 12px rgba(0,0,0,.2)';
  card.dataset.repoName=repo.name;

  const hasUpdates=repo.status?.behind>0;
  const statusBadge=hasUpdates?
    `<span style="display:inline-flex;align-items:center;gap:4px;padding:3px 8px;border-radius:6px;background:rgba(250,179,135,0.15);color:#fab387;border:1px solid rgba(250,179,135,0.3);font-size:11px;font-weight:600">
       <svg class="ti" width="12" height="12" style="stroke:currentColor"><use href="/icons.svg#ti-arrow-down"/></svg>
       ${repo.status.behind} ${t('wtStatusBehind')}
     </span>`:
    `<span style="display:inline-flex;align-items:center;gap:4px;padding:3px 8px;border-radius:6px;background:rgba(166,227,161,0.15);color:#a6e3a1;border:1px solid rgba(166,227,161,0.3);font-size:11px;font-weight:600">
       <svg class="ti" width="12" height="12" style="stroke:currentColor"><use href="/icons.svg#ti-check"/></svg>
       ${t('wtStatusUpdated')}
     </span>`;

  // Contar apenas worktrees adicionais (excluir o principal)
  const additionalWorktrees=(repo.worktrees||[]).filter(wt=>!wt.isMain);
  const worktreeCount=additionalWorktrees.length;

  // Pegar branch principal - fallback chain correto
  const mainWorktree=(repo.worktrees||[]).find(wt=>wt.isMain);
  const currentBranch=repo.currentBranch||mainWorktree?.branch||'main';

  // Última atualização do repositório (dia e horário)
  const lastUpdate=repo.lastModified?new Date(repo.lastModified).toLocaleString(currentLang,{
    day:'2-digit',
    month:'2-digit',
    year:'numeric',
    hour:'2-digit',
    minute:'2-digit'
  }):'--';

  // Traduzir worktree count
  const worktreeLabel=worktreeCount===1?t('wtWorktreeCount_one'):t('wtWorktreeCount');

  card.innerHTML=`
    <div style="padding:16px;display:flex;align-items:center;justify-content:space-between;cursor:pointer" onclick="toggleRepoExpand('${repo.name}')">
      <div style="display:flex;align-items:center;gap:12px">
        <div style="width:20px;height:20px;border-radius:6px;background:${isSelected?'var(--indigo)':'var(--subtle)'};border:${isSelected?'none':'1px solid var(--border)'};display:flex;align-items:center;justify-content:center;cursor:pointer"
             onclick="event.stopPropagation();toggleRepoSelection('${repo.name}')">
          ${isSelected?'<svg class="ti" width="14" height="14" style="stroke:var(--bg)"><use href="/icons.svg#ti-check"/></svg>':''}
        </div>
        <div style="width:36px;height:36px;border-radius:10px;background:rgba(180,155,250,0.15);display:flex;align-items:center;justify-content:center">
          <svg class="ti" width="20" height="20" style="stroke:var(--indigo)"><use href="/icons.svg#ti-folder"/></svg>
        </div>
        <div>
          <div style="display:flex;align-items:center;gap:8px;margin-bottom:2px">
            <span style="font-size:14px;font-weight:700;color:var(--text)">${repo.name}</span>
            <span style="font-size:11px;font-weight:600;color:var(--blue);font-family:monospace;background:rgba(137,220,235,0.15);padding:2px 6px;border-radius:4px">⎇ ${currentBranch}</span>
          </div>
          <div style="display:flex;align-items:center;gap:8px;font-size:12px">
            ${statusBadge}
            ${worktreeCount>0?`<span style="color:var(--indigo);font-weight:500">${worktreeCount} ${worktreeLabel}</span>`:''}
            <span style="color:var(--muted);font-size:11px">• ${t('wtStatusUpdated').toLowerCase()} ${lastUpdate}</span>
          </div>
        </div>
      </div>
      <div style="display:flex;align-items:center;gap:8px">
        ${hasUpdates?`<button style="padding:6px 12px;border-radius:8px;background:rgba(250,179,135,0.15);color:#fab387;border:1px solid rgba(250,179,135,0.3);font-size:11px;font-weight:600;cursor:pointer;transition:all .15s" onclick="event.stopPropagation();updateRepo('${repo.name}')" onmouseover="this.style.background='rgba(250,179,135,0.25)'" onmouseout="this.style.background='rgba(250,179,135,0.15)'">${t('wtUpdate')}</button>`:''}
        <button style="padding:6px 12px;border-radius:8px;background:rgba(166,227,161,0.15);color:#a6e3a1;border:1px solid rgba(166,227,161,0.3);font-size:11px;font-weight:600;cursor:pointer;transition:all .15s" onclick="event.stopPropagation();showCreateWorktreeModal('${repo.name}')" onmouseover="this.style.background='rgba(166,227,161,0.25)'" onmouseout="this.style.background='rgba(166,227,161,0.15)'">${t('wtCreateWorktree')}</button>
        <button style="width:32px;height:32px;border-radius:8px;background:var(--subtle);border:1px solid var(--border);display:flex;align-items:center;justify-content:center;cursor:pointer;transition:all .15s" onmouseover="this.style.borderColor='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)'">
          <svg id="chevron-${repo.name}" class="ti" width="16" height="16" style="stroke:var(--muted);transition:transform .2s"><use href="/icons.svg#ti-chevron-down"/></svg>
        </button>
      </div>
    </div>
    <div id="wt-expand-${repo.name}" style="display:none;padding:16px 20px;background:var(--bg);border-top:1px solid var(--border)">
      ${renderWorktrees(repo)}
    </div>
  `;

  return card;
}

function toggleRepoExpand(repoName){
  const expandEl=document.getElementById(`wt-expand-${repoName}`);
  const chevron=document.getElementById(`chevron-${repoName}`);
  if(!expandEl||!chevron)return;

  const repo=worktreesData.repositories.find(r=>r.name===repoName);
  if(!repo)return;

  // Se não tem worktrees, não expandir
  const worktrees=(repo.worktrees||[]).filter(wt=>!wt.isMain);
  if(worktrees.length===0)return;

  const isHidden=expandEl.style.display==='none';
  expandEl.style.display=isHidden?'block':'none';
  chevron.style.transform=isHidden?'rotate(180deg)':'rotate(0deg)';
}

function renderWorktrees(repo){
  // Filtrar apenas worktrees adicionais (excluir o principal)
  const worktrees=(repo.worktrees||[]).filter(wt=>!wt.isMain);

  if(worktrees.length===0){
    return `<div style="padding:20px;text-align:center;color:var(--muted);font-size:13px">${t('wtNoWorktrees')}</div>`;
  }

  return `<div style="display:flex;flex-direction:column;gap:10px">${worktrees.map(wt=>{
    const wtPath=wt.path||'';
    const wtName=wtPath.split(/[\\/]/).pop()||wtPath;
    const branch=wt.branch||repo.currentBranch||'main';

    return `
      <div style="padding:12px 14px;border-radius:10px;background:var(--surf);border:1px solid var(--border);display:flex;align-items:center;justify-content:space-between;transition:border-color .15s" onmouseover="this.style.borderColor='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)'">
        <div style="display:flex;align-items:center;gap:10px">
          <div style="width:32px;height:32px;border-radius:8px;background:rgba(137,220,235,0.15);display:flex;align-items:center;justify-content:center">
            <svg class="ti" width="16" height="16" style="stroke:#89dceb"><use href="/icons.svg#ti-git-branch"/></svg>
          </div>
          <div>
            <div style="font-size:12px;font-weight:600;color:var(--text);font-family:monospace">${wtName}</div>
            <div style="font-size:11px;color:var(--muted)">${branch}</div>
          </div>
        </div>
        <div style="display:flex;align-items:center;gap:6px">
          <button style="padding:5px 10px;border-radius:6px;background:var(--subtle);border:1px solid var(--border);font-size:11px;font-weight:500;color:var(--text);cursor:pointer;transition:all .15s" onclick="openWorktree('${wtPath.replace(/\\/g,'\\\\')}','explorer')" onmouseover="this.style.borderColor='var(--indigo)'" onmouseout="this.style.borderColor='var(--border)'">${t('wtOpen')}</button>
          <button style="padding:5px 10px;border-radius:6px;background:var(--subtle);border:1px solid var(--border);font-size:11px;font-weight:500;color:var(--text);cursor:pointer;transition:all .15s" onclick="openWorktree('${wtPath.replace(/\\/g,'\\\\')}','terminal')" onmouseover="this.style.borderColor='var(--blue)'" onmouseout="this.style.borderColor='var(--border)'">${t('wtTerminal')}</button>
          <button style="padding:5px 10px;border-radius:6px;background:rgba(243,139,168,0.15);border:1px solid rgba(243,139,168,0.3);font-size:11px;font-weight:500;color:#f38ba8;cursor:pointer;transition:all .15s" onclick="deleteWorktree('${repo.path.replace(/\\/g,'\\\\')}','${wtPath.replace(/\\/g,'\\\\')}')" onmouseover="this.style.background='rgba(243,139,168,0.25)'" onmouseout="this.style.background='rgba(243,139,168,0.15)'">${t('wtDelete')}</button>
        </div>
      </div>
    `;
  }).join('')}</div>`;
}

// ── Worktrees filters ──────────────────────────────────────────
function applyWorktreesFilters(){
  const filtered=worktreesData.repositories.filter(repo=>{
    // Filtro de busca
    if(worktreesFilters.search&&!repo.name.toLowerCase().includes(worktreesFilters.search.toLowerCase())){
      return false;
    }

    // Filtro de status
    if(worktreesFilters.status==='outdated'&&repo.status?.behind===0)return false;
    if(worktreesFilters.status==='with-worktrees'&&repo.worktrees.length===0)return false;
    if(worktreesFilters.status==='uncommitted'&&!repo.status?.hasUncommitted)return false;

    // Filtro de repositório específico
    if(worktreesFilters.selectedRepo!=='all'&&repo.name!==worktreesFilters.selectedRepo)return false;

    return true;
  });

  const container=document.getElementById('wt-repos-list');
  if(!container)return;
  container.innerHTML='';
  filtered.forEach(repo=>container.appendChild(createRepoCard(repo)));
}

// Conectar inputs HTML aos filtros
function initWorktreesFilters(){
  const searchInput=document.getElementById('wt-filter-search');
  const statusSelect=document.getElementById('wt-filter-status');
  const repoSelect=document.getElementById('wt-filter-repo');

  if(searchInput){
    searchInput.addEventListener('input',(e)=>{
      worktreesFilters.search=e.target.value;
      applyWorktreesFilters();
    });
  }

  if(statusSelect){
    statusSelect.addEventListener('change',(e)=>{
      worktreesFilters.status=e.target.value;
      applyWorktreesFilters();
    });
  }

  if(repoSelect){
    repoSelect.addEventListener('change',(e)=>{
      worktreesFilters.selectedRepo=e.target.value;
      applyWorktreesFilters();
    });
  }
}

function showWorktreesError(message){
  console.error(message);
  showToast(message,'error');
}

// Loading removido - agora é progressivo por card

function showWorktreesConfigError(errors){
  const errorMessages={
    rootPath:t('wtErrorRootPath'),
    worktreesPath:t('wtErrorWorktreesPath'),
    rootPathNotFound:t('wtErrorRootPathNotFound'),
    worktreesPathNotFound:t('wtErrorWorktreesPathNotFound')
  };
  const messages=errors.map(e=>errorMessages[e]||e);
  const banner=document.getElementById('wt-error-banner');
  if(!banner)return;
  banner.innerHTML=`
    <div class="bg-app-rose/20 border border-app-rose/40 rounded-xl p-4">
      <div class="flex items-center gap-2 text-app-rose font-semibold mb-2">
        <svg class="ti ti-alert-triangle w-5 h-5"><use href="/icons.svg#ti-alert-triangle"/></svg>
        <span data-i18n="wtErrorConfigTitle">${t('wtErrorConfigTitle')}</span>
      </div>
      <ul class="text-sm text-app-text-main space-y-1 ml-6">
        ${messages.map(m=>`<li>• ${m}</li>`).join('')}
      </ul>
      <button onclick="toggleUserMenu()" class="mt-3 px-3 py-1.5 bg-app-purple rounded-lg text-sm font-semibold">
        <span data-i18n="wtConfigNow">${t('wtConfigNow')}</span>
      </button>
    </div>
  `;
  banner.classList.remove('hidden');
  const mainContent=document.getElementById('wt-main-content');
  if(mainContent)mainContent.classList.add('hidden');
}

function hideWorktreesConfigError(){
  const banner=document.getElementById('wt-error-banner');
  if(banner)banner.classList.add('hidden');
  const mainContent=document.getElementById('wt-main-content');
  if(mainContent)mainContent.classList.remove('hidden');
}

// ── Individual Repository Actions ──────────────────────────────
async function updateRepo(repoName){
  const repo=worktreesData.repositories.find(r=>r.name===repoName);
  if(!repo){
    showToast(currentLang==='pt-BR'?'Repositório não encontrado':'Repository not found','error');
    return;
  }

  // loading removed
  try{
    const result=await fetch('/api/worktrees/update',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({repoPath:repo.path})
    }).then(r=>r.json());

    if(result.success){
      showToast(`${repoName} ${t('wtToastUpdated')}`,'success');
      await loadWorktreesData();
    }else{
      showToast(`${currentLang==='pt-BR'?'Erro ao atualizar':'Error updating'} ${repoName}: ${result.error}`,'error');
    }
  }catch(err){
    console.error('Error updating repo:',err);
    showToast(`${currentLang==='pt-BR'?'Erro ao atualizar':'Error updating'} ${repoName}`,'error');
  }finally{
    // loading removed
  }
}

async function openRepoVSCode(repoName){
  const repo=worktreesData.repositories.find(r=>r.name===repoName);
  if(!repo){
    showToast(currentLang==='pt-BR'?'Repositório não encontrado':'Repository not found','error');
    return;
  }

  try{
    await fetch('/api/worktrees/open',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({path:repo.path,action:'vscode'})
    });
  }catch(err){
    console.error('Error opening VS Code:',err);
    showToast(currentLang==='pt-BR'?'Erro ao abrir VS Code':'Error opening VS Code','error');
  }
}

// ── Worktree Actions ───────────────────────────────────────────
async function openWorktree(worktreePath,action){
  try{
    await fetch('/api/worktrees/open',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({path:worktreePath,action})
    });
  }catch(err){
    console.error('Error opening worktree:',err);
    showToast(`${currentLang==='pt-BR'?'Erro ao abrir':'Error opening'} ${action}`,'error');
  }
}

async function deleteWorktree(repoPath,worktreePath){
  const confirmMsg=currentLang==='pt-BR'
    ?`Tem certeza que deseja excluir a worktree?\n\n${worktreePath}\n\nEsta ação não pode ser desfeita.`
    :`Are you sure you want to delete this worktree?\n\n${worktreePath}\n\nThis action cannot be undone.`;

  if(!confirm(confirmMsg)){
    return;
  }

  // loading removed
  try{
    const result=await fetch('/api/worktrees/delete',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({repoPath,worktreePath})
    }).then(r=>r.json());

    if(result.success){
      showToast(currentLang==='pt-BR'?'Worktree excluída com sucesso!':'Worktree deleted successfully!','success');
      await loadWorktreesData();
    }else{
      showToast(`${currentLang==='pt-BR'?'Erro ao excluir worktree':'Error deleting worktree'}: ${result.error}`,'error');
    }
  }catch(err){
    console.error('Error deleting worktree:',err);
    showToast(currentLang==='pt-BR'?'Erro ao excluir worktree':'Error deleting worktree','error');
  }finally{
    // loading removed
  }
}

function showToast(message,type='info'){
  const colors={
    success:'bg-app-green/20 border-app-green text-app-green',
    error:'bg-app-rose/20 border-app-rose text-app-rose',
    info:'bg-app-blue/20 border-app-blue text-app-blue'
  };

  const toast=document.createElement('div');
  toast.className=`fixed bottom-6 right-6 px-4 py-3 rounded-xl border ${colors[type]} z-50 shadow-lg`;
  toast.textContent=message;

  document.body.appendChild(toast);

  setTimeout(()=>{
    toast.remove();
  },3000);
}

// ── Create Worktree Modal ──────────────────────────────────────
function showCreateWorktreeModal(repoName){
  const repo=worktreesData.repositories.find(r=>r.name===repoName);
  if(!repo)return;

  const modalHtml=`
    <div id="wt-create-modal" class="fixed inset-0 bg-black/60 flex items-center justify-center z-50" onclick="closeCreateWorktreeModal()">
      <div class="bg-app-card rounded-2xl border border-app-border p-6 max-w-md w-full" onclick="event.stopPropagation()">
        <h3 class="text-lg font-bold text-white mb-4">Criar Nova Worktree</h3>
        <div class="space-y-3">
          <div>
            <label class="text-xs text-app-text-muted font-semibold">Nome da Worktree</label>
            <input id="wt-name-input" type="text" class="w-full bg-[#181926] border border-app-border rounded-xl px-3 py-2 text-sm text-white" placeholder="feat-new-feature" />
          </div>
          <div>
            <label class="text-xs text-app-text-muted font-semibold">Branch</label>
            <input id="wt-branch-input" type="text" class="w-full bg-[#181926] border border-app-border rounded-xl px-3 py-2 text-sm text-white" placeholder="main" value="${repo.currentBranch}" />
          </div>
        </div>
        <div class="flex items-center gap-2 mt-6">
          <button class="flex-1 px-4 py-2 rounded-xl bg-app-purple text-[#181926] font-bold text-sm" onclick="createWorktree('${repoName}')">
            Criar Worktree
          </button>
          <button class="px-4 py-2 rounded-xl bg-[#181926] border border-app-border text-sm" onclick="closeCreateWorktreeModal()">
            Cancelar
          </button>
        </div>
      </div>
    </div>
  `;

  document.body.insertAdjacentHTML('beforeend',modalHtml);
}

function closeCreateWorktreeModal(){
  const modal=document.getElementById('wt-create-modal');
  if(modal)modal.remove();
}

async function createWorktree(repoName){
  const repo=worktreesData.repositories.find(r=>r.name===repoName);
  if(!repo){
    showToast(currentLang==='pt-BR'?'Repositório não encontrado':'Repository not found','error');
    return;
  }

  const name=document.getElementById('wt-name-input').value.trim();
  const branch=document.getElementById('wt-branch-input').value.trim();

  if(!name||!branch){
    showToast(currentLang==='pt-BR'?'Preencha nome e branch':'Fill in name and branch','error');
    return;
  }

  // loading removed
  try{
    const result=await fetch('/api/worktrees/create',{
      method:'POST',
      headers:{'Content-Type':'application/json'},
      body:JSON.stringify({repoPath:repo.path,worktreeName:name,branch})
    }).then(r=>r.json());

    if(result.success){
      showToast(currentLang==='pt-BR'?'Worktree criada com sucesso!':'Worktree created successfully!','success');
      closeCreateWorktreeModal();
      await loadWorktreesData();
    }else{
      showToast(`${currentLang==='pt-BR'?'Erro ao criar worktree':'Error creating worktree'}: ${result.error}`,'error');
    }
  }catch(err){
    console.error('Error creating worktree:',err);
    showToast(currentLang==='pt-BR'?'Erro ao criar worktree':'Error creating worktree','error');
  }finally{
    // loading removed
  }
}

// ── Worktrees Polling (Step 17) ────────────────────────────────
function startWorktreesPolling(){
  if(worktreesPollingInterval)return;

  console.log(`[WORKTREES] Iniciando polling a cada ${worktreesPollingRate/1000}s`);

  worktreesPollingInterval=setInterval(async()=>{
    if(currentView!=='worktrees'){
      stopWorktreesPolling();
      return;
    }

    console.log('[WORKTREES] Atualizando dados em background...');
    await loadWorktreesData(false); // Atualização silenciosa (sem loading indicators)
  },worktreesPollingRate);
}

function stopWorktreesPolling(){
  if(worktreesPollingInterval){
    console.log('[WORKTREES] Parando polling');
    clearInterval(worktreesPollingInterval);
    worktreesPollingInterval=null;
  }
}

// Init i18n immediately with cached lang
applyI18n();
updateNotifPrefButtons();
document.getElementById('vbtn-repo')?.classList.toggle('active',myPrsView==='repo');
document.getElementById('vbtn-status')?.classList.toggle('active',myPrsView==='status');
loadWorktreesConfigFromBackend().then(updateWorktreesUI);
go();
