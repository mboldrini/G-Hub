/**
 * config.js - Configuration Management Module
 * Handles app settings, preferences, and localStorage state
 */

export class ConfigManager {
  constructor() {
    // Default configuration
    this.config = {
      theme: 'dark',
      language: 'pt-BR',
      intervalSeconds: 300,
      sidebarOpen: true,
      myPrsView: 'repo',
      myPrsFilter: null,
      repoOrder: [],
      collapsedRepos: new Set(),
      hideBotReviews: false,
      displayName: null,

      // Notification preferences
      notifications: {
        commentVisual: true,
        commentAudio: true,
        ciVisual: true,
        ciAudio: true,
        mergedVisual: true,
        mergedAudio: true,
        approvedVisual: true,
        approvedAudio: true,
        readyVisual: true,
        readyAudio: true
      },

      // Worktrees configuration
      worktrees: {
        rootPath: null,
        worktreesPath: null
      }
    };

    this.listeners = new Map();
    this.loadFromLocalStorage();
  }

  /**
   * Load configuration from localStorage
   */
  loadFromLocalStorage() {
    try {
      // Load collapsed repos
      const collapsed = localStorage.getItem('prd-collapsed');
      if (collapsed) {
        this.config.collapsedRepos = new Set(JSON.parse(collapsed));
      }

      // Load other persisted config if available
      const saved = localStorage.getItem('prd-config');
      if (saved) {
        const parsed = JSON.parse(saved);
        Object.assign(this.config, parsed);
      }
    } catch (error) {
      console.error('Error loading config from localStorage:', error);
    }
  }

  /**
   * Save configuration to localStorage
   */
  saveToLocalStorage() {
    try {
      // Save collapsed repos separately for backward compatibility
      localStorage.setItem('prd-collapsed', JSON.stringify([...this.config.collapsedRepos]));

      // Save full config (excluding collapsedRepos as it's saved separately)
      const toSave = { ...this.config };
      delete toSave.collapsedRepos;
      localStorage.setItem('prd-config', JSON.stringify(toSave));
    } catch (error) {
      console.error('Error saving config to localStorage:', error);
    }
  }

  /**
   * Get a configuration value
   * @param {string} key - Config key (supports dot notation, e.g., 'notifications.ciVisual')
   * @returns {*} Configuration value
   */
  get(key) {
    const keys = key.split('.');
    let value = this.config;
    for (const k of keys) {
      value = value?.[k];
      if (value === undefined) break;
    }
    return value;
  }

  /**
   * Set a configuration value
   * @param {string} key - Config key (supports dot notation)
   * @param {*} value - New value
   * @param {boolean} persist - Whether to save to localStorage
   */
  set(key, value, persist = true) {
    const keys = key.split('.');
    const lastKey = keys.pop();
    let target = this.config;

    for (const k of keys) {
      if (!target[k]) target[k] = {};
      target = target[k];
    }

    const oldValue = target[lastKey];
    target[lastKey] = value;

    if (persist) {
      this.saveToLocalStorage();
    }

    // Notify listeners
    this.notify(key, value, oldValue);
  }

  /**
   * Update multiple configuration values at once
   * @param {Object} updates - Object with keys and values to update
   * @param {boolean} persist - Whether to save to localStorage
   */
  update(updates, persist = true) {
    for (const [key, value] of Object.entries(updates)) {
      this.set(key, value, false);
    }

    if (persist) {
      this.saveToLocalStorage();
    }
  }

  /**
   * Load configuration from server response
   * @param {Object} serverConfig - Configuration from server
   */
  loadFromServer(serverConfig) {
    if (!serverConfig) return;

    // Map server config to internal config structure
    const mapping = {
      theme: 'theme',
      language: 'language',
      intervalSeconds: 'intervalSeconds',
      sidebarOpen: 'sidebarOpen',
      myPrsView: 'myPrsView',
      myPrsFilter: 'myPrsFilter',
      repoOrder: 'repoOrder',
      hideBotReviews: 'hideBotReviews',
      displayName: 'displayName',

      // Notification preferences (flatten from server structure)
      notifCommentVisual: 'notifications.commentVisual',
      notifCommentAudio: 'notifications.commentAudio',
      notifCiVisual: 'notifications.ciVisual',
      notifCiAudio: 'notifications.ciAudio',
      notifMergedVisual: 'notifications.mergedVisual',
      notifMergedAudio: 'notifications.mergedAudio',
      notifApprovedVisual: 'notifications.approvedVisual',
      notifApprovedAudio: 'notifications.approvedAudio',
      notifReadyVisual: 'notifications.readyVisual',
      notifReadyAudio: 'notifications.readyAudio'
    };

    for (const [serverKey, internalKey] of Object.entries(mapping)) {
      if (serverKey in serverConfig) {
        this.set(internalKey, serverConfig[serverKey], false);
      }
    }

    // Handle collapsedRepos separately
    if (serverConfig.collapsedRepos) {
      this.config.collapsedRepos = new Set(serverConfig.collapsedRepos);
    }

    this.saveToLocalStorage();
  }

