using FluentAssertions;
using RnUI.Utils;
using Xunit;

namespace Daeha.RnUI.Tests.Utils;

[Trait("Category", "Unit")]
public class CssUtilsTests
{
    [Fact]
    public void Cn_WithMultipleClasses_JoinsWithSpace()
    {
        var result = CssUtils.Cn("class-a", "class-b", "class-c");
        result.Should().Be("class-a class-b class-c");
    }

    [Fact]
    public void Cn_WithNullAndEmpty_FiltersOut()
    {
        var result = CssUtils.Cn("class-a", null, "", "class-b", "  ");
        result.Should().Be("class-a class-b");
    }

    [Fact]
    public void Cn_WithNoArgs_ReturnsEmpty()
    {
        var result = CssUtils.Cn();
        result.Should().BeEmpty();
    }

    [Fact]
    public void Cn_WithSingleClass_ReturnsThatClass()
    {
        var result = CssUtils.Cn("only-class");
        result.Should().Be("only-class");
    }

    [Fact]
    public void Cn_WithAllNulls_ReturnsEmpty()
    {
        var result = CssUtils.Cn(null, null, null);
        result.Should().BeEmpty();
    }

    [Fact]
    public void Cn_WithWhitespaceOnly_FiltersOut()
    {
        var result = CssUtils.Cn("   ", "\t", "\n");
        result.Should().BeEmpty();
    }

    [Fact]
    public void CnIf_WhenTrue_ReturnsTrueClass()
    {
        var result = CssUtils.CnIf(true, "active");
        result.Should().Be("active");
    }

    [Fact]
    public void CnIf_WhenFalse_ReturnsEmptyByDefault()
    {
        var result = CssUtils.CnIf(false, "active");
        result.Should().BeEmpty();
    }

    [Fact]
    public void CnIf_WhenFalseWithFallback_ReturnsFalseClass()
    {
        var result = CssUtils.CnIf(false, "active", "inactive");
        result.Should().Be("inactive");
    }

    [Fact]
    public void CnIf_WhenTrueWithFallback_IgnoresFallback()
    {
        var result = CssUtils.CnIf(true, "active", "inactive");
        result.Should().Be("active");
    }

    [Fact]
    public void CnDisabled_WhenTrue_ReturnsDisabledClasses()
    {
        var result = CssUtils.CnDisabled(true);
        result.Should().Contain("pointer-events-none");
        result.Should().Contain("opacity-50");
        result.Should().Contain("cursor-not-allowed");
    }

    [Fact]
    public void CnDisabled_WhenFalse_ReturnsEmpty()
    {
        var result = CssUtils.CnDisabled(false);
        result.Should().BeEmpty();
    }

    [Fact]
    public void Cn_PreservesClassOrder()
    {
        var result = CssUtils.Cn("first", "second", "third");
        result.Should().Be("first second third");
    }
}
