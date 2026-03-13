namespace RnUI.Components.UI.Calendar;

/// <summary>
/// RnCalendarEvent component - purely UI-focused model for calendar events.
/// This record contains the minimum required data to render an event correctly
/// on the calendar UI without tight coupling to external APIs or business logic.
/// </summary>
public record RnCalendarEvent
{
    /// <summary>
    /// Unique identifier for the event. Useful for handling interactions like click, drag/drop.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Display title of the event.
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// The start time of the event.
    /// </summary>
    public required DateTime Start { get; init; }

    /// <summary>
    /// The end time of the event. May be null if the event represents a point in time (e.g., a deadline).
    /// </summary>
    public DateTime? End { get; init; }

    /// <summary>
    /// Indicates whether this represents an all-day event.
    /// </summary>
    public bool IsAllDay { get; init; }
    
    /// <summary>
    /// Optional CSS class for custom styling based on the event's category, status, or theme.
    /// </summary>
    public string? EventCssClass { get; init; }
}
