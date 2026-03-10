# RnUI — Blazor shadcn/ui Component Library

shadcn/ui를 Blazor로 포팅한 UI 컴포넌트 라이브러리. NuGet 패키지(`Daeha.RnUI`)로 배포.

## 빌드 & 실행

```bash
# 라이브러리 CSS 컴파일 (shadcn.src.css → shadcn.css)
cd src/Daeha.RnUI && npm run build:css

# 데모 CSS watch (개발 시)
cd src/Daeha.RnUI.Demo.Wasm && npm run watch:css

# 빌드
dotnet build

# 테스트
dotnet test

# NuGet 패키징
dotnet pack src/Daeha.RnUI
```

## 프로젝트 구조

```
src/
├── Daeha.RnUI/                      # 메인 라이브러리 (NuGet 배포 대상)
│   ├── Components/UI/{Name}/        # 컴포넌트 폴더 (Rn{Name}.razor + Enum)
│   ├── Services/                    # RnUIInteropService, ToastService
│   ├── Utils/                       # CssUtils.cs (cn() 유틸리티)
│   ├── wwwroot/css/shadcn.src.css   # ★ CSS 유일한 수정 대상
│   ├── wwwroot/css/shadcn.css       # 컴파일 결과물 (직접 수정 금지)
│   └── wwwroot/js/rnui-interop.js   # JS Interop 모듈
├── Daeha.RnUI.Demo.Wasm/           # WASM 데모 앱
│   ├── Pages/ComponentDemos/        # 카테고리별 데모 페이지
│   └── wwwroot/input.css            # 데모 전용 Tailwind 스캔
└── Daeha.RnUI.Demo/                # Server 데모 앱
tests/
└── Daeha.RnUI.Tests/               # xUnit + bunit 테스트
```

## CSS 파이프라인 (반드시 준수)

**⚠️ 두 개의 독립 파이프라인 — 반드시 둘 다 빌드:**

```
[라이브러리] shadcn.src.css → npm run build:css → shadcn.css (컴포넌트 .cn-* 클래스)
[데모]      input.css       → npm run build:css → tailwindcss.css (Tailwind utility 클래스)
```

```bash
# CSS 변경 후 필수 빌드 순서:
cd src/Daeha.RnUI && npm run build:css              # 1. 라이브러리
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css     # 2. 데모
dotnet build                                          # 3. .NET 빌드
```

- **디자인 토큰, 다크모드, 컴포넌트 CSS** → `shadcn.src.css`만 수정
- **`shadcn.css` 직접 수정 금지** — 빌드 시 덮어씌워짐
- **`input.css`에 디자인 토큰 추가 금지** — 라이브러리와 중복 발생
- **라이브러리 CSS만 빌드 금지** — 데모 CSS도 빌드해야 새 Tailwind utility 클래스 적용
- 상세: `.agents/knowledge/rnui-css-architecture.md` 참조

## 컴포넌트 규칙

### 네이밍

| 대상 | 규칙 | 예시 |
|------|------|------|
| 컴포넌트명 | `Rn` 접두사 + PascalCase | `RnButton`, `RnCard` |
| CSS 베이스 클래스 | `cn-` 접두사 + kebab-case | `.cn-button`, `.cn-card` |
| Variant 클래스 | `cn-{name}-variant-{variant}` | `.cn-button-variant-ghost` |
| Size 클래스 | `cn-{name}-size-{size}` | `.cn-button-size-sm` |
| data-slot | 컴포넌트 식별 | `data-slot="button"` |
| data-variant | variant 값 | `data-variant="ghost"` |

### 필수 Parameter 패턴

모든 컴포넌트에 아래 3개는 반드시 포함:

