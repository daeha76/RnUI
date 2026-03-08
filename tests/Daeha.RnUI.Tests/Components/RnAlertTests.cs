using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.Alert;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnAlertTests : BunitContext
{
    [Fact]
    public void RnAlert_DefaultRender_HasCnAlertClass()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.Find("[data-slot='alert']").ClassList.Should().Contain("cn-alert");
    }

    [Fact]
    public void RnAlert_DefaultRender_HasDefaultVariantClass()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.Find("[data-slot='alert']").ClassList.Should().Contain("cn-alert-variant-default");
    }

    [Fact]
    public void RnAlert_DefaultRender_HasRoleAlert()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.Find("[data-slot='alert']").GetAttribute("role").Should().Be("alert");
    }

    [Fact]
    public void RnAlert_DefaultRender_HasAlertBodyDiv()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.Find(".cn-alert-body").Should().NotBeNull();
    }

    [Fact]
    public void RnAlert_WithChildContent_RendersInAlertBody()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.Find(".cn-alert-body").TextContent.Should().Contain("Alert message");
    }

    [Fact]
    public void RnAlert_WithDestructiveVariant_AppliesCorrectClass()
    {
        var cut = Render<RnAlert>(p => p
            .Add(x => x.Variant, AlertVariant.Destructive)
            .AddChildContent("Error!"));

        var alert = cut.Find("[data-slot='alert']");
        alert.ClassList.Should().Contain("cn-alert-variant-destructive");
        alert.ClassList.Should().NotContain("cn-alert-variant-default");
    }

    [Fact]
    public void RnAlert_WithoutIcon_DoesNotRenderIconSpan()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.FindAll(".cn-alert-icon").Should().BeEmpty();
    }

    [Fact]
    public void RnAlert_WithIcon_RendersIconSpan()
    {
        var cut = Render<RnAlert>(p => p
            .Add(x => x.Icon, builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, "!");
                builder.CloseElement();
            })
            .AddChildContent("Alert message"));

        cut.Find(".cn-alert-icon").Should().NotBeNull();
    }

    [Fact]
    public void RnAlert_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnAlert>(p => p
            .Add(x => x.Class, "my-alert-class")
            .AddChildContent("Alert"));

        cut.Find("[data-slot='alert']").ClassList.Should().Contain("my-alert-class");
    }

    [Fact]
    public void RnAlert_WithDestructiveVariantAndIcon_AppliesDestructiveIconClass()
    {
        var cut = Render<RnAlert>(p => p
            .Add(x => x.Variant, AlertVariant.Destructive)
            .Add(x => x.Icon, builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, "!");
                builder.CloseElement();
            })
            .AddChildContent("Error!"));

        cut.Find(".cn-alert-icon").ClassList.Should().Contain("cn-alert-icon-destructive");
    }
}
