using Bunit;
using FluentAssertions;
using RnUI.Components.UI.Calendar;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnEventCalendarTests : BunitContext
{
    private static List<RnCalendarEvent> CreateSampleEvents() =>
    [
        new()
        {
            Id = "1",
            Title = "Team Meeting",
            Start = new DateTime(2026, 3, 15, 10, 0, 0),
            End = new DateTime(2026, 3, 15, 11, 0, 0),
            Variant = EventVariant.Blue
        },
        new()
        {
            Id = "2",
            Title = "All Day Event",
            Start = new DateTime(2026, 3, 15),
            IsAllDay = true,
            Variant = EventVariant.Green
        },
        new()
        {
            Id = "3",
            Title = "Multi-day Conference",
            Start = new DateTime(2026, 3, 10),
            End = new DateTime(2026, 3, 12),
            Variant = EventVariant.Red
        }
    ];

    [Fact]
    public void RnEventCalendar_DefaultRendering_AppliesBaseClass()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1)));

        var root = cut.Find("[data-slot=\"event-calendar\"]");
        root.ClassList.Should().Contain("cn-event-calendar");
    }

    [Fact]
    public void RnEventCalendar_HasDataSlot()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1)));

        cut.Find("[data-slot=\"event-calendar\"]").Should().NotBeNull();
    }

    [Fact]
    public void RnEventCalendar_HasRoleGrid()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1)));

        var root = cut.Find("[data-slot=\"event-calendar\"]");
        root.GetAttribute("role").Should().Be("grid");
    }

    [Fact]
    public void RnEventCalendar_MonthView_Renders42DayCells()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1)));

        var cells = cut.FindAll("[role=\"gridcell\"]");
        cells.Count.Should().Be(42);
    }

    [Fact]
    public void RnEventCalendar_TodayCell_HasAriaCurrentDate()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, DateTime.Today));

        var todayCells = cut.FindAll("[aria-current=\"date\"]");
        todayCells.Count.Should().Be(1);
    }

    [Fact]
    public void RnEventCalendar_SelectedCell_HasAriaSelectedTrue()
    {
        var selectedDate = new DateTime(2026, 3, 15);
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .Add(x => x.Selected, selectedDate));

        var selected = cut.FindAll("[aria-selected=\"true\"]");
        selected.Count.Should().Be(1);
    }

    [Fact]
    public void RnEventCalendar_OutsideMonthCells_HaveOutsideClass()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1)));

        var outsideCells = cut.FindAll(".cn-event-calendar-cell-outside");
        outsideCells.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void RnEventCalendar_WithEvents_RendersEventButtons()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .Add(x => x.Events, CreateSampleEvents()));

        var events = cut.FindAll(".cn-event-calendar-event");
        events.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void RnEventCalendar_EventVariant_AppliesCorrectClass()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .Add(x => x.Events, CreateSampleEvents()));

        var blueEvents = cut.FindAll(".cn-event-calendar-event-variant-blue");
        blueEvents.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void RnEventCalendar_EventClick_FiresCallback()
    {
        RnCalendarEvent? clickedEvent = null;
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .Add(x => x.Events, CreateSampleEvents())
            .Add(x => x.OnEventClicked, (RnCalendarEvent ev) => { clickedEvent = ev; }));

        var eventBtn = cut.Find(".cn-event-calendar-event");
        eventBtn.Click();

        clickedEvent.Should().NotBeNull();
    }

    [Fact]
    public void RnEventCalendar_CustomClass_IsMerged()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .Add(x => x.Class, "my-custom-class"));

        var root = cut.Find("[data-slot=\"event-calendar\"]");
        root.ClassList.Should().Contain("cn-event-calendar");
        root.ClassList.Should().Contain("my-custom-class");
    }

    [Fact]
    public void RnEventCalendar_AdditionalAttributes_AreSpread()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .AddUnmatched("data-testid", "my-calendar"));

        var root = cut.Find("[data-slot=\"event-calendar\"]");
        root.GetAttribute("data-testid").Should().Be("my-calendar");
    }

    [Fact]
    public void RnEventCalendar_NavigationButtons_HaveAriaLabels()
    {
        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1)));

        var prevBtn = cut.Find("[aria-label=\"Previous month\"]");
        var nextBtn = cut.Find("[aria-label=\"Next month\"]");
        prevBtn.Should().NotBeNull();
        nextBtn.Should().NotBeNull();
    }

    [Fact]
    public void RnEventCalendar_Overflow_ShowsMoreIndicator()
    {
        var events = new List<RnCalendarEvent>();
        for (var i = 0; i < 5; i++)
        {
            events.Add(new RnCalendarEvent
            {
                Id = i.ToString(),
                Title = $"Event {i}",
                Start = new DateTime(2026, 3, 15, 9 + i, 0, 0),
                Variant = EventVariant.Default
            });
        }

        var cut = Render<RnEventCalendar>(p => p
            .Add(x => x.DisplayMonth, new DateTime(2026, 3, 1))
            .Add(x => x.Events, events)
            .Add(x => x.MaxEventsPerDay, 3));

        var overflow = cut.FindAll(".cn-event-calendar-overflow");
        overflow.Count.Should().BeGreaterThan(0);
    }
}

