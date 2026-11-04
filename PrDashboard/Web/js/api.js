/**
 * api.js - API Client Module
 * Handles all HTTP requests to the backend API
 */

export const ApiClient = {
  /**
   * Fetch dashboard data (PRs, reviews, merged)
   * @returns {Promise<Object>} Dashboard data
   */
  async fetchDashboard() {
    try {
      const response = await fetch('/api/dashboard');
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();
    } catch (error) {
      console.error('Error fetching dashboard:', error);
      throw error;
    }
  },

  /**
   * Save settings to backend
   * @param {Object} settings - Settings object to save
   * @returns {Promise<Object>} Updated settings
   */
  async saveSettings(settings) {
    try {
      const response = await fetch('/api/settings', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(settings)
      });
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();
    } catch (error) {
      console.error('Error saving settings:', error);
      throw error;
    }
  },

  /**
   * Load statistics data
   * @returns {Promise<Object>} Statistics data
   */
  async loadStats() {
    try {
      const response = await fetch('/stats-data.json');
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();
    } catch (error) {
      console.error('Error loading stats:', error);
      return { entries: [] };
    }
  },

  /**
   * Load worktrees configuration
   * @returns {Promise<Object>} Worktrees config
   */
  async loadWorktreesConfig() {
    try {
      const response = await fetch('/api/worktrees/config');
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();
    } catch (error) {
      console.error('Error loading worktrees config:', error);
      return {};
    }
  },

  /**
   * Save worktrees configuration
   * @param {Object} config - Worktrees configuration
   * @returns {Promise<Object>} Response
   */
  async saveWorktreesConfig(config) {
    try {
      const response = await fetch('/api/worktrees/config', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(config)
      });
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      return await response.json();
    } catch (error) {
      console.error('Error saving worktrees config:', error);
      throw error;
    }
  },

  /**
   * Log notification event to server
   * @param {string} type - Notification type
   * @param {Object} pr - PR object
   */
  async logNotification(type, pr) {
    try {
      await fetch('/api/notify-log', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          type,
          repo: pr.repo,
          prNumber: pr.number,
          prTitle: pr.title
        })
      });
    } catch (error) {
      console.error('Error logging notification:', error);
    }
  },

  /**
   * Run backfill for a specific month
   * @param {string} month - Month in format 'YYYY-MM'
   * @param {Function} onProgress - Progress callback
   * @returns {Promise<Object>} Backfill result
   */
  async runBackfill(month, onProgress) {
    try {
      const response = await fetch('/api/backfill', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ month })
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const reader = response.body.getReader();
      const decoder = new TextDecoder();
      let buffer = '';

      while (true) {
        const { done, value } = await reader.read();
        if (done) break;

        buffer += decoder.decode(value, { stream: true });
        const lines = buffer.split('\n');
        buffer = lines.pop() || '';

        for (const line of lines) {
          if (line.trim()) {
            try {
              const data = JSON.parse(line);
              if (onProgress) onProgress(data);
            } catch (e) {
              console.warn('Failed to parse progress line:', line);
            }
          }
        }
      }

      return { success: true };
    } catch (error) {
      console.error('Error running backfill:', error);
      throw error;
    }
  },

  /**
   * Export statistics as CSV
   * @param {Array} entries - Statistics entries
   * @param {string} filename - Export filename
   */
  async exportCSV(entries, filename = 'prs-export.csv') {
    try {
      const response = await fetch('/api/export/csv', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ entries, filename })
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = filename;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    } catch (error) {
      console.error('Error exporting CSV:', error);
      throw error;
    }
  }
};
