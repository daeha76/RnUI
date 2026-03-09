using Microsoft.AspNetCore.Components;

namespace Daeha.RnUI.Demo.Wasm.Models;

public record ComponentExample(string Title, string? Description, RenderFragment Demo, string Code);

public record ComponentDemo(
    string Name,
    string Slug,
    string Description,
    RenderFragment Demo,
    RenderFragment Thumbnail,
    string Code,
    string UsageCode,
    ComponentExample[]? Examples = null);
