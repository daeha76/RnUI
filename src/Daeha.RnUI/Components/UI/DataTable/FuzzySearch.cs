namespace RnUI.Components.UI.DataTable;

public static class FuzzySearch
{
    public static double Score(string text, string query)
    {
        if (string.IsNullOrEmpty(query)) return 1.0;
        if (string.IsNullOrEmpty(text)) return 0.0;

        var textLower = text.ToLowerInvariant();
        var queryLower = query.ToLowerInvariant();

        // Exact match
        if (textLower == queryLower) return 1.0;

        // Starts with
        if (textLower.StartsWith(queryLower)) return 0.9;

        // Contains
        if (textLower.Contains(queryLower)) return 0.7;

        // Subsequence match with gap penalty
        var score = SubsequenceScore(textLower, queryLower);
        return score;
    }

    private static double SubsequenceScore(string text, string query)
    {
        var queryIndex = 0;
        var totalGap = 0;
        var lastMatchIndex = -1;

        for (var i = 0; i < text.Length && queryIndex < query.Length; i++)
        {
            if (text[i] == query[queryIndex])
            {
                if (lastMatchIndex >= 0)
                {
                    totalGap += (i - lastMatchIndex - 1);
                }
                lastMatchIndex = i;
                queryIndex++;
            }
        }

        // Not all query characters matched
        if (queryIndex < query.Length) return 0.0;

        // Score based on gap penalty and query coverage
        var maxGap = text.Length - query.Length;
        var gapPenalty = maxGap > 0 ? (double)totalGap / maxGap : 0;
        var coverage = (double)query.Length / text.Length;

        return Math.Max(0.01, 0.5 * (1 - gapPenalty) + 0.1 * coverage);
    }

    public static IEnumerable<(TItem Item, double Score)> Rank<TItem>(
        IEnumerable<TItem> items,
        string query,
        Func<TItem, IEnumerable<string>> accessors,
        double threshold = 0.3)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return items.Select(item => (item, 1.0));
        }

        return items
            .Select(item =>
            {
                var bestScore = accessors(item)
                    .Select(text => Score(text, query))
                    .DefaultIfEmpty(0)
                    .Max();
                return (Item: item, Score: bestScore);
            })
            .Where(x => x.Score >= threshold);
    }
}
