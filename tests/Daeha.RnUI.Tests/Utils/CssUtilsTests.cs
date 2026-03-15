using FluentAssertions;
using RnUI.Utils;
using Xunit;

namespace Daeha.RnUI.Tests.Utils;

[Trait("Category", "Unit")]
public class CssUtilsTests
{
    // --- Cn() ---

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
    public void Cn_PreservesClassOrder()
    {
        var result = CssUtils.Cn("first", "second", "third");
        result.Should().Be("first second third");
    }

    [Fact]
    public void Cn_WithMixedValidAndInvalid_ReturnsOnlyValid()
    {
        var result = CssUtils.Cn(null, "a", "", "b", "  ", "c", null);
        result.Should().Be("a b c");
    }

    [Fact]
    public void Cn_WithMultiWordClassStrings_PreservesSpaces()
    {
        // When a single arg contains multiple classes (e.g. from CnDisabled)
        var result = CssUtils.Cn("cn-button", "pointer-events-none opacity-50");
        result.Should().Be("cn-button pointer-events-none opacity-50");
    }

    [Fact]
    public void Cn_IntegrationWithCnIf_CombinesCorrectly()
    {
        var isActive = true;
        var result = CssUtils.Cn("base", CssUtils.CnIf(isActive, "active", "inactive"));
        result.Should().Be("base active");
    }

    [Fact]
    public void Cn_IntegrationWithCnIf_WhenFalse_ExcludesEmpty()
    {
        var isActive = false;
        var result = CssUtils.Cn("base", CssUtils.CnIf(isActive, "active"));
        result.Should().Be("base");
    }

    [Fact]
    public void Cn_IntegrationWithCnDisabled_WhenDisabled()
    {
        var result = CssUtils.Cn("cn-button", CssUtils.CnDisabled(true));
        result.Should().Contain("cn-button");
        result.Should().Contain("pointer-events-none");
        result.Should().Contain("opacity-50");
    }

    [Fact]
    public void Cn_IntegrationWithCnDisabled_WhenNotDisabled()
    {
        var result = CssUtils.Cn("cn-button", CssUtils.CnDisabled(false));
        result.Should().Be("cn-button");
    }

    // --- CnIf() ---

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
    public void CnIf_WhenFalseWithNullFallback_ReturnsEmpty()
    {
        var result = CssUtils.CnIf(false, "active", null);
        result.Should().BeEmpty();
    }

    // --- CnDisabled() ---

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
    public void CnDisabled_WhenTrue_ReturnsExactString()
    {
        var result = CssUtils.CnDisabled(true);
        result.Should().Be("pointer-events-none opacity-50 cursor-not-allowed");
    }
}
