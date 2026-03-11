<div align="center">

# RnUI

**Blazor port of [shadcn/ui](https://ui.shadcn.com)**

Beautifully designed, accessible UI components built with Tailwind CSS for .NET Blazor applications.

[![NuGet](https://img.shields.io/nuget/v/Daeha.RnUI?style=flat-square&logo=nuget&color=004880)](https://www.nuget.org/packages/Daeha.RnUI)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Daeha.RnUI?style=flat-square&logo=nuget&color=004880)](https://www.nuget.org/packages/Daeha.RnUI)
[![License](https://img.shields.io/github/license/daeha76/RnUI?style=flat-square)](LICENSE.md)
[![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-512bd4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com)

> 이 라이브러리는 딸 리안(Rian)의 이름을 담아 만든 Blazor UI 컴포넌트 라이브러리입니다. 💕

</div>

---

## Why RnUI?

- **54 component categories** — 194개의 Razor 컴포넌트 파일로 구성된 포괄적인 UI 라이브러리
- **shadcn/ui 기반** — 웹에서 검증된 디자인 시스템을 Blazor로 그대로 이식
- **Tailwind CSS** — oklch 컬러 시스템과 CSS 커스텀 프로퍼티 기반 테마
- **다크 모드** — 라이트/다크 모드 기본 지원
- **접근성** — 접근 가능한 컴포넌트 설계
- **외부 의존성 없음** — ASP.NET Core 프레임워크 참조만 사용
- **멀티 타겟** — .NET 8.0, 9.0, 10.0 지원 | Blazor Server & WebAssembly 호환

---

## Quick Start

### 1. 패키지 설치

```bash
dotnet add package Daeha.RnUI
```

### 2. Import 추가

`_Imports.razor`에 다음을 추가합니다:

```razor
@using Daeha.RnUI.Components.UI
```

### 3. 스타일시트 연결

`App.razor` 또는 `_Host.cshtml`에 CSS를 추가합니다:

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. Tailwind CSS 설정

Blazor 프로젝트에 Tailwind CSS가 설정되어 있어야 하며, RnUI 패키지의 클래스명을 스캔하도록 구성합니다.

---

## Components

### Buttons & Inputs

| Component | Description |
|---|---|
| `RnButton` | Default, Secondary, Outline, Ghost, Destructive, Link 변형 지원 |
| `RnInput` | 텍스트 입력 필드 |
| `RnTextarea` | 여러 줄 텍스트 입력 |
| `RnLabel` | 폼 레이블 |
| `RnCheckbox` | 체크박스 |
| `RnSwitch` | 토글 스위치 |
| `RnRadioGroup` | 라디오 버튼 그룹 |
| `RnSelect` | 드롭다운 선택 |
| `RnToggle` | 토글 버튼 |
| `RnToggleGroup` | 토글 버튼 그룹 |
| `RnCombobox` | 검색 가능한 드롭다운 선택 |
| `RnInputOTP` | OTP 입력 필드 |
| `RnField` | 필드 컨테이너 (Label, Description, Error 통합) |
| `RnForm` | 폼 유효성 검사 |

### Cards & Containers

| Component | Description |
|---|---|
| `RnCard` | Header, Title, Description, Content, Footer, Action 구성 |
| `RnAlert` | 알림 메시지 (Default, Destructive) |
| `RnBadge` | 상태 뱃지 |
| `RnAspectRatio` | 종횡비 컨테이너 |
| `RnScrollArea` | 커스텀 스크롤 영역 |
| `RnSeparator` | 구분선 |
| `RnResizable` | 크기 조절 가능 패널 |

### Data Display

| Component | Description |
|---|---|
| `RnTable` | 기본 테이블 (Head, Body, Row, Header, Cell) |
| `RnDataTable` | 고급 데이터 테이블 (정렬, 필터, 페이지네이션, 선택, 행 확장) |
| `RnAvatar` | 사용자 아바타 (그룹 지원) |
| `RnProgress` | 진행률 표시 |
| `RnSlider` | 슬라이더 |
| `RnSkeleton` | 로딩 스켈레톤 |
| `RnSpinner` | 로딩 스피너 |
| `RnKbd` | 키보드 단축키 표시 |
| `RnCalendar` | 달력 |

### Navigation

| Component | Description |
|---|---|
| `RnTabs` | 탭 컴포넌트 (Default, Line 변형) |
| `RnBreadcrumb` | 경로 탐색 |
| `RnPagination` | 페이지네이션 |
| `RnNavigationMenu` | 내비게이션 메뉴 |
| `RnSidebar` | 사이드바 (Header, Content, Footer, Group, Menu) |
| `RnMenubar` | 메뉴바 |

### Overlays

| Component | Description |
|---|---|
| `RnDialog` | 모달 다이얼로그 |
| `RnAlertDialog` | 확인 다이얼로그 |
| `RnSheet` | 사이드 시트 |
| `RnPopover` | 팝오버 |
| `RnTooltip` | 툴팁 |
| `RnHoverCard` | 호버 카드 |
| `RnDropdownMenu` | 드롭다운 메뉴 |
| `RnContextMenu` | 컨텍스트 메뉴 |
| `RnDrawer` | 드로어 (모바일 하단 시트) |
| `RnToast` | 토스트 알림 |

### Disclosure & Misc

| Component | Description |
|---|---|
| `RnAccordion` | 아코디언 |
| `RnCollapsible` | 접기/펼치기 |
| `RnEmpty` | 빈 상태 표시 |
| `RnCarousel` | 캐러셀/슬라이더 |
| `RnCommand` | 커맨드 팔레트 |
| `RnDatePicker` | 날짜 선택기 |
| `RnGantt` | 간트 차트 |
| `RnButtonGroup` | 버튼 그룹 |

---

## Usage Examples

### Button

6가지 Variant와 8가지 Size를 지원합니다.

```razor
@* Variants *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* Sizes *@
<RnButton Size="ButtonSize.Xs">Extra Small</RnButton>
<RnButton Size="ButtonSize.Sm">Small</RnButton>
<RnButton Size="ButtonSize.Default">Default</RnButton>
<RnButton Size="ButtonSize.Lg">Large</RnButton>

@* Icon Buttons *@
<RnButton Size="ButtonSize.Icon">🔔</RnButton>
<RnButton Size="ButtonSize.IconSm">✏️</RnButton>

@* Disabled & Click Event *@
<RnButton Disabled="true">Disabled</RnButton>
<RnButton OnClick="HandleClick">Click Me</RnButton>
<RnButton Type="submit">Submit Form</RnButton>
```

### Input & Textarea

```razor
@* 기본 텍스트 입력 *@
<RnInput Placeholder="Enter your name" @bind-Value="_name" />

@* 타입별 입력 *@
<RnInput Type="email" Placeholder="you@example.com" @bind-Value="_email" />
<RnInput Type="password" Placeholder="Password" @bind-Value="_password" />
<RnInput Type="number" Placeholder="0" @bind-Value="_number" />

@* Disabled *@
<RnInput Disabled="true" Value="Read only value" />

@* Textarea *@
<RnTextarea Placeholder="Write your message..." @bind-Value="_message" Rows="5" />
<RnTextarea Disabled="true" Value="Read only textarea" />
```

### Label

```razor
<div class="space-y-2">
    <RnLabel For="email">Email Address</RnLabel>
    <RnInput id="email" Type="email" Placeholder="you@example.com" @bind-Value="_email" />
</div>
```

### Checkbox & Switch

```razor
@* Checkbox *@
<div class="flex items-center space-x-2">
    <RnCheckbox @bind-Checked="_accepted" />
    <RnLabel>Accept terms and conditions</RnLabel>
</div>

<div class="flex items-center space-x-2">
    <RnCheckbox @bind-Checked="_marketing" Disabled="true" />
    <RnLabel>Disabled checkbox</RnLabel>
</div>

@* Switch *@
<div class="flex items-center space-x-2">
    <RnSwitch @bind-Checked="_darkMode" />
    <RnLabel>Dark Mode</RnLabel>
</div>

<div class="flex items-center space-x-2">
    <RnSwitch @bind-Checked="_airplane" Size="ComponentSize.Sm" />
    <RnLabel>Airplane Mode (Small)</RnLabel>
</div>
```

### RadioGroup

```razor
<RnRadioGroup @bind-Value="_plan">
    <div class="flex items-center space-x-2">
        <RnRadioGroupItem Value="free" />
        <RnLabel>Free</RnLabel>
    </div>
    <div class="flex items-center space-x-2">
        <RnRadioGroupItem Value="pro" />
        <RnLabel>Pro</RnLabel>
    </div>
    <div class="flex items-center space-x-2">
        <RnRadioGroupItem Value="enterprise" Disabled="true" />
        <RnLabel>Enterprise (Coming soon)</RnLabel>
    </div>
</RnRadioGroup>
```

### Select

```razor
<RnSelect @bind-Value="_fruit" Placeholder="Select a fruit">
    <RnSelectItem Value="apple">Apple</RnSelectItem>
    <RnSelectItem Value="banana">Banana</RnSelectItem>
    <RnSelectItem Value="cherry">Cherry</RnSelectItem>
    <RnSelectItem Value="grape" Disabled="true">Grape (Sold out)</RnSelectItem>
</RnSelect>

@* Small size *@
<RnSelect @bind-Value="_size" Placeholder="Size" Size="ComponentSize.Sm">
    <RnSelectItem Value="sm">Small</RnSelectItem>
    <RnSelectItem Value="md">Medium</RnSelectItem>
    <RnSelectItem Value="lg">Large</RnSelectItem>
</RnSelect>
```

### Toggle

```razor
<RnToggle @bind-Pressed="_bold">
    <strong>B</strong>
</RnToggle>

<RnToggle @bind-Pressed="_italic" Variant="ToggleVariant.Outline">
    <em>I</em>
</RnToggle>

<RnToggle @bind-Pressed="_small" Size="ToggleSize.Sm">Sm</RnToggle>
<RnToggle @bind-Pressed="_large" Size="ToggleSize.Lg">Lg</RnToggle>
```

### Card

Header, Title, Description, Content, Footer, Action으로 구성합니다.

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

@* Small size card *@
<RnCard Size="ComponentSize.Sm">
    <RnCardHeader>
        <RnCardTitle>Compact Card</RnCardTitle>
    </RnCardHeader>
    <RnCardContent>
        <p>Smaller padding for compact layouts.</p>
    </RnCardContent>
</RnCard>

@* Card with action button *@
<RnCard>
    <RnCardHeader>
        <RnCardTitle>Notifications</RnCardTitle>
        <RnCardAction>
            <RnButton Size="ButtonSize.IconSm" Variant="ButtonVariant.Ghost">⚙️</RnButton>
        </RnCardAction>
    </RnCardHeader>
    <RnCardContent>
        <p>You have 3 unread messages.</p>
    </RnCardContent>
</RnCard>
```

### Alert

```razor
@* Default *@
<RnAlert>
    <RnAlertTitle>Heads up!</RnAlertTitle>
    <RnAlertDescription>You can add components to your app using the CLI.</RnAlertDescription>
</RnAlert>

@* Destructive *@
<RnAlert Variant="AlertVariant.Destructive">
    <RnAlertTitle>Error</RnAlertTitle>
    <RnAlertDescription>Your session has expired. Please log in again.</RnAlertDescription>
</RnAlert>
```

### Badge

```razor
<RnBadge>Default</RnBadge>
<RnBadge Variant="BadgeVariant.Secondary">Secondary</RnBadge>
<RnBadge Variant="BadgeVariant.Outline">Outline</RnBadge>
<RnBadge Variant="BadgeVariant.Destructive">Destructive</RnBadge>
<RnBadge Variant="BadgeVariant.Ghost">Ghost</RnBadge>
```

### Avatar

```razor
@* Image avatar *@
<RnAvatar Src="/images/profile.jpg" Alt="User Name" />

@* Fallback (이미지 없을 때 이니셜 표시) *@
<RnAvatar>
    <Fallback>JD</Fallback>
</RnAvatar>

@* Size variants *@
<RnAvatar Src="/images/user.jpg" Size="ComponentSize.Sm" />
<RnAvatar Src="/images/user.jpg" Size="ComponentSize.Default" />

@* Avatar Group *@
<RnAvatarGroup>
    <RnAvatar Src="/images/user1.jpg" />
    <RnAvatar Src="/images/user2.jpg" />
    <RnAvatar><Fallback>+3</Fallback></RnAvatar>
</RnAvatarGroup>
```

### Progress & Slider

```razor
@* Progress *@
<RnProgress Value="60" />
<RnProgress Value="@_progress">
    <span>@_progress%</span>
</RnProgress>

@* Slider *@
<RnSlider @bind-Value="_volume" Min="0" Max="100" Step="1" />
<RnSlider @bind-Value="_opacity" Min="0" Max="1" Step="0.1" Disabled="true" />
```

### Skeleton & Spinner

```razor
@* Skeleton - 로딩 플레이스홀더 *@
<RnSkeleton Class="h-4 w-[250px]" />
<RnSkeleton Class="h-4 w-[200px]" />
<RnSkeleton Class="h-12 w-12 rounded-full" />

@* Spinner *@
<RnSpinner />
<RnSpinner Size="ComponentSize.Sm" />
<RnSpinner Size="ComponentSize.Lg" />
```

### DataTable

정렬, 필터, 페이지네이션, 행 선택, 행 확장을 지원하는 고급 데이터 테이블입니다.

```razor
@using RnUI.Components.UI.DataTable

<RnDataTable TItem="PaymentRecord"
             Items="_payments"
             SelectionMode="SelectionMode.Multiple"
             PageSize="10"
             PageSizeOptions="@(new[] { 5, 10, 20, 50 })">
    <Toolbar>
        <div class="flex items-center justify-between w-full">
            <RnDataTableSearch TItem="PaymentRecord" Placeholder="Search..." />
            <RnDataTableColumnToggle TItem="PaymentRecord" />
        </div>
    </Toolbar>
    <Columns>
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Id" Title="ID" Sortable />
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Name" Title="Name" Sortable Filterable />
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Amount" Title="Amount" Sortable>
            <CellTemplate Context="item">
                <div class="text-right font-medium">@item.Amount.ToString("C")</div>
            </CellTemplate>
        </RnDataTableColumn>
        <RnDataTableColumn TItem="PaymentRecord" Title="" Id="actions">
            <CellTemplate Context="item">
                <RnDataTableRowActions>
                    <RnDropdownMenuItem>View</RnDropdownMenuItem>
                    <RnDropdownMenuItem Class="text-destructive">Delete</RnDropdownMenuItem>
                </RnDataTableRowActions>
            </CellTemplate>
        </RnDataTableColumn>
    </Columns>
    <EmptyContent>
        <p class="py-10 text-center text-muted-foreground">No results.</p>
    </EmptyContent>
</RnDataTable>

@code {
    public record PaymentRecord(string Id, string Name, decimal Amount);

    // Important: use non-readonly list and immutable updates for add/remove
    private List<PaymentRecord> _payments = [ /* your data */ ];
}
```

### Table

```razor
<RnTable>
    <RnTableHeader>
        <RnTableRow>
            <RnTableHead>Name</RnTableHead>
            <RnTableHead>Email</RnTableHead>
            <RnTableHead>Role</RnTableHead>
        </RnTableRow>
    </RnTableHeader>
    <RnTableBody>
        <RnTableRow>
            <RnTableCell>John Doe</RnTableCell>
            <RnTableCell>john@example.com</RnTableCell>
            <RnTableCell>Admin</RnTableCell>
        </RnTableRow>
        <RnTableRow>
            <RnTableCell>Jane Smith</RnTableCell>
            <RnTableCell>jane@example.com</RnTableCell>
            <RnTableCell>User</RnTableCell>
        </RnTableRow>
    </RnTableBody>
</RnTable>
```

### Tabs

Default와 Line 변형, 가로/세로 방향을 지원합니다.

```razor
@* Default tabs *@
<RnTabs @bind-ActiveTab="_activeTab">
    <RnTabsList>
        <RnTabsTrigger Value="account">Account</RnTabsTrigger>
        <RnTabsTrigger Value="password">Password</RnTabsTrigger>
        <RnTabsTrigger Value="settings">Settings</RnTabsTrigger>
    </RnTabsList>
    <RnTabsContent Value="account">
        <p>Account settings here.</p>
    </RnTabsContent>
    <RnTabsContent Value="password">
        <p>Password settings here.</p>
    </RnTabsContent>
    <RnTabsContent Value="settings">
        <p>General settings here.</p>
    </RnTabsContent>
</RnTabs>

@* Line variant *@
<RnTabs @bind-ActiveTab="_tab2">
    <RnTabsList Variant="TabsListVariant.Line">
        <RnTabsTrigger Value="overview">Overview</RnTabsTrigger>
        <RnTabsTrigger Value="analytics">Analytics</RnTabsTrigger>
    </RnTabsList>
    <RnTabsContent Value="overview">Overview content</RnTabsContent>
    <RnTabsContent Value="analytics">Analytics content</RnTabsContent>
</RnTabs>

@* Vertical orientation *@
<RnTabs @bind-ActiveTab="_tab3" Orientation="Orientation.Vertical">
    <RnTabsList>
        <RnTabsTrigger Value="general">General</RnTabsTrigger>
        <RnTabsTrigger Value="display">Display</RnTabsTrigger>
    </RnTabsList>
    <RnTabsContent Value="general">General settings</RnTabsContent>
    <RnTabsContent Value="display">Display settings</RnTabsContent>
</RnTabs>
```

### Breadcrumb

```razor
<RnBreadcrumb>
    <RnBreadcrumbItem>
        <RnBreadcrumbLink Href="/">Home</RnBreadcrumbLink>
    </RnBreadcrumbItem>
    <RnBreadcrumbSeparator />
    <RnBreadcrumbItem>
        <RnBreadcrumbLink Href="/components">Components</RnBreadcrumbLink>
    </RnBreadcrumbItem>
    <RnBreadcrumbSeparator />
    <RnBreadcrumbItem>
        <RnBreadcrumbPage>Breadcrumb</RnBreadcrumbPage>
    </RnBreadcrumbItem>
</RnBreadcrumb>
```

### Pagination

```razor
<RnPagination @bind-CurrentPage="_page" TotalPages="10" />
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

@* Close 버튼 숨기기 *@
<RnDialog @bind-Open="_dialog2" ShowCloseButton="false">
    <RnDialogHeader>
        <RnDialogTitle>Custom Dialog</RnDialogTitle>
    </RnDialogHeader>
    <p>This dialog has no close button.</p>
</RnDialog>
```

### AlertDialog

사용자 확인이 필요한 중요한 액션에 사용합니다. 오버레이 클릭으로 닫히지 않습니다.

```razor
<RnButton Variant="ButtonVariant.Destructive" OnClick="() => _alertOpen = true">
    Delete Account
</RnButton>

<RnAlertDialog @bind-Open="_alertOpen">
    <RnAlertDialogHeader>
        <RnAlertDialogTitle>Are you absolutely sure?</RnAlertDialogTitle>
        <RnAlertDialogDescription>
            This action cannot be undone. This will permanently delete your account.
        </RnAlertDialogDescription>
    </RnAlertDialogHeader>
    <RnAlertDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _alertOpen = false">Cancel</RnButton>
        <RnButton Variant="ButtonVariant.Destructive" OnClick="DeleteAccount">Delete</RnButton>
    </RnAlertDialogFooter>
</RnAlertDialog>
```

### Sheet

화면 가장자리에서 슬라이드되는 패널입니다. Top, Bottom, Left, Right 방향을 지원합니다.

```razor
<RnButton OnClick="() => _sheetOpen = true">Open Sheet</RnButton>

<RnSheet @bind-Open="_sheetOpen" Side="Side.Right">
    <RnSheetHeader>
        <RnSheetTitle>Edit Profile</RnSheetTitle>
        <RnSheetDescription>Make changes to your profile.</RnSheetDescription>
    </RnSheetHeader>
    <div class="py-4">
        <RnInput Placeholder="Name" @bind-Value="_name" />
    </div>
    <RnSheetFooter>
        <RnButton OnClick="() => _sheetOpen = false">Save</RnButton>
    </RnSheetFooter>
</RnSheet>

@* Left side sheet *@
<RnSheet @bind-Open="_menuOpen" Side="Side.Left" ShowCloseButton="false">
    <RnSheetHeader>
        <RnSheetTitle>Navigation</RnSheetTitle>
    </RnSheetHeader>
    <nav>Menu items here</nav>
</RnSheet>
```

### Popover

```razor
<RnPopover Side="Side.Bottom" Align="Alignment.Start">
    <Trigger>
        <RnButton Variant="ButtonVariant.Outline">Open Popover</RnButton>
    </Trigger>
    <ChildContent>
        <div class="space-y-2 p-4">
            <h4 class="font-medium">Dimensions</h4>
            <RnInput Placeholder="Width" @bind-Value="_width" />
            <RnInput Placeholder="Height" @bind-Value="_height" />
        </div>
    </ChildContent>
</RnPopover>
```

### Tooltip

```razor
<RnTooltip Side="Side.Top">
    <Trigger>
        <RnButton Variant="ButtonVariant.Outline">Hover me</RnButton>
    </Trigger>
    <ChildContent>
        <p>This is the tooltip content.</p>
    </ChildContent>
</RnTooltip>

@* 방향별 *@
<RnTooltip Side="Side.Bottom">
    <Trigger><RnButton>Bottom</RnButton></Trigger>
    <ChildContent><p>Bottom tooltip</p></ChildContent>
</RnTooltip>

<RnTooltip Side="Side.Left">
    <Trigger><RnButton>Left</RnButton></Trigger>
    <ChildContent><p>Left tooltip</p></ChildContent>
</RnTooltip>
```

### HoverCard

```razor
<RnHoverCard Side="Side.Bottom" Align="Alignment.Start">
    <Trigger>
        <a href="#" class="underline">@shadcn</a>
    </Trigger>
    <ChildContent>
        <div class="flex gap-4">
            <RnAvatar Src="/images/user.jpg" />
            <div>
                <h4 class="font-semibold">shadcn</h4>
                <p class="text-sm text-muted-foreground">
                    Creator of shadcn/ui and taxonomy.
                </p>
            </div>
        </div>
    </ChildContent>
</RnHoverCard>
```

### DropdownMenu

```razor
<RnDropdownMenu Side="Side.Bottom" Align="Alignment.End">
    <Trigger>
        <RnButton Variant="ButtonVariant.Outline">Open Menu</RnButton>
    </Trigger>
    <ChildContent>
        <RnDropdownMenuLabel>My Account</RnDropdownMenuLabel>
        <RnDropdownMenuSeparator />
        <RnDropdownMenuItem OnClick="GoToProfile">Profile</RnDropdownMenuItem>
        <RnDropdownMenuItem OnClick="GoToSettings">Settings</RnDropdownMenuItem>
        <RnDropdownMenuSeparator />
        <RnDropdownMenuItem Variant="destructive" OnClick="Logout">Log out</RnDropdownMenuItem>
    </ChildContent>
</RnDropdownMenu>
```

### ContextMenu

우클릭(컨텍스트 메뉴)으로 열리는 메뉴입니다.

```razor
<RnContextMenu>
    <Trigger>
        <div class="flex h-32 w-full items-center justify-center rounded-md border border-dashed">
            Right click here
        </div>
    </Trigger>
    <ChildContent>
        <RnDropdownMenuItem OnClick="Cut">Cut</RnDropdownMenuItem>
        <RnDropdownMenuItem OnClick="Copy">Copy</RnDropdownMenuItem>
        <RnDropdownMenuItem OnClick="Paste">Paste</RnDropdownMenuItem>
    </ChildContent>
</RnContextMenu>
```

### Accordion

단일 선택 / 다중 선택 모드를 지원합니다.

```razor
@* 단일 선택 (기본) *@
<RnAccordion @bind-ExpandedItem="_expandedItem">
    <RnAccordionItem Value="item-1">
        <RnAccordionTrigger>Is it accessible?</RnAccordionTrigger>
        <RnAccordionContent>
            Yes. It adheres to the WAI-ARIA design pattern.
        </RnAccordionContent>
    </RnAccordionItem>
    <RnAccordionItem Value="item-2">
        <RnAccordionTrigger>Is it styled?</RnAccordionTrigger>
        <RnAccordionContent>
            Yes. It comes with default styles using Tailwind CSS.
        </RnAccordionContent>
    </RnAccordionItem>
    <RnAccordionItem Value="item-3">
        <RnAccordionTrigger>Is it animated?</RnAccordionTrigger>
        <RnAccordionContent>
            Yes. It uses CSS transitions for smooth open/close animations.
        </RnAccordionContent>
    </RnAccordionItem>
</RnAccordion>

@* 다중 선택 *@
<RnAccordion Multiple="true">
    <RnAccordionItem Value="a">
        <RnAccordionTrigger>Section A</RnAccordionTrigger>
        <RnAccordionContent>Content A</RnAccordionContent>
    </RnAccordionItem>
    <RnAccordionItem Value="b">
        <RnAccordionTrigger>Section B</RnAccordionTrigger>
        <RnAccordionContent>Content B</RnAccordionContent>
    </RnAccordionItem>
</RnAccordion>
```

### Collapsible

```razor
<RnCollapsible @bind-Open="_isOpen">
    <Trigger>
        <RnButton Variant="ButtonVariant.Ghost">
            Toggle Content @(_isOpen ? "▲" : "▼")
        </RnButton>
    </Trigger>
    <ChildContent>
        <div class="rounded-md border p-4">
            This content can be toggled on and off.
        </div>
    </ChildContent>
</RnCollapsible>
```

### Separator

```razor
@* Horizontal (기본) *@
<RnSeparator />

@* Vertical *@
<div class="flex h-5 items-center space-x-4">
    <span>Blog</span>
    <RnSeparator Orientation="Orientation.Vertical" />
    <span>Docs</span>
    <RnSeparator Orientation="Orientation.Vertical" />
    <span>Source</span>
</div>
```

### AspectRatio

```razor
@* 16:9 (기본) *@
<RnAspectRatio>
    <img src="/images/photo.jpg" alt="Photo" class="object-cover w-full h-full rounded-md" />
</RnAspectRatio>

@* 4:3 *@
<RnAspectRatio Ratio="4.0/3.0">
    <img src="/images/photo.jpg" alt="Photo" class="object-cover w-full h-full" />
</RnAspectRatio>

@* 1:1 (정사각형) *@
<RnAspectRatio Ratio="1">
    <img src="/images/photo.jpg" alt="Photo" class="object-cover w-full h-full rounded-full" />
</RnAspectRatio>
```

### ScrollArea

```razor
<RnScrollArea Class="h-[200px] rounded-md border">
    <div class="p-4">
        @for (int i = 1; i <= 50; i++)
        {
            <p>Item @i</p>
        }
    </div>
</RnScrollArea>
```

### Kbd

```razor
<p>Press <RnKbd>Ctrl</RnKbd> + <RnKbd>S</RnKbd> to save</p>
<p>Press <RnKbd>⌘</RnKbd> + <RnKbd>K</RnKbd> to search</p>
```

### Empty

```razor
<RnEmpty>
    <p class="text-lg font-medium">No results found</p>
    <p class="text-sm text-muted-foreground">Try adjusting your search or filters.</p>
    <RnButton Class="mt-4" Variant="ButtonVariant.Outline">Clear Filters</RnButton>
</RnEmpty>
```

---

## Customization

RnUI는 CSS 커스텀 프로퍼티를 사용하여 테마를 구성합니다. 아래 변수를 오버라이드하여 색상을 커스터마이즈할 수 있습니다:

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

데모 앱이 `src/Daeha.RnUI.Demo`에 포함되어 있습니다:

```bash
cd src/Daeha.RnUI.Demo
dotnet run
```

브라우저에서 `https://localhost:7100`을 열어 모든 컴포넌트를 확인할 수 있습니다.

---

## Contributing

기여를 환영합니다! 이슈나 풀 리퀘스트를 자유롭게 제출해 주세요.

1. 이 저장소를 Fork합니다
2. 피처 브랜치를 생성합니다 (`git checkout -b feature/amazing-feature`)
3. 변경 사항을 커밋합니다 (`git commit -m 'feat: Add amazing feature'`)
4. 브랜치에 Push합니다 (`git push origin feature/amazing-feature`)
5. Pull Request를 생성합니다

---

## License

[MIT](LICENSE.md) License

---

<div align="center">

Made with ❤️ for the Blazor community

</div>
