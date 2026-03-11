namespace RnUI.Components.UI.Gantt;

/// <summary>A single task/allocation bar on the Gantt chart.</summary>
public class GanttTask
{
    /// <summary>Unique identifier.</summary>
    public string Id { get; set; } = "";

    /// <summary>Display label shown inside the bar.</summary>
    public string Label { get; set; } = "";

    /// <summary>Start date (inclusive).</summary>
    public DateTime StartDate { get; set; }

    /// <summary>End date (inclusive).</summary>
    public DateTime EndDate { get; set; }

    /// <summary>Bar variant (determines color scheme).</summary>
    public BarVariant Variant { get; set; } = BarVariant.Default;

    /// <summary>Custom CSS color (overrides Variant if set).</summary>
    public string? Color { get; set; }

    /// <summary>Tooltip text (additional details shown on hover).</summary>
    public string? TooltipText { get; set; }

    /// <summary>Arbitrary data for callback identification.</summary>
    public object? Tag { get; set; }
}

/// <summary>A row in the Gantt chart containing one or more tasks.</summary>
public class GanttRow
{
    /// <summary>Unique identifier.</summary>
    public string Id { get; set; } = "";

    /// <summary>Primary label (e.g., engineer name).</summary>
    public string Label { get; set; } = "";

    /// <summary>Secondary label (e.g., role/title).</summary>
    public string? SubLabel { get; set; }

    /// <summary>Tasks/allocations for this row.</summary>
    public List<GanttTask> Tasks { get; set; } = [];

    /// <summary>Optional group identifier for visual grouping.</summary>
    public string? GroupId { get; set; }
}

/// <summary>A visual grouping of rows.</summary>
public class GanttGroup
{
    /// <summary>Unique identifier matching GanttRow.GroupId.</summary>
    public string Id { get; set; } = "";

    /// <summary>Display label for the group header.</summary>
    public string Label { get; set; } = "";
}

/// <summary>Legend item for the footer.</summary>
public class GanttLegendItem
{
    /// <summary>Legend label.</summary>
    public string Label { get; set; } = "";

    /// <summary>Bar variant.</summary>
    public BarVariant Variant { get; set; } = BarVariant.Default;
}

/// <summary>Represents a single header cell in the timeline header.</summary>
public record GanttHeaderCell(
    string Label,
    int ColumnSpan,
    double WidthPx
);
