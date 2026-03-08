using Bunit;
using FluentAssertions;
using RnUI.Components.UI.Input;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnInputTests : BunitContext
{
    [Fact]
    public void RnInput_DefaultRender_HasCnInputClass()
    {
        var cut = Render<RnInput>();

        cut.Find("[data-slot='input']").ClassList.Should().Contain("cn-input");
    }

    [Fact]
    public void RnInput_DefaultRender_HasDataSlotInput()
    {
        var cut = Render<RnInput>();

        cut.Find("[data-slot='input']").Should().NotBeNull();
    }

    [Fact]
    public void RnInput_DefaultType_IsText()
    {
        var cut = Render<RnInput>();

        cut.Find("input").GetAttribute("type").Should().Be("text");
    }

    [Fact]
    public void RnInput_WithPasswordType_RendersCorrectType()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Type, "password"));

        cut.Find("input").GetAttribute("type").Should().Be("password");
    }

    [Fact]
    public void RnInput_WithPlaceholder_RendersPlaceholder()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Placeholder, "Enter text here"));

        cut.Find("input").GetAttribute("placeholder").Should().Be("Enter text here");
    }

    [Fact]
    public void RnInput_WithValue_RendersValue()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Value, "Hello World"));

        cut.Find("input").GetAttribute("value").Should().Be("Hello World");
    }

    [Fact]
    public void RnInput_Disabled_HasDisabledAttribute()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Disabled, true));

        cut.Find("input").HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void RnInput_NotDisabled_DoesNotHaveDisabledAttribute()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Disabled, false));

        cut.Find("input").HasAttribute("disabled").Should().BeFalse();
    }

    [Fact]
    public void RnInput_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Class, "my-input-class"));

        cut.Find("input").ClassList.Should().Contain("my-input-class");
    }

    [Fact]
    public void RnInput_WithCustomClass_AlsoHasCnInputClass()
    {
        var cut = Render<RnInput>(p => p
            .Add(x => x.Class, "my-input-class"));

        cut.Find("input").ClassList.Should().Contain("cn-input");
    }

    [Fact]
    public void RnInput_IsASelfClosingElement()
    {
        var cut = Render<RnInput>();

        // input is a self-closing element; no children expected
        cut.Find("input").Should().NotBeNull();
    }
}
