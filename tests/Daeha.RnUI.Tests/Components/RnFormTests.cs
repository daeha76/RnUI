using Bunit;
using FluentAssertions;
using RnUI.Components.UI.Form;
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class RnFormItemTests : BunitContext
{
    [Fact]
    public void RnFormItem_DefaultRendering_HasDataSlot()
    {
        var cut = Render<RnFormItem>(p => p.AddChildContent("Content"));
        cut.Find("[data-slot='form-item']").Should().NotBeNull();
    }

    [Fact]
    public void RnFormItem_DefaultRendering_HasBaseClass()
    {
        var cut = Render<RnFormItem>(p => p.AddChildContent("Content"));
        cut.Find("div").ClassList.Should().Contain("cn-form-item");
    }

    [Fact]
    public void RnFormItem_WithChildContent_RendersContent()
    {
        var cut = Render<RnFormItem>(p => p.AddChildContent("<span>Inner</span>"));
        cut.Find("span").TextContent.Should().Be("Inner");
    }

    [Fact]
    public void RnFormItem_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnFormItem>(p => p
            .Add(x => x.Class, "my-class")
            .AddChildContent("Content"));

        var div = cut.Find("div");
        div.ClassList.Should().Contain("cn-form-item");
        div.ClassList.Should().Contain("my-class");
    }

    [Fact]
    public void RnFormItem_WithAdditionalAttributes_PassesThrough()
    {
        var cut = Render<RnFormItem>(p => p
            .Add(x => x.AdditionalAttributes, new Dictionary<string, object> { { "id", "form-item-1" } })
            .AddChildContent("Content"));

        cut.Find("div").GetAttribute("id").Should().Be("form-item-1");
    }
}

[Trait("Category", "Unit")]
public class RnFormLabelTests : BunitContext
{
    [Fact]
    public void RnFormLabel_DefaultRendering_HasDataSlot()
    {
        var cut = Render<RnFormLabel>(p => p.AddChildContent("Label"));
        cut.Find("[data-slot='form-label']").Should().NotBeNull();
    }

    [Fact]
    public void RnFormLabel_DefaultRendering_HasBaseClass()
    {
        var cut = Render<RnFormLabel>(p => p.AddChildContent("Label"));
        cut.Find("label").ClassList.Should().Contain("cn-form-label");
    }

    [Fact]
    public void RnFormLabel_WithChildContent_RendersText()
    {
        var cut = Render<RnFormLabel>(p => p.AddChildContent("Username"));
        cut.Find("label").TextContent.Trim().Should().Be("Username");
    }

    [Fact]
    public void RnFormLabel_WithFor_SetsForAttribute()
    {
        var cut = Render<RnFormLabel>(p => p
            .Add(x => x.For, "email-input")
            .AddChildContent("Email"));

        cut.Find("label").GetAttribute("for").Should().Be("email-input");
    }

    [Fact]
    public void RnFormLabel_WithHasError_SetsDataErrorAttribute()
    {
        var cut = Render<RnFormLabel>(p => p
            .Add(x => x.HasError, true)
            .AddChildContent("Field"));

        cut.Find("label").GetAttribute("data-error").Should().Be("true");
    }

    [Fact]
    public void RnFormLabel_WithoutError_NoDataErrorAttribute()
    {
        var cut = Render<RnFormLabel>(p => p
            .Add(x => x.HasError, false)
            .AddChildContent("Field"));

        cut.Find("label").GetAttribute("data-error").Should().BeNull();
    }

    [Fact]
    public void RnFormLabel_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnFormLabel>(p => p
            .Add(x => x.Class, "extra")
            .AddChildContent("Label"));

        var label = cut.Find("label");
        label.ClassList.Should().Contain("cn-form-label");
        label.ClassList.Should().Contain("extra");
    }

    [Fact]
    public void RnFormLabel_WithAdditionalAttributes_PassesThrough()
    {
        var cut = Render<RnFormLabel>(p => p
            .Add(x => x.AdditionalAttributes, new Dictionary<string, object> { { "id", "lbl-1" } })
            .AddChildContent("Label"));

        cut.Find("label").GetAttribute("id").Should().Be("lbl-1");
    }
}

