/**
 * dropdown.js - Dropdown Component
 * Handles dropdown menus and select-like components
 */

export class Dropdown {
  constructor(triggerId, contentId, options = {}) {
    this.trigger = document.getElementById(triggerId);
    this.content = document.getElementById(contentId);
    this.options = {
      closeOnClick: true,
      closeOnClickOutside: true,
      position: 'bottom-right', // 'bottom-right', 'bottom-left', 'top-right', 'top-left'
      onOpen: null,
      onClose: null,
      ...options
    };

    if (!this.trigger || !this.content) {
      console.warn(`Dropdown elements #${triggerId} or #${contentId} not found`);
      return;
    }

    this.isOpen = false;
    this.setupEventListeners();
  }

  /**
   * Setup event listeners
   */
  setupEventListeners() {
    // Toggle on trigger click
    this.trigger.addEventListener('click', (e) => {
      e.stopPropagation();
      this.toggle();
    });

    // Close on content click (if enabled)
    if (this.options.closeOnClick) {
      this.content.addEventListener('click', (e) => {
        // Don't close if clicking on input elements
        if (!e.target.closest('input, select, textarea')) {
          this.close();
        }
      });
    }

    // Close on outside click
    if (this.options.closeOnClickOutside) {
      this.outsideClickHandler = (e) => {
        if (this.isOpen &&
            !this.trigger.contains(e.target) &&
            !this.content.contains(e.target)) {
          this.close();
        }
      };
      document.addEventListener('click', this.outsideClickHandler);
    }
  }

  /**
   * Open dropdown
   */
  open() {
    if (!this.content || this.isOpen) return;

    this.content.style.display = 'block';
    this.isOpen = true;

    // Position the dropdown
    this.position();

    if (this.options.onOpen) {
      try {
        this.options.onOpen();
      } catch (error) {
        console.error('Error in dropdown onOpen callback:', error);
      }
    }
  }

  /**
   * Close dropdown
   */
  close() {
    if (!this.content || !this.isOpen) return;

    this.content.style.display = 'none';
    this.isOpen = false;

    if (this.options.onClose) {
      try {
        this.options.onClose();
      } catch (error) {
        console.error('Error in dropdown onClose callback:', error);
      }
    }
  }

  /**
   * Toggle dropdown
   */
  toggle() {
    if (this.isOpen) {
      this.close();
    } else {
      this.open();
    }
  }

  /**
   * Position the dropdown relative to trigger
   */
  position() {
    if (!this.trigger || !this.content) return;

    const triggerRect = this.trigger.getBoundingClientRect();
    const contentRect = this.content.getBoundingClientRect();

    // Reset position
    this.content.style.position = 'absolute';
    this.content.style.top = '';
    this.content.style.bottom = '';
    this.content.style.left = '';
    this.content.style.right = '';

    // Apply position based on option
    switch (this.options.position) {
      case 'bottom-left':
        this.content.style.top = `${triggerRect.bottom}px`;
        this.content.style.left = `${triggerRect.left}px`;
        break;
      case 'top-right':
        this.content.style.bottom = `${window.innerHeight - triggerRect.top}px`;
        this.content.style.right = `${window.innerWidth - triggerRect.right}px`;
        break;
      case 'top-left':
        this.content.style.bottom = `${window.innerHeight - triggerRect.top}px`;
        this.content.style.left = `${triggerRect.left}px`;
        break;
      case 'bottom-right':
      default:
        this.content.style.top = `${triggerRect.bottom}px`;
        this.content.style.right = `${window.innerWidth - triggerRect.right}px`;
        break;
    }
  }

  /**
   * Destroy dropdown (remove event listeners)
   */
  destroy() {
    if (this.outsideClickHandler) {
      document.removeEventListener('click', this.outsideClickHandler);
    }
  }
}

/**
 * Dropdown Manager - Manages multiple dropdowns
 */
export class DropdownManager {
  constructor() {
    this.dropdowns = new Map();
  }

  /**
   * Register a dropdown
   * @param {string} id - Dropdown ID
   * @param {string} triggerId - Trigger element ID
   * @param {string} contentId - Content element ID
   * @param {Object} options - Dropdown options
   * @returns {Dropdown} Dropdown instance
   */
  register(id, triggerId, contentId, options = {}) {
    if (this.dropdowns.has(id)) {
      return this.dropdowns.get(id);
    }

    const dropdown = new Dropdown(triggerId, contentId, {
      ...options,
      onOpen: () => {
        // Close other dropdowns when this one opens
        this.closeAllExcept(id);
        if (options.onOpen) options.onOpen();
      }
    });

    this.dropdowns.set(id, dropdown);
    return dropdown;
  }

