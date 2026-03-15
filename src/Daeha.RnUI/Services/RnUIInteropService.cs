using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RnUI.Services;

/// <summary>
/// Provides JavaScript interop helpers for RnUI overlay components.
/// Handles body scroll locking, focus trapping, escape key listeners,
/// and click-outside listeners.
/// </summary>
public sealed class RnUIInteropService : IAsyncDisposable, IDisposable
{
    private readonly IJSRuntime _js;
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public RnUIInteropService(IJSRuntime js)
    {
        _js = js;
        _moduleTask = new Lazy<Task<IJSObjectReference>>(
            () => _js.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Daeha.RnUI/js/rnui-interop.js").AsTask());
    }

    // ─── Body Scroll Lock ────────────────────────────────────────────────────

    /// <summary>Prevents body scrolling (e.g., when a modal is open).</summary>
    public async ValueTask LockBodyScrollAsync()
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("lockBodyScroll");
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Restores body scrolling.</summary>
    public async ValueTask UnlockBodyScrollAsync()
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("unlockBodyScroll");
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Focus Trap ──────────────────────────────────────────────────────────

    /// <summary>
    /// Traps keyboard focus within <paramref name="element"/> and moves focus to
    /// the first focusable child immediately.
    /// </summary>
    public async ValueTask EnableFocusTrapAsync(string id, ElementReference element)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("enableFocusTrap", id, element);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Removes the focus trap registered for <paramref name="id"/>.</summary>
    public async ValueTask DisableFocusTrapAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("disableFocusTrap", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Escape Key Listener ─────────────────────────────────────────────────

    /// <summary>
    /// Listens for the Escape key and invokes <paramref name="methodName"/> on
    /// <paramref name="dotNetRef"/> when pressed.
    /// </summary>
    public async ValueTask AddEscapeListenerAsync<T>(
        string id, DotNetObjectReference<T> dotNetRef, string methodName)
        where T : class
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("addEscapeListener", id, dotNetRef, methodName);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Removes the escape listener registered for <paramref name="id"/>.</summary>
    public async ValueTask RemoveEscapeListenerAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("removeEscapeListener", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Click Outside Listener ───────────────────────────────────────────────

    /// <summary>
    /// Listens for clicks outside <paramref name="element"/> and invokes
    /// <paramref name="methodName"/> on <paramref name="dotNetRef"/> when detected.
    /// </summary>
    public async ValueTask AddClickOutsideListenerAsync<T>(
        string id, ElementReference element,
        DotNetObjectReference<T> dotNetRef, string methodName)
        where T : class
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("addClickOutsideListener", id, element, dotNetRef, methodName);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Removes the click-outside listener registered for <paramref name="id"/>.</summary>
    public async ValueTask RemoveClickOutsideListenerAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("removeClickOutsideListener", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Keyboard Shortcut Listener ────────────────────────────────────────────

    /// <summary>
    /// Listens for Ctrl+B / Cmd+B and invokes <paramref name="methodName"/> on
    /// <paramref name="dotNetRef"/> when pressed.
    /// </summary>
    public async ValueTask AddKeyboardShortcutListenerAsync<T>(
        string id, DotNetObjectReference<T> dotNetRef, string methodName)
        where T : class
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("addKeyboardShortcutListener", id, dotNetRef, methodName);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Removes the keyboard shortcut listener registered for <paramref name="id"/>.</summary>
    public async ValueTask RemoveKeyboardShortcutListenerAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("removeKeyboardShortcutListener", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Element Position ──────────────────────────────────────────────────────

    /// <summary>
    /// Returns the bounding client rect of <paramref name="element"/>.
    /// Used for fixed-positioning overlays relative to a trigger element.
    /// </summary>
    public async ValueTask<ElementRect?> GetElementRectAsync(ElementReference element)
    {
        try
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<ElementRect?>("getElementRect", element);
        }
        catch (JSDisconnectedException) { return null; }
    }

    // ─── Scroll Area ────────────────────────────────────────────────────────

    /// <summary>Initializes custom scrollbar tracking for a scroll area viewport.</summary>
    public async ValueTask InitScrollAreaAsync(string id, ElementReference viewport)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("initScrollArea", id, viewport);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Disposes scroll area listeners and observers.</summary>
    public async ValueTask DisposeScrollAreaAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("disposeScrollArea", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Gantt Scroll Sync ────────────────────────────────────────────────────

    /// <summary>Initializes scroll synchronization for a Gantt chart.</summary>
    public async ValueTask InitGanttScrollSyncAsync(
        string id, ElementReference scrollEl,
        ElementReference headerEl, ElementReference sidebarBodyEl)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("initGanttScrollSync", id, scrollEl, headerEl, sidebarBodyEl);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Disposes Gantt scroll synchronization.</summary>
    public async ValueTask DisposeGanttScrollSyncAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("disposeGanttScrollSync", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Dropdown Fixed Position ────────────────────────────────────────────

    /// <summary>
    /// Positions a dropdown content element using fixed positioning relative to its trigger,
    /// and registers a scroll listener that closes the dropdown on ancestor scroll.
    /// </summary>
    public async ValueTask PositionDropdownAsync<T>(
        string id, ElementReference triggerEl, ElementReference contentEl,
        string side, string align, int offsetPx,
        DotNetObjectReference<T> dotNetRef, string closeMethodName)
        where T : class
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("positionDropdown",
                id, triggerEl, contentEl, side, align, offsetPx, dotNetRef, closeMethodName);
        }
        catch (JSDisconnectedException) { }
    }

    /// <summary>Disposes dropdown position listeners.</summary>
    public async ValueTask DisposeDropdownPositionAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("disposeDropdownPosition", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── Dispose All ─────────────────────────────────────────────────────────

    /// <summary>Removes all JS listeners registered under <paramref name="id"/>.</summary>
    public async ValueTask DisposeAllAsync(string id)
    {
        try
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("disposeAll", id);
        }
        catch (JSDisconnectedException) { }
    }

    // ─── IDisposable / IAsyncDisposable ──────────────────────────────────────

    public void Dispose()
    {
        // Synchronous dispose — no-op; real cleanup is in DisposeAsync.
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            try
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
            catch (JSDisconnectedException) { }
        }
    }
}
