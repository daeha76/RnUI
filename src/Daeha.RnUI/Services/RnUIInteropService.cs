using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RnUI.Services;

/// <summary>
/// Provides JavaScript interop helpers for RnUI overlay components.
/// Handles body scroll locking, focus trapping, escape key listeners,
/// and click-outside listeners.
/// </summary>
public sealed class RnUIInteropService : IAsyncDisposable
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

    // ─── IAsyncDisposable ────────────────────────────────────────────────────

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
