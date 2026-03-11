using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RnUI.Components.UI.Gantt;
using RnUI.Services;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnGanttTests : BunitContext
{
    public RnGanttTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddSingleton<RnUIInteropService>();
    }

    private static List<GanttRow> CreateSampleRows() =>
    [
        new()
        {
            Id = "1",
            Label = "Alex Morgan",
            SubLabel = "Engineer",
            Tasks =
            [
                new()
                {
                    Id = "t1",
                    Label = "Project Alpha",
                    StartDate = new(2026, 1, 1),
                    EndDate = new(2026, 3, 31),
                    Variant = BarVariant.Blue
                }
            ]
        },
        new()
        {
            Id = "2",
            Label = "Sarah Chen",
            Tasks = []
        }
    ];

    [Fact]
    public void RnGantt_DefaultRendering_AppliesBaseClass()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        cut.Find("[data-slot=\"gantt\"]").ClassList.Should().Contain("cn-gantt");
    }

    [Fact]
    public void RnGantt_WithCustomClass_MergesClasses()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows())
            .Add(p => p.Class, "my-custom-class"));

        var el = cut.Find("[data-slot=\"gantt\"]");
        el.ClassList.Should().Contain("cn-gantt");
        el.ClassList.Should().Contain("my-custom-class");
    }

    [Fact]
    public void RnGantt_RendersDataSlots()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        cut.Find("[data-slot=\"gantt\"]").Should().NotBeNull();
        cut.Find("[data-slot=\"gantt-container\"]").Should().NotBeNull();
        cut.Find("[data-slot=\"gantt-sidebar\"]").Should().NotBeNull();
        cut.Find("[data-slot=\"gantt-timeline\"]").Should().NotBeNull();
    }

    [Fact]
    public void RnGantt_RendersSidebarRows()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        var rows = cut.FindAll("[data-slot=\"gantt-sidebar-row\"]");
        rows.Count.Should().Be(2);
    }

    [Fact]
    public void RnGantt_SidebarRow_ShowsLabelAndSubLabel()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        var firstRow = cut.FindAll("[data-slot=\"gantt-sidebar-row\"]")[0];
        firstRow.TextContent.Should().Contain("Alex Morgan");
        firstRow.TextContent.Should().Contain("Engineer");
    }

    [Fact]
    public void RnGantt_RendersTimelineRows()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        var timelineRows = cut.FindAll("[data-slot=\"gantt-timeline-row\"]");
        timelineRows.Count.Should().Be(2);
    }

    [Fact]
    public void RnGantt_RendersBar_WithDataSlot()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        var bars = cut.FindAll("[data-slot=\"gantt-bar\"]");
        bars.Count.Should().Be(1); // Only first row has a task
    }

    [Fact]
    public void RnGantt_Bar_HasVariantAttribute()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        var bar = cut.Find("[data-slot=\"gantt-bar\"]");
        bar.GetAttribute("data-variant").Should().Be("blue");
    }

    [Fact]
    public void RnGantt_Bar_DisplaysLabel()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows()));

        var bar = cut.Find("[data-slot=\"gantt-bar\"]");
        bar.TextContent.Should().Contain("Project Alpha");
    }

    [Fact]
    public void RnGantt_ShowLegend_RendersLegendItems()
    {
        var legendItems = new List<GanttLegendItem>
        {
            new() { Label = "Structural", Variant = BarVariant.Blue },
            new() { Label = "MEP", Variant = BarVariant.Green }
        };

        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows())
            .Add(p => p.ShowLegend, true)
            .Add(p => p.LegendItems, legendItems));

        var legend = cut.Find("[data-slot=\"gantt-legend\"]");
        legend.TextContent.Should().Contain("Structural");
        legend.TextContent.Should().Contain("MEP");
    }

    [Fact]
    public void RnGantt_NoLegendItems_HidesLegend()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows())
            .Add(p => p.ShowLegend, true));

        cut.FindAll("[data-slot=\"gantt-legend\"]").Count.Should().Be(0);
    }

    [Fact]
    public void RnGantt_RendersHeaderCells()
    {
        var cut = Render<RnGantt>(parameters => parameters
            .Add(p => p.Rows, CreateSampleRows())
            .Add(p => p.ViewMode, GanttViewMode.Month));

        var headerCells = cut.FindAll(".cn-gantt-header-cell");
        headerCells.Count.Should().BeGreaterThan(0);
    }
}
