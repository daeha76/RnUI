// RnUI JavaScript Interop Module
// Handles: focus trap, body scroll lock, click-outside, escape listeners

const _listeners = new Map(); // id -> { type, handler, element }

// ─── Body Scroll Lock ──────────────────────────────────────────────────────────

let _scrollLockCount = 0;
let _savedOverflow = '';

export function lockBodyScroll() {
    if (_scrollLockCount === 0) {
        _savedOverflow = document.body.style.overflow;
        document.body.style.overflow = 'hidden';
    }
    _scrollLockCount++;
}

export function unlockBodyScroll() {
    if (_scrollLockCount > 0) {
        _scrollLockCount--;
        if (_scrollLockCount === 0) {
            document.body.style.overflow = _savedOverflow;
        }
    }
}

// ─── Focus Trap ───────────────────────────────────────────────────────────────

const FOCUSABLE_SELECTORS = [
    'a[href]',
    'button:not([disabled])',
    'input:not([disabled])',
    'select:not([disabled])',
    'textarea:not([disabled])',
    '[tabindex]:not([tabindex="-1"])',
    '[contenteditable="true"]'
].join(',');

function getFocusableElements(container) {
    return Array.from(container.querySelectorAll(FOCUSABLE_SELECTORS))
        .filter(el => !el.closest('[hidden]') && getComputedStyle(el).display !== 'none');
}

export function enableFocusTrap(id, element) {
    if (!element) return;

    const focusableEls = getFocusableElements(element);
    if (focusableEls.length > 0) {
        focusableEls[0].focus();
    }

    const handler = (e) => {
        if (e.key !== 'Tab') return;
        const focusable = getFocusableElements(element);
        if (focusable.length === 0) { e.preventDefault(); return; }

        const first = focusable[0];
        const last = focusable[focusable.length - 1];

        if (e.shiftKey) {
            if (document.activeElement === first) {
                e.preventDefault();
                last.focus();
            }
        } else {
            if (document.activeElement === last) {
                e.preventDefault();
                first.focus();
            }
        }
    };

    document.addEventListener('keydown', handler);
    _storeListener(id, 'focustrap', handler, document);
}

export function disableFocusTrap(id) {
    _removeListener(id, 'focustrap');
}

// ─── Escape Key Listener ─────────────────────────────────────────────────────

export function addEscapeListener(id, dotNetRef, methodName) {
    const handler = (e) => {
        if (e.key === 'Escape') {
            dotNetRef.invokeMethodAsync(methodName);
        }
    };
    document.addEventListener('keydown', handler);
    _storeListener(id, 'escape', handler, document);
}

export function removeEscapeListener(id) {
    _removeListener(id, 'escape');
}

// ─── Click Outside Listener ──────────────────────────────────────────────────

export function addClickOutsideListener(id, element, dotNetRef, methodName) {
    if (!element) return;

    // Delay to avoid the triggering click from being counted as "outside"
    setTimeout(() => {
        const handler = (e) => {
            if (!element.contains(e.target)) {
                dotNetRef.invokeMethodAsync(methodName);
            }
        };
        document.addEventListener('click', handler, true);
        _storeListener(id, 'clickoutside', handler, document, true);
    }, 0);
}

export function removeClickOutsideListener(id) {
    _removeListener(id, 'clickoutside');
}

// ─── Dispose All ─────────────────────────────────────────────────────────────

export function disposeAll(id) {
    const keyPrefix = `${id}:`;
    for (const [key] of _listeners) {
        if (key.startsWith(keyPrefix)) {
            _removeListenerByKey(key);
        }
    }
}

// ─── Internal Helpers ────────────────────────────────────────────────────────

function _storeListener(id, type, handler, target, useCapture = false) {
    const key = `${id}:${type}`;
    // Remove previous listener of same type if exists
    if (_listeners.has(key)) {
        _removeListenerByKey(key);
    }
    _listeners.set(key, { handler, target, useCapture });
}

function _removeListener(id, type) {
    const key = `${id}:${type}`;
    _removeListenerByKey(key);
}

function _removeListenerByKey(key) {
    const entry = _listeners.get(key);
    if (!entry) return;
    entry.target.removeEventListener(
        key.includes('clickoutside') ? 'click' : 'keydown',
        entry.handler,
        entry.useCapture
    );
    _listeners.delete(key);
}
