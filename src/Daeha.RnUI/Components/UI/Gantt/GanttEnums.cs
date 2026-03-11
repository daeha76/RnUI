namespace RnUI.Components.UI.Gantt;

/// <summary>Time scale for the Gantt chart.</summary>
public enum GanttViewMode
{
    /// <summary>Daily view - each cell = 1 day.</summary>
    Day,

    /// <summary>Weekly view - each cell = 1 week.</summary>
    Week,

    /// <summary>Monthly view - each cell = 1 month.</summary>
    Month,

    /// <summary>Quarterly view - each cell = 3 months.</summary>
    Quarter,

    /// <summary>Yearly overview - each cell = 1 year.</summary>
    Year
}

/// <summary>Color variants for Gantt bars.</summary>
public enum BarVariant
{
    Default,
    Blue,
    Green,
    Orange,
    Red,
    Purple,
    Yellow,
    Teal,
    Muted
}