  /**
   * Get a registered dropdown
   * @param {string} id - Dropdown ID
   * @returns {Dropdown|null} Dropdown instance or null
   */
  get(id) {
    return this.dropdowns.get(id) || null;
  }

  /**
   * Open a dropdown by ID
   * @param {string} id - Dropdown ID
   */
  open(id) {
    const dropdown = this.get(id);
    if (dropdown) {
      dropdown.open();
    }
  }

  /**
   * Close a dropdown by ID
   * @param {string} id - Dropdown ID
   */
  close(id) {
    const dropdown = this.get(id);
    if (dropdown) {
      dropdown.close();
    }
  }

  /**
   * Toggle a dropdown by ID
   * @param {string} id - Dropdown ID
   */
  toggle(id) {
    const dropdown = this.get(id);
    if (dropdown) {
      dropdown.toggle();
    }
  }

  /**
   * Close all dropdowns
   */
  closeAll() {
    for (const dropdown of this.dropdowns.values()) {
      if (dropdown.isOpen) {
        dropdown.close();
      }
    }
  }

  /**
   * Close all dropdowns except one
   * @param {string} exceptId - ID of dropdown to keep open
   */
  closeAllExcept(exceptId) {
    for (const [id, dropdown] of this.dropdowns.entries()) {
      if (id !== exceptId && dropdown.isOpen) {
        dropdown.close();
      }
    }
  }

  /**
   * Unregister and destroy a dropdown
   * @param {string} id - Dropdown ID
   */
  unregister(id) {
    const dropdown = this.dropdowns.get(id);
    if (dropdown) {
      dropdown.destroy();
      this.dropdowns.delete(id);
    }
  }

  /**
   * Destroy all dropdowns
   */
  destroyAll() {
    for (const dropdown of this.dropdowns.values()) {
      dropdown.destroy();
    }
    this.dropdowns.clear();
  }
}

/**
 * Simple dropdown menu helper
 * Creates a dropdown from a button and a list of options
 * @param {string} buttonId - Button element ID
 * @param {Array} options - Array of {label, value, onClick} objects
 * @param {Object} config - Configuration options
 * @returns {Dropdown} Dropdown instance
 */
export function createDropdownMenu(buttonId, options, config = {}) {
  const button = document.getElementById(buttonId);
  if (!button) {
    console.warn(`Button #${buttonId} not found`);
    return null;
  }

  // Create dropdown content
  const contentId = `${buttonId}-dropdown`;
  let content = document.getElementById(contentId);

  if (!content) {
    content = document.createElement('div');
    content.id = contentId;
    content.style.cssText = `
      display: none;
      position: absolute;
      background: var(--card);
      border: 1px solid var(--border);
      border-radius: 8px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
      min-width: 180px;
      padding: 6px;
      z-index: 1000;
    `;
    document.body.appendChild(content);
  }

  // Populate menu
  content.innerHTML = options.map((opt, index) => {
    if (opt.divider) {
      return `<div style="height: 1px; background: var(--border); margin: 4px 0"></div>`;
    }

    const disabled = opt.disabled ? 'opacity: 0.5; cursor: not-allowed;' : '';
    return `
      <button
        data-option-index="${index}"
        style="
          display: block;
          width: 100%;
          padding: 8px 12px;
          border: none;
          background: transparent;
          color: var(--text);
          text-align: left;
          cursor: pointer;
          border-radius: 6px;
          font-size: 14px;
          ${disabled}
        "
        ${opt.disabled ? 'disabled' : ''}
      >
        ${opt.icon || ''} ${escapeHtml(opt.label)}
      </button>
    `;
  }).join('');

  // Add hover effects
  content.querySelectorAll('button:not([disabled])').forEach(btn => {
    btn.addEventListener('mouseenter', () => {
      btn.style.background = 'var(--hover)';
    });
    btn.addEventListener('mouseleave', () => {
      btn.style.background = 'transparent';
    });
  });

  // Add click handlers
  content.addEventListener('click', (e) => {
    const btn = e.target.closest('button[data-option-index]');
    if (btn && !btn.disabled) {
      const index = parseInt(btn.dataset.optionIndex);
      const option = options[index];
      if (option && option.onClick) {
        option.onClick(option.value, option);
      }
    }
  });

  // Create dropdown
  return new Dropdown(buttonId, contentId, config);
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
export const dropdownManager = new DropdownManager();
