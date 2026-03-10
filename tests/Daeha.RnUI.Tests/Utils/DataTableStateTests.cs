using FluentAssertions;
using RnUI.Components.UI.DataTable;
using Xunit;

namespace Daeha.RnUI.Tests.Utils;

public record TestItem(string Id, string Name, string Status, decimal Amount, string Department);

[Trait("Category", "Unit")]
public class DataTableStateTests
{
    private static readonly List<TestItem> TestData =
    [
        new("1", "Alice", "Active", 100m, "Sales"),
        new("2", "Bob", "Inactive", 200m, "Sales"),
        new("3", "Charlie", "Active", 300m, "Dev"),
        new("4", "Diana", "Active", 150m, "Dev"),
        new("5", "Eve", "Inactive", 250m, "Dev"),
    ];

    private DataTableState<TestItem> CreateStateWithColumns()
    {
        var state = new DataTableState<TestItem>();
        state.RegisterColumn(new DataTableColumnDef<TestItem>
        {
            Id = "Id", Title = "ID", Sortable = true, Filterable = true,
            PropertyAccessor = x => x.Id
        });
        state.RegisterColumn(new DataTableColumnDef<TestItem>
        {
            Id = "Name", Title = "Name", Sortable = true, Filterable = true,
            PropertyAccessor = x => x.Name
        });
        state.RegisterColumn(new DataTableColumnDef<TestItem>
        {
            Id = "Status", Title = "Status", Sortable = true, Filterable = true,
            PropertyAccessor = x => x.Status
        });
        state.RegisterColumn(new DataTableColumnDef<TestItem>
        {
            Id = "Amount", Title = "Amount", Sortable = true,
            PropertyAccessor = x => x.Amount
        });
        state.RegisterColumn(new DataTableColumnDef<TestItem>
        {
            Id = "Department", Title = "Department", Sortable = true, Filterable = true,
            PropertyAccessor = x => x.Department, MergeRows = true
        });
        state.PageSize = 10;
        state.SetItems(TestData);
        return state;
    }

    // --- Basic ---
    [Fact]
    public void SetItems_SetsPagedItems()
    {
        var state = CreateStateWithColumns();
        state.PagedItems.Should().HaveCount(5);
    }

    [Fact]
    public void GetVisibleColumns_ReturnsAllByDefault()
    {
        var state = CreateStateWithColumns();
        state.GetVisibleColumns().Should().HaveCount(5);
    }

