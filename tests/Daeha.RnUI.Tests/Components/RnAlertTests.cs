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
    public void RnAlert_WithChildContent_RendersContent()
    {
        var cut = Render<RnAlert>(p => p.AddChildContent("Alert message"));

        cut.Find("[data-slot='alert']").TextContent.Should().Contain("Alert message");
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
    public void RnAlert_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnAlert>(p => p
            .Add(x => x.Class, "my-alert-class")
            .AddChildContent("Alert"));

        cut.Find("[data-slot='alert']").ClassList.Should().Contain("my-alert-class");
    }


}
