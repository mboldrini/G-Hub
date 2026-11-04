/**
 * modal.js - Modal/Dialog Component
 * Handles modal dialogs and overlays
 */

export class Modal {
  constructor(id, options = {}) {
    this.id = id;
    this.element = document.getElementById(id);
    this.options = {
      closeOnEscape: true,
      closeOnBackdropClick: true,
      onOpen: null,
      onClose: null,
      ...options
    };

    if (!this.element) {
      console.warn(`Modal element #${id} not found`);
      return;
    }

    this.setupEventListeners();
  }

  /**
   * Setup event listeners for modal
   */
  setupEventListeners() {
    // Close on backdrop click
    if (this.options.closeOnBackdropClick) {
      this.element.addEventListener('click', (e) => {
        if (e.target === this.element) {
          this.close();
        }
      });
    }

    // Close on escape key
    if (this.options.closeOnEscape) {
      this.escapeHandler = (e) => {
        if (e.key === 'Escape' && this.isOpen()) {
          this.close();
        }
      };
      document.addEventListener('keydown', this.escapeHandler);
    }

    // Setup close buttons
    const closeButtons = this.element.querySelectorAll('[data-modal-close]');
    closeButtons.forEach(btn => {
      btn.addEventListener('click', () => this.close());
    });
  }

  /**
   * Open the modal
   * @param {Object} data - Optional data to pass to onOpen callback
   */
  open(data = null) {
    if (!this.element) return;

    this.element.style.display = 'flex';
    document.body.style.overflow = 'hidden'; // Prevent body scroll

    if (this.options.onOpen) {
      try {
        this.options.onOpen(data);
      } catch (error) {
        console.error('Error in modal onOpen callback:', error);
      }
    }
  }

  /**
   * Close the modal
   */
  close() {
    if (!this.element) return;

    this.element.style.display = 'none';
    document.body.style.overflow = ''; // Restore body scroll

    if (this.options.onClose) {
      try {
        this.options.onClose();
      } catch (error) {
        console.error('Error in modal onClose callback:', error);
      }
    }
  }

  /**
   * Toggle modal visibility
   */
  toggle() {
    if (this.isOpen()) {
      this.close();
    } else {
      this.open();
    }
  }

  /**
   * Check if modal is open
   * @returns {boolean} True if modal is visible
   */
  isOpen() {
    if (!this.element) return false;
    return this.element.style.display !== 'none';
  }

  /**
   * Destroy the modal (remove event listeners)
   */
  destroy() {
    if (this.escapeHandler) {
      document.removeEventListener('keydown', this.escapeHandler);
    }
  }
}

/**
 * Modal Manager - Manages multiple modals
 */
export class ModalManager {
  constructor() {
    this.modals = new Map();
  }

  /**
   * Register a modal
   * @param {string} id - Modal element ID
   * @param {Object} options - Modal options
   * @returns {Modal} Modal instance
   */
  register(id, options = {}) {
    if (this.modals.has(id)) {
      return this.modals.get(id);
    }

    const modal = new Modal(id, options);
    this.modals.set(id, modal);
    return modal;
  }

  /**
   * Get a registered modal
   * @param {string} id - Modal ID
   * @returns {Modal|null} Modal instance or null
   */
  get(id) {
    return this.modals.get(id) || null;
  }

  /**
   * Open a modal by ID
   * @param {string} id - Modal ID
   * @param {Object} data - Optional data
   */
  open(id, data = null) {
    const modal = this.get(id);
    if (modal) {
      modal.open(data);
    }
  }

  /**
   * Close a modal by ID
   * @param {string} id - Modal ID
   */
  close(id) {
    const modal = this.get(id);
    if (modal) {
      modal.close();
    }
  }

  /**
   * Close all open modals
   */
  closeAll() {
    for (const modal of this.modals.values()) {
      if (modal.isOpen()) {
        modal.close();
      }
    }
  }

  /**
   * Unregister and destroy a modal
   * @param {string} id - Modal ID
   */
  unregister(id) {
    const modal = this.modals.get(id);
    if (modal) {
      modal.destroy();
      this.modals.delete(id);
    }
  }

  /**
   * Destroy all modals
   */
  destroyAll() {
    for (const [id, modal] of this.modals.entries()) {
      modal.destroy();
    }
    this.modals.clear();
  }
}

/**
 * Simple confirmation dialog
 * @param {string} message - Confirmation message
 * @param {string} title - Dialog title
 * @returns {Promise<boolean>} True if confirmed, false if cancelled
 */
