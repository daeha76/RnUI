using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.Switch;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnSwitchTests : BunitContext
{
    [Fact]
    public void RnSwitch_DefaultRender_HasDataSlotSwitch()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch']").Should().NotBeNull();
    }

    [Fact]
    public void RnSwitch_DefaultRender_HasCnSwitchClass()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch']").ClassList.Should().Contain("cn-switch");
    }

    [Fact]
    public void RnSwitch_DefaultRender_HasRoleSwitch()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch']").GetAttribute("role").Should().Be("switch");
    }

    [Fact]
    public void RnSwitch_DefaultChecked_IsAriaCheckedFalse()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch']").GetAttribute("aria-checked").Should().Be("false");
    }

    [Fact]
    public void RnSwitch_WithCheckedTrue_IsAriaCheckedTrue()
    {
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Checked, true));

        cut.Find("[data-slot='switch']").GetAttribute("aria-checked").Should().Be("true");
    }

    [Fact]
    public void RnSwitch_WithCheckedFalse_HasDataStateUnchecked()
    {
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Checked, false));

        cut.Find("[data-slot='switch']").GetAttribute("data-state").Should().Be("unchecked");
    }

    [Fact]
    public void RnSwitch_WithCheckedTrue_HasDataStateChecked()
    {
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Checked, true));

        cut.Find("[data-slot='switch']").GetAttribute("data-state").Should().Be("checked");
    }

    [Fact]
    public void RnSwitch_HasSwitchThumb()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch-thumb']").Should().NotBeNull();
    }

    [Fact]
    public void RnSwitch_SwitchThumb_HasCnSwitchThumbClass()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch-thumb']").ClassList.Should().Contain("cn-switch-thumb");
    }

    [Fact]
    public void RnSwitch_Disabled_HasDisabledAttribute()
    {
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Disabled, true));

        cut.Find("[data-slot='switch']").HasAttribute("disabled").Should().BeTrue();
    }

    [Fact]
    public void RnSwitch_NotDisabled_HasNoDisabledAttribute()
    {
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Disabled, false));

        cut.Find("[data-slot='switch']").HasAttribute("disabled").Should().BeFalse();
    }

    [Fact]
    public void RnSwitch_DefaultSize_HasDefaultDataSize()
    {
        var cut = Render<RnSwitch>();

        cut.Find("[data-slot='switch']").GetAttribute("data-size").Should().Be("default");
    }

    [Fact]
    public void RnSwitch_SmSize_HasSmDataSize()
    {
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Size, ComponentSize.Sm));

        cut.Find("[data-slot='switch']").GetAttribute("data-size").Should().Be("sm");
    }

    [Fact]
    public void RnSwitch_Click_TogglesCheckedAndInvokesCallback()
    {
        var newCheckedValue = false;
        var cut = Render<RnSwitch>(p => p
            .Add(x => x.Checked, false)
            .Add(x => x.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => newCheckedValue = v)));

        cut.Find("[data-slot='switch']").Click();
        newCheckedValue.Should().BeTrue();
    }
}
