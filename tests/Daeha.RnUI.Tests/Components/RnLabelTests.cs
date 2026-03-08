using Bunit;
using FluentAssertions;
using RnUI.Components.UI.Label;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnLabelTests : BunitContext
{
    [Fact]
    public void RnLabel_DefaultRender_HasDataSlotLabel()
    {
        var cut = Render<RnLabel>(p => p.AddChildContent("Username"));

        cut.Find("[data-slot='label']").Should().NotBeNull();
    }

    [Fact]
    public void RnLabel_DefaultRender_HasCnLabelClass()
    {
        var cut = Render<RnLabel>(p => p.AddChildContent("Username"));

        cut.Find("[data-slot='label']").ClassList.Should().Contain("cn-label");
    }

    [Fact]
    public void RnLabel_DefaultRender_RendersAsLabelElement()
    {
        var cut = Render<RnLabel>(p => p.AddChildContent("Username"));

        cut.Find("label").Should().NotBeNull();
    }

    [Fact]
    public void RnLabel_WithChildContent_RendersText()
    {
        var cut = Render<RnLabel>(p => p.AddChildContent("Username"));

        cut.Find("label").TextContent.Trim().Should().Be("Username");
    }

    [Fact]
    public void RnLabel_WithFor_RendersForAttribute()
    {
        var cut = Render<RnLabel>(p => p
            .Add(x => x.For, "input-id")
            .AddChildContent("Username"));

        cut.Find("label").GetAttribute("for").Should().Be("input-id");
    }

    [Fact]
    public void RnLabel_WithNullFor_DoesNotHaveForAttribute()
    {
        var cut = Render<RnLabel>(p => p
            .AddChildContent("Username"));

        // When For is null, attribute is not rendered or is null
        var forAttr = cut.Find("label").GetAttribute("for");
        forAttr.Should().BeNullOrEmpty();
    }

    [Fact]
    public void RnLabel_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnLabel>(p => p
            .Add(x => x.Class, "text-sm")
            .AddChildContent("Username"));

        cut.Find("label").ClassList.Should().Contain("text-sm");
    }

    [Fact]
    public void RnLabel_WithCustomClass_AlsoHasCnLabelClass()
    {
        var cut = Render<RnLabel>(p => p
            .Add(x => x.Class, "text-sm")
            .AddChildContent("Username"));

        cut.Find("label").ClassList.Should().Contain("cn-label");
    }
}
