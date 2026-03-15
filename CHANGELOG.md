# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.11.4] - 2025-06-15

### Fixed

- RnDropdownMenu: position `absolute` changed to `fixed` for scroll container compatibility

## [0.11.2] - 2025-06-14

### Changed

- Internal improvements and minor fixes

## [0.11.1] - 2025-06-13

### Added

- RnEventCalendar: N-week view mode with `CalendarViewMode` enum
- RnEventCalendar: layout model extraction and enums refactoring

### Fixed

- RnEventCalendar: namespace resolution for `CalendarViewMode` enum
- RnEventCalendar: cell heights fixed to 32, card border color alignment
- RnEventCalendar: daily events truncated to max 3 with overflow indicator

## [0.10.2] - 2025-06-10

### Changed

- Internal improvements and minor fixes

## [0.10.1] - 2025-06-09

### Added

- RnCalendar: `Xs` size variant and `ComponentSize.Xs` enum value
- RnCalendar: `Size` parameter support (`Default` / `Sm`)

## [0.8.6] - 2025-06-05

### Added

- Robust access control and validation for message deletion

## [0.8.1] - 2025-05-28

### Added

- Demo site links and Tailwind CSS setup guide in README
- Component demo URLs in documentation
- Multi-language README translations (Korean, Spanish, German, Japanese, Chinese)

### Fixed

- `PackageProjectUrl` set to GitHub Pages site

## [0.8.0] - 2025-05-27

### Added

- RnGantt: new Gantt chart component with supporting models, enums, state, and utilities
- RnScrollArea: full scroll area implementation
- RnNavigationMenu: viewport and indicator support
- RnRadioGroup: context support
- RnDataTable: advanced column grouping with visual borders
- RnSheet: demo content
- RnSelectSeparator: component addition
- RnTabs: `StateHasChanged` for tab updates, refined trigger styling

### Fixed

- RnDataTable: various bug fixes
- Layout classes adjusted for consistent padding and full-width content
- Docs sidebar href updated to use relative path

### Removed

- Legacy demo projects and Bootstrap assets

[0.11.4]: https://github.com/daeha76/RnUI/compare/v0.11.2...v0.11.4
[0.11.2]: https://github.com/daeha76/RnUI/compare/v0.11.1...v0.11.2
[0.11.1]: https://github.com/daeha76/RnUI/compare/v0.10.2...v0.11.1
[0.10.2]: https://github.com/daeha76/RnUI/compare/v0.10.1...v0.10.2
[0.10.1]: https://github.com/daeha76/RnUI/compare/v0.8.6...v0.10.1
[0.8.6]: https://github.com/daeha76/RnUI/compare/v0.8.1...v0.8.6
[0.8.1]: https://github.com/daeha76/RnUI/compare/v0.8.0...v0.8.1
[0.8.0]: https://github.com/daeha76/RnUI/releases/tag/v0.8.0
