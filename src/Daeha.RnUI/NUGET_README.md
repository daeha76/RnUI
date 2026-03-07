# Shadrazor

A Blazor port of [shadcn/ui](https://ui.shadcn.com). Beautifully designed, accessible UI components built with Tailwind CSS for .NET Blazor applications.

## Quick Start

### 1. Install the package

```bash
dotnet add package Daeha.Shadrazor
```

### 2. Add imports to `_Imports.razor`

```razor
@using Daeha.Shadrazor.Components.UI
```

### 3. Add stylesheet to `App.razor`

```html
<link rel="stylesheet" href="_content/Daeha.Shadrazor/css/shadcn.css" />
```

### 4. Set up Tailwind CSS

Ensure Tailwind CSS is configured in your project and set to scan the Shadrazor package for class names.

## Components

37+ component categories including:

**Buttons & Inputs** — Button, Input, Textarea, Label, Checkbox, Switch, RadioGroup, Select, Toggle

**Cards & Containers** — Card, Alert, Badge, AspectRatio, ScrollArea, Separator

**Data Display** — Table, Avatar, AvatarGroup, Progress, Slider, Skeleton, Spinner, Kbd

**Navigation** — Tabs, Breadcrumb, Pagination, NavigationMenu, Sidebar

**Overlays** — Dialog, AlertDialog, Sheet, Popover, Tooltip, HoverCard, DropdownMenu, ContextMenu

**Disclosure** — Accordion, Collapsible

## Usage Examples

### Button

```razor
<Button>Default</Button>
<Button Variant="ButtonVariant.Secondary">Secondary</Button>
<Button Variant="ButtonVariant.Outline">Outline</Button>
<Button Size="ButtonSize.Sm">Small</Button>
```

### Card

```razor
<Card>
    <CardHeader>
        <CardTitle>Card Title</CardTitle>
        <CardDescription>Description here.</CardDescription>
    </CardHeader>
    <CardContent>
        <p>Card content.</p>
    </CardContent>
    <CardFooter>
        <Button>Action</Button>
    </CardFooter>
</Card>
```

### Dialog

```razor
<Button OnClick="() => _open = true">Open</Button>
<Dialog @bind-Open="_open">
    <DialogHeader>
        <DialogTitle>Title</DialogTitle>
    </DialogHeader>
    <DialogContent>Content here.</DialogContent>
    <DialogFooter>
        <Button OnClick="() => _open = false">Close</Button>
    </DialogFooter>
</Dialog>
```

## Key Features

- 97 Razor components across 37+ categories
- Tailwind CSS with oklch color system
- Light / dark mode support
- Fully accessible
- Zero external NuGet dependencies
- .NET 10.0 compatible

## Customization

Override CSS custom properties to theme all components:

```css
:root {
    --background: 0 0% 100%;
    --foreground: 0 0% 3.9%;
    --primary: 0 0% 9%;
    --radius-lg: 0.5rem;
}
```

## License

[MIT](https://licenses.nuget.org/MIT)
