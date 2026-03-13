<div align="center">

# RnUI

**Blazor port of [shadcn/ui](https://ui.shadcn.com)**

Beautifully designed, accessible UI components built with Tailwind CSS for .NET Blazor applications.

[![NuGet](https://img.shields.io/nuget/v/Daeha.RnUI?style=flat-square&logo=nuget&color=004880)](https://www.nuget.org/packages/Daeha.RnUI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Daeha.RnUI?style=flat-square&logo=nuget&color=004880)](https://www.nuget.org/packages/Daeha.RnUI)
[![License](https://img.shields.io/github/license/daeha76/RnUI?style=flat-square)](LICENSE.md)
[![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-512bd4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com)

[**Live Demo**](https://daeha76.github.io/RnUI/)

**Documentation**: [한국어](docs/README.ko.md) | [中文](docs/README.zh-CN.md) | [Español](docs/README.es.md) | [Deutsch](docs/README.de.md) | [日本語](docs/README.ja.md)

</div>

---

## Why RnUI?

- **54 component categories** — A comprehensive UI library with 194 Razor component files
- **Based on shadcn/ui** — A proven design system from the web, ported directly to Blazor
- **Tailwind CSS** — oklch color system with CSS custom property-based theming
- **Dark mode** — Built-in light/dark mode support
- **Accessible** — Components designed with accessibility in mind
- **Zero external dependencies** — Only ASP.NET Core framework references
- **Multi-target** — .NET 8.0, 9.0, 10.0 support | Blazor Server & WebAssembly compatible

---

## Quick Start

### 1. Install the package

```bash
dotnet add package Daeha.RnUI
```

### 2. Add imports

Add the following to your `_Imports.razor`:

```razor
@using Daeha.RnUI.Components.UI
```

### 3. Link the stylesheet

Add the CSS to your `App.razor` or `_Host.cshtml`:

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. Set up Tailwind CSS

RnUI uses Tailwind CSS v4. You need to set up Tailwind CSS in your Blazor project to scan and compile utility classes.

#### 4-1. Install Tailwind CSS

Install Tailwind CSS in your project root:

```bash
npm init -y
npm install -D tailwindcss @tailwindcss/cli
```

#### 4-2. Create the CSS entry file

Create a `wwwroot/input.css` file and include your Razor files as scan targets:

```css
@import "tailwindcss";
@source "../**/*.razor";
```

> **When using the NuGet package**: If installed via NuGet, the library's `.razor` files are not available locally. However, RnUI component base styles (`.cn-*` classes) are already included in `shadcn.css`, so they work without additional `@source` configuration. You only need to scan Tailwind utility classes used in your own `.razor` files.

#### 4-3. Add build scripts

Add build scripts to your `package.json`:

```json
{
  "scripts": {
    "build:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css",
    "watch:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css --watch"
  }
}
```

#### 4-4. Link the stylesheets

Link both the RnUI styles and the Tailwind CSS output file in your `App.razor` or `_Host.cshtml`:

```html
<!-- RnUI component styles -->
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
<!-- Tailwind utility classes -->
<link rel="stylesheet" href="css/tailwindcss.css" />
```

#### 4-5. Build CSS

Use watch mode during development and build for production:

```bash
npm run watch:css   # Development (watches for file changes)
npm run build:css   # Production (one-time build)
```

---

## Components

> See live demos and detailed usage for each component on the [**Demo Site**](https://daeha76.github.io/RnUI/components).

### Buttons & Inputs

| Component | Description | Demo |
|---|---|---|
| `RnButton` | Supports Default, Secondary, Outline, Ghost, Destructive, Link variants | [Live](https://daeha76.github.io/RnUI/components/button) |
| `RnInput` | Text input field | [Live](https://daeha76.github.io/RnUI/components/input) |
| `RnTextarea` | Multi-line text input | [Live](https://daeha76.github.io/RnUI/components/textarea) |
| `RnLabel` | Form label | [Live](https://daeha76.github.io/RnUI/components/label) |
| `RnCheckbox` | Checkbox | [Live](https://daeha76.github.io/RnUI/components/checkbox) |
| `RnSwitch` | Toggle switch | [Live](https://daeha76.github.io/RnUI/components/switch) |
| `RnRadioGroup` | Radio button group | [Live](https://daeha76.github.io/RnUI/components/radio-group) |
| `RnSelect` | Dropdown selection | [Live](https://daeha76.github.io/RnUI/components/select) |
| `RnToggle` | Toggle button | [Live](https://daeha76.github.io/RnUI/components/toggle) |
| `RnToggleGroup` | Toggle button group | [Live](https://daeha76.github.io/RnUI/components/toggle-group) |
| `RnCombobox` | Searchable dropdown selection | [Live](https://daeha76.github.io/RnUI/components/combobox) |
| `RnInputOTP` | OTP input field | [Live](https://daeha76.github.io/RnUI/components/input-otp) |
| `RnField` | Field container (integrates Label, Description, Error) | [Live](https://daeha76.github.io/RnUI/components/field) |
| `RnForm` | Form validation | [Live](https://daeha76.github.io/RnUI/components/form) |

### Cards & Containers

| Component | Description | Demo |
|---|---|---|
| `RnCard` | Composed of Header, Title, Description, Content, Footer, Action | [Live](https://daeha76.github.io/RnUI/components/card) |
| `RnAlert` | Alert message (Default, Destructive) | [Live](https://daeha76.github.io/RnUI/components/alert) |
| `RnBadge` | Status badge | [Live](https://daeha76.github.io/RnUI/components/badge) |
| `RnAspectRatio` | Aspect ratio container | [Live](https://daeha76.github.io/RnUI/components/aspect-ratio) |
| `RnScrollArea` | Custom scroll area | [Live](https://daeha76.github.io/RnUI/components/scroll-area) |
| `RnSeparator` | Divider | [Live](https://daeha76.github.io/RnUI/components/separator) |
| `RnResizable` | Resizable panels | [Live](https://daeha76.github.io/RnUI/components/resizable) |

### Data Display

| Component | Description | Demo |
|---|---|---|
| `RnTable` | Basic table (Head, Body, Row, Header, Cell) | [Live](https://daeha76.github.io/RnUI/components/table) |
| `RnDataTable` | Advanced data table (sorting, filtering, pagination, selection, row expansion) | [Live](https://daeha76.github.io/RnUI/components/data-table) |
| `RnAvatar` | User avatar (with group support) | [Live](https://daeha76.github.io/RnUI/components/avatar) |
| `RnProgress` | Progress bar | [Live](https://daeha76.github.io/RnUI/components/progress) |
| `RnSlider` | Slider | [Live](https://daeha76.github.io/RnUI/components/slider) |
| `RnSkeleton` | Loading skeleton | [Live](https://daeha76.github.io/RnUI/components/skeleton) |
| `RnSpinner` | Loading spinner | [Live](https://daeha76.github.io/RnUI/components/spinner) |
| `RnKbd` | Keyboard shortcut display | [Live](https://daeha76.github.io/RnUI/components/kbd) |
| `RnCalendar` | Calendar | [Live](https://daeha76.github.io/RnUI/components/calendar) |

### Navigation

| Component | Description | Demo |
|---|---|---|
| `RnTabs` | Tab component (Default, Line variants) | [Live](https://daeha76.github.io/RnUI/components/tabs) |
| `RnBreadcrumb` | Breadcrumb navigation | [Live](https://daeha76.github.io/RnUI/components/breadcrumb) |
| `RnPagination` | Pagination | [Live](https://daeha76.github.io/RnUI/components/pagination) |
| `RnNavigationMenu` | Navigation menu | [Live](https://daeha76.github.io/RnUI/components/navigation-menu) |
| `RnSidebar` | Sidebar (Header, Content, Footer, Group, Menu) | [Live](https://daeha76.github.io/RnUI/components/sidebar) |
| `RnMainLayout01` | Three-slot layout (Header/Content/Footer) for use inside RnSidebarInset | [Live](https://daeha76.github.io/RnUI/components/main-layout-01) |
| `RnMenubar` | Menu bar | [Live](https://daeha76.github.io/RnUI/components/menubar) |

### Overlays

| Component | Description | Demo |
|---|---|---|
| `RnDialog` | Modal dialog | [Live](https://daeha76.github.io/RnUI/components/dialog) |
| `RnAlertDialog` | Confirmation dialog | [Live](https://daeha76.github.io/RnUI/components/alert-dialog) |
| `RnSheet` | Side sheet | [Live](https://daeha76.github.io/RnUI/components/sheet) |
| `RnPopover` | Popover | [Live](https://daeha76.github.io/RnUI/components/popover) |
| `RnTooltip` | Tooltip | [Live](https://daeha76.github.io/RnUI/components/tooltip) |
| `RnHoverCard` | Hover card | [Live](https://daeha76.github.io/RnUI/components/hover-card) |
| `RnDropdownMenu` | Dropdown menu | [Live](https://daeha76.github.io/RnUI/components/dropdown-menu) |
| `RnContextMenu` | Context menu | [Live](https://daeha76.github.io/RnUI/components/context-menu) |
| `RnDrawer` | Drawer (mobile bottom sheet) | [Live](https://daeha76.github.io/RnUI/components/drawer) |
| `RnToast` | Toast notification | [Live](https://daeha76.github.io/RnUI/components/toast) |

### Disclosure & Misc

| Component | Description | Demo |
|---|---|---|
| `RnAccordion` | Accordion | [Live](https://daeha76.github.io/RnUI/components/accordion) |
| `RnCollapsible` | Collapsible | [Live](https://daeha76.github.io/RnUI/components/collapsible) |
| `RnEmpty` | Empty state | [Live](https://daeha76.github.io/RnUI/components/empty) |
| `RnCarousel` | Carousel / slider | [Live](https://daeha76.github.io/RnUI/components/carousel) |
| `RnCommand` | Command palette | [Live](https://daeha76.github.io/RnUI/components/command) |
| `RnDatePicker` | Date picker | [Live](https://daeha76.github.io/RnUI/components/date-picker) |
| `RnGantt` | Gantt chart | [Live](https://daeha76.github.io/RnUI/components/gantt) |
| `RnButtonGroup` | Button group | [Live](https://daeha76.github.io/RnUI/components/button-group) |

---

## Usage Examples

### Button

Supports 6 variants and 8 sizes.

```razor
@* Variants *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* Sizes *@
<RnButton Size="ButtonSize.Sm">Small</RnButton>
<RnButton Size="ButtonSize.Default">Default</RnButton>
<RnButton Size="ButtonSize.Lg">Large</RnButton>
<RnButton Size="ButtonSize.Icon">🔔</RnButton>
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
        <RnButton>Save</RnButton>
        <RnButton Variant="ButtonVariant.Outline">Cancel</RnButton>
    </RnCardFooter>
</RnCard>
```

### Dialog

```razor
<RnButton OnClick="() => _dialogOpen = true">Open Dialog</RnButton>

<RnDialog @bind-Open="_dialogOpen">
    <RnDialogHeader>
        <RnDialogTitle>Edit Profile</RnDialogTitle>
        <RnDialogDescription>Make changes to your profile here.</RnDialogDescription>
    </RnDialogHeader>
    <div class="space-y-4 py-4">
        <RnInput Placeholder="Name" @bind-Value="_name" />
        <RnInput Type="email" Placeholder="Email" @bind-Value="_email" />
    </div>
    <RnDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">Cancel</RnButton>
        <RnButton OnClick="Save">Save Changes</RnButton>
    </RnDialogFooter>
</RnDialog>
```

### DataTable

```razor
<RnDataTable TItem="PaymentRecord"
             Items="_payments"
             SelectionMode="SelectionMode.Multiple"
             PageSize="10">
    <Columns>
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Id" Title="ID" Sortable />
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Name" Title="Name" Sortable Filterable />
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Amount" Title="Amount" Sortable>
            <CellTemplate Context="item">
                <div class="text-right font-medium">@item.Amount.ToString("C")</div>
            </CellTemplate>
        </RnDataTableColumn>
    </Columns>
</RnDataTable>
```

> For more examples, visit the [Demo Site](https://daeha76.github.io/RnUI/).

---

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

---

## Demo

**Online demo**: [https://daeha76.github.io/RnUI/](https://daeha76.github.io/RnUI/)

To run the demo locally, use the `Daeha.RnUI.Demo.Wasm` project:

```bash
# Build demo CSS
cd src/Daeha.RnUI.Demo.Wasm && npm install && npm run build:css

# Run the demo
dotnet run --project src/Daeha.RnUI.Demo.Wasm
```

Open `https://localhost:7256` in your browser to explore all components.

---

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'feat: Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## License

[MIT](LICENSE.md) License

---

<div align="center">

Made with ❤️ for the Blazor community

</div>
