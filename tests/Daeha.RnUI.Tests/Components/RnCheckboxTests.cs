using Bunit;
using FluentAssertions;
using RnUI.Components.UI.Checkbox;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnCheckboxTests : BunitContext
{
    [Fact]
    public void RnCheckbox_DefaultRender_HasDataSlotCheckbox()
    {
        var cut = Render<RnCheckbox>();

        cut.Find("[data-slot='checkbox']").Should().NotBeNull();
    }

    [Fact]
    public void RnCheckbox_DefaultRender_HasCnCheckboxClass()
    {
        var cut = Render<RnCheckbox>();

        cut.Find("[data-slot='checkbox']").ClassList.Should().Contain("cn-checkbox");
    }

    [Fact]
    public void RnCheckbox_DefaultRender_HasRoleCheckbox()
    {
        var cut = Render<RnCheckbox>();

        cut.Find("[data-slot='checkbox']").GetAttribute("role").Should().Be("checkbox");
    }

    [Fact]
    public void RnCheckbox_DefaultChecked_IsAriaCheckedFalse()
    {
        var cut = Render<RnCheckbox>();

        cut.Find("[data-slot='checkbox']").GetAttribute("aria-checked").Should().Be("false");
    }

    [Fact]
    public void RnCheckbox_WithCheckedTrue_IsAriaCheckedTrue()
    {
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, true));

        cut.Find("[data-slot='checkbox']").GetAttribute("aria-checked").Should().Be("true");
    }

    [Fact]
    public void RnCheckbox_WithCheckedFalse_HasNoDataCheckedAttribute()
    {
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, false));

        cut.Find("[data-slot='checkbox']").HasAttribute("data-checked").Should().BeFalse();
    }

    [Fact]
    public void RnCheckbox_WithCheckedTrue_HasDataCheckedAttribute()
    {
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, true));

        cut.Find("[data-slot='checkbox']").HasAttribute("data-checked").Should().BeTrue();
    }

    [Fact]
    public void RnCheckbox_WhenChecked_RendersIndicator()
    {
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, true));

        cut.Find("[data-slot='checkbox-indicator']").Should().NotBeNull();
    }

    [Fact]
    public void RnCheckbox_WhenNotChecked_DoesNotRenderIndicator()
    {
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, false));

        cut.FindAll("[data-slot='checkbox-indicator']").Should().BeEmpty();
    }

    [Fact]
    public void RnCheckbox_Click_TogglesCheckedAndInvokesCallback()
    {
        var newCheckedValue = false;
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, false)
            .Add(x => x.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => newCheckedValue = v)));

        cut.Find("[data-slot='checkbox']").Click();
        newCheckedValue.Should().BeTrue();
    }

    [Fact]
    public void RnCheckbox_ClickOnChecked_TogglesBackToFalse()
    {
        var newCheckedValue = true;
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Checked, true)
            .Add(x => x.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => newCheckedValue = v)));

        cut.Find("[data-slot='checkbox']").Click();
        newCheckedValue.Should().BeFalse();
    }

    [Fact]
    public void RnCheckbox_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnCheckbox>(p => p
            .Add(x => x.Class, "my-checkbox-class"));

        cut.Find("[data-slot='checkbox']").ClassList.Should().Contain("my-checkbox-class");
    }
}