[Trait("Category", "Unit")]
public class EventCalendarLayoutTests
{
    [Fact]
    public void GetSingleDayEvents_FiltersCorrectly()
    {
        var events = new List<RnCalendarEvent>
        {
            new() { Id = "1", Title = "Single", Start = new DateTime(2026, 3, 15, 10, 0, 0) },
            new() { Id = "2", Title = "Multi", Start = new DateTime(2026, 3, 14), End = new DateTime(2026, 3, 16) }
        };

        var result = EventCalendarLayout.GetSingleDayEvents(events, new DateTime(2026, 3, 15));

        result.Should().HaveCount(1);
        result[0].Title.Should().Be("Single");
    }

    [Fact]
    public void CalculateWeekSlots_PositionsMultiDayEvent()
    {
        var events = new List<RnCalendarEvent>
        {
            new()
            {
                Id = "1",
                Title = "3-day event",
                Start = new DateTime(2026, 3, 9),   // Monday
                End = new DateTime(2026, 3, 11)      // Wednesday
            }
        };

        var weekStart = new DateTime(2026, 3, 8); // Sunday
        var slots = EventCalendarLayout.CalculateWeekSlots(events, weekStart);

        slots.Should().HaveCount(1);
        slots[0].StartColumn.Should().Be(1); // Monday = column 1
        slots[0].ColumnSpan.Should().Be(3);  // Mon-Wed = 3 columns
        slots[0].IsStart.Should().BeTrue();
        slots[0].IsEnd.Should().BeTrue();
    }

    [Fact]
    public void CalculateWeekSlots_ClampsToWeekBoundary()
    {
        var events = new List<RnCalendarEvent>
        {
            new()
            {
                Id = "1",
                Title = "Spans two weeks",
                Start = new DateTime(2026, 3, 6),   // Friday (previous week)
                End = new DateTime(2026, 3, 10)      // Tuesday (this week)
            }
        };

        var weekStart = new DateTime(2026, 3, 8); // Sunday
        var slots = EventCalendarLayout.CalculateWeekSlots(events, weekStart);

        slots.Should().HaveCount(1);
        slots[0].StartColumn.Should().Be(0); // Clamped to Sunday
        slots[0].ColumnSpan.Should().Be(3);  // Sun-Tue = 3 columns
        slots[0].IsStart.Should().BeFalse(); // Doesn't start in this week
        slots[0].IsEnd.Should().BeTrue();    // Ends in this week
    }

    [Fact]
    public void IsMultiDay_ReturnsTrueForMultiDayEvents()
    {
        var ev = new RnCalendarEvent
        {
            Id = "1",
            Title = "Multi",
            Start = new DateTime(2026, 3, 10),
            End = new DateTime(2026, 3, 12)
        };

        EventCalendarLayout.IsMultiDay(ev).Should().BeTrue();
    }

    [Fact]
    public void IsMultiDay_ReturnsFalseForSingleDayEvents()
    {
        var ev = new RnCalendarEvent
        {
            Id = "1",
            Title = "Single",
            Start = new DateTime(2026, 3, 10, 10, 0, 0),
            End = new DateTime(2026, 3, 10, 11, 0, 0)
        };

        EventCalendarLayout.IsMultiDay(ev).Should().BeFalse();
    }
}
