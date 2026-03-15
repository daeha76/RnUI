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

    [Fact]
    public void SetItems_SetsFilteredItemsEqualToSource()
    {
        var state = CreateStateWithColumns();
        state.FilteredItems.Should().HaveCount(5);
    }

    [Fact]
    public void SetItems_SetsSortedItemsEqualToSource()
    {
        var state = CreateStateWithColumns();
        state.SortedItems.Should().HaveCount(5);
    }

    [Fact]
    public void TotalItems_ReturnsFilteredCount()
    {
        var state = CreateStateWithColumns();
        state.TotalItems.Should().Be(5);
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

    [Fact]
    public void ToggleSort_WithoutMultiSort_ReplacesExisting()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Department");
        state.ToggleSort("Name"); // not multiSort, should replace
        state.SortDescriptors.Should().HaveCount(1);
        state.GetSortDirection("Department").Should().Be(SortDirection.None);
        state.GetSortDirection("Name").Should().Be(SortDirection.Ascending);
    }

    [Fact]
    public void ToggleSort_NonSortableColumn_DoesNothing()
    {
        var state = new DataTableState<TestItem>();
        state.RegisterColumn(new DataTableColumnDef<TestItem>
        {
            Id = "NoAccessor", Title = "No Accessor", Sortable = true
            // No PropertyAccessor
        });
        state.SetItems(TestData);
        state.ToggleSort("NoAccessor");
        state.SortDescriptors.Should().BeEmpty();
    }

    [Fact]
    public void GetSortDirection_UnknownColumn_ReturnsNone()
    {
        var state = CreateStateWithColumns();
        state.GetSortDirection("NonExistent").Should().Be(SortDirection.None);
    }

    [Fact]
    public void GetSortIndex_SingleSort_ReturnsNegativeOne()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Name");
        // With only 1 sort descriptor, index is -1 (not shown)
        state.GetSortIndex("Name").Should().Be(-1);
    }

    [Fact]
    public void ToggleSort_ByAmount_SortsNumerically()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Amount");
        state.PagedItems[0].Amount.Should().Be(100m);
        state.PagedItems[4].Amount.Should().Be(300m);
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

    [Fact]
    public void SetColumnFilter_MultipleColumns_IntersectsFilters()
    {
        var state = CreateStateWithColumns();
        state.SetColumnFilter("Status", "Active");
        state.SetColumnFilter("Department", "Dev");
        state.FilteredItems.Should().HaveCount(2); // Charlie and Diana
    }

    [Fact]
    public void SetColumnFilter_EmptyValue_ShowsAll()
    {
        var state = CreateStateWithColumns();
        state.SetColumnFilter("Status", "Inactive");
        state.SetColumnFilter("Status", "");
        state.FilteredItems.Should().HaveCount(5);
    }

    [Fact]
    public void SetGlobalFilter_ResetsPageIndex()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToNextPage();
        state.PageIndex.Should().Be(1);
        state.SetGlobalFilter("Alice");
        state.PageIndex.Should().Be(0);
    }

    [Fact]
    public void SetColumnFilter_ResetsPageIndex()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToNextPage();
        state.SetColumnFilter("Status", "Active");
        state.PageIndex.Should().Be(0);
    }

    [Fact]
    public void SetGlobalFilter_MatchesAcrossMultipleColumns()
    {
        var state = CreateStateWithColumns();
        state.SetGlobalFilter("Sales");
        state.FilteredItems.Should().HaveCount(2); // Alice and Bob in Sales department
    }

    [Fact]
    public void SetGlobalFilter_FuzzyMode_ReturnsResults()
    {
        var state = CreateStateWithColumns();
        state.GlobalFilterMode = FilterMode.Fuzzy;
        state.SetGlobalFilter("Alic");
        state.FilteredItems.Should().Contain(i => i.Name == "Alice");
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

    [Fact]
    public void GoToPreviousPage_DecrementsPage()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToNextPage();
        state.GoToNextPage();
        state.PageIndex.Should().Be(2);
        state.GoToPreviousPage();
        state.PageIndex.Should().Be(1);
    }

    [Fact]
    public void GoToPreviousPage_AtFirstPage_StaysAtZero()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToPreviousPage();
        state.PageIndex.Should().Be(0);
    }

    [Fact]
    public void GoToFirstPage_ResetsToZero()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToLastPage();
        state.GoToFirstPage();
        state.PageIndex.Should().Be(0);
    }

    [Fact]
    public void GoToPage_SpecificPage_NavigatesCorrectly()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToPage(1);
        state.PageIndex.Should().Be(1);
    }

    [Fact]
    public void GoToPage_BeyondMax_ClampsToLastPage()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToPage(100);
        state.PageIndex.Should().Be(2); // last page
    }

    [Fact]
    public void GoToPage_NegativeIndex_ClampsToZero()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 2;
        state.Recalculate();
        state.GoToPage(-5);
        state.PageIndex.Should().Be(0);
    }

    [Fact]
    public void TotalPages_WithZeroPageSize_ReturnsOne()
    {
        var state = CreateStateWithColumns();
        state.PageSize = 0;
        state.Recalculate();
        state.TotalPages.Should().Be(1);
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

    [Fact]
    public void SelectionMode_None_DoesNotSelect()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.None;
        state.ToggleRow(TestData[0], 0);
        state.SelectedItems.Should().BeEmpty();
    }

    [Fact]
    public void ToggleAllOnPage_InSingleMode_DoesNothing()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Single;
        state.ToggleAllOnPage();
        state.SelectedItems.Should().BeEmpty();
    }

    [Fact]
    public void SingleSelect_ToggleSameItem_Deselects()
    {
        var state = CreateStateWithColumns();
        state.SelectionMode = SelectionMode.Single;
        state.ToggleRow(TestData[0], 0);
        state.IsSelected(TestData[0]).Should().BeTrue();
        state.ToggleRow(TestData[0], 0);
        state.IsSelected(TestData[0]).Should().BeFalse();
    }

    [Fact]
    public void IsAllOnPageSelected_EmptyPage_ReturnsFalse()
    {
        var state = CreateStateWithColumns();
        state.SetGlobalFilter("zzzzz_no_match");
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

    [Fact]
    public void IsColumnVisible_DefaultTrue()
    {
        var state = CreateStateWithColumns();
        state.IsColumnVisible("Name").Should().BeTrue();
    }

    [Fact]
    public void IsColumnVisible_AfterHide_ReturnsFalse()
    {
        var state = CreateStateWithColumns();
        state.SetColumnVisibility("Name", false);
        state.IsColumnVisible("Name").Should().BeFalse();
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

    [Fact]
    public void ToggleRowExpansion_MultipleRows_IndependentState()
    {
        var state = CreateStateWithColumns();
        state.ToggleRowExpansion(0);
        state.ToggleRowExpansion(2);
        state.IsRowExpanded(0).Should().BeTrue();
        state.IsRowExpanded(1).Should().BeFalse();
        state.IsRowExpanded(2).Should().BeTrue();
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

    [Fact]
    public void GetRowSpan_NonMergeColumn_ReturnsOne()
    {
        var state = CreateStateWithColumns();
        state.GetRowSpan("Name", 0).Should().Be(1);
    }

    [Fact]
    public void GetRowSpan_OutOfBounds_ReturnsOne()
    {
        var state = CreateStateWithColumns();
        state.ToggleSort("Department");
        state.GetRowSpan("Department", 100).Should().Be(1);
        state.GetRowSpan("Department", -1).Should().Be(1);
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

    [Fact]
    public void RegisterColumn_AssignsOrder()
    {
        var state = new DataTableState<TestItem>();
        state.RegisterColumn(new DataTableColumnDef<TestItem> { Id = "a", Title = "A" });
        state.RegisterColumn(new DataTableColumnDef<TestItem> { Id = "b", Title = "B" });
        state.Columns[0].Order.Should().Be(0);
        state.Columns[1].Order.Should().Be(1);
    }

    // --- Column groups ---
    [Fact]
    public void RegisterColumnGroup_AddsGroup()
    {
        var state = CreateStateWithColumns();
        state.RegisterColumnGroup(new DataTableColumnGroupDef { Id = "group1", Title = "Group 1" });
        state.ColumnGroups.Should().HaveCount(1);
    }

    [Fact]
    public void RegisterColumnGroup_PreventsDuplicate()
    {
        var state = CreateStateWithColumns();
        state.RegisterColumnGroup(new DataTableColumnGroupDef { Id = "g1", Title = "G1" });
        state.RegisterColumnGroup(new DataTableColumnGroupDef { Id = "g1", Title = "G1 Dup" });
        state.ColumnGroups.Should().HaveCount(1);
    }

    // --- OnChange event ---
    [Fact]
    public void SetGlobalFilter_TriggersOnChange()
    {
        var state = CreateStateWithColumns();
        var changed = false;
        state.OnChange += () => changed = true;
        state.SetGlobalFilter("test");
        changed.Should().BeTrue();
    }

    [Fact]
    public void SetColumnFilter_TriggersOnChange()
    {
        var state = CreateStateWithColumns();
        var changed = false;
        state.OnChange += () => changed = true;
        state.SetColumnFilter("Status", "Active");
        changed.Should().BeTrue();
    }

    [Fact]
    public void SetColumnVisibility_TriggersOnChange()
    {
        var state = CreateStateWithColumns();
        var changed = false;
        state.OnChange += () => changed = true;
        state.SetColumnVisibility("Name", false);
        changed.Should().BeTrue();
    }
}
