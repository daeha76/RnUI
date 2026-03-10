---
trigger: always_on
---

# Layout Patterns (Sidebar/Dashboard Blocks)

## Sidebar Block 기본 구조

shadcn/ui의 Sidebar 블록은 `SidebarProvider` + `Sidebar` + `SidebarInset` 패턴.
RnUI에서는 기존 `RnSidebar*` 컴포넌트를 조합:

```razor
@* 기본 Sidebar + Content 레이아웃 *@
<div class="flex min-h-svh">
    @* Sidebar *@
    <RnSidebar>
        <RnSidebarHeader>
            @* 로고 / 앱 이름 *@
        </RnSidebarHeader>
        <RnSidebarContent>
            <RnSidebarGroup>
                <RnSidebarGroupLabel>Menu</RnSidebarGroupLabel>
                <RnSidebarMenu>
                    @foreach (var item in _menuItems)
                    {
                        <RnSidebarMenuItem>
                            <RnSidebarMenuButton>
                                <RnIcon Icon="@item.Icon" />
                                <span>@item.Label</span>
                            </RnSidebarMenuButton>
                        </RnSidebarMenuItem>
                    }
                </RnSidebarMenu>
            </RnSidebarGroup>
        </RnSidebarContent>
        <RnSidebarFooter>
            @* 사용자 프로필 / 설정 *@
        </RnSidebarFooter>
    </RnSidebar>

    @* Main Content (Inset) *@
    <RnSidebarInset>
        <header class="flex h-16 items-center gap-2 border-b px-4">
            @* SidebarTrigger + Breadcrumb *@
        </header>
        <main class="flex-1 p-4">
            @* 페이지 콘텐츠 *@
        </main>
    </RnSidebarInset>
</div>
```

---

## Sidebar 변형 패턴

### Inset (sidebar-01)

사이드바가 메인 콘텐츠 안쪽에 위치:

```razor
<RnSidebar Variant="SidebarVariant.Inset">
```

### Floating (sidebar-02)

사이드바가 떠있는 형태:

```razor
<RnSidebar Variant="SidebarVariant.Floating">
```

### Collapsible (sidebar-05)

그룹이 접히는 사이드바:

```razor
<RnSidebar Collapsible="SidebarCollapsible.Icon">
    @* 접힐 때 아이콘만 표시 *@
</RnSidebar>
```

---

## Header 패턴

### Site Header (Breadcrumb + Trigger)

```razor
<header class="flex h-16 shrink-0 items-center gap-2 border-b px-4">
    @* 사이드바 토글 *@
    <button @onclick="ToggleSidebar" class="...">
        <RnIcon Icon="RnIcons.PanelLeft" />
    </button>

    <RnSeparator Orientation="Orientation.Vertical" Class="mr-2 h-4" />

    @* Breadcrumb *@
    <RnBreadcrumb>
        <RnBreadcrumbItem>
            <RnBreadcrumbLink Href="/">Home</RnBreadcrumbLink>
        </RnBreadcrumbItem>
        <RnBreadcrumbSeparator />
        <RnBreadcrumbItem>
            <RnBreadcrumbPage>Dashboard</RnBreadcrumbPage>
        </RnBreadcrumbItem>
    </RnBreadcrumb>
</header>
```

---

## Dashboard 섹션 패턴

### Stats Cards (KPI)

```razor
<div class="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
    @foreach (var stat in _stats)
    {
        <RnCard>
            <RnCardHeader>
                <RnCardDescription>@stat.Label</RnCardDescription>
                <RnCardTitle>@stat.Value</RnCardTitle>
            </RnCardHeader>
            <RnCardContent>
                <p class="text-xs text-muted-foreground">@stat.Change</p>
            </RnCardContent>
        </RnCard>
    }
</div>
```

### Content Grid

```razor
@* 2열 레이아웃: 큰 차트 + 작은 카드 *@
<div class="grid gap-4 md:grid-cols-2 lg:grid-cols-7">
    <div class="lg:col-span-4">
        @* 메인 차트/테이블 *@
    </div>
    <div class="lg:col-span-3">
        @* 사이드 카드들 *@
    </div>
</div>
```

---

## 반응형 그리드 기준

| 화면 | Tailwind | 레이아웃 |
|------|----------|---------|
| 모바일 (<768px) | 기본 | 1열, 사이드바 숨김 |
| 태블릿 (md) | `md:` | 2열 |
| 데스크톱 (lg) | `lg:` | 3~4열, 사이드바 표시 |

```razor
@* 반응형 그리드 예시 *@
<div class="grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
```

---

## 블록 데모용 목(Mock) 데이터

블록에서 사용할 데모 데이터는 블록 컴포넌트 내부에 정의:

```csharp
@code {
    private record MenuItem(string Label, string Icon, string Href);
    private record StatItem(string Label, string Value, string Change);

    private readonly MenuItem[] _menuItems =
    [
        new("Dashboard", "RnIcons.LayoutDashboard", "/"),
        new("Orders", "RnIcons.ShoppingCart", "/orders"),
        new("Products", "RnIcons.Package", "/products"),
        new("Customers", "RnIcons.Users", "/customers"),
        new("Settings", "RnIcons.Settings", "/settings"),
    ];

    private readonly StatItem[] _stats =
    [
        new("Total Revenue", "$45,231.89", "+20.1% from last month"),
        new("Subscriptions", "+2,350", "+180.1% from last month"),
        new("Sales", "+12,234", "+19% from last month"),
        new("Active Now", "+573", "+201 since last hour"),
    ];
}
```

---

## Sticky Header + Scrollable Content

```razor
<div class="flex h-svh flex-col">
    @* 고정 헤더 *@
    <header class="sticky top-0 z-10 flex h-16 items-center border-b bg-background px-4">
        ...
    </header>

    @* 스크롤 가능한 콘텐츠 *@
    <main class="flex-1 overflow-y-auto p-4">
        ...
    </main>
</div>
```

`h-svh` — viewport height 전체 사용.
`sticky top-0` — 스크롤해도 헤더 고정.
`overflow-y-auto` — 콘텐츠만 스크롤.
