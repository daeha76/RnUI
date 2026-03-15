namespace RnUI.Components.UI.DataTable;

public class SortDescriptor<TItem>
{
    public string ColumnId { get; set; } = "";
    public Func<TItem, IComparable?> KeySelector { get; set; } = _ => null;
    public SortDirection Direction { get; set; } = SortDirection.Ascending;
}

public class DataTableState<TItem>
{
    // --- Change notification (for cascading child components) ---
    public event Action? OnChange;

    private void NotifyChange() => OnChange?.Invoke();

    // --- Source data ---
    private IReadOnlyList<TItem> _sourceItems = [];

    // --- Sorting ---
    public List<SortDescriptor<TItem>> SortDescriptors { get; } = [];

    // --- Filtering ---
    public string GlobalFilter { get; set; } = "";
    public FilterMode GlobalFilterMode { get; set; } = FilterMode.Contains;
    public Dictionary<string, string> ColumnFilters { get; } = new();

    // --- Pagination ---
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 10;
    public int[] PageSizeOptions { get; set; } = [10, 20, 50, 100];
    public int TotalItems => FilteredItems.Count;
    public int TotalPages => PageSize <= 0 ? 1 : (int)Math.Ceiling((double)TotalItems / PageSize);

    // --- Selection ---
    public HashSet<TItem> SelectedItems { get; set; } = new();
    public SelectionMode SelectionMode { get; set; } = SelectionMode.Multiple;
    private int _lastSelectedIndex = -1;

    // --- Column visibility ---
    public Dictionary<string, bool> ColumnVisibility { get; } = new();

    // --- Column definitions ---
    public List<DataTableColumnDef<TItem>> Columns { get; } = [];

    // --- Column groups ---
    public List<DataTableColumnGroupDef> ColumnGroups { get; } = [];

    // --- Row expansion ---
    public HashSet<int> ExpandedRowIndices { get; } = new();

    // --- Row span cache ---
    public Dictionary<string, int[]> RowSpanCache { get; } = new();

    // --- Computed properties ---
    public IReadOnlyList<TItem> FilteredItems { get; private set; } = [];
    public IReadOnlyList<TItem> SortedItems { get; private set; } = [];
    public IReadOnlyList<TItem> PagedItems { get; private set; } = [];

    // --- Pipeline ---
    public void SetItems(IReadOnlyList<TItem> items)
    {
        _sourceItems = items;
        Recalculate();
    }

    public void Recalculate()
    {
        FilteredItems = ApplyFilters(_sourceItems);
        SortedItems = ApplySort(FilteredItems);
        PagedItems = ApplyPaging(SortedItems);
        CalculateRowSpans();
    }

