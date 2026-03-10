using FluentAssertions;
using RnUI.Components.UI.DataTable;
using Xunit;

namespace Daeha.RnUI.Tests.Utils;

[Trait("Category", "Unit")]
public class FuzzySearchTests
{
    [Fact]
    public void Score_ExactMatch_ReturnsOne()
    {
        FuzzySearch.Score("hello", "hello").Should().Be(1.0);
    }

    [Fact]
    public void Score_ExactMatchCaseInsensitive_ReturnsOne()
    {
        FuzzySearch.Score("Hello", "hello").Should().Be(1.0);
    }

    [Fact]
    public void Score_StartsWith_ReturnsHighScore()
    {
        FuzzySearch.Score("hello world", "hello").Should().Be(0.9);
    }

    [Fact]
    public void Score_Contains_ReturnsModerateScore()
    {
        FuzzySearch.Score("say hello world", "hello").Should().Be(0.7);
    }

    [Fact]
    public void Score_NoMatch_ReturnsZero()
    {
        FuzzySearch.Score("hello", "xyz").Should().Be(0.0);
    }

    [Fact]
    public void Score_EmptyQuery_ReturnsOne()
    {
        FuzzySearch.Score("hello", "").Should().Be(1.0);
    }

    [Fact]
    public void Score_EmptyText_ReturnsZero()
    {
        FuzzySearch.Score("", "hello").Should().Be(0.0);
    }

    [Fact]
    public void Score_SubsequenceMatch_ReturnsPositive()
    {
        var score = FuzzySearch.Score("abcdef", "ace");
        score.Should().BeGreaterThan(0.0);
        score.Should().BeLessThan(0.7); // less than contains
    }

    [Fact]
    public void Rank_FiltersAndSorts()
    {
        var items = new[] { "apple", "banana", "grape", "pineapple" };
        var results = FuzzySearch.Rank(
            items, "app",
            item => new[] { item },
            threshold: 0.3
        ).ToList();

        results.Should().NotBeEmpty();
        results[0].Item.Should().Be("apple"); // best match
    }

    [Fact]
    public void Rank_EmptyQuery_ReturnsAll()
    {
        var items = new[] { "a", "b", "c" };
        var results = FuzzySearch.Rank(items, "", item => new[] { item }).ToList();
        results.Should().HaveCount(3);
    }

    [Fact]
    public void Rank_NoMatches_ReturnsEmpty()
    {
        var items = new[] { "apple", "banana" };
        var results = FuzzySearch.Rank(items, "xyz", item => new[] { item }, threshold: 0.3).ToList();
        results.Should().BeEmpty();
    }
}
