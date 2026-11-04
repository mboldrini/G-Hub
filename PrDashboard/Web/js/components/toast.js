/**
 * toast.js - Toast Notification Component
 * Handles temporary notification messages (toasts/snackbars)
 */

export class ToastManager {
  constructor(containerId = 'nstack') {
    this.container = document.getElementById(containerId);
    if (!this.container) {
      console.warn(`Toast container #${containerId} not found`);
    }
    this.defaultDuration = 8000;
    this.activeToasts = new Set();
  }

  /**
   * Show a toast notification
   * @param {Object} options - Toast configuration
   * @param {string} options.type - Toast type: 'comment', 'ci', 'merged', 'approved', 'ready', 'info', 'success', 'warning', 'error'
   * @param {string} options.title - Toast title/main message
   * @param {string} options.body - Toast body/secondary message
   * @param {number} options.duration - Display duration in ms (0 = no auto-close)
   * @param {boolean} options.closeable - Whether to show close button
   * @param {Function} options.onClick - Click handler for toast
   * @returns {HTMLElement} Toast element
   */
  show(options = {}) {
    if (!this.container) return null;

    const {
      type = 'info',
      title = '',
      body = '',
      duration = this.defaultDuration,
      closeable = true,
      onClick = null
    } = options;

    const toast = this.createToast(type, title, body, closeable);

    if (onClick) {
      toast.style.cursor = 'pointer';
      toast.addEventListener('click', (e) => {
        if (!e.target.closest('.nclose')) {
          onClick(e);
        }
      });
    }

    this.container.prepend(toast);
    this.activeToasts.add(toast);

    // Auto-remove after duration
    if (duration > 0) {
      setTimeout(() => {
        this.remove(toast);
      }, duration);
    }

    return toast;
  }

  /**
   * Create toast DOM element
   * @param {string} type - Toast type
   * @param {string} title - Toast title
   * @param {string} body - Toast body
   * @param {boolean} closeable - Show close button
   * @returns {HTMLElement} Toast element
   */
  createToast(type, title, body, closeable) {
    const toast = document.createElement('div');
    toast.className = `notif ${type}`;

    let html = '';

    if (closeable) {
      html += `<button class="nclose" aria-label="Close">×</button>`;
    }

    if (title) {
      html += `<div class="ntitle">${this.escapeHtml(title)}</div>`;
    }

    if (body) {
      html += `<div class="nbody">${this.escapeHtml(body)}</div>`;
    }

    toast.innerHTML = html;

    // Attach close handler
    if (closeable) {
      const closeBtn = toast.querySelector('.nclose');
      if (closeBtn) {
        closeBtn.addEventListener('click', () => this.remove(toast));
      }
    }

    return toast;
  }

  /**
   * Show a PR comment notification
   * @param {Object} pr - PR object with { number, title, repo }
   * @param {string} lang - Language code ('pt-BR' or 'en-US')
   */
  showComment(pr, lang = 'pt-BR') {
    const title = lang === 'en-US'
      ? `💬 New comment on #${pr.number}`
      : `💬 Novo comentário em #${pr.number}`;

    return this.show({
      type: 'comment',
      title,
      body: `${pr.repo} · ${pr.title}`,
      duration: 8000
    });
  }

  /**
   * Show a CI failure notification
   * @param {Object} pr - PR object with { number, title, repo }
   * @param {string} lang - Language code
   */
  showCIFailure(pr, lang = 'pt-BR') {
    const title = lang === 'en-US'
      ? `🔴 CI failed on #${pr.number}`
      : `🔴 CI com erro em #${pr.number}`;

    return this.show({
      type: 'ci',
      title,
      body: `${pr.repo} · ${pr.title}`,
      duration: 8000
    });
  }

  /**
   * Show a PR merged notification
   * @param {Object} pr - PR object with { number, title, repo }
   * @param {string} lang - Language code
   */
  showMerged(pr, lang = 'pt-BR') {
    const title = lang === 'en-US'
      ? `🎉 Merged! #${pr.number}`
      : `🎉 Mergeado! #${pr.number}`;

    const toast = this.show({
      type: 'merged',
      title,
      body: `${pr.repo} · ${pr.title}`,
      duration: 12000
    });

    // Special styling for merged notification
    const titleEl = toast.querySelector('.ntitle');
    if (titleEl) {
      titleEl.style.color = 'var(--green)';
    }

    return toast;
  }

  /**
   * Show a PR approved notification
   * @param {Object} pr - PR object with { number, title, repo, reviewedBy }
   * @param {string} lang - Language code
   */
  showApproved(pr, lang = 'pt-BR') {
    const reviewedBy = pr.reviewedBy || '?';
    const title = lang === 'en-US'
      ? `✅ Approved! #${pr.number} by ${reviewedBy}`
      : `✅ Aprovado! #${pr.number} por ${reviewedBy}`;

    const toast = this.show({
      type: 'approved',
      title,
      body: `${pr.repo} · ${pr.title}`,
      duration: 10000
    });

    // Special styling for approved notification
    const titleEl = toast.querySelector('.ntitle');
    if (titleEl) {
      titleEl.style.color = 'var(--green)';
    }

    return toast;
  }

  /**
   * Show a PR ready for review notification
   * @param {Object} pr - PR object with { number, title, repo }
   * @param {string} lang - Language code
   */
  showReady(pr, lang = 'pt-BR') {
    const title = lang === 'en-US'
      ? `🔵 PR ready for review #${pr.number}`
      : `🔵 PR pronto para review #${pr.number}`;

    return this.show({
      type: 'ready',
      title,
      body: `${pr.repo} · ${pr.title}`,
      duration: 8000
    });
  }

  /**
   * Show a generic info toast
   * @param {string} message - Message to display
   * @param {number} duration - Display duration
   */
  info(message, duration = 5000) {
    return this.show({
      type: 'info',
      title: message,
      duration
    });
  }

  /**
   * Show a success toast
   * @param {string} message - Message to display
   * @param {number} duration - Display duration
   */
  success(message, duration = 5000) {
    return this.show({
      type: 'success',
      title: message,
      duration
    });
  }

  /**
   * Show a warning toast
   * @param {string} message - Message to display
   * @param {number} duration - Display duration
   */
  warning(message, duration = 5000) {
    return this.show({
      type: 'warning',
      title: message,
      duration
    });
  }

  /**
   * Show an error toast
   * @param {string} message - Message to display
   * @param {number} duration - Display duration (0 = manual close only)
   */
  error(message, duration = 0) {
    return this.show({
      type: 'error',
      title: message,
      duration
    });
  }

  /**
   * Remove a toast
   * @param {HTMLElement} toast - Toast element to remove
   */
  remove(toast) {
    if (!toast || !toast.parentNode) return;

    toast.style.opacity = '0';
    toast.style.transform = 'translateX(100%)';

    setTimeout(() => {
      if (toast.parentNode) {
        toast.remove();
      }
      this.activeToasts.delete(toast);
    }, 300);
  }

  /**
   * Remove all active toasts
   */
  clear() {
    for (const toast of this.activeToasts) {
      this.remove(toast);
    }
  }

  /**
   * Escape HTML to prevent XSS
   * @param {string} str - String to escape
   * @returns {string} Escaped string
   */
  escapeHtml(str) {
    if (!str) return '';
    return String(str)
      .replace(/&/g, '&amp;')
      .replace(/</g, '&lt;')
      .replace(/>/g, '&gt;')
      .replace(/"/g, '&quot;')
      .replace(/'/g, '&#039;');
  }
}

// Export singleton instance
export const toast = new ToastManager();
