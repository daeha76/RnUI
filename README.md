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

- **37+ component categories** — 97개의 Razor 컴포넌트 파일로 구성된 포괄적인 UI 라이브러리
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

### Cards & Containers

| Component | Description |
|---|---|
| `RnCard` | Header, Title, Description, Content, Footer, Action 구성 |
| `RnAlert` | 알림 메시지 (Default, Destructive) |
| `RnBadge` | 상태 뱃지 |
| `RnAspectRatio` | 종횡비 컨테이너 |
| `RnScrollArea` | 커스텀 스크롤 영역 |
| `RnSeparator` | 구분선 |

### Data Display

| Component | Description |
|---|---|
| `RnTable` | 데이터 테이블 (Head, Body, Row, Header, Cell) |
| `RnAvatar` | 사용자 아바타 (그룹 지원) |
| `RnProgress` | 진행률 표시 |
| `RnSlider` | 슬라이더 |
| `RnSkeleton` | 로딩 스켈레톤 |
| `RnSpinner` | 로딩 스피너 |
| `RnKbd` | 키보드 단축키 표시 |

### Navigation

| Component | Description |
|---|---|
| `RnTabs` | 탭 컴포넌트 (Default, Line 변형) |
| `RnBreadcrumb` | 경로 탐색 |
| `RnPagination` | 페이지네이션 |
| `RnNavigationMenu` | 내비게이션 메뉴 |
| `RnSidebar` | 사이드바 (Header, Content, Footer, Group, Menu) |

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

### Disclosure & Misc

| Component | Description |
|---|---|
| `RnAccordion` | 아코디언 |
| `RnCollapsible` | 접기/펼치기 |
| `RnEmpty` | 빈 상태 표시 |

---

## Usage Examples

### Button

```razor
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Size="ButtonSize.Sm">Small</RnButton>
<RnButton Size="ButtonSize.Lg">Large</RnButton>
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
        <RnButton>Action</RnButton>
    </RnCardFooter>
</RnCard>
```

### Form Controls

```razor
<div class="space-y-2">
    <RnLabel For="email">Email</RnLabel>
    <RnInput Type="email" Placeholder="Enter your email" @bind-Value="_email" />
</div>

<div class="flex items-center space-x-2">
    <RnCheckbox @bind-Checked="_checked" />
    <RnLabel>Accept terms and conditions</RnLabel>
</div>

<div class="flex items-center space-x-2">
    <RnSwitch @bind-Checked="_switched" />
    <RnLabel>Enable notifications</RnLabel>
</div>
```

### Dialog

```razor
<RnButton OnClick="() => _dialogOpen = true">Open Dialog</RnButton>
<RnDialog @bind-Open="_dialogOpen">
    <RnDialogHeader>
        <RnDialogTitle>Edit Profile</RnDialogTitle>
        <RnDialogDescription>Make changes to your profile here.</RnDialogDescription>
    </RnDialogHeader>
    <p>Dialog content goes here.</p>
    <RnDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">Cancel</RnButton>
        <RnButton OnClick="() => _dialogOpen = false">Save</RnButton>
    </RnDialogFooter>
</RnDialog>
```

### Tabs

```razor
<RnTabs @bind-ActiveTab="_activeTab">
    <RnTabsList>
        <RnTabsTrigger Value="account">Account</RnTabsTrigger>
        <RnTabsTrigger Value="password">Password</RnTabsTrigger>
    </RnTabsList>
    <RnTabsContent Value="account">
        <p>Account settings here.</p>
    </RnTabsContent>
    <RnTabsContent Value="password">
        <p>Password settings here.</p>
    </RnTabsContent>
</RnTabs>
```

### Tooltip

```razor
<RnTooltip Side="Side.Top">
    <Trigger>
        <RnButton>Hover me</RnButton>
    </Trigger>
    <ChildContent>
        <p>This is the tooltip content.</p>
    </ChildContent>
</RnTooltip>
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
