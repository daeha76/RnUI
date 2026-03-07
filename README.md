# RnUI

A Blazor port of [shadcn/ui](https://ui.shadcn.com). Beautifully designed, accessible UI components built with Tailwind CSS for .NET Blazor applications.

> 이 라이브러리는 딸 리안(Rian)의 이름을 담아 만든 Blazor UI 컴포넌트 라이브러리입니다. 💕

## Features

- 37+ UI component categories with 97 Razor component files
- Built with Tailwind CSS and oklch color system
- Light and dark mode support out of the box
- Fully accessible components
- Zero external NuGet dependencies (framework-only)
- .NET 10.0 / Blazor Server & WebAssembly compatible

## Installation

```bash
dotnet add package Daeha.RnUI
```

## Setup

### 1. Add Imports

Add the following to your `_Imports.razor`:

```razor
@using Daeha.RnUI.Components.UI
```

### 2. Include Stylesheet

Add the RnUI CSS to your `App.razor` or `_Host.cshtml`:

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 3. Configure Tailwind CSS

RnUI uses Tailwind CSS for styling. Make sure Tailwind CSS is set up in your Blazor project and configured to scan the RnUI package for class names.

## Components

| Category | Components |
|---|---|
| **Buttons & Inputs** | Button, Input, Textarea, Label, Checkbox, Switch, RadioGroup, Select, Toggle |
| **Cards & Containers** | Card, Alert, Badge, AspectRatio, ScrollArea, Separator |
| **Data Display** | Table, Avatar, AvatarGroup, Progress, Slider, Skeleton, Spinner, Kbd |
| **Navigation** | Tabs, Breadcrumb, Pagination, NavigationMenu, Sidebar |
| **Overlays** | Dialog, AlertDialog, Sheet, Popover, Tooltip, HoverCard, DropdownMenu, ContextMenu |
| **Disclosure** | Accordion, Collapsible |
| **Misc** | Empty |

## Usage

### Button

```razor
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Size="ButtonSize.Sm">Small</RnButton>
<RnButton Size="ButtonSize.Lg">Large</RnButton>
```

### Card

```razor
<RnCard>
    <RnCardHeader>
        <RnCardTitle>Card Title</RnCardTitle>
        <RnCardDescription>Card description goes here.</RnCardDescription>
    </RnCardHeader>
    <RnCardContent>
        <p>Card content with some example text.</p>
    </RnCardContent>
    <RnCardFooter>
        <RnButton>Action</RnButton>
    </RnCardFooter>
</RnCard>
```

### Form Controls

```razor
<div class="space-y-2">
    <RnLabel For="email">Email</RnLabel>
    <RnInput Type="email" Placeholder="Enter your email" @bind-Value="_email" />
</div>

<div class="flex items-center space-x-2">
    <RnCheckbox @bind-Checked="_checked" />
    <RnLabel>Accept terms and conditions</RnLabel>
</div>

<div class="flex items-center space-x-2">
    <RnSwitch @bind-Checked="_switched" />
    <RnLabel>Enable notifications</RnLabel>
</div>
```

### Dialog

```razor
<RnButton OnClick="() => _dialogOpen = true">Open Dialog</RnButton>
<RnDialog @bind-Open="_dialogOpen">
    <RnDialogHeader>
        <RnDialogTitle>Edit Profile</RnDialogTitle>
        <RnDialogDescription>Make changes to your profile here.</RnDialogDescription>
    </RnDialogHeader>
    <p>Dialog content goes here.</p>
    <RnDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">Cancel</RnButton>
        <RnButton OnClick="() => _dialogOpen = false">Save</RnButton>
    </RnDialogFooter>
</RnDialog>
```

### Tabs

```razor
<RnTabs @bind-ActiveTab="_activeTab">
    <RnTabsList>
        <RnTabsTrigger Value="account">Account</RnTabsTrigger>
        <RnTabsTrigger Value="password">Password</RnTabsTrigger>
    </RnTabsList>
    <RnTabsContent Value="account">
        <p>Account settings here.</p>
    </RnTabsContent>
    <RnTabsContent Value="password">
        <p>Password settings here.</p>
    </RnTabsContent>
</RnTabs>
```

### Tooltip

```razor
<RnTooltip Side="Side.Top">
    <Trigger>
        <RnButton>Hover me</RnButton>
    </Trigger>
    <ChildContent>
        <p>This is the tooltip content.</p>
    </ChildContent>
</RnTooltip>
```

## Customization

RnUI uses CSS custom properties for theming. Override the following variables to customize colors:

```css
:root {
    --background: 0 0% 100%;
    --foreground: 0 0% 3.9%;
    --primary: 0 0% 9%;
    --primary-foreground: 0 0% 98%;
    --secondary: 0 0% 96.1%;
    --destructive: 0 84.2% 60.2%;
    --muted: 0 0% 96.1%;
    --accent: 0 0% 96.1%;
    --border: 0 0% 89.8%;
    --ring: 0 0% 3.9%;
    --radius-lg: 0.5rem;
}
```

## Demo

A demo application is included in `src/Daeha.RnUI.Demo`. To run it:

```bash
cd src/Daeha.RnUI.Demo
dotnet run
```

Then open `https://localhost:7100` in your browser.

## License

[MIT](LICENSE.md)
