# Contributing to RnUI

Thank you for your interest in contributing to RnUI! This guide will help you get started.

## Getting Started

### Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download) (multi-target: net8.0, net9.0, net10.0)
- [Node.js 20+](https://nodejs.org/) (for Tailwind CSS build)
- Git

### Setup

```bash
git clone https://github.com/daeha76/RnUI.git
cd RnUI

# Install npm dependencies
cd src/Daeha.RnUI && npm install && cd ../..
cd src/Daeha.RnUI.Demo.Wasm && npm install && cd ../..

# Build CSS
cd src/Daeha.RnUI && npm run build:css && cd ../..
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css && cd ../..

# Build and test
dotnet build
dotnet test
```

## How to Contribute

### Good First Issues

Look for these labels to find beginner-friendly tasks:

- `good-first-issue` — Test additions, documentation improvements
- `help-wanted` — New component porting, feature implementation
- `i18n` — Translation contributions

### Reporting Bugs

Use the [Bug Report](https://github.com/daeha76/RnUI/issues/new?template=bug_report.md) template. Include:

- RnUI version and .NET target framework
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable

### Requesting Features

Use the [Feature Request](https://github.com/daeha76/RnUI/issues/new?template=feature_request.md) template.

### Requesting New Components

Use the [Component Request](https://github.com/daeha76/RnUI/issues/new?template=component_request.md) template. Reference the shadcn/ui original when possible.

## Component Contribution Guide

### Step 1: Analyze the shadcn/ui Original

Study the React source at [ui.shadcn.com](https://ui.shadcn.com/docs/components). Focus on:
- HTML structure and CSS classes
- Variants and sizes
- Accessibility attributes (`role`, `aria-*`)
- Keyboard interactions

### Step 2: Port to Blazor

Follow the project conventions in `CLAUDE.md`:

| React Pattern | Blazor Equivalent |
|---------------|-------------------|
| `className={cn(...)}` | `class="@ComputedClass"` + `CssUtils.Cn()` |
| `{children}` | `@ChildContent` (RenderFragment) |
| `useState` | `[Parameter]` + `EventCallback<T>` |
| `useEffect` | `OnAfterRenderAsync` |
| `useContext` | `CascadingValue` / `CascadingParameter` |
| `forwardRef` | `@ref` + `ElementReference` |
| `cva()` variants | `enum` + `switch` → CSS class |

### Step 3: File Structure

```
src/Daeha.RnUI/Components/UI/{Name}/
├── Rn{Name}.razor           # Main component
├── {Name}Enums.cs            # Variant/Size enums (if needed)
├── Rn{Name}Header.razor      # Sub-components (for compound components)
└── Rn{Name}Content.razor
```

### Step 4: CSS

Edit **only** `src/Daeha.RnUI/wwwroot/css/shadcn.src.css`:

```css
.cn-{name} {
  @apply /* base styles */;
}
.cn-{name}-variant-{variant} {
  @apply /* variant styles */;
}
.cn-{name}-size-{size} {
  @apply /* size styles */;
}
```

**Never** edit `shadcn.css` directly — it's auto-generated.

### Step 5: Required Parameters

Every component **must** include:

```csharp
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter] public string? Class { get; set; }
[Parameter(CaptureUnmatchedValues = true)]
public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

### Step 6: Add Demo

Create a demo page in `src/Daeha.RnUI.Demo.Wasm/Pages/ComponentDemos/`.

### Step 7: Write Tests

Add bUnit tests in `tests/Daeha.RnUI.Tests/`:

```csharp
[Trait("Category", "Unit")]
public class Rn{Name}Tests : BunitContext
{
    [Fact]
    public void Rn{Name}_DefaultRendering_AppliesBaseClass()
    {
        var cut = Render<Rn{Name}>();
        cut.Find("[data-slot=\"{name}\"]").ClassList.Should().Contain("cn-{name}");
    }
}
```

### Step 8: Build CSS (Both Pipelines!)

```bash
cd src/Daeha.RnUI && npm run build:css          # Library CSS
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css  # Demo CSS
dotnet build
```

### Step 9: Submit PR

- Fill out the PR template completely
- Ensure CI passes
- Reference the related issue if applicable

## Code Style

- Component names: `Rn` prefix + PascalCase (`RnButton`, `RnCard`)
- CSS base class: `cn-` prefix + kebab-case (`.cn-button`, `.cn-card`)
- All components must include `data-slot` attribute
- Use `oklch()` color space (Tailwind v4)
- Borders must use `var(--color-border-subtle)`, not Tailwind border utilities

## Documentation Sync

When adding or modifying components, update these files:

1. `README.md` — Components table + Usage Examples
2. `docs/README.ko.md` — Korean
3. `docs/README.zh-CN.md` — Chinese
4. `docs/README.es.md` — Spanish
5. `docs/README.de.md` — German
6. `docs/README.ja.md` — Japanese
7. `.agents/knowledge/component-catalog.md` — Component catalog

## Questions?

- Open a [Discussion](https://github.com/daeha76/RnUI/discussions) for Q&A
- Check existing [Issues](https://github.com/daeha76/RnUI/issues) before filing new ones

Thank you for helping make RnUI better!
