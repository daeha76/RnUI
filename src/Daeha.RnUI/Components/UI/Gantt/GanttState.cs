namespace RnUI.Components.UI.Gantt;

/// <summary>Centralized Gantt chart state, cascaded to all child components.</summary>
public class GanttState
{
    public event Action? OnChange;

    private void NotifyChange() => OnChange?.Invoke();

    // --- View Configuration ---
    public GanttViewMode ViewMode { get; private set; } = GanttViewMode.Month;
    public int CellWidthPx { get; private set; } = 80;
    public int RowHeightPx { get; set; } = 48;
    public int SidebarWidthPx { get; set; } = 250;

    // --- Date Range ---
    public DateTime RangeStart { get; private set; }
    public DateTime RangeEnd { get; private set; }

    // --- Data ---
    public IReadOnlyList<GanttRow> Rows { get; private set; } = [];
    public IReadOnlyList<GanttGroup> Groups { get; private set; } = [];

    // --- Computed ---
    public int TotalColumns => GanttDateUtils.GetColumnCount(RangeStart, RangeEnd, ViewMode);
    public double TimelineWidthPx => TotalColumns * CellWidthPx;

    // --- Header heights ---
    public int PrimaryHeaderHeightPx => ViewMode == GanttViewMode.Year ? 0 : 28;
    public int SecondaryHeaderHeightPx => 32;
    public int TotalHeaderHeightPx => PrimaryHeaderHeightPx + SecondaryHeaderHeightPx;

    // --- Mutators ---
    public void SetViewMode(GanttViewMode mode)
    {
        ViewMode = mode;
        CellWidthPx = GetDefaultCellWidth(mode);
        NotifyChange();
    }

    public void SetCellWidth(int widthPx)
    {
        CellWidthPx = Math.Clamp(widthPx, 16, 300);
        NotifyChange();
    }

    public void SetRows(IReadOnlyList<GanttRow> rows)
    {
        Rows = rows;
        NotifyChange();
    }

    public void SetGroups(IReadOnlyList<GanttGroup> groups)
    {
        Groups = groups;
        NotifyChange();
    }

    public void SetDateRange(DateTime start, DateTime end)
    {
        RangeStart = start;
        RangeEnd = end;
        NotifyChange();
    }

    /// <summary>Auto-calculate range from task dates with padding.</summary>
    public void AutoCalculateRange()
    {
        var allTasks = Rows.SelectMany(r => r.Tasks).ToList();
        if (allTasks.Count == 0) return;

        var min = allTasks.Min(t => t.StartDate);
        var max = allTasks.Max(t => t.EndDate);

        RangeStart = new DateTime(min.Year, min.Month, 1).AddMonths(-1);
        RangeEnd = new DateTime(max.Year, max.Month, 1).AddMonths(2);
    }

    public static int GetDefaultCellWidth(GanttViewMode mode) => mode switch
    {
        GanttViewMode.Day => 30,
        GanttViewMode.Week => 60,
        GanttViewMode.Month => 80,
        GanttViewMode.Quarter => 120,
        GanttViewMode.Year => 160,
        _ => 80
    };
}
