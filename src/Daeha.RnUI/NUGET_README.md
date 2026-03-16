# Daeha.RnUI

> **⚠️ 베타 버전 안내 (BETA WARNING)**
> 현재 이 라이브러리는 초기 오픈소스 작업 중인 베타 버전이며 여러 버그와 오류가 포함되어 있을 수 있습니다. 운영 환경(Production)에서의 사용을 권장하지 않습니다. 피드백 및 기여는 언제나 환영합니다!
>
> **⚠️ BETA WARNING**
> This library is currently in early beta and may contain many bugs and errors. We do not recommend using it in a production environment. Feedback and contributions are always welcome!

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

**54 component categories** ported from shadcn/ui:

**Buttons & Inputs** — Button, ButtonGroup, Input, InputOTP, Textarea, Label, Checkbox, Switch, RadioGroup, Select, Toggle, ToggleGroup, Combobox, Field, Form

**Cards & Containers** — Card, Alert, Badge, AspectRatio, ScrollArea, Separator, Resizable

**Data Display** — Table, DataTable, Avatar, Progress, Slider, Skeleton, Spinner, Kbd, Calendar

**Navigation** — Tabs, Breadcrumb, Pagination, NavigationMenu, Sidebar, Menubar

**Overlays** — Dialog, AlertDialog, Sheet, Drawer, Popover, Tooltip, HoverCard, DropdownMenu, ContextMenu, Toast

**Disclosure** — Accordion, Collapsible

**Other** — Command, Carousel, DatePicker, Gantt, Empty

## Usage Examples

### Button

```razor
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Size="ButtonSize.Sm">Small</RnButton>
```

### Card

```razor
<RnCard>
    <RnCardHeader>
        <RnCardTitle>Card Title</RnCardTitle>
        <RnCardDescription>Description here.</RnCardDescription>
    </RnCardHeader>
    <RnCardContent>
        <p>Card content.</p>
    </RnCardContent>
    <RnCardFooter>
        <RnButton>Action</RnButton>
    </RnCardFooter>
</RnCard>
```

### Dialog

```razor
<RnButton OnClick="() => _open = true">Open</RnButton>
<RnDialog @bind-Open="_open">
    <RnDialogHeader>
        <RnDialogTitle>Title</RnDialogTitle>
    </RnDialogHeader>
    <p>Content here.</p>
    <RnDialogFooter>
        <RnButton OnClick="() => _open = false">Close</RnButton>
    </RnDialogFooter>
</RnDialog>
```

## Key Features

- 54 component categories faithfully ported from shadcn/ui
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

## Changelog

### v0.11.5
- **CSS**: Wrapped all component styles in `@layer components` for better Tailwind CSS v4 cascade control

### v0.11.4
- **RnDataTable**: Added `OnRowDoubleClick` (`EventCallback<TItem>`) parameter — triggers on row double-click; rows automatically apply `cursor-pointer` when the callback is bound

## License

[MIT](https://licenses.nuget.org/MIT)