export function confirm(message, title = 'Confirmar') {
  return new Promise((resolve) => {
    // Create temporary modal
    const overlay = document.createElement('div');
    overlay.style.cssText = `
      position: fixed;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      background: rgba(0, 0, 0, 0.6);
      display: flex;
      align-items: center;
      justify-content: center;
      z-index: 9999;
    `;

    const modal = document.createElement('div');
    modal.style.cssText = `
      background: var(--card);
      border: 1px solid var(--border);
      border-radius: 8px;
      padding: 24px;
      max-width: 400px;
      width: 90%;
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.3);
    `;

    modal.innerHTML = `
      <div style="font-size: 18px; font-weight: 600; margin-bottom: 12px; color: var(--text)">
        ${escapeHtml(title)}
      </div>
      <div style="font-size: 14px; color: var(--muted); margin-bottom: 20px; line-height: 1.5">
        ${escapeHtml(message)}
      </div>
      <div style="display: flex; gap: 8px; justify-content: flex-end">
        <button id="modal-cancel" style="
          padding: 8px 16px;
          border: 1px solid var(--border);
          background: transparent;
          color: var(--text);
          border-radius: 6px;
          cursor: pointer;
          font-size: 14px;
        ">Cancelar</button>
        <button id="modal-confirm" style="
          padding: 8px 16px;
          border: none;
          background: var(--primary);
          color: white;
          border-radius: 6px;
          cursor: pointer;
          font-size: 14px;
        ">Confirmar</button>
      </div>
    `;

    overlay.appendChild(modal);
    document.body.appendChild(overlay);

    const cleanup = () => {
      document.body.removeChild(overlay);
    };

    modal.querySelector('#modal-confirm').addEventListener('click', () => {
      cleanup();
      resolve(true);
    });

    modal.querySelector('#modal-cancel').addEventListener('click', () => {
      cleanup();
      resolve(false);
    });

    overlay.addEventListener('click', (e) => {
      if (e.target === overlay) {
        cleanup();
        resolve(false);
      }
    });
  });
}

/**
 * Simple prompt dialog
 * @param {string} message - Prompt message
 * @param {string} defaultValue - Default input value
 * @param {string} title - Dialog title
 * @returns {Promise<string|null>} Input value or null if cancelled
 */
export function prompt(message, defaultValue = '', title = 'Entrada') {
  return new Promise((resolve) => {
    // Create temporary modal
    const overlay = document.createElement('div');
    overlay.style.cssText = `
      position: fixed;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      background: rgba(0, 0, 0, 0.6);
      display: flex;
      align-items: center;
      justify-content: center;
      z-index: 9999;
    `;

    const modal = document.createElement('div');
    modal.style.cssText = `
      background: var(--card);
      border: 1px solid var(--border);
      border-radius: 8px;
      padding: 24px;
      max-width: 400px;
      width: 90%;
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.3);
    `;

    modal.innerHTML = `
      <div style="font-size: 18px; font-weight: 600; margin-bottom: 12px; color: var(--text)">
        ${escapeHtml(title)}
      </div>
      <div style="font-size: 14px; color: var(--muted); margin-bottom: 12px">
        ${escapeHtml(message)}
      </div>
      <input type="text" id="modal-input" value="${escapeHtml(defaultValue)}" style="
        width: 100%;
        padding: 8px 12px;
        border: 1px solid var(--border);
        background: var(--bg);
        color: var(--text);
        border-radius: 6px;
        font-size: 14px;
        margin-bottom: 20px;
      " />
      <div style="display: flex; gap: 8px; justify-content: flex-end">
        <button id="modal-cancel" style="
          padding: 8px 16px;
          border: 1px solid var(--border);
          background: transparent;
          color: var(--text);
          border-radius: 6px;
          cursor: pointer;
          font-size: 14px;
        ">Cancelar</button>
        <button id="modal-ok" style="
          padding: 8px 16px;
          border: none;
          background: var(--primary);
          color: white;
          border-radius: 6px;
          cursor: pointer;
          font-size: 14px;
        ">OK</button>
      </div>
    `;

    overlay.appendChild(modal);
    document.body.appendChild(overlay);

    const input = modal.querySelector('#modal-input');
    input.focus();
    input.select();

    const cleanup = () => {
      document.body.removeChild(overlay);
    };

    const submit = () => {
      const value = input.value.trim();
      cleanup();
      resolve(value || null);
    };

    modal.querySelector('#modal-ok').addEventListener('click', submit);

    modal.querySelector('#modal-cancel').addEventListener('click', () => {
      cleanup();
      resolve(null);
    });

    input.addEventListener('keydown', (e) => {
      if (e.key === 'Enter') {
        submit();
      } else if (e.key === 'Escape') {
        cleanup();
        resolve(null);
      }
    });

    overlay.addEventListener('click', (e) => {
      if (e.target === overlay) {
        cleanup();
        resolve(null);
      }
    });
  });
}

/**
 * Escape HTML to prevent XSS
 * @param {string} str - String to escape
 * @returns {string} Escaped string
 */
function escapeHtml(str) {
  if (!str) return '';
  return String(str)
    .replace(/&/g, '&amp;')
    .replace(/</g, '&lt;')
    .replace(/>/g, '&gt;')
    .replace(/"/g, '&quot;')
    .replace(/'/g, '&#039;');
}

// Export singleton instance
export const modalManager = new ModalManager();
