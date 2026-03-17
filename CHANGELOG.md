# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.11.9] - 2026-03-17

### Changed

- Version bump only (no functional changes)

## [0.11.8] - 2026-03-17

### Added

- RnDataTable: `FullHeight` parameter to stretch table to fill parent container height

## [0.11.7] - 2026-03-17

### Fixed

- RnInput, RnSelect: background changed from `bg-transparent` to `bg-background` for consistent appearance

## [0.11.6] - 2026-03-16

### Changed

- NuGet README: replaced inline changelog with a link to the external CHANGELOG.md

## [0.11.5] - 2026-03-16

### Added

- bUnit test coverage for CssUtils, DataTableState, RnButton, and RnForm
- CI/CD npm install steps for library and demo projects (MSBuild targets)
- Roadmap, contributing guide, and GitHub issue templates

### Fixed

- RnDataTable: column filter uses exact matching instead of `Contains`

## [0.11.4] - 2026-03-15

### Fixed

- Version bump only (v0.11.3 content published as v0.11.4)

## [0.11.3] - 2026-03-15

### Fixed

- RnDropdownMenu: position `absolute` changed to `fixed` for scroll container compatibility

## [0.11.2] - 2026-03-15

### Changed

- Version bump only (no functional changes)

## [0.11.1] - 2026-03-13

### Changed

- Version bump only (v0.11.0 content published as v0.11.1)

## [0.11.0] - 2026-03-13

### Added

- RnEventCalendar: new custom Event Calendar component with data model
- RnEventCalendar: N-week view mode with `CalendarViewMode` enum
- RnEventCalendar: layout model extraction and enums refactoring
- RnEventCalendar: DocsSidebar menu entry

### Fixed

- RnEventCalendar: namespace resolution for `CalendarViewMode` enum
- RnEventCalendar: cell heights fixed to 32, card border color alignment
- RnEventCalendar: daily events truncated to max 3 with overflow indicator
- RnEventCalendar: horizontal formatting with `cn-calendar-grid` and `cn-card`

## [0.10.2] - 2026-03-13

### Changed

- Version bump only (no functional changes)

## [0.10.1] - 2026-03-13

### Changed

- Version bump only (v0.10.0 content published as v0.10.1)

## [0.10.0] - 2026-03-13

### Added

- RnCalendar: `Xs` size variant and `ComponentSize.Xs` enum value

## [0.9.0] - 2026-03-13

### Added

- RnCalendar: `Size` parameter support (`Default` / `Sm`)

## [0.8.6] - 2026-03-13

### Added

- Robust access control and validation for message deletion

## [0.8.5] - 2026-03-13

### Changed

- NuGet upload fixes (no functional changes)

## [0.8.4] - 2026-03-12

### Fixed

- `global.json` SDK version fix
- NuGet publish workflow added

## [0.8.3] - 2026-03-12

### Added

- Multi-language README translations (Korean, Spanish, German, Japanese)
- Demo site links and Tailwind CSS setup guide in README
- Component demo URLs in documentation

### Fixed

- `PackageProjectUrl` set to GitHub Pages site

## [0.8.1] - 2026-03-12

### Changed

- README and NUGET_README documentation updates
- Version bump to 0.8.1

## [0.8.0] - 2026-03-11

### Added

- RnGantt: new Gantt chart component with supporting models, enums, state, and utilities
- RnScrollArea: full scroll area implementation
- RnNavigationMenu: viewport and indicator support
- RnRadioGroup: context support
- RnDataTable: comprehensive component with 11 interactive examples, column grouping, visual borders
- RnSheet: demo content
- RnSelectSeparator: component addition
- RnTabs: `StateHasChanged` for tab updates, refined trigger styling
- Custom component rules and commands for non-shadcn components

### Fixed

- RnDataTable: various bug fixes
- Layout classes adjusted for consistent padding and full-width content
- Docs sidebar href updated to use relative path
- Task record renamed to TaskItem to avoid `System.Threading.Tasks.Task` conflict

### Removed

- Legacy demo projects and Bootstrap assets

[0.11.9]: https://github.com/daeha76/RnUI/compare/v0.11.8...v0.11.9
[0.11.8]: https://github.com/daeha76/RnUI/compare/v0.11.7...v0.11.8
[0.11.7]: https://github.com/daeha76/RnUI/compare/v0.11.6...v0.11.7
[0.11.6]: https://github.com/daeha76/RnUI/compare/v0.11.5...v0.11.6
[0.11.5]: https://github.com/daeha76/RnUI/compare/v0.11.4...v0.11.5
[0.11.4]: https://github.com/daeha76/RnUI/compare/v0.11.3...v0.11.4
[0.11.3]: https://github.com/daeha76/RnUI/compare/v0.11.2...v0.11.3
[0.11.2]: https://github.com/daeha76/RnUI/compare/v0.11.1...v0.11.2
[0.11.1]: https://github.com/daeha76/RnUI/compare/v0.11.0...v0.11.1
[0.11.0]: https://github.com/daeha76/RnUI/compare/v0.10.2...v0.11.0
[0.10.2]: https://github.com/daeha76/RnUI/compare/v0.10.1...v0.10.2
[0.10.1]: https://github.com/daeha76/RnUI/compare/v0.10.0...v0.10.1
[0.10.0]: https://github.com/daeha76/RnUI/compare/v0.9.0...v0.10.0
[0.9.0]: https://github.com/daeha76/RnUI/compare/v0.8.6...v0.9.0
[0.8.6]: https://github.com/daeha76/RnUI/compare/v0.8.5...v0.8.6
[0.8.5]: https://github.com/daeha76/RnUI/compare/v0.8.4...v0.8.5
[0.8.4]: https://github.com/daeha76/RnUI/compare/v0.8.3...v0.8.4
[0.8.3]: https://github.com/daeha76/RnUI/compare/v0.8.1...v0.8.3
[0.8.1]: https://github.com/daeha76/RnUI/compare/v0.8.0...v0.8.1
[0.8.0]: https://github.com/daeha76/RnUI/releases/tag/v0.8.0
