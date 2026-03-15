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

// ─── Keyboard Shortcut Listener ─────────────────────────────────────────────

export function addKeyboardShortcutListener(id, dotNetRef, methodName) {
    const handler = (e) => {
        if ((e.ctrlKey || e.metaKey) && e.key === 'b') {
            e.preventDefault();
            dotNetRef.invokeMethodAsync(methodName);
        }
    };
    document.addEventListener('keydown', handler);
    _storeListener(id, 'keyboardshortcut', handler, document);
}

export function removeKeyboardShortcutListener(id) {
    _removeListener(id, 'keyboardshortcut');
}

// ─── Element Position ────────────────────────────────────────────────────────

export function getElementRect(element) {
    if (!element) return null;
    const rect = element.getBoundingClientRect();
    return {
        top: rect.top,
        left: rect.left,
        bottom: rect.bottom,
        right: rect.right,
        width: rect.width,
        height: rect.height
    };
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

// ─── Scroll Area ────────────────────────────────────────────────────────────

const _scrollAreas = new Map();

export function initScrollArea(id, viewport) {
    if (!viewport) return;

    const root = viewport.closest('[data-slot="scroll-area"]');
    if (!root) return;

    const state = { viewport, root, resizeObserver: null, scrollHandler: null, thumbHandlers: [] };

    const update = () => {
        const { scrollHeight, clientHeight, scrollWidth, clientWidth, scrollTop, scrollLeft } = viewport;

        // Vertical scrollbar
        const vBar = root.querySelector('[data-slot="scroll-area-scrollbar"][data-orientation="vertical"]');
        if (vBar) {
            const vRatio = clientHeight / scrollHeight;
            const vThumb = vBar.querySelector('[data-slot="scroll-area-thumb"]');
            if (vRatio >= 1) {
                vBar.style.display = 'none';
            } else {
                vBar.style.display = '';
                if (vThumb) {
                    const thumbHeight = Math.max(vRatio * 100, 10);
                    vThumb.style.height = thumbHeight + '%';
                    vThumb.style.width = '';
                    const scrollRatio = scrollTop / (scrollHeight - clientHeight);
                    const availableTrack = 100 - thumbHeight;
                    vThumb.style.transform = `translateY(${scrollRatio * availableTrack / thumbHeight * 100}%)`;
                }
            }
        }

        // Horizontal scrollbar
        const hBar = root.querySelector('[data-slot="scroll-area-scrollbar"][data-orientation="horizontal"]');
        if (hBar) {
            const hRatio = clientWidth / scrollWidth;
            const hThumb = hBar.querySelector('[data-slot="scroll-area-thumb"]');
            if (hRatio >= 1) {
                hBar.style.display = 'none';
            } else {
                hBar.style.display = '';
                if (hThumb) {
                    const thumbWidth = Math.max(hRatio * 100, 10);
                    hThumb.style.width = thumbWidth + '%';
                    hThumb.style.height = '';
                    const scrollRatio = scrollLeft / (scrollWidth - clientWidth);
                    const availableTrack = 100 - thumbWidth;
                    hThumb.style.transform = `translateX(${scrollRatio * availableTrack / thumbWidth * 100}%)`;
                }
            }
        }
    };

    // Scroll listener
    state.scrollHandler = () => requestAnimationFrame(update);
    viewport.addEventListener('scroll', state.scrollHandler, { passive: true });

    // Resize observer for viewport and content
    state.resizeObserver = new ResizeObserver(() => requestAnimationFrame(update));
    state.resizeObserver.observe(viewport);
    if (viewport.firstElementChild) {
        state.resizeObserver.observe(viewport.firstElementChild);
    }

    // Thumb drag handlers
    const setupThumbDrag = (scrollbar) => {
        const thumb = scrollbar.querySelector('[data-slot="scroll-area-thumb"]');
        if (!thumb) return;

        const orientation = scrollbar.dataset.orientation;

        const onThumbDown = (e) => {
            e.preventDefault();
            e.stopPropagation();

            const startPos = orientation === 'vertical' ? e.clientY : e.clientX;
            const startScroll = orientation === 'vertical' ? viewport.scrollTop : viewport.scrollLeft;
            const trackSize = orientation === 'vertical' ? scrollbar.clientHeight : scrollbar.clientWidth;
            const contentSize = orientation === 'vertical' ? viewport.scrollHeight : viewport.scrollWidth;
            const viewSize = orientation === 'vertical' ? viewport.clientHeight : viewport.clientWidth;
            const ratio = viewSize / contentSize;
            const thumbSize = trackSize * Math.max(ratio, 0.1);
            const availableTrack = trackSize - thumbSize;

            const onMove = (me) => {
                if (availableTrack <= 0) return;
                const delta = (orientation === 'vertical' ? me.clientY : me.clientX) - startPos;
                const scrollRange = contentSize - viewSize;
                const scrollDelta = (delta / availableTrack) * scrollRange;
                if (orientation === 'vertical') {
                    viewport.scrollTop = startScroll + scrollDelta;
                } else {
                    viewport.scrollLeft = startScroll + scrollDelta;
                }
            };

            const onUp = () => {
                document.removeEventListener('mousemove', onMove);
                document.removeEventListener('mouseup', onUp);
                document.body.style.userSelect = '';
            };

            document.body.style.userSelect = 'none';
            document.addEventListener('mousemove', onMove);
            document.addEventListener('mouseup', onUp);
        };

        thumb.addEventListener('mousedown', onThumbDown);
        state.thumbHandlers.push({ thumb, handler: onThumbDown });

        // Track click — scroll to clicked position
        const onTrackClick = (e) => {
            if (e.target === thumb) return;
            const rect = scrollbar.getBoundingClientRect();
            if (orientation === 'vertical') {
                const clickRatio = (e.clientY - rect.top) / rect.height;
                viewport.scrollTop = clickRatio * (viewport.scrollHeight - viewport.clientHeight);
            } else {
                const clickRatio = (e.clientX - rect.left) / rect.width;
                viewport.scrollLeft = clickRatio * (viewport.scrollWidth - viewport.clientWidth);
            }
        };

        scrollbar.addEventListener('mousedown', onTrackClick);
        state.thumbHandlers.push({ thumb: scrollbar, handler: onTrackClick, event: 'mousedown' });
    };

    root.querySelectorAll('[data-slot="scroll-area-scrollbar"]').forEach(setupThumbDrag);

    _scrollAreas.set(id, state);

    // Initial update
    requestAnimationFrame(update);
}

export function disposeScrollArea(id) {
    const state = _scrollAreas.get(id);
    if (!state) return;

    if (state.scrollHandler) {
        state.viewport.removeEventListener('scroll', state.scrollHandler);
    }
    if (state.resizeObserver) {
        state.resizeObserver.disconnect();
    }
    for (const { thumb, handler, event } of state.thumbHandlers) {
        thumb.removeEventListener(event || 'mousedown', handler);
    }

    _scrollAreas.delete(id);
}

// ─── Gantt Scroll Sync ──────────────────────────────────────────────────────

const _ganttInstances = new Map();

export function initGanttScrollSync(id, scrollEl, headerEl, sidebarBodyEl) {
    if (!scrollEl) return;

    // Dispose existing if any
    disposeGanttScrollSync(id);

    const state = { scrollEl, headerEl, sidebarBodyEl, handler: null };

    state.handler = () => {
        requestAnimationFrame(() => {
            if (headerEl) {
                headerEl.scrollLeft = scrollEl.scrollLeft;
            }
            if (sidebarBodyEl) {
                sidebarBodyEl.scrollTop = scrollEl.scrollTop;
            }
        });
    };

    scrollEl.addEventListener('scroll', state.handler, { passive: true });
    _ganttInstances.set(id, state);
}

export function disposeGanttScrollSync(id) {
    const state = _ganttInstances.get(id);
    if (!state) return;

    if (state.handler) {
        state.scrollEl.removeEventListener('scroll', state.handler);
    }
    _ganttInstances.delete(id);
}

// ─── Dropdown Fixed Position ────────────────────────────────────────────────

const _dropdowns = new Map();

export function positionDropdown(id, triggerEl, contentEl, side, align, offsetPx, dotNetRef, closeMethodName) {
    if (!triggerEl || !contentEl) return;

    // Dispose existing if any
    disposeDropdownPosition(id);

    const update = () => {
        const tr = triggerEl.getBoundingClientRect();
        const cr = contentEl.getBoundingClientRect();
        const vw = window.innerWidth;
        const vh = window.innerHeight;
        let top, left;

        // Calculate position based on side
        switch (side) {
            case 'Top':
                top = tr.top - cr.height - offsetPx;
                break;
            case 'Left':
                top = tr.top + (tr.height - cr.height) / 2;
                left = tr.left - cr.width - offsetPx;
                break;
            case 'Right':
                top = tr.top + (tr.height - cr.height) / 2;
                left = tr.right + offsetPx;
                break;
            default: // Bottom
                top = tr.bottom + offsetPx;
                break;
        }

        // Calculate horizontal alignment for Top/Bottom
        if (side === 'Top' || side === 'Bottom') {
            switch (align) {
                case 'End':
                    left = tr.right - cr.width;
                    break;
                case 'Center':
                    left = tr.left + (tr.width - cr.width) / 2;
                    break;
                default: // Start
                    left = tr.left;
                    break;
            }
        }

        // Viewport boundary clamping
        if (left + cr.width > vw - 8) left = vw - cr.width - 8;
        if (left < 8) left = 8;
        if (top + cr.height > vh - 8) top = vh - cr.height - 8;
        if (top < 8) top = 8;

        contentEl.style.top = `${top}px`;
        contentEl.style.left = `${left}px`;
    };

    // Initial position
    requestAnimationFrame(update);

    // Close on any scroll (ancestor scroll containers)
    const onScroll = (e) => {
        // Ignore scroll events from within the dropdown content itself
        if (contentEl.contains(e.target)) return;
        dotNetRef.invokeMethodAsync(closeMethodName);
    };

    // Use capture to catch scroll events on any ancestor
    document.addEventListener('scroll', onScroll, { capture: true, passive: true });

    _dropdowns.set(id, { onScroll });
}

export function disposeDropdownPosition(id) {
    const state = _dropdowns.get(id);
    if (!state) return;
    document.removeEventListener('scroll', state.onScroll, { capture: true });
    _dropdowns.delete(id);
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
    const eventType = key.includes('clickoutside') ? 'click' : 'keydown';
    entry.target.removeEventListener(eventType, entry.handler, entry.useCapture);
    _listeners.delete(key);
}
