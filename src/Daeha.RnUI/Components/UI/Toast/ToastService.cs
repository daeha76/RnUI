namespace RnUI.Components.UI.Toast;

/// <summary>Represents a single toast message.</summary>
public class ToastMessage
{
    public string Id { get; init; } = Guid.NewGuid().ToString("N")[..8];
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public ToastVariant Variant { get; init; } = ToastVariant.Default;
    public int DurationMs { get; init; } = 5000;
}

/// <summary>Service for programmatically showing toasts.</summary>
public class ToastService
{
    /// <summary>Raised when a new toast should be displayed.</summary>
    public event Action<ToastMessage>? OnShow;

    /// <summary>Raised when a toast should be dismissed by ID.</summary>
    public event Action<string>? OnDismiss;

    /// <summary>Show a toast message.</summary>
    public void Show(
        string title,
        string? description = null,
        ToastVariant variant = ToastVariant.Default,
        int durationMs = 5000)
    {
        var message = new ToastMessage
        {
            Title = title,
            Description = description,
            Variant = variant,
            DurationMs = durationMs
        };
        OnShow?.Invoke(message);
    }

    /// <summary>Dismiss a toast by its ID.</summary>
    public void Dismiss(string id)
    {
        OnDismiss?.Invoke(id);
    }
}
