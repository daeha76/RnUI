using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.TextField;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnTextFieldTests : BunitContext
{
    [Fact]
    public void RnTextField_DefaultRender_HasFieldWrapper()
    {
        var cut = Render<RnTextField>();

        cut.Find("[data-slot='field']").ClassList.Should().Contain("cn-field");
    }

    [Fact]
    public void RnTextField_DefaultRender_HasInput()
    {
        var cut = Render<RnTextField>();

        cut.Find("[data-slot='input']").ClassList.Should().Contain("cn-input");
    }

    [Fact]
    public void RnTextField_WithLabel_RendersFieldLabel()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Label, "이름"));

        var label = cut.Find("[data-slot='field-label']");
        label.TextContent.Should().Contain("이름");
    }

    [Fact]
    public void RnTextField_WithoutLabel_DoesNotRenderFieldLabel()
    {
        var cut = Render<RnTextField>();

        cut.FindAll("[data-slot='field-label']").Should().BeEmpty();
    }

    [Fact]
    public void RnTextField_WithValue_RendersInputValue()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Value, "Hello"));

        cut.Find("input").GetAttribute("value").Should().Be("Hello");
    }

    [Fact]
    public void RnTextField_WithPlaceholder_RendersPlaceholder()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Placeholder, "입력하세요"));

        cut.Find("input").GetAttribute("placeholder").Should().Be("입력하세요");
    }

    [Fact]
    public void RnTextField_WithPasswordType_RendersCorrectType()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Type, "password"));

        cut.Find("input").GetAttribute("type").Should().Be("password");
    }

    [Fact]
    public void RnTextField_Disabled_HasDisabledAttribute()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Disabled, true));

        cut.Find("input").HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void RnTextField_WithClass_AppliedToFieldWrapper()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Class, "my-field"));

        cut.Find("[data-slot='field']").ClassList.Should().Contain("my-field");
    }

    [Fact]
    public void RnTextField_WithInputClass_AppliedToInput()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.InputClass, "my-input"));

        cut.Find("input").ClassList.Should().Contain("my-input");
    }

    [Fact]
    public void RnTextField_WithId_LinksLabelFor()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Label, "이름")
            .Add(x => x.Id, "name-field"));

        cut.Find("[data-slot='field-label']")
            .GetAttribute("for").Should().Be("name-field");
    }

    [Fact]
    public void RnTextField_HorizontalOrientation_SetsDataAttribute()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Orientation, FieldOrientation.Horizontal));

        cut.Find("[data-slot='field']")
            .GetAttribute("data-orientation").Should().Be("horizontal");
    }

    [Fact]
    public void RnTextField_WithDescription_RendersFieldDescription()
    {
        var cut = Render<RnTextField>(p => p
            .Add(x => x.Description, "도움말 텍스트"));

        var desc = cut.Find("[data-slot='field-description']");
        desc.TextContent.Should().Contain("도움말 텍스트");
    }

    [Fact]
    public void RnTextField_WithoutDescription_DoesNotRenderFieldDescription()
    {
        var cut = Render<RnTextField>();

        cut.FindAll("[data-slot='field-description']").Should().BeEmpty();
    }
}
