# RnUI

**Documentation**: [English](../README.md) | [한국어](README.ko.md) | [中文](README.zh-CN.md) | **Español** | [Deutsch](README.de.md) | [日本語](README.ja.md)

**Port de [shadcn/ui](https://ui.shadcn.com) para Blazor**

Componentes de interfaz de usuario bellamente diseñados y accesibles, construidos con Tailwind CSS para aplicaciones .NET Blazor.

[**Demo en vivo**](https://daeha76.github.io/RnUI/)

---

## ¿Por qué RnUI?

- **54 categorías de componentes** — Una biblioteca de UI completa con 194 archivos de componentes Razor
- **Basado en shadcn/ui** — Un sistema de diseño probado de la web, portado directamente a Blazor
- **Tailwind CSS** — Sistema de color oklch con tematización basada en propiedades personalizadas CSS
- **Modo oscuro** — Soporte integrado de modo claro/oscuro
- **Accesible** — Componentes diseñados con la accesibilidad en mente
- **Cero dependencias externas** — Solo referencias del framework ASP.NET Core
- **Multi-target** — Soporte para .NET 8.0, 9.0, 10.0 | Compatible con Blazor Server y WebAssembly

---

## Inicio rápido

### 1. Instalar el paquete

```bash
dotnet add package Daeha.RnUI
```

### 2. Agregar las importaciones

Añade lo siguiente a tu archivo `_Imports.razor`:

```razor
@using Daeha.RnUI.Components.UI
```

### 3. Vincular la hoja de estilos

Añade el CSS a tu archivo `App.razor` o `_Host.cshtml`:

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. Configurar Tailwind CSS

RnUI utiliza Tailwind CSS v4. Necesitas configurar Tailwind CSS en tu proyecto Blazor para escanear y compilar las clases de utilidad.

#### 4-1. Instalar Tailwind CSS

Instala Tailwind CSS en la raíz de tu proyecto:

```bash
npm init -y
npm install -D tailwindcss @tailwindcss/cli
```

#### 4-2. Crear el archivo CSS de entrada

Crea un archivo `wwwroot/input.css` e incluye tus archivos Razor como objetivos de escaneo:

```css
@import "tailwindcss";
@source "../**/*.razor";
```

> **Al usar el paquete NuGet**: Si se instala mediante NuGet, los archivos `.razor` de la biblioteca no están disponibles localmente. Sin embargo, los estilos base de los componentes RnUI (clases `.cn-*`) ya están incluidos en `shadcn.css`, por lo que funcionan sin configuración adicional de `@source`. Solo necesitas escanear las clases de utilidad de Tailwind utilizadas en tus propios archivos `.razor`.

#### 4-3. Agregar scripts de compilación

Añade scripts de compilación a tu `package.json`:

```json
{
  "scripts": {
    "build:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css",
    "watch:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css --watch"
  }
}
```

#### 4-4. Vincular las hojas de estilos

Vincula tanto los estilos de RnUI como el archivo de salida de Tailwind CSS en tu `App.razor` o `_Host.cshtml`:

```html
<!-- Estilos de componentes RnUI -->
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
<!-- Clases de utilidad Tailwind -->
<link rel="stylesheet" href="css/tailwindcss.css" />
```

#### 4-5. Compilar CSS

Usa el modo de vigilancia durante el desarrollo y compila para producción:

```bash
npm run watch:css   # Desarrollo (vigila cambios en archivos)
npm run build:css   # Producción (compilación única)
```

---

## Componentes

> Consulta las demostraciones en vivo y el uso detallado de cada componente en el [**Sitio de demostración**](https://daeha76.github.io/RnUI/components).

### Botones y entradas

| Componente | Descripción | Demo |
|---|---|---|
| `RnButton` | Soporta variantes Default, Secondary, Outline, Ghost, Destructive y Link | [Ver](https://daeha76.github.io/RnUI/components/button) |
| `RnInput` | Campo de entrada de texto | [Ver](https://daeha76.github.io/RnUI/components/input) |
| `RnTextarea` | Entrada de texto multilínea | [Ver](https://daeha76.github.io/RnUI/components/textarea) |
| `RnLabel` | Etiqueta de formulario | [Ver](https://daeha76.github.io/RnUI/components/label) |
| `RnCheckbox` | Casilla de verificación | [Ver](https://daeha76.github.io/RnUI/components/checkbox) |
| `RnSwitch` | Interruptor de alternancia | [Ver](https://daeha76.github.io/RnUI/components/switch) |
| `RnRadioGroup` | Grupo de botones de radio | [Ver](https://daeha76.github.io/RnUI/components/radio-group) |
| `RnSelect` | Selección desplegable | [Ver](https://daeha76.github.io/RnUI/components/select) |
| `RnToggle` | Botón de alternancia | [Ver](https://daeha76.github.io/RnUI/components/toggle) |
| `RnToggleGroup` | Grupo de botones de alternancia | [Ver](https://daeha76.github.io/RnUI/components/toggle-group) |
| `RnCombobox` | Selección desplegable con búsqueda | [Ver](https://daeha76.github.io/RnUI/components/combobox) |
| `RnInputOTP` | Campo de entrada OTP | [Ver](https://daeha76.github.io/RnUI/components/input-otp) |
| `RnField` | Contenedor de campo (integra Label, Description, Error) | [Ver](https://daeha76.github.io/RnUI/components/field) |
| `RnForm` | Validación de formularios | [Ver](https://daeha76.github.io/RnUI/components/form) |

### Tarjetas y contenedores

| Componente | Descripción | Demo |
|---|---|---|
| `RnCard` | Compuesto por Header, Title, Description, Content, Footer, Action | [Ver](https://daeha76.github.io/RnUI/components/card) |
| `RnAlert` | Mensaje de alerta (Default, Destructive) | [Ver](https://daeha76.github.io/RnUI/components/alert) |
| `RnBadge` | Insignia de estado | [Ver](https://daeha76.github.io/RnUI/components/badge) |
| `RnAspectRatio` | Contenedor de relación de aspecto | [Ver](https://daeha76.github.io/RnUI/components/aspect-ratio) |
| `RnScrollArea` | Área de desplazamiento personalizada | [Ver](https://daeha76.github.io/RnUI/components/scroll-area) |
| `RnSeparator` | Divisor | [Ver](https://daeha76.github.io/RnUI/components/separator) |
| `RnResizable` | Paneles redimensionables | [Ver](https://daeha76.github.io/RnUI/components/resizable) |

### Visualización de datos

| Componente | Descripción | Demo |
|---|---|---|
| `RnTable` | Tabla básica (Head, Body, Row, Header, Cell) | [Ver](https://daeha76.github.io/RnUI/components/table) |
| `RnDataTable` | Tabla de datos avanzada (ordenación, filtrado, paginación, selección, expansión de filas) | [Ver](https://daeha76.github.io/RnUI/components/data-table) |
| `RnAvatar` | Avatar de usuario (con soporte de grupo) | [Ver](https://daeha76.github.io/RnUI/components/avatar) |
| `RnProgress` | Barra de progreso | [Ver](https://daeha76.github.io/RnUI/components/progress) |
| `RnSlider` | Control deslizante | [Ver](https://daeha76.github.io/RnUI/components/slider) |
| `RnSkeleton` | Esqueleto de carga | [Ver](https://daeha76.github.io/RnUI/components/skeleton) |
| `RnSpinner` | Indicador de carga giratorio | [Ver](https://daeha76.github.io/RnUI/components/spinner) |
| `RnKbd` | Visualización de atajos de teclado | [Ver](https://daeha76.github.io/RnUI/components/kbd) |
| `RnCalendar` | Calendario | [Ver](https://daeha76.github.io/RnUI/components/calendar) |

### Navegación

| Componente | Descripción | Demo |
|---|---|---|
| `RnTabs` | Componente de pestañas (variantes Default y Line) | [Ver](https://daeha76.github.io/RnUI/components/tabs) |
| `RnBreadcrumb` | Navegación de migas de pan | [Ver](https://daeha76.github.io/RnUI/components/breadcrumb) |
| `RnPagination` | Paginación | [Ver](https://daeha76.github.io/RnUI/components/pagination) |
| `RnNavigationMenu` | Menú de navegación | [Ver](https://daeha76.github.io/RnUI/components/navigation-menu) |
| `RnSidebar` | Barra lateral (Header, Content, Footer, Group, Menu) | [Ver](https://daeha76.github.io/RnUI/components/sidebar) |
| `RnMainLayout01` | Diseño de tres ranuras (Header/Content/Footer) para usar dentro de RnSidebarInset | [Ver](https://daeha76.github.io/RnUI/components/main-layout-01) |
| `RnMenubar` | Barra de menú | [Ver](https://daeha76.github.io/RnUI/components/menubar) |

### Superposiciones

| Componente | Descripción | Demo |
|---|---|---|
| `RnDialog` | Diálogo modal | [Ver](https://daeha76.github.io/RnUI/components/dialog) |
| `RnAlertDialog` | Diálogo de confirmación | [Ver](https://daeha76.github.io/RnUI/components/alert-dialog) |
| `RnSheet` | Panel lateral | [Ver](https://daeha76.github.io/RnUI/components/sheet) |
| `RnPopover` | Popover | [Ver](https://daeha76.github.io/RnUI/components/popover) |
| `RnTooltip` | Tooltip | [Ver](https://daeha76.github.io/RnUI/components/tooltip) |
| `RnHoverCard` | Tarjeta emergente al pasar el cursor | [Ver](https://daeha76.github.io/RnUI/components/hover-card) |
| `RnDropdownMenu` | Menú desplegable | [Ver](https://daeha76.github.io/RnUI/components/dropdown-menu) |
| `RnContextMenu` | Menú contextual | [Ver](https://daeha76.github.io/RnUI/components/context-menu) |
| `RnDrawer` | Cajón (panel inferior móvil) | [Ver](https://daeha76.github.io/RnUI/components/drawer) |
| `RnToast` | Notificación toast | [Ver](https://daeha76.github.io/RnUI/components/toast) |

### Desplegables y otros

| Componente | Descripción | Demo |
|---|---|---|
| `RnAccordion` | Acordeón | [Ver](https://daeha76.github.io/RnUI/components/accordion) |
| `RnCollapsible` | Colapsable | [Ver](https://daeha76.github.io/RnUI/components/collapsible) |
| `RnEmpty` | Estado vacío | [Ver](https://daeha76.github.io/RnUI/components/empty) |
| `RnCarousel` | Carrusel / deslizador | [Ver](https://daeha76.github.io/RnUI/components/carousel) |
| `RnCommand` | Paleta de comandos | [Ver](https://daeha76.github.io/RnUI/components/command) |
| `RnDatePicker` | Selector de fecha | [Ver](https://daeha76.github.io/RnUI/components/date-picker) |
| `RnGantt` | Diagrama de Gantt | [Ver](https://daeha76.github.io/RnUI/components/gantt) |
| `RnButtonGroup` | Grupo de botones | [Ver](https://daeha76.github.io/RnUI/components/button-group) |

---

## Ejemplos de uso

### Button

Soporta 6 variantes y 8 tamaños.

```razor
@* Variantes *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* Tamaños *@
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

> Para más ejemplos, visita el [Sitio de demostración](https://daeha76.github.io/RnUI/).

---

## Personalización

RnUI utiliza propiedades personalizadas CSS para la tematización. Sobrescribe las siguientes variables para personalizar los colores:

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

**Demo en línea**: [https://daeha76.github.io/RnUI/](https://daeha76.github.io/RnUI/)

Para ejecutar la demo localmente, usa el proyecto `Daeha.RnUI.Demo.Wasm`:

```bash
# Compilar CSS de la demo
cd src/Daeha.RnUI.Demo.Wasm && npm install && npm run build:css

# Ejecutar la demo
dotnet run --project src/Daeha.RnUI.Demo.Wasm
```

Abre `https://localhost:7256` en tu navegador para explorar todos los componentes.

---

## Contribuir

¡Las contribuciones son bienvenidas! No dudes en enviar issues o pull requests.

1. Haz un fork del repositorio
2. Crea una rama de funcionalidad (`git checkout -b feature/amazing-feature`)
3. Confirma tus cambios (`git commit -m 'feat: Add amazing feature'`)
4. Sube la rama (`git push origin feature/amazing-feature`)
5. Abre un Pull Request

---

## Licencia

Licencia [MIT](../LICENSE.md)

---

<div align="center">

Hecho con ❤️ para la comunidad Blazor

</div>
