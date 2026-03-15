# Daeha.RnUI.Templates

Project templates for creating Blazor applications with [RnUI](https://github.com/daeha76/RnUI) — a Blazor port of shadcn/ui built with Tailwind CSS v4.

## Installation

```bash
dotnet new install Daeha.RnUI.Templates
```

## Available Templates

| Template | Short Name | Description |
|----------|------------|-------------|
| RnUI Blazor WebAssembly | `rnui-wasm` | Blazor WASM app with RnUI + Tailwind CSS v4 pre-configured |

## Usage

```bash
# Create a new project
dotnet new rnui-wasm -n MyApp

# Install npm dependencies (Tailwind CSS)
cd MyApp
npm install

# Build and run
dotnet run
```

## What's Included

- **Daeha.RnUI** NuGet package reference
- **Daeha.RnLucazor.Blazor** for Lucide icons
- **Tailwind CSS v4** with `@tailwindcss/cli`
- **shadcn.css** design tokens and component styles pre-linked
- MSBuild target for automatic CSS compilation
- Starter layout with RnUI components
- Dark mode enabled by default

## Uninstall

```bash
dotnet new uninstall Daeha.RnUI.Templates
```