```csharp
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter] public string? Class { get; set; }
[Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

### ComputedClass 패턴

```csharp
private string ComputedClass => CssUtils.Cn(
    "cn-{name}",       // 베이스 클래스
    VariantClass,       // variant enum → CSS 클래스 매핑
    SizeClass,          // size enum → CSS 클래스 매핑
    Class               // 사용자 커스텀 클래스
);
```

### 파일 구조 (새 컴포넌트 생성 시)

```
Components/UI/{Name}/
├── Rn{Name}.razor           # 메인 컴포넌트
├── {Name}Enums.cs           # Variant/Size enum (필요시)
├── Rn{Name}Header.razor     # 서브 컴포넌트 (복합 컴포넌트일 경우)
└── Rn{Name}Content.razor
```

## shadcn/ui 참조 규칙

- shadcn/ui React 원본의 **CSS와 HTML 구조**를 따름
- Radix UI 프리미티브는 **순수 Blazor로 구현** (JS Interop 최소화)
- React 패턴 → Blazor 변환:
  - `className={cn(...)}` → `class="@ComputedClass"` + `CssUtils.Cn()`
  - `{children}` → `@ChildContent` (RenderFragment)
  - `useState` → `[Parameter]` + `EventCallback<T>`
  - `useEffect` → `OnAfterRenderAsync`
  - `useContext` → `CascadingValue` / `CascadingParameter`
  - `forwardRef` → `@ref` + `ElementReference`
  - `data-state="open"` → `data-state="@(Open ? "open" : "closed")"`
  - `cva()` variants → `enum` + `switch` → CSS class string

## CSS 작성 규칙

- **oklch() 색공간** 사용 (Tailwind v4)
- 라이트모드 기본값: `@theme {}` 블록
- 다크모드 오버라이드: `.dark {}` 블록
- subtle border: `var(--color-border-subtle)` 사용 (`border-b` Tailwind 클래스 대신)
- 컴포넌트 CSS는 `@apply`로 Tailwind 유틸리티 활용

```css
/* shadcn.src.css 작성 예시 */
.cn-button {
  @apply inline-flex items-center justify-center rounded-md text-sm font-medium
         transition-all focus-visible:outline-none focus-visible:ring-2
         disabled:pointer-events-none disabled:opacity-50;
}
.cn-button-variant-default {
  @apply bg-primary text-primary-foreground shadow-xs hover:bg-primary/90;
}
.cn-button-size-default {
  @apply h-9 px-4 py-2;
}
```

## JS Interop 규칙

- **OnAfterRenderAsync 이후에만 호출** (SSR 중 호출 시 런타임 에러)
- `RnUIInteropService` 사용 (직접 `IJSRuntime` 호출 최소화)
- 오버레이 컴포넌트는 반드시 `IAsyncDisposable` 구현
- 제공 기능: body scroll lock, focus trap, escape listener, click-outside

## 접근성 필수

- `role` 속성 명시 (`dialog`, `switch`, `menu` 등)
- `aria-*` 속성 (modal, checked, expanded, labelledby 등)
- 키보드 네비게이션 (Tab, Escape, Enter)
- focus visible ring
- sr-only 텍스트 (시각적으로 숨겨진 설명)

## 테스트 (xUnit + bunit)

```csharp
[Trait("Category", "Unit")]
public class Rn{Name}Tests : BunitContext
{
    [Fact]
    public void Rn{Name}_DefaultRendering_AppliesBaseClass()
    {
        var cut = Render<Rn{Name}>();
        cut.Find("[data-slot=\"{name}\"]").ClassList.Should().Contain("cn-{name}");
    }
}
```

## 절대 금지

- `shadcn.css` 직접 수정
- `input.css`에 디자인 토큰/컴포넌트 CSS 추가
- 컴포넌트에서 인라인 스타일 사용
- Tailwind utility만으로 컴포넌트 스타일링 (반드시 `.cn-*` 클래스 정의)
- `OnInitializedAsync`에서 JS Interop 호출
- `Rn` 접두사 누락
- `data-slot` 속성 누락
- `AdditionalAttributes` 미포함
- `IAsyncDisposable` 미구현 (오버레이 컴포넌트)
- **라이브러리 CSS만 빌드하고 데모 CSS 빌드 누락** (새 utility 클래스 미적용)

## 기술 스택

- .NET SDK 10.0.103 (multi-target: net8.0, net9.0, net10.0)
- Tailwind CSS v4.2.1 (oklch 색공간)
- bunit 2.6.2 + xUnit + FluentAssertions + Moq
- 외부 UI 라이브러리 의존성 없음
