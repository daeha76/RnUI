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
    public void RnButton_WithSmSize_AppliesCorrectClass()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Size, ButtonSize.Sm)
            .AddChildContent("Small"));

        cut.Find("button").ClassList.Should().Contain("cn-button-size-sm");
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

    [Fact]
    public void RnButton_WithDataSlotAttribute()
    {
        var cut = Render<RnButton>(parameters => parameters
            .AddChildContent("Button"));

        cut.Find("button").GetAttribute("data-slot").Should().Be("button");
    }

    [Fact]
    public void RnButton_WithSubmitType_RendersCorrectType()
    {
        var cut = Render<RnButton>(parameters => parameters
            .Add(p => p.Type, "submit")
            .AddChildContent("Submit"));

        cut.Find("button").GetAttribute("type").Should().Be("submit");
    }
}
