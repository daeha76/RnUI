using Microsoft.AspNetCore.Components;

namespace Daeha.RnUI.Demo.Wasm.Models;

public record BlockFile(string FileName, string Code);

public record BlockDemo(
    string Name,
    string Slug,
    string Description,
    string Category,
    RenderFragment Demo,
    RenderFragment Thumbnail,
    BlockFile[] Files);
