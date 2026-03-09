using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace Daeha.RnUI.Demo.Wasm.Shared;

/// <summary>
/// Razor/XML 코드를 인라인 스타일로 color-highlighting하는 유틸리티.
/// Prism Tomorrow 테마 색상 기준.
/// </summary>
public static class SyntaxHighlighter
{
    public static MarkupString HighlightXml(string? code)
    {
        if (string.IsNullOrEmpty(code)) return new MarkupString(string.Empty);

        // 1. 코드를 모두 인코딩하여 안전한 텍스트로 만듦
        var html = WebUtility.HtmlEncode(code); // <, >, " 가 &lt;, &gt;, &quot; 로 변환됨.

        // 2. 주석 <!-- ... -->
        html = Regex.Replace(html,
            @"&lt;!--(.*?)--&gt;",
            m => Span("#999", m.Value),
            RegexOptions.Singleline);

        // 3. 태그 &lt;TagName attrs... /&gt; 매칭
        html = Regex.Replace(html,
            @"(&lt;/?)([\w][\w:.-]*)(.*?)(/?&gt;)",
            m => HighlightTag(m),
            RegexOptions.Singleline);

        // 4. 태그 외부(Inner Text)에 있는 Razor 표현식 (@MyVar 등)
        html = Regex.Replace(html,
            @"(@)([a-zA-Z_]\w*(?:\.[a-zA-Z_]\w*)*)",
            m => Span("#c678dd", m.Groups[1].Value) + Span("#e5c07b", m.Groups[2].Value));

        return new MarkupString(html);
    }

    private static string HighlightTag(Match m)
    {
        var open  = m.Groups[1].Value;  // &lt; or &lt;/
        var tag   = m.Groups[2].Value;  // 태그명
        var attrs = m.Groups[3].Value;  // attributes 부분 (이 안의 따옴표는 &quot;)
        var close = m.Groups[4].Value;  // &gt; or /&gt;

        // 속성값 내의 Razor 표현식을 먼저 임시 토큰으로 치환하여 보호
        var razorExprs = new Dictionary<string, string>();
        int idx = 0;
        attrs = Regex.Replace(attrs,
            @"(@)([a-zA-Z_]\w*(?:\.[a-zA-Z_]\w*)*)",
            m2 => {
                var key = $"__RAZOR_TOKEN_{idx++}__";
                razorExprs[key] = Span("#c678dd", m2.Groups[1].Value) + Span("#e5c07b", m2.Groups[2].Value);
                return key;
            });

        // 속성값: =&quot;...&quot; (토큰이 포함되어 있을 수 있음)
        attrs = Regex.Replace(attrs,
            @"(=&quot;)(.*?)(&quot;)",
            m2 => m2.Groups[1].Value +
                  Span("#7ec699", m2.Groups[2].Value) + // 값을 녹색으로
                  m2.Groups[3].Value,
            RegexOptions.Singleline);

        // 속성명: 영문자로 시작하는 단어
        attrs = Regex.Replace(attrs,
            @"\s+([\w][\w:@.-]*)(?==&quot;|\s|$)",
            m2 => " " + Span("#d19a66", m2.Groups[1].Value));

        // 토큰 복구
        foreach (var kvp in razorExprs)
        {
            attrs = attrs.Replace(kvp.Key, kvp.Value);
        }

        return Span("#ccc", open) +
               Span("#e2777a", tag) +
               attrs +
               Span("#ccc", close);
    }

    private static string Span(string color, string content) =>
        $"<span style=\"color:{color}\">{content}</span>";
}