    // --- Filtering ---
    private IReadOnlyList<TItem> ApplyFilters(IReadOnlyList<TItem> items)
    {
        var result = items.AsEnumerable();

        // Global filter
        if (!string.IsNullOrWhiteSpace(GlobalFilter))
        {
            var filterableColumns = GetVisibleColumns().Where(c => c.Filterable || c.PropertyAccessor != null).ToList();
            if (filterableColumns.Count > 0)
            {
                if (GlobalFilterMode == FilterMode.Fuzzy)
                {
                    var ranked = FuzzySearch.Rank(
                        result,
                        GlobalFilter,
                        item => filterableColumns.Select(c => c.GetStringValue(item)),
                        threshold: 0.3
                    );
                    return ranked.OrderByDescending(r => r.Score).Select(r => r.Item).ToList();
                }
                else
                {
                    result = result.Where(item =>
                        filterableColumns.Any(col =>
                            col.GetStringValue(item).Contains(GlobalFilter, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }

        // Column filters
        foreach (var (columnId, filterValue) in ColumnFilters)
        {
            if (string.IsNullOrWhiteSpace(filterValue)) continue;
            var column = Columns.FirstOrDefault(c => c.Id == columnId);
            if (column == null) continue;
            var fv = filterValue;
            result = result.Where(item =>
                column.GetStringValue(item).Equals(fv, StringComparison.OrdinalIgnoreCase));
        }

        return result.ToList();
    }

    // --- Sorting ---
    private IReadOnlyList<TItem> ApplySort(IReadOnlyList<TItem> items)
    {
        if (SortDescriptors.Count == 0) return items;

        IOrderedEnumerable<TItem>? ordered = null;
        foreach (var descriptor in SortDescriptors)
        {
            if (ordered == null)
            {
                ordered = descriptor.Direction == SortDirection.Ascending
                    ? items.OrderBy(descriptor.KeySelector, Comparer<IComparable?>.Default)
                    : items.OrderByDescending(descriptor.KeySelector, Comparer<IComparable?>.Default);
            }
            else
            {
                ordered = descriptor.Direction == SortDirection.Ascending
                    ? ordered.ThenBy(descriptor.KeySelector, Comparer<IComparable?>.Default)
                    : ordered.ThenByDescending(descriptor.KeySelector, Comparer<IComparable?>.Default);
            }
        }

        return ordered?.ToList() ?? items.ToList();
    }

    // --- Paging ---
    private IReadOnlyList<TItem> ApplyPaging(IReadOnlyList<TItem> items)
    {
        if (PageSize <= 0) return items;

        // Clamp page index
        var maxPage = Math.Max(0, (int)Math.Ceiling((double)items.Count / PageSize) - 1);
        if (PageIndex > maxPage) PageIndex = maxPage;
        if (PageIndex < 0) PageIndex = 0;

        return items.Skip(PageIndex * PageSize).Take(PageSize).ToList();
    }

    // --- Column helpers ---
    public IReadOnlyList<DataTableColumnDef<TItem>> GetVisibleColumns()
    {
        return Columns
            .Where(c => !ColumnVisibility.TryGetValue(c.Id, out var visible) || visible)
            .Where(c => c.Visible)
            .OrderBy(c => c.Order)
            .ToList();
    }

    public void RegisterColumn(DataTableColumnDef<TItem> column)
    {
        // Prevent duplicate registration
        if (Columns.Any(c => c.Id == column.Id)) return;
        column.Order = Columns.Count;
        Columns.Add(column);
    }

    public void RegisterColumnGroup(DataTableColumnGroupDef group)
    {
        if (ColumnGroups.Any(g => g.Id == group.Id)) return;
        group.Order = ColumnGroups.Count;
        ColumnGroups.Add(group);
    }

    // --- Sorting helpers ---
    public SortDirection GetSortDirection(string columnId)
    {
        var descriptor = SortDescriptors.FirstOrDefault(d => d.ColumnId == columnId);
        return descriptor?.Direction ?? SortDirection.None;
    }

    public int GetSortIndex(string columnId)
    {
        if (SortDescriptors.Count <= 1) return -1;
        var index = SortDescriptors.FindIndex(d => d.ColumnId == columnId);
        return index >= 0 ? index + 1 : -1;
    }

    public void ToggleSort(string columnId, bool multiSort = false)
    {
        var column = Columns.FirstOrDefault(c => c.Id == columnId);
        if (column?.PropertyAccessor == null) return;

        var existing = SortDescriptors.FirstOrDefault(d => d.ColumnId == columnId);

        if (existing != null)
        {
            if (existing.Direction == SortDirection.Ascending)
            {
                existing.Direction = SortDirection.Descending;
            }
            else
            {
                SortDescriptors.Remove(existing);
            }
        }
        else
        {
            if (!multiSort) SortDescriptors.Clear();
            SortDescriptors.Add(new SortDescriptor<TItem>
            {
                ColumnId = columnId,
                KeySelector = item => column.GetSortValue(item),
                Direction = SortDirection.Ascending
            });
        }

        Recalculate();
    }

    // --- Selection helpers ---
    public void ToggleRow(TItem item, int rowIndex, bool shiftKey = false)
    {
        if (SelectionMode == SelectionMode.None) return;

        if (SelectionMode == SelectionMode.Single)
        {
            if (SelectedItems.Contains(item))
            {
                SelectedItems.Clear();
            }
            else
            {
                SelectedItems.Clear();
                SelectedItems.Add(item);
            }
            _lastSelectedIndex = rowIndex;
            return;
        }

        // Multiple selection
        if (shiftKey && _lastSelectedIndex >= 0)
        {
            var start = Math.Min(_lastSelectedIndex, rowIndex);
            var end = Math.Max(_lastSelectedIndex, rowIndex);
            for (var i = start; i <= end; i++)
            {
                if (i < PagedItems.Count)
                {
                    SelectedItems.Add(PagedItems[i]);
                }
            }
        }
        else
        {
            if (!SelectedItems.Remove(item))
            {
                SelectedItems.Add(item);
            }
        }

        _lastSelectedIndex = rowIndex;
    }

    public void ToggleAllOnPage()
    {
        if (SelectionMode != SelectionMode.Multiple) return;

        if (IsAllOnPageSelected)
        {
            foreach (var item in PagedItems)
            {
                SelectedItems.Remove(item);
            }
        }
        else
        {
            foreach (var item in PagedItems)
            {
                SelectedItems.Add(item);
            }
        }
    }

    public bool IsSelected(TItem item) => SelectedItems.Contains(item);

    public bool IsAllOnPageSelected =>
        PagedItems.Count > 0 && PagedItems.All(item => SelectedItems.Contains(item));

    public bool IsSomeSelected =>
        SelectedItems.Count > 0 && PagedItems.Any(item => SelectedItems.Contains(item)) && !IsAllOnPageSelected;

    // --- Row expansion ---
    public void ToggleRowExpansion(int rowIndex)
    {
        if (!ExpandedRowIndices.Remove(rowIndex))
        {
            ExpandedRowIndices.Add(rowIndex);
        }
    }

    public bool IsRowExpanded(int rowIndex) => ExpandedRowIndices.Contains(rowIndex);

    // --- Row span calculation ---
    private void CalculateRowSpans()
    {
        RowSpanCache.Clear();
        var mergeColumns = GetVisibleColumns().Where(c => c.MergeRows && c.PropertyAccessor != null).ToList();
        if (mergeColumns.Count == 0) return;

        foreach (var column in mergeColumns)
        {
            var spans = new int[PagedItems.Count];
            if (PagedItems.Count == 0) continue;

            var i = 0;
            while (i < PagedItems.Count)
            {
                var currentValue = column.GetStringValue(PagedItems[i]);
                var spanStart = i;
                var count = 1;
                while (i + count < PagedItems.Count &&
                       column.GetStringValue(PagedItems[i + count]) == currentValue)
                {
                    count++;
                }

                spans[spanStart] = count;
                for (var j = 1; j < count; j++)
                {
                    spans[spanStart + j] = 0; // 0 = skip rendering
                }
                i += count;
            }

            RowSpanCache[column.Id] = spans;
        }
    }

    public int GetRowSpan(string columnId, int rowIndex)
    {
        if (!RowSpanCache.TryGetValue(columnId, out var spans)) return 1;
        if (rowIndex < 0 || rowIndex >= spans.Length) return 1;
        return spans[rowIndex];
    }

    // --- Pagination helpers ---
    public void GoToPage(int page)
    {
        PageIndex = Math.Max(0, Math.Min(page, TotalPages - 1));
        Recalculate();
    }

    public void GoToFirstPage() => GoToPage(0);
    public void GoToPreviousPage() => GoToPage(PageIndex - 1);
    public void GoToNextPage() => GoToPage(PageIndex + 1);
    public void GoToLastPage() => GoToPage(TotalPages - 1);

    public void SetPageSize(int pageSize)
    {
        PageSize = pageSize;
        PageIndex = 0;
        Recalculate();
    }

    // --- Global filter ---
    public void SetGlobalFilter(string filter)
    {
        GlobalFilter = filter;
        PageIndex = 0;
        Recalculate();
        NotifyChange();
    }

    // --- Column filter ---
    public void SetColumnFilter(string columnId, string filter)
    {
        ColumnFilters[columnId] = filter;
        PageIndex = 0;
        Recalculate();
        NotifyChange();
    }

    // --- Column visibility ---
    public void SetColumnVisibility(string columnId, bool visible)
    {
        ColumnVisibility[columnId] = visible;
        NotifyChange();
    }

    public bool IsColumnVisible(string columnId)
    {
        if (!ColumnVisibility.TryGetValue(columnId, out var visible)) return true;
        return visible;
    }
}
