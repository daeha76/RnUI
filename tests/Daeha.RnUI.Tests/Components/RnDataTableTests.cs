using Bunit;
using FluentAssertions;
using RnUI.Components.UI.DataTable;
using RnUI.Components.UI.Table;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

public record TableTestItem(string Id, string Name, string Status, decimal Amount);

[Trait("Category", "Unit")]
public class RnDataTableTests : BunitContext
{
    private static readonly List<TableTestItem> TestItems =
    [
        new("1", "Alice", "Active", 100m),
        new("2", "Bob", "Inactive", 200m),
        new("3", "Charlie", "Active", 300m),
    ];

    [Fact]
    public void RnDataTable_DefaultRendering_AppliesBaseClass()
    {
        var cut = Render<RnDataTable<TableTestItem>>(p => p
            .Add(x => x.Items, TestItems)
            .Add(x => x.Columns, builder =>
            {
                builder.OpenComponent<RnDataTableColumn<TableTestItem>>(0);
                builder.AddAttribute(1, "Id", "Name");
                builder.AddAttribute(2, "Title", "Name");
                builder.AddAttribute(3, "Property",
                    (System.Linq.Expressions.Expression<Func<TableTestItem, object?>>)(x => x.Name));
                builder.CloseComponent();
            }));

        cut.Find("[data-slot='data-table']").ClassList.Should().Contain("cn-data-table");
    }

    [Fact]
    public void RnDataTable_DefaultRendering_HasDataSlot()
    {
        var cut = Render<RnDataTable<TableTestItem>>(p => p
            .Add(x => x.Items, TestItems)
            .Add(x => x.Columns, builder =>
            {
                builder.OpenComponent<RnDataTableColumn<TableTestItem>>(0);
                builder.AddAttribute(1, "Id", "Name");
                builder.AddAttribute(2, "Title", "Name");
                builder.AddAttribute(3, "Property",
                    (System.Linq.Expressions.Expression<Func<TableTestItem, object?>>)(x => x.Name));
                builder.CloseComponent();
            }));

        cut.Find("[data-slot='data-table']").Should().NotBeNull();
    }

    [Fact]
    public void RnDataTable_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnDataTable<TableTestItem>>(p => p
            .Add(x => x.Items, TestItems)
            .Add(x => x.Class, "my-table")
            .Add(x => x.Columns, builder =>
            {
                builder.OpenComponent<RnDataTableColumn<TableTestItem>>(0);
                builder.AddAttribute(1, "Id", "Name");
                builder.AddAttribute(2, "Title", "Name");
                builder.AddAttribute(3, "Property",
                    (System.Linq.Expressions.Expression<Func<TableTestItem, object?>>)(x => x.Name));
                builder.CloseComponent();
            }));

        cut.Find("[data-slot='data-table']").ClassList.Should().Contain("my-table");
    }

    [Fact]
    public void RnDataTable_Loading_ShowsSkeleton()
    {
        var cut = Render<RnDataTable<TableTestItem>>(p => p
            .Add(x => x.Items, TestItems)
            .Add(x => x.Loading, true)
            .Add(x => x.SkeletonRows, 3));

        cut.FindAll("[data-slot='skeleton']").Should().NotBeEmpty();
    }

    [Fact]
    public void RnDataTable_EmptyItems_ShowsEmptyMessage()
    {
        var cut = Render<RnDataTable<TableTestItem>>(p => p
            .Add(x => x.Items, new List<TableTestItem>())
            .Add(x => x.Columns, builder =>
            {
                builder.OpenComponent<RnDataTableColumn<TableTestItem>>(0);
                builder.AddAttribute(1, "Id", "Name");
                builder.AddAttribute(2, "Title", "Name");
                builder.AddAttribute(3, "Property",
                    (System.Linq.Expressions.Expression<Func<TableTestItem, object?>>)(x => x.Name));
                builder.CloseComponent();
            }));

        cut.Markup.Should().Contain("No results.");
    }
}
