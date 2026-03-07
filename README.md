# Shadrazor

A Blazor port of [shadcn/ui](https://ui.shadcn.com). Beautifully designed, accessible UI components built with Tailwind CSS for .NET Blazor applications.

## Features

- 37+ UI component categories with 97 Razor component files
- Built with Tailwind CSS and oklch color system
- Light and dark mode support out of the box
- Fully accessible components
- Zero external NuGet dependencies (framework-only)
- .NET 10.0 / Blazor Server & WebAssembly compatible

## Installation

```bash
dotnet add package Daeha.Shadrazor
```

## Setup

### 1. Add Imports

Add the following to your `_Imports.razor`:

```razor
@using Daeha.Shadrazor.Components.UI
```

### 2. Include Stylesheet

Add the Shadrazor CSS to your `App.razor` or `_Host.cshtml`:

```html
<link rel="stylesheet" href="_content/Daeha.Shadrazor/css/shadcn.css" />
```

### 3. Configure Tailwind CSS

Shadrazor uses Tailwind CSS for styling. Make sure Tailwind CSS is set up in your Blazor project and configured to scan the Shadrazor package for class names.

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
<Button>Default</Button>
<Button Variant="ButtonVariant.Secondary">Secondary</Button>
<Button Variant="ButtonVariant.Outline">Outline</Button>
<Button Variant="ButtonVariant.Destructive">Destructive</Button>
<Button Size="ButtonSize.Sm">Small</Button>
<Button Size="ButtonSize.Lg">Large</Button>
```

### Card

```razor
<Card>
    <CardHeader>
        <CardTitle>Card Title</CardTitle>
        <CardDescription>Card description goes here.</CardDescription>
    </CardHeader>
    <CardContent>
        <p>Card content with some example text.</p>
    </CardContent>
    <CardFooter>
        <Button>Action</Button>
    </CardFooter>
</Card>
```

### Form Controls

```razor
<div class="space-y-2">
    <Label For="email">Email</Label>
    <Input Type="email" Placeholder="Enter your email" />
</div>

<div class="flex items-center space-x-2">
    <Checkbox @bind-Checked="_checked" />
    <Label>Accept terms and conditions</Label>
</div>

<div class="flex items-center space-x-2">
    <Switch @bind-Checked="_switched" />
    <Label>Enable notifications</Label>
</div>
```

### Dialog

```razor
<Button OnClick="() => _dialogOpen = true">Open Dialog</Button>
<Dialog @bind-Open="_dialogOpen">
    <DialogHeader>
        <DialogTitle>Edit Profile</DialogTitle>
        <DialogDescription>Make changes to your profile here.</DialogDescription>
    </DialogHeader>
    <DialogContent>
        <p>Dialog content goes here.</p>
    </DialogContent>
    <DialogFooter>
        <Button Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">Cancel</Button>
        <Button OnClick="() => _dialogOpen = false">Save</Button>
    </DialogFooter>
</Dialog>
```

### Tabs

```razor
<Tabs @bind-ActiveTab="_activeTab">
    <TabsList>
        <TabsTrigger Value="account">Account</TabsTrigger>
        <TabsTrigger Value="password">Password</TabsTrigger>
    </TabsList>
    <TabsContent Value="account">
        <p>Account settings here.</p>
    </TabsContent>
    <TabsContent Value="password">
        <p>Password settings here.</p>
    </TabsContent>
</Tabs>
```

### Tooltip

```razor
<Tooltip Side="Side.Top">
    <Trigger>Hover me</Trigger>
    <p>This is the tooltip content.</p>
</Tooltip>
```

## Customization

Shadrazor uses CSS custom properties for theming. Override the following variables to customize colors:

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

A demo application is included in `src/Daeha.Shadrazor.Demo`. To run it:

```bash
cd src/Daeha.Shadrazor.Demo
dotnet run
```

Then open `https://localhost:7100` in your browser.

## License

[MIT](LICENSE.md)
