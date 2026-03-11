namespace RnUI.Services;

/// <summary>
/// Represents a bounding client rect returned from JavaScript interop.
/// </summary>
public record ElementRect(
    double Top,
    double Left,
    double Bottom,
    double Right,
    double Width,
    double Height);