[Trait("Category", "Unit")]
public class RnFormDescriptionTests : BunitContext
{
    [Fact]
    public void RnFormDescription_DefaultRendering_HasDataSlot()
    {
        var cut = Render<RnFormDescription>(p => p.AddChildContent("Help text"));
        cut.Find("[data-slot='form-description']").Should().NotBeNull();
    }

    [Fact]
    public void RnFormDescription_DefaultRendering_HasBaseClass()
    {
        var cut = Render<RnFormDescription>(p => p.AddChildContent("Help text"));
        cut.Find("p").ClassList.Should().Contain("cn-form-description");
    }

    [Fact]
    public void RnFormDescription_WithChildContent_RendersText()
    {
        var cut = Render<RnFormDescription>(p => p.AddChildContent("Enter your email"));
        cut.Find("p").TextContent.Trim().Should().Be("Enter your email");
    }

    [Fact]
    public void RnFormDescription_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnFormDescription>(p => p
            .Add(x => x.Class, "help-text")
            .AddChildContent("Description"));

        var p = cut.Find("p");
        p.ClassList.Should().Contain("cn-form-description");
        p.ClassList.Should().Contain("help-text");
    }

    [Fact]
    public void RnFormDescription_WithAdditionalAttributes_PassesThrough()
    {
        var cut = Render<RnFormDescription>(p => p
            .Add(x => x.AdditionalAttributes, new Dictionary<string, object> { { "id", "desc-1" } })
            .AddChildContent("Desc"));

        cut.Find("p").GetAttribute("id").Should().Be("desc-1");
    }
}

[Trait("Category", "Unit")]
public class RnFormMessageTests : BunitContext
{
    [Fact]
    public void RnFormMessage_WithMessage_RendersText()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.Message, "This field is required"));

        cut.Find("p").TextContent.Trim().Should().Be("This field is required");
    }

    [Fact]
    public void RnFormMessage_WithMessage_HasDataSlot()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.Message, "Error"));

        cut.Find("[data-slot='form-message']").Should().NotBeNull();
    }

    [Fact]
    public void RnFormMessage_WithMessage_HasBaseClass()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.Message, "Error"));

        cut.Find("p").ClassList.Should().Contain("cn-form-message");
    }

    [Fact]
    public void RnFormMessage_WithChildContent_RendersChildContent()
    {
        var cut = Render<RnFormMessage>(p => p
            .AddChildContent("<strong>Error!</strong>"));

        cut.Find("strong").TextContent.Should().Be("Error!");
    }

    [Fact]
    public void RnFormMessage_WithBothChildContentAndMessage_PrefersChildContent()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.Message, "Message text")
            .AddChildContent("Child text"));

        cut.Find("p").TextContent.Trim().Should().Be("Child text");
    }

    [Fact]
    public void RnFormMessage_WithNoMessageOrChildContent_RendersNothing()
    {
        var cut = Render<RnFormMessage>();
        cut.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public void RnFormMessage_WithEmptyMessage_RendersNothing()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.Message, ""));

        cut.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public void RnFormMessage_WithCustomClass_IncludesCustomClass()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.Class, "error-msg")
            .Add(x => x.Message, "Error"));

        var elem = cut.Find("p");
        elem.ClassList.Should().Contain("cn-form-message");
        elem.ClassList.Should().Contain("error-msg");
    }

    [Fact]
    public void RnFormMessage_WithAdditionalAttributes_PassesThrough()
    {
        var cut = Render<RnFormMessage>(p => p
            .Add(x => x.AdditionalAttributes, new Dictionary<string, object> { { "role", "alert" } })
            .Add(x => x.Message, "Error"));

        cut.Find("p").GetAttribute("role").Should().Be("alert");
    }
}
