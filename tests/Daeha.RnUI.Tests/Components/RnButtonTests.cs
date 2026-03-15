using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.Button;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnButtonTests : BunitContext
{
    // --- Default rendering ---

    [Fact]
    public void RnButton_DefaultRender_HasCnButtonClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Click me"));

        var button = cut.Find("button");
        button.ClassList.Should().Contain("cn-button");
    }

    [Fact]
    public void RnButton_DefaultRender_HasDefaultVariantClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Click me"));

        var button = cut.Find("button");
        button.ClassList.Should().Contain("cn-button-variant-default");
    }

    [Fact]
    public void RnButton_DefaultRender_HasDefaultSizeClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Click me"));

        var button = cut.Find("button");
        button.ClassList.Should().Contain("cn-button-size-default");
    }

    [Fact]
    public void RnButton_WithChildContent_RendersText()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Click me"));

        cut.Find("button").TextContent.Trim().Should().Be("Click me");
    }

    [Fact]
    public void RnButton_DefaultType_IsButton()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Click me"));

        cut.Find("button").GetAttribute("type").Should().Be("button");
    }

    // --- data-slot attribute ---

    [Fact]
    public void RnButton_WithDataSlotAttribute()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Button"));

        cut.Find("button").GetAttribute("data-slot").Should().Be("button");
    }

    // --- data-variant attribute ---

    [Fact]
    public void RnButton_DefaultRender_HasDataVariantDefault()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Btn"));

        cut.Find("button").GetAttribute("data-variant").Should().Be("default");
    }

    [Fact]
    public void RnButton_WithGhostVariant_HasDataVariantGhost()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Ghost)
            .AddChildContent("Ghost"));

        cut.Find("button").GetAttribute("data-variant").Should().Be("ghost");
    }

    // --- data-size attribute ---

    [Fact]
    public void RnButton_DefaultRender_HasDataSizeDefault()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Btn"));

        cut.Find("button").GetAttribute("data-size").Should().Be("default");
    }

    [Fact]
    public void RnButton_WithLgSize_HasDataSizeLg()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Lg)
            .AddChildContent("Large"));

        cut.Find("button").GetAttribute("data-size").Should().Be("lg");
    }

    // --- All Variants ---

    [Fact]
    public void RnButton_WithDestructiveVariant_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Destructive)
            .AddChildContent("Delete"));

        var button = cut.Find("button");
        button.ClassList.Should().Contain("cn-button-variant-destructive");
        button.ClassList.Should().NotContain("cn-button-variant-default");
    }

    [Fact]
    public void RnButton_WithOutlineVariant_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Outline)
            .AddChildContent("Outline"));

        cut.Find("button").ClassList.Should().Contain("cn-button-variant-outline");
    }

    [Fact]
    public void RnButton_WithSecondaryVariant_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Secondary)
            .AddChildContent("Secondary"));

        cut.Find("button").ClassList.Should().Contain("cn-button-variant-secondary");
    }

    [Fact]
    public void RnButton_WithGhostVariant_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Ghost)
            .AddChildContent("Ghost"));

        cut.Find("button").ClassList.Should().Contain("cn-button-variant-ghost");
    }

    [Fact]
    public void RnButton_WithLinkVariant_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Link)
            .AddChildContent("Link"));

        cut.Find("button").ClassList.Should().Contain("cn-button-variant-link");
    }

    // --- All Sizes ---

    [Fact]
    public void RnButton_WithXsSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Xs)
            .AddChildContent("XS"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-xs");
    }

    [Fact]
    public void RnButton_WithSmSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Sm)
            .AddChildContent("Small"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-sm");
    }

    [Fact]
    public void RnButton_WithLgSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Lg)
            .AddChildContent("Large"));

        var button = cut.Find("button");
        button.ClassList.Should().Contain("cn-button-size-lg");
        button.ClassList.Should().NotContain("cn-button-size-default");
    }

    [Fact]
    public void RnButton_WithIconSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Icon)
            .AddChildContent("I"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-icon");
    }

    [Fact]
    public void RnButton_WithIconXsSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.IconXs)
            .AddChildContent("I"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-icon-xs");
    }

    [Fact]
    public void RnButton_WithIconSmSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.IconSm)
            .AddChildContent("I"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-icon-sm");
    }

    [Fact]
    public void RnButton_WithIconLgSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.IconLg)
            .AddChildContent("I"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-icon-lg");
    }

    // --- Disabled state ---

    [Fact]
    public void RnButton_Disabled_HasDisabledAttribute()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Disabled, true)
            .AddChildContent("Disabled"));

        var button = cut.Find("button");
        button.HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void RnButton_NotDisabled_DoesNotHaveDisabledAttribute()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Disabled, false)
            .AddChildContent("Enabled"));

        var button = cut.Find("button");
        button.HasAttribute("disabled").Should().BeFalse();
    }

    // --- Click handling ---

    [Fact]
    public void RnButton_Click_InvokesOnClick()
    {
        var clicked = false;
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => clicked = true))
            .AddChildContent("Click"));

        cut.Find("button").Click();
        clicked.Should().BeTrue();
    }

    [Fact]
    public void RnButton_MultipleClicks_InvokesEachTime()
    {
        var clickCount = 0;
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => clickCount++))
            .AddChildContent("Click"));

        cut.Find("button").Click();
        cut.Find("button").Click();
        cut.Find("button").Click();
        clickCount.Should().Be(3);
    }

    // --- Custom class ---

    [Fact]
    public void RnButton_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Class, "my-custom-class")
            .AddChildContent("Custom"));

        cut.Find("button").ClassList.Should().Contain("my-custom-class");
    }

    [Fact]
    public void RnButton_WithCustomClass_AlsoHasCnButtonClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Class, "my-custom-class")
            .AddChildContent("Custom"));

        cut.Find("button").ClassList.Should().Contain("cn-button");
    }

    // --- Type attribute ---

    [Fact]
    public void RnButton_WithSubmitType_RendersCorrectType()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Type, "submit")
            .AddChildContent("Submit"));

        cut.Find("button").GetAttribute("type").Should().Be("submit");
    }

    [Fact]
    public void RnButton_WithResetType_RendersCorrectType()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Type, "reset")
            .AddChildContent("Reset"));

        cut.Find("button").GetAttribute("type").Should().Be("reset");
    }

    // --- AdditionalAttributes ---

    [Fact]
    public void RnButton_WithAdditionalAttributes_PassesThrough()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.AdditionalAttributes, new Dictionary<string, object> { { "aria-label", "Close" } })
            .AddChildContent("X"));

        cut.Find("button").GetAttribute("aria-label").Should().Be("Close");
    }

    [Fact]
    public void RnButton_WithId_PassesThrough()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.AdditionalAttributes, new Dictionary<string, object> { { "id", "my-btn" } })
            .AddChildContent("Btn"));

        cut.Find("button").GetAttribute("id").Should().Be("my-btn");
    }
}
