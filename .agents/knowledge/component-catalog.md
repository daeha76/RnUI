# Component Catalog

## 구현 완료 컴포넌트 (53개 카테고리)

### Forms — 입력/선택

| 컴포넌트 | 파일 | Variant/Size | 패턴 |
|----------|------|-------------|------|
| **Button** | RnButton | ButtonVariant(6), ButtonSize(8) | Variant |
| **ButtonGroup** | RnButtonGroup + Separator + Text | ButtonGroupOrientation | Composite |
| **Input** | RnInput | — | Simple |
| **Textarea** | RnTextarea | — | Simple |
| **Label** | RnLabel | — | Simple |
| **Checkbox** | RnCheckbox | — | Stateful (two-way) |
| **Switch** | RnSwitch | ComponentSize | Stateful (two-way) |
| **RadioGroup** | RnRadioGroup + Item | — | Stateful (CascadingValue) |
| **Select** | RnSelect + Group + Item + Label | — | Stateful |
| **Combobox** | RnCombobox + Item | — | Stateful |
| **Slider** | RnSlider | — | Stateful |
| **Toggle** | RnToggle | ToggleVariant, ToggleSize | Variant + Stateful |
| **ToggleGroup** | RnToggleGroup + Item | ToggleGroupType, ToggleGroupVariant, ToggleGroupSize | Stateful (CascadingValue) |
| **InputOTP** | RnInputOTP + Group + Slot + Separator | — | Stateful |
| **DatePicker** | RnDatePicker | — | Stateful |
| **Calendar** | RnCalendar | — | Stateful |
| **Form** | RnFormItem + Label + Description + Message | — | Composite |
| **Field** | RnField + Group/Label/Description/Error/Set/Legend/Content/Title/Separator | — | Composite |

### Data Display — 데이터 표시

| 컴포넌트 | 파일 | Variant/Size | 패턴 |
|----------|------|-------------|------|
| **Card** | RnCard + Header/Title/Description/Content/Footer/Action | ComponentSize | Composite |
| **Table** | RnTable + Header/Body/Footer/Row/Head/Cell/Caption | — | Composite |
| **DataTable** | RnDataTable + Column/Search/ColumnToggle/Pagination/RowActions/ColumnHeader | SelectionMode | Stateful (CascadingValue) |
| **Badge** | RnBadge | BadgeVariant(6) | Variant |
| **Avatar** | RnAvatar + Fallback | — | Composite |
| **AvatarGroup** | RnAvatarGroup | — | Composite |
| **Progress** | RnProgress | — | Simple |
| **Skeleton** | RnSkeleton | — | Simple |
| **Kbd** | RnKbd + Group | — | Simple |
| **Empty** | RnEmpty | — | Simple |

### Feedback — 알림/피드백

| 컴포넌트 | 파일 | Variant/Size | 패턴 |
|----------|------|-------------|------|
| **Alert** | RnAlert + Title/Description/Action | AlertVariant(2) | Variant + Composite |
| **Toast** | RnToast + Title/Description/Action/Close + Provider | ToastVariant(2) | Service 패턴 |
| **Spinner** | RnSpinner | — | Simple |

### Layout — 레이아웃/오버레이

| 컴포넌트 | 파일 | Variant/Size | 패턴 |
|----------|------|-------------|------|
| **Dialog** | RnDialog + Header/Title/Description/Footer | — | Overlay |
| **AlertDialog** | RnAlertDialog + Header/Title/Description/Footer | — | Overlay |
| **Sheet** | RnSheet + Header/Title/Description/Footer | — | Overlay |
| **Drawer** | RnDrawer + Header/Title/Description/Content/Footer | — | Overlay |
| **Popover** | RnPopover | — | Overlay |
| **HoverCard** | RnHoverCard | — | Overlay |
| **Tooltip** | RnTooltip | — | Overlay |
| **AspectRatio** | RnAspectRatio | — | Simple |
| **ScrollArea** | RnScrollArea | — | Simple |
| **Separator** | RnSeparator | — | Simple |
| **Resizable** | RnResizablePanelGroup + Panel + Handle | — | Stateful |
| **Collapsible** | RnCollapsible | — | Stateful |

### Navigation — 네비게이션

| 컴포넌트 | 파일 | Variant/Size | 패턴 |
|----------|------|-------------|------|
| **Sidebar** | RnSidebar + Content/Header/Footer/Group/Menu/MenuItem/MenuButton + Sub | SidebarSide, SidebarVariant, SidebarCollapsible | Stateful (CascadingValue) |
| **NavigationMenu** | RnNavigationMenu + List/Item/Trigger/Content/Link | — | Stateful (CascadingValue) |
| **Breadcrumb** | RnBreadcrumb + Item/Link/Page/Separator | — | Composite |
| **Pagination** | RnPagination | — | Stateful |
| **Tabs** | RnTabs + List/Trigger/Content | TabsOrientation | Stateful (CascadingValue) |
| **Menubar** | RnMenubar + Menu/Trigger/Content/Item/Separator | — | Stateful |
| **ContextMenu** | RnContextMenu | — | Overlay |
| **DropdownMenu** | RnDropdownMenu + Item/Label/Separator | — | Overlay |

### Data Entry — 특수 입력

| 컴포넌트 | 파일 | Variant/Size | 패턴 |
|----------|------|-------------|------|
| **Command** | RnCommand + Dialog/Input/List/Group/Item/Empty/Separator | — | Stateful |
| **Carousel** | RnCarousel + Content/Item/Previous/Next | — | Stateful |

---

## Enum 파일 목록

| 파일 | 정의된 Enum |
|------|------------|
| `ButtonEnums.cs` | ButtonVariant, ButtonSize |
| `ButtonGroupEnums.cs` | ButtonGroupOrientation |
| `BadgeEnums.cs` | BadgeVariant |
| `AlertEnums.cs` | AlertVariant |
| `AccordionEnums.cs` | AccordionType |
| `ToggleEnums.cs` | ToggleVariant, ToggleSize |
| `ToggleGroupEnums.cs` | ToggleGroupType, ToggleGroupVariant, ToggleGroupSize |
| `ToastEnums.cs` | ToastVariant |
| `TabsEnums.cs` | TabsOrientation |
| `SidebarEnums.cs` | SidebarSide, SidebarVariant, SidebarCollapsible |
| `DataTableEnums.cs` | SelectionMode |
| `SharedEnums.cs` | ComponentSize, Side, Alignment, Orientation |

---

## shadcn/ui 미구현 컴포넌트 (후보)

참고: https://ui.shadcn.com/docs/components

- Sonner (toast 대체 — 현재 RnToast로 커버)
- Chart (Recharts 기반 — Blazor 차트 라이브러리 필요)
