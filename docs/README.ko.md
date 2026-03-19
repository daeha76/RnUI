# RnUI

**Documentation**: [English](../README.md) | **한국어** | [中文](README.zh-CN.md) | [Español](README.es.md) | [Deutsch](README.de.md) | [日本語](README.ja.md)

---

**[shadcn/ui](https://ui.shadcn.com)의 Blazor 포팅**

.NET Blazor 애플리케이션을 위해 Tailwind CSS로 만들어진, 아름답고 접근성 높은 UI 컴포넌트 라이브러리입니다.

[**라이브 데모**](https://daeha76.github.io/RnUI/)

---

## 왜 RnUI인가?

- **55개 컴포넌트 카테고리** — 195개 Razor 컴포넌트 파일로 구성된 종합 UI 라이브러리
- **shadcn/ui 기반** — 웹에서 검증된 디자인 시스템을 Blazor로 직접 포팅
- **Tailwind CSS** — oklch 색공간 기반의 CSS 커스텀 속성 테마 시스템
- **다크 모드** — 라이트/다크 모드 기본 지원
- **접근성** — 접근성을 고려한 컴포넌트 설계
- **외부 의존성 없음** — ASP.NET Core 프레임워크 참조만 사용
- **멀티 타겟** — .NET 8.0, 9.0, 10.0 지원 | Blazor Server 및 WebAssembly 호환

---

## 빠른 시작

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

RnUI는 Tailwind CSS v4를 사용합니다. Blazor 프로젝트에서 유틸리티 클래스를 스캔하고 컴파일하려면 Tailwind CSS 설정이 필요합니다.

#### 4-1. Tailwind CSS 설치

프로젝트 루트에 Tailwind CSS를 설치합니다:

```bash
npm init -y
npm install -D tailwindcss @tailwindcss/cli
```

#### 4-2. CSS 엔트리 파일 생성

`wwwroot/input.css` 파일을 생성하고 Razor 파일을 스캔 대상에 포함합니다:

```css
@import "tailwindcss";
@source "../**/*.razor";
```

> **NuGet 패키지 사용 시**: NuGet으로 설치한 경우 라이브러리의 `.razor` 파일은 로컬에 존재하지 않습니다. 그러나 RnUI 컴포넌트 기본 스타일(`.cn-*` 클래스)은 이미 `shadcn.css`에 포함되어 있으므로 추가 `@source` 설정 없이도 동작합니다. 프로젝트 내 `.razor` 파일에서 사용하는 Tailwind 유틸리티 클래스만 스캔하면 됩니다.

#### 4-3. 빌드 스크립트 추가

`package.json`에 빌드 스크립트를 추가합니다:

```json
{
  "scripts": {
    "build:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css",
    "watch:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css --watch"
  }
}
```

#### 4-4. 스타일시트 연결

`App.razor` 또는 `_Host.cshtml`에 RnUI 스타일과 Tailwind CSS 출력 파일을 모두 연결합니다:

```html
<!-- RnUI 컴포넌트 스타일 -->
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
<!-- Tailwind 유틸리티 클래스 -->
<link rel="stylesheet" href="css/tailwindcss.css" />
```

#### 4-5. CSS 빌드

개발 중에는 watch 모드를, 프로덕션에서는 빌드 명령을 사용합니다:

```bash
npm run watch:css   # 개발 (파일 변경 감시)
npm run build:css   # 프로덕션 (일회성 빌드)
```

---

## 컴포넌트

> 각 컴포넌트의 라이브 데모와 상세한 사용법은 [**데모 사이트**](https://daeha76.github.io/RnUI/components)에서 확인할 수 있습니다.

### 버튼 및 입력

| 컴포넌트 | 설명 | 데모 |
|---|---|---|
| `RnButton` | Default, Secondary, Outline, Ghost, Destructive, Link 변형 지원 | [Live](https://daeha76.github.io/RnUI/components/button) |
| `RnInput` | 텍스트 입력 필드 | [Live](https://daeha76.github.io/RnUI/components/input) |
| `RnTextarea` | 여러 줄 텍스트 입력 | [Live](https://daeha76.github.io/RnUI/components/textarea) |
| `RnLabel` | 폼 라벨 | [Live](https://daeha76.github.io/RnUI/components/label) |
| `RnCheckbox` | 체크박스 | [Live](https://daeha76.github.io/RnUI/components/checkbox) |
| `RnSwitch` | 토글 스위치 | [Live](https://daeha76.github.io/RnUI/components/switch) |
| `RnRadioGroup` | 라디오 버튼 그룹 | [Live](https://daeha76.github.io/RnUI/components/radio-group) |
| `RnSelect` | 드롭다운 선택 | [Live](https://daeha76.github.io/RnUI/components/select) |
| `RnToggle` | 토글 버튼 | [Live](https://daeha76.github.io/RnUI/components/toggle) |
| `RnToggleGroup` | 토글 버튼 그룹 | [Live](https://daeha76.github.io/RnUI/components/toggle-group) |
| `RnCombobox` | 검색 가능한 드롭다운 선택 | [Live](https://daeha76.github.io/RnUI/components/combobox) |
| `RnInputOTP` | OTP 입력 필드 | [Live](https://daeha76.github.io/RnUI/components/input-otp) |
| `RnField` | 필드 컨테이너 (Label, Description, Error 통합) | [Live](https://daeha76.github.io/RnUI/components/field) |
| `RnTextField` | RnField + RnFieldLabel + RnInput 편의 래퍼 | [Live](https://daeha76.github.io/RnUI/components/text-field) |
| `RnForm` | 폼 유효성 검사 | [Live](https://daeha76.github.io/RnUI/components/form) |

### 카드 및 컨테이너

| 컴포넌트 | 설명 | 데모 |
|---|---|---|
| `RnCard` | Header, Title, Description, Content, Footer, Action으로 구성 | [Live](https://daeha76.github.io/RnUI/components/card) |
| `RnAlert` | 알림 메시지 (Default, Destructive) | [Live](https://daeha76.github.io/RnUI/components/alert) |
| `RnBadge` | 상태 배지 | [Live](https://daeha76.github.io/RnUI/components/badge) |
| `RnAspectRatio` | 종횡비 컨테이너 | [Live](https://daeha76.github.io/RnUI/components/aspect-ratio) |
| `RnScrollArea` | 커스텀 스크롤 영역 | [Live](https://daeha76.github.io/RnUI/components/scroll-area) |
| `RnSeparator` | 구분선 | [Live](https://daeha76.github.io/RnUI/components/separator) |
| `RnResizable` | 크기 조절 가능한 패널 | [Live](https://daeha76.github.io/RnUI/components/resizable) |

### 데이터 표시

| 컴포넌트 | 설명 | 데모 |
|---|---|---|
| `RnTable` | 기본 테이블 (Head, Body, Row, Header, Cell) | [Live](https://daeha76.github.io/RnUI/components/table) |
| `RnDataTable` | 고급 데이터 테이블 (정렬, 필터링, 페이지네이션, 선택, 행 확장) | [Live](https://daeha76.github.io/RnUI/components/data-table) |
| `RnAvatar` | 사용자 아바타 (그룹 지원) | [Live](https://daeha76.github.io/RnUI/components/avatar) |
| `RnProgress` | 진행률 표시줄 | [Live](https://daeha76.github.io/RnUI/components/progress) |
| `RnSlider` | 슬라이더 | [Live](https://daeha76.github.io/RnUI/components/slider) |
| `RnSkeleton` | 로딩 스켈레톤 | [Live](https://daeha76.github.io/RnUI/components/skeleton) |
| `RnSpinner` | 로딩 스피너 | [Live](https://daeha76.github.io/RnUI/components/spinner) |
| `RnKbd` | 키보드 단축키 표시 | [Live](https://daeha76.github.io/RnUI/components/kbd) |
| `RnCalendar` | 달력 | [Live](https://daeha76.github.io/RnUI/components/calendar) |

### 내비게이션

| 컴포넌트 | 설명 | 데모 |
|---|---|---|
| `RnTabs` | 탭 컴포넌트 (Default, Line 변형) | [Live](https://daeha76.github.io/RnUI/components/tabs) |
| `RnBreadcrumb` | 브레드크럼 내비게이션 | [Live](https://daeha76.github.io/RnUI/components/breadcrumb) |
| `RnPagination` | 페이지네이션 | [Live](https://daeha76.github.io/RnUI/components/pagination) |
| `RnNavigationMenu` | 내비게이션 메뉴 | [Live](https://daeha76.github.io/RnUI/components/navigation-menu) |
| `RnSidebar` | 사이드바 (Header, Content, Footer, Group, Menu) | [Live](https://daeha76.github.io/RnUI/components/sidebar) |
| `RnMainLayout01` | 3슬롯 레이아웃 (Header/Content/Footer), RnSidebarInset 내부에서 사용 | [Live](https://daeha76.github.io/RnUI/components/main-layout-01) |
| `RnMenubar` | 메뉴바 | [Live](https://daeha76.github.io/RnUI/components/menubar) |

### 오버레이

| 컴포넌트 | 설명 | 데모 |
|---|---|---|
| `RnDialog` | 모달 대화상자 | [Live](https://daeha76.github.io/RnUI/components/dialog) |
| `RnAlertDialog` | 확인 대화상자 | [Live](https://daeha76.github.io/RnUI/components/alert-dialog) |
| `RnSheet` | 사이드 시트 | [Live](https://daeha76.github.io/RnUI/components/sheet) |
| `RnPopover` | 팝오버 | [Live](https://daeha76.github.io/RnUI/components/popover) |
| `RnTooltip` | 툴팁 | [Live](https://daeha76.github.io/RnUI/components/tooltip) |
| `RnHoverCard` | 호버 카드 | [Live](https://daeha76.github.io/RnUI/components/hover-card) |
| `RnDropdownMenu` | 드롭다운 메뉴 | [Live](https://daeha76.github.io/RnUI/components/dropdown-menu) |
| `RnContextMenu` | 컨텍스트 메뉴 | [Live](https://daeha76.github.io/RnUI/components/context-menu) |
| `RnDrawer` | 드로어 (모바일 바텀 시트) | [Live](https://daeha76.github.io/RnUI/components/drawer) |
| `RnToast` | 토스트 알림 | [Live](https://daeha76.github.io/RnUI/components/toast) |

### 펼침 및 기타

| 컴포넌트 | 설명 | 데모 |
|---|---|---|
| `RnAccordion` | 아코디언 | [Live](https://daeha76.github.io/RnUI/components/accordion) |
| `RnCollapsible` | 접기/펼치기 | [Live](https://daeha76.github.io/RnUI/components/collapsible) |
| `RnEmpty` | 빈 상태 | [Live](https://daeha76.github.io/RnUI/components/empty) |
| `RnCarousel` | 캐러셀 / 슬라이더 | [Live](https://daeha76.github.io/RnUI/components/carousel) |
| `RnCommand` | 명령 팔레트 | [Live](https://daeha76.github.io/RnUI/components/command) |
| `RnDatePicker` | 날짜 선택기 | [Live](https://daeha76.github.io/RnUI/components/date-picker) |
| `RnGantt` | 간트 차트 | [Live](https://daeha76.github.io/RnUI/components/gantt) |
| `RnButtonGroup` | 버튼 그룹 | [Live](https://daeha76.github.io/RnUI/components/button-group) |

---

## 사용 예시

### Button

6가지 변형과 8가지 크기를 지원합니다.

```razor
@* 변형 *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* 크기 *@
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

### TextField

RnField + RnFieldLabel + RnInput를 하나의 컴포넌트로 결합한 편의 래퍼입니다.

```razor
<RnTextField Label="이름" @bind-Value="_name" Placeholder="이름을 입력하세요" />
<RnTextField Label="이메일" @bind-Value="_email" Type="email" Placeholder="이메일을 입력하세요" />
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

> 더 많은 예시는 [데모 사이트](https://daeha76.github.io/RnUI/)에서 확인하세요.

---

## 커스터마이징

RnUI는 테마 설정에 CSS 커스텀 속성을 사용합니다. 아래 변수를 오버라이드하여 색상을 커스터마이징할 수 있습니다:

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

## 데모

**온라인 데모**: [https://daeha76.github.io/RnUI/](https://daeha76.github.io/RnUI/)

로컬에서 데모를 실행하려면 `Daeha.RnUI.Demo.Wasm` 프로젝트를 사용합니다:

```bash
# 데모 CSS 빌드
cd src/Daeha.RnUI.Demo.Wasm && npm install && npm run build:css

# 데모 실행
dotnet run --project src/Daeha.RnUI.Demo.Wasm
```

브라우저에서 `https://localhost:7256`을 열어 모든 컴포넌트를 확인할 수 있습니다.

---

## 기여하기

기여를 환영합니다! 이슈나 풀 리퀘스트를 자유롭게 제출해 주세요.

1. 저장소를 포크합니다
2. 기능 브랜치를 생성합니다 (`git checkout -b feature/amazing-feature`)
3. 변경 사항을 커밋합니다 (`git commit -m 'feat: Add amazing feature'`)
4. 브랜치에 푸시합니다 (`git push origin feature/amazing-feature`)
5. 풀 리퀘스트를 생성합니다

---

## 라이선스

[MIT](../LICENSE.md) License

---

<div align="center">

Blazor 커뮤니티를 위해 만들었습니다

</div>