    // --- Sorting ---
    [Fact]
    public void ToggleSort_SingleColumn_Ascending()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Name");
        state.GetSortDirection("Name").Should().Be(SortDirection.Ascending);
        state.PagedItems[0].Name.Should().Be("Alice");
        state.PagedItems[4].Name.Should().Be("Eve");
    }

    [Fact]
    public void ToggleSort_Twice_Descending()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Name");
        state.ToggleSort("Name");
        state.GetSortDirection("Name").Should().Be(SortDirection.Descending);
        state.PagedItems[0].Name.Should().Be("Eve");
    }

    [Fact]
    public void ToggleSort_ThreeTimes_RemovesSort()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Name");
        state.ToggleSort("Name");
        state.ToggleSort("Name");
        state.GetSortDirection("Name").Should().Be(SortDirection.None);
    }

    [Fact]
    public void ToggleSort_MultiSort_WithShift()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Department");
        state.ToggleSort("Name", multiSort: true);
        state.SortDescriptors.Should().HaveCount(2);
        state.GetSortIndex("Department").Should().Be(1);
        state.GetSortIndex("Name").Should().Be(2);
    }

    // --- Filtering ---
    [Fact]
    public void SetGlobalFilter_FiltersItems()
    {
        var state = CreateStateWithColumns();
        state.SetGlobalFilter("Alice");
        state.FilteredItems.Should().HaveCount(1);
        state.FilteredItems[0].Name.Should().Be("Alice");
    }

    [Fact]
    public void SetGlobalFilter_CaseInsensitive()
    {
        var state = CreateStateWithColumns();
        state.SetGlobalFilter("active");
        state.FilteredItems.Should().HaveCount(5); // "Active" and "Inactive" both contain "active"
    }

    [Fact]
    public void SetGlobalFilter_Empty_ShowsAll()
    {
        var state = CreateStateWithColumns();
        state.SetGlobalFilter("Alice");
        state.SetGlobalFilter("");
        state.FilteredItems.Should().HaveCount(5);
    }

    [Fact]
    public void SetColumnFilter_FiltersSpecificColumn()
    {
        var state = CreateStateWithColumns();
        state.SetColumnFilter("Status", "Inactive");
        state.FilteredItems.Should().HaveCount(2);
    }

    // --- Pagination ---
    [Fact]
    public void Pagination_PageSize_LimitsResults()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.PagedItems.Should().HaveCount(2);
        state.TotalPages.Should().Be(3);
    }

    [Fact]
    public void GoToNextPage_AdvancesPage()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToNextPage();
        state.PageIndex.Should().Be(1);
        state.PagedItems[0].Should().Be(TestData[2]);
    }

    [Fact]
    public void GoToLastPage_GoesToEnd()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToLastPage();
        state.PageIndex.Should().Be(2);
        state.PagedItems.Should().HaveCount(1);
    }

    [Fact]
    public void SetPageSize_ResetsToPage0()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToNextPage();
        state.SetPageSize(10);
        state.PageIndex.Should().Be(0);
    }

    [Fact]
    public void PageIndex_ClampsAfterFilterReducesTotal()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToLastPage(); // page 2
        state.SetGlobalFilter("Alice"); // only 1 result, page 2 invalid
        state.PageIndex.Should().Be(0);
    }

    // --- Selection ---
    [Fact]
    public void ToggleRow_SelectsItem()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Multiple;
        state.ToggleRow(TestData[0], 0);
        state.IsSelected(TestData[0]).Should().BeTrue();
    }

    [Fact]
    public void ToggleRow_Twice_DeselectsItem()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Multiple;
        state.ToggleRow(TestData[0], 0);
        state.ToggleRow(TestData[0], 0);
        state.IsSelected(TestData[0]).Should().BeFalse();
    }

    [Fact]
    public void ToggleAllOnPage_SelectsAll()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Multiple;
        state.ToggleAllOnPage();
        state.IsAllOnPageSelected.Should().BeTrue();
        state.SelectedItems.Should().HaveCount(5);
    }

    [Fact]
    public void ToggleAllOnPage_Twice_DeselectsAll()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Multiple;
        state.ToggleAllOnPage();
        state.ToggleAllOnPage();
        state.SelectedItems.Should().HaveCount(0);
    }

    [Fact]
    public void ShiftClickRange_SelectsAllBetween()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Multiple;
        state.ToggleRow(TestData[1], 1, shiftKey: false);
        state.ToggleRow(TestData[4], 4, shiftKey: true);
        state.SelectedItems.Should().HaveCount(4); // rows 1,2,3,4
        state.IsSelected(TestData[1]).Should().BeTrue();
        state.IsSelected(TestData[2]).Should().BeTrue();
        state.IsSelected(TestData[3]).Should().BeTrue();
        state.IsSelected(TestData[4]).Should().BeTrue();
    }

    [Fact]
    public void SingleSelectMode_OnlyOneSelected()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Single;
        state.ToggleRow(TestData[0], 0);
        state.ToggleRow(TestData[1], 1);
        state.SelectedItems.Should().HaveCount(1);
        state.IsSelected(TestData[1]).Should().BeTrue();
        state.IsSelected(TestData[0]).Should().BeFalse();
    }

    [Fact]
    public void IsSomeSelected_TrueWhenPartialSelection()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Multiple;
        state.ToggleRow(TestData[0], 0);
        state.IsSomeSelected.Should().BeTrue();
        state.IsAllOnPageSelected.Should().BeFalse();
    }

    // --- Column visibility ---
    [Fact]
    public void SetColumnVisibility_HidesColumn()
    {
        var state = CreateStateWithColumns();
        state.SetColumnVisibility("Status", false);
        state.GetVisibleColumns().Should().HaveCount(4);
        state.GetVisibleColumns().Should().NotContain(c => c.Id == "Status");
    }

    [Fact]
    public void SetColumnVisibility_ShowsColumn()
    {
        var state = CreateStateWithColumns();
        state.SetColumnVisibility("Status", false);
        state.SetColumnVisibility("Status", true);
        state.GetVisibleColumns().Should().HaveCount(5);
    }

    // --- Row expansion ---
    [Fact]
    public void ToggleRowExpansion_ExpandsRow()
    {
        var state = CreateStateWithColumns();
        state.ToggleRowExpansion(0);
        state.IsRowExpanded(0).Should().BeTrue();
    }

    [Fact]
    public void ToggleRowExpansion_Twice_CollapsesRow()
    {
        var state = CreateStateWithColumns();
        state.ToggleRowExpansion(0);
        state.ToggleRowExpansion(0);
        state.IsRowExpanded(0).Should().BeFalse();
    }

    // --- Row spans ---
    [Fact]
    public void RowSpan_CalculatedForMergeRows()
    {
        var state = CreateStateWithColumns();
        // Sort by department to group them
        state.ToggleSort("Department");
        // After sort: Dev(3), Sales(2)
        state.GetRowSpan("Department", 0).Should().Be(3); // Dev spans 3 rows
        state.GetRowSpan("Department", 1).Should().Be(0); // skipped
        state.GetRowSpan("Department", 2).Should().Be(0); // skipped
        state.GetRowSpan("Department", 3).Should().Be(2); // Sales spans 2 rows
        state.GetRowSpan("Department", 4).Should().Be(0); // skipped
    }

    // --- Column registration ---
    [Fact]
    public void RegisterColumn_PreventsDuplicate()
    {
        var state = new DataTableState<TestItem>();
        state.RegisterColumn(new DataTableColumnDef<TestItem> { Id = "col1", Title = "Col 1" });
        state.RegisterColumn(new DataTableColumnDef<TestItem> { Id = "col1", Title = "Col 1 Dup" });
        state.Columns.Should().HaveCount(1);
    }
}
