# Daeha.RnUI

A Blazor port of [shadcn/ui](https://ui.shadcn.com). Beautifully designed, accessible UI components built with Tailwind CSS v4 for .NET Blazor applications.

[![NuGet](https://img.shields.io/nuget/v/Daeha.RnUI)](https://www.nuget.org/packages/Daeha.RnUI)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Quick Start

### 1. Install the package

```bash
dotnet add package Daeha.RnUI
```

### 2. Add imports to `_Imports.razor`

```razor
@using Daeha.RnUI.Components.UI
```

### 3. Add stylesheet to `App.razor`

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. Set up Tailwind CSS v4

Add the RnUI package source path to your Tailwind v4 config so it scans component class names:

```css
@source "../../{YourProject}/_content/Daeha.RnUI/**/*.razor";
```

## Components

**53 component categories** ported from shadcn/ui:

**Buttons & Inputs** — Button, ButtonGroup, Input, InputOTP, Textarea, Label, Checkbox, Switch, RadioGroup, Select, Toggle, ToggleGroup, Combobox

**Cards & Containers** — Card, Alert, Badge, AspectRatio, ScrollArea, Separator, Resizable

**Data Display** — Table, Avatar, Progress, Slider, Skeleton, Spinner, Kbd, Calendar

**Navigation** — Tabs, Breadcrumb, Pagination, NavigationMenu, Sidebar, Menubar

**Overlays** — Dialog, AlertDialog, Sheet, Drawer, Popover, Tooltip, HoverCard, DropdownMenu, ContextMenu

**Disclosure** — Accordion, Collapsible

**Other** — Form, Command, Carousel, DatePicker, Toast, Empty

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

- 53 component categories faithfully ported from shadcn/ui
- Tailwind CSS v4 with oklch color system
- Light / dark mode support via CSS custom properties
- Fully accessible
- Zero external NuGet dependencies
- Supports .NET 8.0, 9.0, and 10.0

## Customization

Override CSS custom properties to theme all components:

```css
:root {
    --background: oklch(1 0 0);
    --foreground: oklch(0.141 0.005 285.823);
    --primary: oklch(0.21 0.006 285.885);
    --radius-lg: 0.5rem;
}
```

## License

[MIT](https://licenses.nuget.org/MIT)
