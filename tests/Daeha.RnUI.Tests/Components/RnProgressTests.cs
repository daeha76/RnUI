using Bunit;
using FluentAssertions;
using RnUI.Components.UI.Progress;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnProgressTests : BunitContext
{
    [Fact]
    public void RnProgress_DefaultRender_HasDataSlotProgress()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress']").Should().NotBeNull();
    }

    [Fact]
    public void RnProgress_DefaultRender_HasCnProgressClass()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress']").ClassList.Should().Contain("cn-progress");
    }



    [Fact]
    public void RnProgress_DefaultRender_HasProgressIndicator()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress-indicator']").Should().NotBeNull();
    }

    [Fact]
    public void RnProgress_ProgressTrack_HasRoleProgressbar()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress']").GetAttribute("role").Should().Be("progressbar");
    }

    [Fact]
    public void RnProgress_DefaultMax_IsOneHundred()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress']").GetAttribute("aria-valuemax").Should().Be("100");
    }

    [Fact]
    public void RnProgress_WithCustomMax_RendersCorrectAriaMax()
    {
        var cut = Render<RnProgress>(p => p
            .Add(x => x.Max, 200));

        cut.Find("[data-slot='progress']").GetAttribute("aria-valuemax").Should().Be("200");
    }

    [Fact]
    public void RnProgress_DefaultValue_IsZero()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress']").GetAttribute("aria-valuenow").Should().Be("0");
    }

    [Fact]
    public void RnProgress_WithValue50_RendersCorrectAriaNow()
    {
        var cut = Render<RnProgress>(p => p
            .Add(x => x.Value, 50));

        cut.Find("[data-slot='progress']").GetAttribute("aria-valuenow").Should().Be("50");
    }

    [Fact]
    public void RnProgress_WithValue50OutOf100_IndicatorHas50PercentWidth()
    {
        var cut = Render<RnProgress>(p => p
            .Add(x => x.Value, 50)
            .Add(x => x.Max, 100));

        var style = cut.Find("[data-slot='progress-indicator']").GetAttribute("style");
        style.Should().Contain("50");
    }

    [Fact]
    public void RnProgress_WithValue0_IndicatorHas0PercentWidth()
    {
        var cut = Render<RnProgress>(p => p
            .Add(x => x.Value, 0)
            .Add(x => x.Max, 100));

        var style = cut.Find("[data-slot='progress-indicator']").GetAttribute("style");
        style.Should().Contain("0");
    }

    [Fact]
    public void RnProgress_WithValue100_IndicatorHas100PercentWidth()
    {
        var cut = Render<RnProgress>(p => p
            .Add(x => x.Value, 100)
            .Add(x => x.Max, 100));

        var style = cut.Find("[data-slot='progress-indicator']").GetAttribute("style");
        style.Should().Contain("0%");
    }

    [Fact]
    public void RnProgress_ProgressTrack_HasAriaValueMin0()
    {
        var cut = Render<RnProgress>();

        cut.Find("[data-slot='progress']").GetAttribute("aria-valuemin").Should().Be("0");
    }

    [Fact]
    public void RnProgress_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnProgress>(p => p
            .Add(x => x.Class, "my-progress-class"));

        cut.Find("[data-slot='progress']").ClassList.Should().Contain("my-progress-class");
    }


}
