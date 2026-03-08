using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.Card;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnCardTests : BunitContext
{
    [Fact]
    public void RnCard_DefaultRender_HasCnCardClass()
    {
        var cut = Render<RnCard>(p => p.AddChildContent("Card content"));

        var card = cut.Find("[data-slot='card']");
        card.ClassList.Should().Contain("cn-card");
    }

    [Fact]
    public void RnCard_DefaultRender_HasDefaultDataSize()
    {
        var cut = Render<RnCard>(p => p.AddChildContent("Card content"));

        var card = cut.Find("[data-slot='card']");
        card.GetAttribute("data-size").Should().Be("default");
    }

    [Fact]
    public void RnCard_WithSmSize_HasSmDataSize()
    {
        var cut = Render<RnCard>(p => p
            .Add(x => x.Size, ComponentSize.Sm)
            .AddChildContent("Card content"));

        var card = cut.Find("[data-slot='card']");
        card.GetAttribute("data-size").Should().Be("sm");
    }

    [Fact]
    public void RnCard_WithChildContent_RendersContent()
    {
        var cut = Render<RnCard>(p => p.AddChildContent("Hello Card"));

        cut.Find("[data-slot='card']").TextContent.Should().Contain("Hello Card");
    }

    [Fact]
    public void RnCard_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnCard>(p => p
            .Add(x => x.Class, "my-card-class")
            .AddChildContent("Card"));

        cut.Find("[data-slot='card']").ClassList.Should().Contain("my-card-class");
    }

    [Fact]
    public void RnCardHeader_DefaultRender_HasCorrectDataSlot()
    {
        var cut = Render<RnCardHeader>(p => p.AddChildContent("Header"));

        cut.Find("[data-slot='card-header']").Should().NotBeNull();
    }

    [Fact]
    public void RnCardHeader_DefaultRender_HasCnCardHeaderClass()
    {
        var cut = Render<RnCardHeader>(p => p.AddChildContent("Header"));

        cut.Find("[data-slot='card-header']").ClassList.Should().Contain("cn-card-header");
    }

    [Fact]
    public void RnCardTitle_DefaultRender_HasCorrectDataSlot()
    {
        var cut = Render<RnCardTitle>(p => p.AddChildContent("Title"));

        cut.Find("[data-slot='card-title']").Should().NotBeNull();
    }

    [Fact]
    public void RnCardTitle_DefaultRender_HasCnCardTitleClass()
    {
        var cut = Render<RnCardTitle>(p => p.AddChildContent("Title"));

        cut.Find("[data-slot='card-title']").ClassList.Should().Contain("cn-card-title");
    }

    [Fact]
    public void RnCardContent_DefaultRender_HasCorrectDataSlot()
    {
        var cut = Render<RnCardContent>(p => p.AddChildContent("Content"));

        cut.Find("[data-slot='card-content']").Should().NotBeNull();
    }

    [Fact]
    public void RnCardContent_DefaultRender_HasCnCardContentClass()
    {
        var cut = Render<RnCardContent>(p => p.AddChildContent("Content"));

        cut.Find("[data-slot='card-content']").ClassList.Should().Contain("cn-card-content");
    }

    [Fact]
    public void RnCardFooter_DefaultRender_HasCorrectDataSlot()
    {
        var cut = Render<RnCardFooter>(p => p.AddChildContent("Footer"));

        cut.Find("[data-slot='card-footer']").Should().NotBeNull();
    }

    [Fact]
    public void RnCardFooter_DefaultRender_HasCnCardFooterClass()
    {
        var cut = Render<RnCardFooter>(p => p.AddChildContent("Footer"));

        cut.Find("[data-slot='card-footer']").ClassList.Should().Contain("cn-card-footer");
    }

    [Fact]
    public void RnCardTitle_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnCardTitle>(p => p
            .Add(x => x.Class, "text-xl")
            .AddChildContent("Title"));

        cut.Find("[data-slot='card-title']").ClassList.Should().Contain("text-xl");
    }
}
