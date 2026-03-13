namespace RnUI.Components.UI.Calendar;

/// <summary>
/// Represents a positioned event slot within a calendar week row.
/// Used for rendering multi-day spanning event bars.
/// </summary>
public record EventCalendarSlot
{
    /// <summary>The source event.</summary>
    public required RnCalendarEvent Event { get; init; }

    /// <summary>Start column index (0-6, Sunday=0).</summary>
    public required int StartColumn { get; init; }

    /// <summary>Number of columns this event spans (1-7).</summary>
    public required int ColumnSpan { get; init; }

    /// <summary>Row index within the cell's spanning area (0-based).</summary>
    public required int Row { get; init; }

    /// <summary>Whether this segment is the visual start of the event.</summary>
    public required bool IsStart { get; init; }

    /// <summary>Whether this segment is the visual end of the event.</summary>
    public required bool IsEnd { get; init; }
}

/// <summary>
/// Pure layout calculator for positioning multi-day events in a week row.
/// No side effects, no state — takes events in, returns positioned slots out.
/// </summary>
public static class EventCalendarLayout
{
    /// <summary>
    /// Calculate event slot positions for a single week row.
    /// </summary>
    /// <param name="events">All events that may overlap this week.</param>
    /// <param name="weekStart">The Sunday that starts this week row.</param>
    /// <param name="maxRows">Maximum number of spanning rows to allocate.</param>
    /// <returns>Positioned slots for rendering.</returns>
    public static IReadOnlyList<EventCalendarSlot> CalculateWeekSlots(
        IEnumerable<RnCalendarEvent> events,
        DateTime weekStart,
        int maxRows = 3)
    {
        var weekEnd = weekStart.AddDays(6);
        var slots = new List<EventCalendarSlot>();

        // Filter to multi-day events that overlap this week
        var multiDayEvents = events
            .Where(e => IsMultiDay(e) && Overlaps(e, weekStart, weekEnd))
            .OrderByDescending(e => GetDuration(e))
            .ThenBy(e => e.Start)
            .ToList();

        // Track which columns are occupied in each row
        // rows[rowIndex] = array of 7 bools (one per day column)
        var rows = new List<bool[]>();

        foreach (var ev in multiDayEvents)
        {
            var evStart = ev.Start.Date;
            var evEnd = (ev.End ?? ev.Start).Date;

            // Clamp to this week's boundaries
            var clampedStart = evStart < weekStart ? weekStart : evStart;
            var clampedEnd = evEnd > weekEnd ? weekEnd : evEnd;

            var startCol = (int)(clampedStart - weekStart).TotalDays;
            var endCol = (int)(clampedEnd - weekStart).TotalDays;
            var span = endCol - startCol + 1;

            // Find the first row where all needed columns are free
            var assignedRow = -1;
            for (var r = 0; r < rows.Count && r < maxRows; r++)
            {
                var canFit = true;
                for (var c = startCol; c <= endCol; c++)
                {
                    if (rows[r][c])
                    {
                        canFit = false;
                        break;
                    }
                }

                if (canFit)
                {
                    assignedRow = r;
                    break;
                }
            }

            // Need a new row
            if (assignedRow < 0)
            {
                if (rows.Count >= maxRows)
                    continue; // Skip — exceeds max rows

                rows.Add(new bool[7]);
                assignedRow = rows.Count - 1;
            }

            // Mark columns as occupied
            for (var c = startCol; c <= endCol; c++)
            {
                rows[assignedRow][c] = true;
            }

            slots.Add(new EventCalendarSlot
            {
                Event = ev,
                StartColumn = startCol,
                ColumnSpan = span,
                Row = assignedRow,
                IsStart = evStart >= weekStart,
                IsEnd = evEnd <= weekEnd
            });
        }

        return slots;
    }

    /// <summary>Get single-day events for a specific date (excludes multi-day events).</summary>
    public static IReadOnlyList<RnCalendarEvent> GetSingleDayEvents(
        IEnumerable<RnCalendarEvent> events,
        DateTime date)
    {
        return events
            .Where(e => !IsMultiDay(e) && e.Start.Date == date.Date)
            .OrderBy(e => !e.IsAllDay)
            .ThenBy(e => e.Start)
            .ToList();
    }

    /// <summary>Check if an event spans more than one day.</summary>
    public static bool IsMultiDay(RnCalendarEvent ev)
    {
        return ev.End.HasValue && ev.End.Value.Date > ev.Start.Date;
    }

    private static bool Overlaps(RnCalendarEvent ev, DateTime weekStart, DateTime weekEnd)
    {
        var evStart = ev.Start.Date;
        var evEnd = (ev.End ?? ev.Start).Date;
        return evStart <= weekEnd && evEnd >= weekStart;
    }

    private static int GetDuration(RnCalendarEvent ev)
    {
        if (!ev.End.HasValue) return 1;
        return Math.Max(1, (int)(ev.End.Value.Date - ev.Start.Date).TotalDays + 1);
    }
}
