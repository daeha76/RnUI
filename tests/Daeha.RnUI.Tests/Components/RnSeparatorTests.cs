using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.Separator;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnSeparatorTests : BunitContext
{
    [Fact]
    public void RnSeparator_DefaultRender_IsHorizontal()
    {
        var cut = Render<RnSeparator>();

        var separator = cut.Find("[data-slot='separator']");
        separator.GetAttribute("data-orientation").Should().Be("horizontal");
    }

    [Fact]
    public void RnSeparator_DefaultRender_HasRoleSeparator()
    {
        var cut = Render<RnSeparator>();

        cut.Find("[data-slot='separator']").GetAttribute("role").Should().Be("separator");
    }



    [Fact]
    public void RnSeparator_VerticalOrientation_HasCorrectDataOrientation()
    {
        var cut = Render<RnSeparator>(p => p
            .Add(x => x.Orientation, Orientation.Vertical));

        var separator = cut.Find("[data-slot='separator']");
        separator.GetAttribute("data-orientation").Should().Be("vertical");
    }



    [Fact]
    public void RnSeparator_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnSeparator>(p => p
            .Add(x => x.Class, "my-separator-class"));

        cut.Find("[data-slot='separator']").ClassList.Should().Contain("my-separator-class");
    }


}
