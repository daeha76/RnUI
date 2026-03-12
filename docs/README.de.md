# RnUI

**Documentation**: [English](../README.md) | [한국어](README.ko.md) | [中文](README.zh-CN.md) | [Español](README.es.md) | **Deutsch** | [日本語](README.ja.md)

**Blazor-Portierung von [shadcn/ui](https://ui.shadcn.com)**

Ansprechend gestaltete, barrierefreie UI-Komponenten mit Tailwind CSS fuer .NET Blazor-Anwendungen.

[**Live-Demo**](https://daeha76.github.io/RnUI/)

---

## Warum RnUI?

- **54 Komponentenkategorien** — Eine umfassende UI-Bibliothek mit 194 Razor-Komponentendateien
- **Basierend auf shadcn/ui** — Ein bewaehrtes Designsystem aus dem Web, direkt nach Blazor portiert
- **Tailwind CSS** — oklch-Farbsystem mit CSS-Custom-Property-basiertem Theming
- **Dunkelmodus** — Integrierte Unterstuetzung fuer hellen/dunklen Modus
- **Barrierefrei** — Komponenten mit Barrierefreiheit im Fokus entwickelt
- **Keine externen Abhaengigkeiten** — Nur ASP.NET Core Framework-Referenzen
- **Multi-Target** — Unterstuetzung fuer .NET 8.0, 9.0, 10.0 | Kompatibel mit Blazor Server & WebAssembly

---

## Schnellstart

### 1. Paket installieren

```bash
dotnet add package Daeha.RnUI
```

### 2. Imports hinzufuegen

Fuegen Sie Folgendes in Ihre `_Imports.razor` ein:

```razor
@using Daeha.RnUI.Components.UI
```

### 3. Stylesheet einbinden

Fuegen Sie das CSS in Ihre `App.razor` oder `_Host.cshtml` ein:

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. Tailwind CSS einrichten

RnUI verwendet Tailwind CSS v4. Sie muessen Tailwind CSS in Ihrem Blazor-Projekt einrichten, um Utility-Klassen zu scannen und zu kompilieren.

#### 4-1. Tailwind CSS installieren

Installieren Sie Tailwind CSS im Stammverzeichnis Ihres Projekts:

```bash
npm init -y
npm install -D tailwindcss @tailwindcss/cli
```

#### 4-2. CSS-Einstiegsdatei erstellen

Erstellen Sie eine `wwwroot/input.css`-Datei und binden Sie Ihre Razor-Dateien als Scan-Ziele ein:

```css
@import "tailwindcss";
@source "../**/*.razor";
```

> **Bei Verwendung des NuGet-Pakets**: Wenn die Bibliothek ueber NuGet installiert wurde, sind die `.razor`-Dateien der Bibliothek nicht lokal verfuegbar. Die Basisstile der RnUI-Komponenten (`.cn-*`-Klassen) sind jedoch bereits in `shadcn.css` enthalten und funktionieren ohne zusaetzliche `@source`-Konfiguration. Sie muessen nur die Tailwind-Utility-Klassen scannen, die in Ihren eigenen `.razor`-Dateien verwendet werden.

#### 4-3. Build-Skripte hinzufuegen

Fuegen Sie Build-Skripte zu Ihrer `package.json` hinzu:

```json
{
  "scripts": {
    "build:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css",
    "watch:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css --watch"
  }
}
```

#### 4-4. Stylesheets einbinden

Binden Sie sowohl die RnUI-Stile als auch die Tailwind-CSS-Ausgabedatei in Ihre `App.razor` oder `_Host.cshtml` ein:

```html
<!-- RnUI-Komponentenstile -->
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
<!-- Tailwind-Utility-Klassen -->
<link rel="stylesheet" href="css/tailwindcss.css" />
```

#### 4-5. CSS kompilieren

Verwenden Sie den Watch-Modus waehrend der Entwicklung und den Build fuer die Produktion:

```bash
npm run watch:css   # Entwicklung (ueberwacht Dateiaenderungen)
npm run build:css   # Produktion (einmaliger Build)
```

---

## Komponenten

> Live-Demos und detaillierte Verwendungshinweise fuer jede Komponente finden Sie auf der [**Demo-Seite**](https://daeha76.github.io/RnUI/components).

### Schaltflaechen & Eingaben

| Komponente | Beschreibung | Demo |
|---|---|---|
| `RnButton` | Unterstuetzt die Varianten Default, Secondary, Outline, Ghost, Destructive, Link | [Live](https://daeha76.github.io/RnUI/components/button) |
| `RnInput` | Texteingabefeld | [Live](https://daeha76.github.io/RnUI/components/input) |
| `RnTextarea` | Mehrzeiliges Texteingabefeld | [Live](https://daeha76.github.io/RnUI/components/textarea) |
| `RnLabel` | Formularbeschriftung | [Live](https://daeha76.github.io/RnUI/components/label) |
| `RnCheckbox` | Kontrollkaestchen | [Live](https://daeha76.github.io/RnUI/components/checkbox) |
| `RnSwitch` | Umschalter | [Live](https://daeha76.github.io/RnUI/components/switch) |
| `RnRadioGroup` | Optionsfeldgruppe | [Live](https://daeha76.github.io/RnUI/components/radio-group) |
| `RnSelect` | Dropdown-Auswahl | [Live](https://daeha76.github.io/RnUI/components/select) |
| `RnToggle` | Umschaltflaeche | [Live](https://daeha76.github.io/RnUI/components/toggle) |
| `RnToggleGroup` | Umschaltflaechengruppe | [Live](https://daeha76.github.io/RnUI/components/toggle-group) |
| `RnCombobox` | Durchsuchbare Dropdown-Auswahl | [Live](https://daeha76.github.io/RnUI/components/combobox) |
| `RnInputOTP` | OTP-Eingabefeld | [Live](https://daeha76.github.io/RnUI/components/input-otp) |
| `RnField` | Feldcontainer (integriert Label, Beschreibung, Fehlermeldung) | [Live](https://daeha76.github.io/RnUI/components/field) |
| `RnForm` | Formularvalidierung | [Live](https://daeha76.github.io/RnUI/components/form) |

### Karten & Container

| Komponente | Beschreibung | Demo |
|---|---|---|
| `RnCard` | Bestehend aus Header, Title, Description, Content, Footer, Action | [Live](https://daeha76.github.io/RnUI/components/card) |
| `RnAlert` | Warnmeldung (Default, Destructive) | [Live](https://daeha76.github.io/RnUI/components/alert) |
| `RnBadge` | Statusabzeichen | [Live](https://daeha76.github.io/RnUI/components/badge) |
| `RnAspectRatio` | Seitenverhaeltnis-Container | [Live](https://daeha76.github.io/RnUI/components/aspect-ratio) |
| `RnScrollArea` | Benutzerdefinierter Scrollbereich | [Live](https://daeha76.github.io/RnUI/components/scroll-area) |
| `RnSeparator` | Trennlinie | [Live](https://daeha76.github.io/RnUI/components/separator) |
| `RnResizable` | Groessenveraenderbare Panels | [Live](https://daeha76.github.io/RnUI/components/resizable) |

### Datenanzeige

| Komponente | Beschreibung | Demo |
|---|---|---|
| `RnTable` | Basistabelle (Head, Body, Row, Header, Cell) | [Live](https://daeha76.github.io/RnUI/components/table) |
| `RnDataTable` | Erweiterte Datentabelle (Sortierung, Filterung, Paginierung, Auswahl, Zeilenerweiterung) | [Live](https://daeha76.github.io/RnUI/components/data-table) |
| `RnAvatar` | Benutzeravatar (mit Gruppenunterstuetzung) | [Live](https://daeha76.github.io/RnUI/components/avatar) |
| `RnProgress` | Fortschrittsanzeige | [Live](https://daeha76.github.io/RnUI/components/progress) |
| `RnSlider` | Schieberegler | [Live](https://daeha76.github.io/RnUI/components/slider) |
| `RnSkeleton` | Lade-Platzhalter | [Live](https://daeha76.github.io/RnUI/components/skeleton) |
| `RnSpinner` | Ladeanimation | [Live](https://daeha76.github.io/RnUI/components/spinner) |
| `RnKbd` | Tastaturkuerzel-Anzeige | [Live](https://daeha76.github.io/RnUI/components/kbd) |
| `RnCalendar` | Kalender | [Live](https://daeha76.github.io/RnUI/components/calendar) |

### Navigation

| Komponente | Beschreibung | Demo |
|---|---|---|
| `RnTabs` | Tab-Komponente (Varianten: Default, Line) | [Live](https://daeha76.github.io/RnUI/components/tabs) |
| `RnBreadcrumb` | Brotkruemelnavigation | [Live](https://daeha76.github.io/RnUI/components/breadcrumb) |
| `RnPagination` | Seitennavigation | [Live](https://daeha76.github.io/RnUI/components/pagination) |
| `RnNavigationMenu` | Navigationsmenue | [Live](https://daeha76.github.io/RnUI/components/navigation-menu) |
| `RnSidebar` | Seitenleiste (Header, Content, Footer, Group, Menu) | [Live](https://daeha76.github.io/RnUI/components/sidebar) |
| `RnMenubar` | Menuezeile | [Live](https://daeha76.github.io/RnUI/components/menubar) |

### Overlays

| Komponente | Beschreibung | Demo |
|---|---|---|
| `RnDialog` | Modaler Dialog | [Live](https://daeha76.github.io/RnUI/components/dialog) |
| `RnAlertDialog` | Bestaetigungsdialog | [Live](https://daeha76.github.io/RnUI/components/alert-dialog) |
| `RnSheet` | Seitenpanel | [Live](https://daeha76.github.io/RnUI/components/sheet) |
| `RnPopover` | Popover | [Live](https://daeha76.github.io/RnUI/components/popover) |
| `RnTooltip` | Tooltip | [Live](https://daeha76.github.io/RnUI/components/tooltip) |
| `RnHoverCard` | Hover-Karte | [Live](https://daeha76.github.io/RnUI/components/hover-card) |
| `RnDropdownMenu` | Dropdown-Menue | [Live](https://daeha76.github.io/RnUI/components/dropdown-menu) |
| `RnContextMenu` | Kontextmenue | [Live](https://daeha76.github.io/RnUI/components/context-menu) |
| `RnDrawer` | Drawer (mobiles Bottom-Sheet) | [Live](https://daeha76.github.io/RnUI/components/drawer) |
| `RnToast` | Toast-Benachrichtigung | [Live](https://daeha76.github.io/RnUI/components/toast) |

### Aufklappbereiche & Sonstiges

| Komponente | Beschreibung | Demo |
|---|---|---|
| `RnAccordion` | Akkordeon | [Live](https://daeha76.github.io/RnUI/components/accordion) |
| `RnCollapsible` | Einklappbar | [Live](https://daeha76.github.io/RnUI/components/collapsible) |
| `RnEmpty` | Leerer Zustand | [Live](https://daeha76.github.io/RnUI/components/empty) |
| `RnCarousel` | Karussell / Slider | [Live](https://daeha76.github.io/RnUI/components/carousel) |
| `RnCommand` | Befehlspalette | [Live](https://daeha76.github.io/RnUI/components/command) |
| `RnDatePicker` | Datumsauswahl | [Live](https://daeha76.github.io/RnUI/components/date-picker) |
| `RnGantt` | Gantt-Diagramm | [Live](https://daeha76.github.io/RnUI/components/gantt) |
| `RnButtonGroup` | Schaltflaechengruppe | [Live](https://daeha76.github.io/RnUI/components/button-group) |

---

## Verwendungsbeispiele

### Button

Unterstuetzt 6 Varianten und 8 Groessen.

```razor
@* Varianten *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* Groessen *@
<RnButton Size="ButtonSize.Sm">Klein</RnButton>
<RnButton Size="ButtonSize.Default">Standard</RnButton>
<RnButton Size="ButtonSize.Lg">Gross</RnButton>
<RnButton Size="ButtonSize.Icon">🔔</RnButton>
```

### Card

```razor
<RnCard>
    <RnCardHeader>
        <RnCardTitle>Kartentitel</RnCardTitle>
        <RnCardDescription>Hier steht die Kartenbeschreibung.</RnCardDescription>
    </RnCardHeader>
    <RnCardContent>
        <p>Karteninhalt mit etwas Beispieltext.</p>
    </RnCardContent>
    <RnCardFooter>
        <RnButton>Speichern</RnButton>
        <RnButton Variant="ButtonVariant.Outline">Abbrechen</RnButton>
    </RnCardFooter>
</RnCard>
```

### Dialog

```razor
<RnButton OnClick="() => _dialogOpen = true">Dialog oeffnen</RnButton>

<RnDialog @bind-Open="_dialogOpen">
    <RnDialogHeader>
        <RnDialogTitle>Profil bearbeiten</RnDialogTitle>
        <RnDialogDescription>Nehmen Sie hier Aenderungen an Ihrem Profil vor.</RnDialogDescription>
    </RnDialogHeader>
    <div class="space-y-4 py-4">
        <RnInput Placeholder="Name" @bind-Value="_name" />
        <RnInput Type="email" Placeholder="E-Mail" @bind-Value="_email" />
    </div>
    <RnDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">Abbrechen</RnButton>
        <RnButton OnClick="Save">Aenderungen speichern</RnButton>
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
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Amount" Title="Betrag" Sortable>
            <CellTemplate Context="item">
                <div class="text-right font-medium">@item.Amount.ToString("C")</div>
            </CellTemplate>
        </RnDataTableColumn>
    </Columns>
</RnDataTable>
```

> Weitere Beispiele finden Sie auf der [Demo-Seite](https://daeha76.github.io/RnUI/).

---

## Anpassung

RnUI verwendet CSS-Custom-Properties fuer das Theming. Ueberschreiben Sie die folgenden Variablen, um die Farben anzupassen:

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

**Online-Demo**: [https://daeha76.github.io/RnUI/](https://daeha76.github.io/RnUI/)

Um die Demo lokal auszufuehren, verwenden Sie das Projekt `Daeha.RnUI.Demo.Wasm`:

```bash
# Demo-CSS kompilieren
cd src/Daeha.RnUI.Demo.Wasm && npm install && npm run build:css

# Demo starten
dotnet run --project src/Daeha.RnUI.Demo.Wasm
```

Oeffnen Sie `https://localhost:7256` in Ihrem Browser, um alle Komponenten zu erkunden.

---

## Mitwirken

Beitraege sind willkommen! Reichen Sie gerne Issues oder Pull Requests ein.

1. Forken Sie das Repository
2. Erstellen Sie einen Feature-Branch (`git checkout -b feature/tolles-feature`)
3. Committen Sie Ihre Aenderungen (`git commit -m 'feat: Tolles Feature hinzufuegen'`)
4. Pushen Sie den Branch (`git push origin feature/tolles-feature`)
5. Erstellen Sie einen Pull Request

---

## Lizenz

[MIT](../LICENSE.md)-Lizenz

---

<div align="center">

Mit Liebe fuer die Blazor-Community erstellt

</div>