  /**
   * Get configuration object to send to server
   * @returns {Object} Server-compatible configuration
   */
  toServerFormat() {
    return {
      theme: this.config.theme,
      language: this.config.language,
      intervalSeconds: this.config.intervalSeconds,
      sidebarOpen: this.config.sidebarOpen,
      myPrsView: this.config.myPrsView,
      myPrsFilter: this.config.myPrsFilter,
      repoOrder: this.config.repoOrder,
      collapsedRepos: [...this.config.collapsedRepos],
      hideBotReviews: this.config.hideBotReviews,
      displayName: this.config.displayName,

      // Notification preferences
      notifCommentVisual: this.config.notifications.commentVisual,
      notifCommentAudio: this.config.notifications.commentAudio,
      notifCiVisual: this.config.notifications.ciVisual,
      notifCiAudio: this.config.notifications.ciAudio,
      notifMergedVisual: this.config.notifications.mergedVisual,
      notifMergedAudio: this.config.notifications.mergedAudio,
      notifApprovedVisual: this.config.notifications.approvedVisual,
      notifApprovedAudio: this.config.notifications.approvedAudio,
      notifReadyVisual: this.config.notifications.readyVisual,
      notifReadyAudio: this.config.notifications.readyAudio
    };
  }

  /**
   * Toggle a boolean configuration value
   * @param {string} key - Config key
   * @param {boolean} persist - Whether to save to localStorage
   * @returns {boolean} New value
   */
  toggle(key, persist = true) {
    const currentValue = this.get(key);
    const newValue = !currentValue;
    this.set(key, newValue, persist);
    return newValue;
  }

  /**
   * Register a listener for configuration changes
   * @param {string} key - Config key to listen for (or '*' for all changes)
   * @param {Function} callback - Callback function (receives key, newValue, oldValue)
   */
  onChange(key, callback) {
    if (!this.listeners.has(key)) {
      this.listeners.set(key, []);
    }
    this.listeners.get(key).push(callback);
  }

  /**
   * Notify listeners of configuration changes
   * @param {string} key - Changed config key
   * @param {*} newValue - New value
   * @param {*} oldValue - Old value
   */
  notify(key, newValue, oldValue) {
    // Notify specific key listeners
    if (this.listeners.has(key)) {
      for (const callback of this.listeners.get(key)) {
        try {
          callback(key, newValue, oldValue);
        } catch (error) {
          console.error('Error in config listener:', error);
        }
      }
    }

    // Notify wildcard listeners
    if (this.listeners.has('*')) {
      for (const callback of this.listeners.get('*')) {
        try {
          callback(key, newValue, oldValue);
        } catch (error) {
          console.error('Error in config listener:', error);
        }
      }
    }
  }

  /**
   * Reset configuration to defaults
   */
  reset() {
    this.config = {
      theme: 'dark',
      language: 'pt-BR',
      intervalSeconds: 300,
      sidebarOpen: true,
      myPrsView: 'repo',
      myPrsFilter: null,
      repoOrder: [],
      collapsedRepos: new Set(),
      hideBotReviews: false,
      displayName: null,
      notifications: {
        commentVisual: true,
        commentAudio: true,
        ciVisual: true,
        ciAudio: true,
        mergedVisual: true,
        mergedAudio: true,
        approvedVisual: true,
        approvedAudio: true,
        readyVisual: true,
        readyAudio: true
      },
      worktrees: {
        rootPath: null,
        worktreesPath: null
      }
    };
    this.saveToLocalStorage();
  }
}

// Export singleton instance
export const config = new ConfigManager();
