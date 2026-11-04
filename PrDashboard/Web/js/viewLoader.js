// ═══════════════════════════════════════════════════════════════
// View Loader Module
// Dynamically loads HTML views and initializes view-specific logic
// ═══════════════════════════════════════════════════════════════

import * as Dashboard from './views/dashboard.js';
import * as Statistics from './views/statistics.js';

// ── View Loading ───────────────────────────────────────────────
const viewCache = {};

export async function loadView(viewName) {
  // Check cache first
  if (viewCache[viewName]) {
    return viewCache[viewName];
  }

  try {
    const response = await fetch(`/views/${viewName}.html`);
    if (!response.ok) {
      throw new Error(`Failed to load view: ${viewName}`);
    }
    const html = await response.text();
    viewCache[viewName] = html;
    return html;
  } catch (error) {
    console.error(`Error loading view ${viewName}:`, error);
    return null;
  }
}

export function injectView(containerId, html) {
  const container = document.getElementById(containerId);
  if (container && html) {
    container.innerHTML = html;
    return true;
  }
  return false;
}

// ── View Initialization ────────────────────────────────────────
export async function initView(viewName) {
  switch (viewName) {
    case 'dashboard':
      // Dashboard view is already in the main HTML
      // Just ensure modules are ready
      return true;

    case 'statistics':
      // Initialize statistics view
      if (typeof Statistics.buildCharts === 'function') {
        Statistics.st_syncI18n();
        Statistics.buildCharts();
        await Statistics.loadData();
      }
      return true;

    case 'worktrees':
      // Worktrees view initialization (if needed)
      return true;

    default:
      console.warn(`Unknown view: ${viewName}`);
      return false;
  }
}

// ── Expose to Global Scope ─────────────────────────────────────
// Make view functions available globally for onclick handlers
window.loadView = loadView;
window.injectView = injectView;
window.initView = initView;

// Expose Dashboard functions
window.setMyView = Dashboard.setMyView;
window.setMyFilter = Dashboard.setMyFilter;
window.setMySearch = Dashboard.setMySearch;
window.setRevSearch = Dashboard.setRevSearch;
window.setMrgSearch = Dashboard.setMrgSearch;
window.setRevFilter = Dashboard.setRevFilter;
window.setRevAuthorFilter = Dashboard.setRevAuthorFilter;
window.togglePrcDetail = Dashboard.togglePrcDetail;
window.toggleMoreCmts = Dashboard.toggleMoreCts;
window.renderMyPrs = Dashboard.renderMyPrs;
window.renderRevFull = Dashboard.renderRevFull;
window.renderMrgFull = Dashboard.renderMrgFull;
window.renderRevSidebar = Dashboard.renderRevSidebar;
window.renderMrgSidebar = Dashboard.renderMrgSidebar;
window.updateMetrics = Dashboard.updateMetrics;
window.renderStatusFilters = Dashboard.renderStatusFilters;
window.reRenderMyPrs = Dashboard.reRenderMyPrs;
window.reRenderRevPrs = Dashboard.reRenderRevPrs;

// Expose Statistics functions
window.toggleMS = Statistics.toggleMS;
window.toggleMSItem = Statistics.toggleMSItem;
window.onFilterChange = Statistics.onFilterChange;
window.resetFilters = Statistics.resetFilters;
window.goToPRPage = Statistics.goToPRPage;
window.setPRSort = Statistics.setPRSort;
window.setPRFilter = Statistics.setPRFilter;
window.resetPRFilters = Statistics.resetPRFilters;
window.onPRSearch = Statistics.onPRSearch;
window.togglePRExpand = Statistics.togglePRExpand;
window.toggleExportMenu = Statistics.toggleExportMenu;
window.exportCSV = Statistics.exportCSV;
window.exportMD = Statistics.exportMD;
window.exportPDF = Statistics.exportPDF;
window.st_syncI18n = Statistics.st_syncI18n;
window.st_applyChartTheme = Statistics.st_applyChartTheme;
window.buildCharts = Statistics.buildCharts;
window.loadData = Statistics.loadData;
window.st_render = Statistics.st_render;

// Signal that modules are loaded
window._modulesLoaded = true;
