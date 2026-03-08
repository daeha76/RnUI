using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.Badge;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnBadgeTests : BunitContext
{
    [Fact]
    public void RnBadge_DefaultRender_HasCnBadgeClass()
    {
        var cut = Render<RnBadge>(p => p.AddChildContent("Badge"));

        var badge = cut.Find("[data-slot='badge']");
        badge.ClassList.Should().Contain("cn-badge");
    }

    [Fact]
    public void RnBadge_DefaultRender_HasDefaultVariantClass()
    {
        var cut = Render<RnBadge>(p => p.AddChildContent("Badge"));

        var badge = cut.Find("[data-slot='badge']");
        badge.ClassList.Should().Contain("cn-badge-variant-default");
    }

    [Fact]
    public void RnBadge_DefaultRender_RendersAsSpan()
    {
        var cut = Render<RnBadge>(p => p.AddChildContent("Badge"));

        cut.Find("span").Should().NotBeNull();
    }

    [Fact]
    public void RnBadge_WithChildContent_RendersText()
    {
        var cut = Render<RnBadge>(p => p.AddChildContent("Hello Badge"));

        cut.Find("[data-slot='badge']").TextContent.Trim().Should().Be("Hello Badge");
    }

    [Fact]
    public void RnBadge_WithDestructiveVariant_AppliesCorrectClass()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Destructive)
            .AddChildContent("Error"));

        var badge = cut.Find("[data-slot='badge']");
        badge.ClassList.Should().Contain("cn-badge-variant-destructive");
        badge.ClassList.Should().NotContain("cn-badge-variant-default");
    }

    [Fact]
    public void RnBadge_WithSecondaryVariant_AppliesCorrectClass()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Secondary)
            .AddChildContent("Secondary"));

        cut.Find("[data-slot='badge']").ClassList.Should().Contain("cn-badge-variant-secondary");
    }

    [Fact]
    public void RnBadge_WithOutlineVariant_AppliesCorrectClass()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Outline)
            .AddChildContent("Outline"));

        cut.Find("[data-slot='badge']").ClassList.Should().Contain("cn-badge-variant-outline");
    }

    [Fact]
    public void RnBadge_WithGhostVariant_AppliesCorrectClass()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Ghost)
            .AddChildContent("Ghost"));

        cut.Find("[data-slot='badge']").ClassList.Should().Contain("cn-badge-variant-ghost");
    }

    [Fact]
    public void RnBadge_WithLinkVariant_AppliesCorrectClass()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Link)
            .AddChildContent("Link"));

        cut.Find("[data-slot='badge']").ClassList.Should().Contain("cn-badge-variant-link");
    }

    [Fact]
    public void RnBadge_WithCustomClass_MergesClasses()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Class, "custom-class")
            .AddChildContent("Badge"));

        var badge = cut.Find("[data-slot='badge']");
        badge.ClassList.Should().Contain("custom-class");
        badge.ClassList.Should().Contain("cn-badge");
    }

    [Fact]
    public void RnBadge_HasDataVariantAttribute()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Default)
            .AddChildContent("Badge"));

        var badge = cut.Find("[data-slot='badge']");
        badge.GetAttribute("data-variant").Should().Be("default");
    }

    [Fact]
    public void RnBadge_WithDestructiveVariant_HasCorrectDataVariantAttribute()
    {
        var cut = Render<RnBadge>(p => p
            .Add(x => x.Variant, BadgeVariant.Destructive)
            .AddChildContent("Badge"));

        var badge = cut.Find("[data-slot='badge']");
        badge.GetAttribute("data-variant").Should().Be("destructive");
    }
}
