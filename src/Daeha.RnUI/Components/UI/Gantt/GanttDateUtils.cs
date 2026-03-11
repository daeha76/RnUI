using System.Globalization;

namespace RnUI.Components.UI.Gantt;

/// <summary>Date-to-pixel calculation and header generation utilities.</summary>
public static class GanttDateUtils
{
    /// <summary>Returns the number of columns for the given date range and view mode.</summary>
    public static int GetColumnCount(DateTime rangeStart, DateTime rangeEnd, GanttViewMode mode)
    {
        if (rangeEnd <= rangeStart) return 0;

        return mode switch
        {
            GanttViewMode.Day => (int)(rangeEnd - rangeStart).TotalDays,
            GanttViewMode.Week => (int)Math.Ceiling((rangeEnd - rangeStart).TotalDays / 7.0),
            GanttViewMode.Month => MonthsBetween(rangeStart, rangeEnd),
            GanttViewMode.Quarter => (int)Math.Ceiling(MonthsBetween(rangeStart, rangeEnd) / 3.0),
            GanttViewMode.Year => rangeEnd.Year - rangeStart.Year + (rangeEnd.Month > 1 || rangeEnd.Day > 1 ? 1 : 0),
            _ => 0
        };
    }

    /// <summary>
    /// Calculates the left pixel offset and width for a task bar.
    /// </summary>
    public static (double Left, double Width) GetBarPosition(
        GanttTask task, DateTime rangeStart, DateTime rangeEnd, double timelineWidthPx)
    {
        var totalDays = (rangeEnd - rangeStart).TotalDays;
        if (totalDays <= 0) return (0, 0);

        var pixelsPerDay = timelineWidthPx / totalDays;
        var startOffset = (task.StartDate - rangeStart).TotalDays;
        var duration = (task.EndDate - task.StartDate).TotalDays + 1;

        var left = startOffset * pixelsPerDay;
        var width = Math.Max(duration * pixelsPerDay, 2); // min 2px

        return (Math.Round(left, 2), Math.Round(width, 2));
    }

    /// <summary>Generates primary header cells (upper row).</summary>
    public static IReadOnlyList<GanttHeaderCell> GetPrimaryHeaders(
        DateTime rangeStart, DateTime rangeEnd, GanttViewMode mode, int cellWidthPx)
    {
        var headers = new List<GanttHeaderCell>();
        if (rangeEnd <= rangeStart) return headers;

        switch (mode)
        {
            case GanttViewMode.Day:
            case GanttViewMode.Week:
                // Primary = months
                var current = new DateTime(rangeStart.Year, rangeStart.Month, 1);
                while (current < rangeEnd)
                {
                    var nextMonth = current.AddMonths(1);
                    var effectiveEnd = nextMonth < rangeEnd ? nextMonth : rangeEnd;
                    var effectiveStart = current < rangeStart ? rangeStart : current;

                    int cols;
                    if (mode == GanttViewMode.Day)
                    {
                        cols = (int)(effectiveEnd - effectiveStart).TotalDays;
                    }
                    else
                    {
                        cols = (int)Math.Ceiling((effectiveEnd - effectiveStart).TotalDays / 7.0);
                    }

                    if (cols > 0)
                    {
                        headers.Add(new GanttHeaderCell(
                            current.ToString("yyyy MMM", CultureInfo.InvariantCulture),
                            cols,
                            cols * cellWidthPx));
                    }

                    current = nextMonth;
                }

                break;

            case GanttViewMode.Month:
            case GanttViewMode.Quarter:
                // Primary = years
                for (var year = rangeStart.Year; year <= rangeEnd.Year; year++)
                {
                    var yearStart = new DateTime(year, 1, 1);
                    var yearEnd = new DateTime(year + 1, 1, 1);
                    var effectiveStart = yearStart < rangeStart ? rangeStart : yearStart;
                    var effectiveEnd = yearEnd > rangeEnd ? rangeEnd : yearEnd;

                    int cols;
                    if (mode == GanttViewMode.Month)
                    {
                        cols = MonthsBetween(effectiveStart, effectiveEnd);
                    }
                    else
                    {
                        cols = (int)Math.Ceiling(MonthsBetween(effectiveStart, effectiveEnd) / 3.0);
                    }

                    if (cols > 0)
                    {
                        headers.Add(new GanttHeaderCell(year.ToString(), cols, cols * cellWidthPx));
                    }
                }

                break;

            case GanttViewMode.Year:
                // No primary header for Year mode (single row)
                break;
        }

        return headers;
    }

    /// <summary>Generates secondary header cells (lower row).</summary>
    public static IReadOnlyList<GanttHeaderCell> GetSecondaryHeaders(
        DateTime rangeStart, DateTime rangeEnd, GanttViewMode mode, int cellWidthPx)
    {
        var headers = new List<GanttHeaderCell>();
        if (rangeEnd <= rangeStart) return headers;

        switch (mode)
        {
            case GanttViewMode.Day:
                var dayDate = rangeStart.Date;
                while (dayDate < rangeEnd)
                {
                    headers.Add(new GanttHeaderCell(dayDate.Day.ToString(), 1, cellWidthPx));
                    dayDate = dayDate.AddDays(1);
                }

                break;

            case GanttViewMode.Week:
                var weekDate = rangeStart.Date;
                var weekNum = 1;
                while (weekDate < rangeEnd)
                {
                    headers.Add(new GanttHeaderCell($"W{weekNum}", 1, cellWidthPx));
                    weekDate = weekDate.AddDays(7);
                    weekNum++;
                    if (weekDate.Day <= 7) weekNum = 1;
                }

                break;

            case GanttViewMode.Month:
                var monthDate = new DateTime(rangeStart.Year, rangeStart.Month, 1);
                while (monthDate < rangeEnd)
                {
                    headers.Add(new GanttHeaderCell(
                        monthDate.ToString("MMM", CultureInfo.InvariantCulture),
                        1,
                        cellWidthPx));
                    monthDate = monthDate.AddMonths(1);
                }

                break;

            case GanttViewMode.Quarter:
                var qDate = new DateTime(rangeStart.Year, ((rangeStart.Month - 1) / 3) * 3 + 1, 1);
                while (qDate < rangeEnd)
                {
                    var quarter = (qDate.Month - 1) / 3 + 1;
                    headers.Add(new GanttHeaderCell($"Q{quarter}", 1, cellWidthPx));
                    qDate = qDate.AddMonths(3);
                }

                break;

            case GanttViewMode.Year:
                for (var year = rangeStart.Year; year <= rangeEnd.Year; year++)
                {
                    if (year == rangeEnd.Year && rangeEnd.Month == 1 && rangeEnd.Day == 1) break;
                    headers.Add(new GanttHeaderCell(year.ToString(), 1, cellWidthPx));
                }

                break;
        }

        return headers;
    }

    /// <summary>
    /// Returns the pixel offset for a reference date from the range start.
    /// Returns null if the date is outside the range.
    /// </summary>
    public static double? GetTodayOffset(
        DateTime rangeStart, DateTime rangeEnd, double timelineWidthPx, DateTime? referenceDate = null)
    {
        var target = referenceDate?.Date ?? DateTime.Today;
        if (target < rangeStart || target > rangeEnd) return null;

        var totalDays = (rangeEnd - rangeStart).TotalDays;
        if (totalDays <= 0) return null;

        var offset = (target - rangeStart).TotalDays;
        return Math.Round(offset / totalDays * timelineWidthPx, 2);
    }

    private static int MonthsBetween(DateTime start, DateTime end)
    {
        return (end.Year - start.Year) * 12 + (end.Month - start.Month);
    }
}
