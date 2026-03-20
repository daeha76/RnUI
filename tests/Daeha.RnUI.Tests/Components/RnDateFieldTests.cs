using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using RnUI.Components.UI;
using RnUI.Components.UI.DateField;
using RnUI.Services;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnDateFieldTests : BunitContext
{
    public RnDateFieldTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
        Services.AddSingleton<RnUIInteropService>();
    }
    [Fact]
    public void RnDateField_DefaultRender_HasFieldWrapper()
    {
        var cut = Render<RnDateField>();

        cut.Find("[data-slot='field']").ClassList.Should().Contain("cn-field");
    }

    [Fact]
    public void RnDateField_DefaultRender_HasDatePicker()
    {
        var cut = Render<RnDateField>();

        cut.Find("[data-slot='date-picker']").Should().NotBeNull();
    }

    [Fact]
    public void RnDateField_WithLabel_RendersFieldLabel()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Label, "생년월일"));

        var label = cut.Find("[data-slot='field-label']");
        label.TextContent.Should().Contain("생년월일");
    }

    [Fact]
    public void RnDateField_WithoutLabel_DoesNotRenderFieldLabel()
    {
        var cut = Render<RnDateField>();

        cut.FindAll("[data-slot='field-label']").Should().BeEmpty();
    }

    [Fact]
    public void RnDateField_WithStringValue_ParsesAndDisplays()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Value, "2025-03-15"));

        var input = cut.Find("[data-slot='date-picker-input']");
        input.GetAttribute("value").Should().Be("2025-03-15");
    }

    [Fact]
    public void RnDateField_WithEmptyValue_InputIsEmpty()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Value, ""));

        var input = cut.Find("[data-slot='date-picker-input']");
        input.GetAttribute("value").Should().BeEmpty();
    }

    [Fact]
    public void RnDateField_WithNullValue_InputIsEmpty()
    {
        var cut = Render<RnDateField>();

        var input = cut.Find("[data-slot='date-picker-input']");
        input.GetAttribute("value").Should().BeEmpty();
    }

    [Fact]
    public void RnDateField_Disabled_DisablesDatePicker()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Disabled, true));

        cut.Find("[data-slot='date-picker-input']")
            .HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void RnDateField_WithClass_AppliedToFieldWrapper()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Class, "my-field"));

        cut.Find("[data-slot='field']").ClassList.Should().Contain("my-field");
    }

    [Fact]
    public void RnDateField_HorizontalOrientation_SetsDataAttribute()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Orientation, FieldOrientation.Horizontal));

        cut.Find("[data-slot='field']")
            .GetAttribute("data-orientation").Should().Be("horizontal");
    }

    [Fact]
    public void RnDateField_WithDescription_RendersFieldDescription()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Description, "도움말"));

        cut.Find("[data-slot='field-description']")
            .TextContent.Should().Contain("도움말");
    }

    [Fact]
    public void RnDateField_WithoutDescription_DoesNotRenderFieldDescription()
    {
        var cut = Render<RnDateField>();

        cut.FindAll("[data-slot='field-description']").Should().BeEmpty();
    }

    [Fact]
    public void RnDateField_WithPlaceholder_PassesToDatePicker()
    {
        var cut = Render<RnDateField>(p => p
            .Add(x => x.Placeholder, "날짜 선택..."));

        cut.Find("[data-slot='date-picker-input']")
            .GetAttribute("placeholder").Should().Be("날짜 선택...");
    }
}
