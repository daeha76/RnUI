namespace RnUI.Components.UI.DataTable;

public class DataTableRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 10;
    public string? GlobalFilter { get; set; }
    public FilterMode GlobalFilterMode { get; set; } = FilterMode.Contains;
    public Dictionary<string, string> ColumnFilters { get; set; } = new();
    public List<DataTableSortInfo> SortDescriptors { get; set; } = [];
}

public class DataTableSortInfo
{
    public string ColumnId { get; set; } = "";
    public SortDirection Direction { get; set; } = SortDirection.Ascending;
}

public class DataTableResponse<TItem>
{
    public IReadOnlyList<TItem> Items { get; set; } = [];
    public int TotalItems { get; set; }
}
