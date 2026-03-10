---
trigger: always_on
---

# Block Development Rules

## Blocks vs Components

| | Components | Blocks |
|---|---|---|
| 비유 | 레고 브릭 | 레고 완성 세트 |
| 위치 | `Daeha.RnUI` (NuGet 라이브러리) | `Demo.Wasm` (데모 앱) |
| 목적 | 어디서든 재사용 | 복사해서 커스터마이징하는 템플릿 |
| 단위 | 단일 UI 요소 | 완성된 페이지/섹션 |

---

## 파일 구조

```
src/Daeha.RnUI.Demo.Wasm/
├── Pages/
│   └── Blocks/
│       └── Blocks.razor                   ← /blocks 라우트 (목록 + 상세)
├── BlockDemos/
│   ├── Login/
│   │   ├── Login01Demo.razor              ← GetDemo() 정의
│   │   └── Components/
│   │       └── LoginForm01.razor          ← 블록 전용 조합 컴포넌트
│   ├── Signup/
│   │   ├── Signup01Demo.razor
│   │   └── Components/
│   │       └── SignupForm01.razor
│   ├── Sidebar/
│   │   ├── Sidebar01Demo.razor
│   │   └── Components/
│   │       └── AppSidebar01.razor
│   └── Dashboard/
│       ├── Dashboard01Demo.razor
│       └── Components/
│           ├── AppSidebar.razor
│           ├── SiteHeader.razor
│           └── SectionCards.razor
└── Models/
    └── BlockDemo.cs                       ← Block 데모 데이터 모델
```

---

## BlockDemo 데이터 모델

기존 `ComponentDemo` 패턴을 확장:

```csharp
public record BlockDemo(
    string Name,                    // "Login 01"
    string Slug,                    // "login-01"
    string Description,             // "A simple login form with email and password."
    string Category,                // "login" | "signup" | "sidebar" | "dashboard"
    RenderFragment Demo,            // 라이브 프리뷰
    RenderFragment Thumbnail,       // 목록 그리드용 썸네일
    BlockFile[] Files               // 소스 파일 목록 (코드 보기용)
);

public record BlockFile(
    string FileName,                // "login-form.razor"
    string Code                     // 전체 소스 코드 문자열
);
```

### ComponentDemo와의 차이

| | ComponentDemo | BlockDemo |
|---|---|---|
| Code | 단일 string | `BlockFile[]` (다중 파일) |
| UsageCode | 있음 | 없음 (블록은 복사 대상이므로) |
| Examples | variant별 예시 | 없음 (블록 자체가 하나의 완성 예시) |
| Category | 없음 | "login", "sidebar" 등 카테고리 필터 |
| Files | 없음 | 파일별 코드 탭으로 표시 |

---

## 블록 데모 페이지 패턴

### 목록 페이지 (/blocks)

```
카테고리 필터 탭 (All | Featured | Login | Signup | Sidebar)
  └── 블록 카드 그리드 (2열)
      └── 각 카드: 썸네일 + 이름 + 설명
```

### 상세 페이지 (/blocks/{slug})

```
블록 이름 + 설명
├── 라이브 프리뷰 (전체 너비, 실제 페이지처럼 보임)
└── 소스 코드 영역
    ├── 파일 탭 (login-form.razor | page.razor)
    └── 코드 블록 (SyntaxHighlighter + 복사 버튼)
```

shadcn 홈페이지처럼 **파일 탭**으로 여러 파일의 코드를 전환하며 볼 수 있어야 한다.

---

## 블록 프리뷰 표시 규칙

### 라이브 프리뷰 영역

```razor
@* 블록 프리뷰는 전체 너비로 실제 페이지처럼 렌더링 *@
<div class="rounded-xl border border-[var(--color-border-subtle)] overflow-hidden">
    <div class="min-h-[500px]">
        @Demo
    </div>
</div>
```

- ComponentPreview와 달리 **p-10 패딩 없음** — 블록은 전체 레이아웃이므로
- `min-h-[500px]` — 충분한 높이로 실제 페이지처럼 보이게
- 반응형 리사이즈 버튼 (Desktop/Tablet/Mobile) — 선택적

### 코드 표시 영역

```razor
@* 파일 탭 + 코드 블록 *@
<div class="border rounded-xl overflow-hidden mt-4">
    <div class="flex border-b bg-zinc-950">
        @foreach (var file in block.Files)
        {
            <button class="px-4 py-2 text-sm @(activeFile == file ? "text-white border-b-2 border-white" : "text-zinc-400")">
                @file.FileName
            </button>
        }
    </div>
    <div class="bg-zinc-950">
        <pre><code>@SyntaxHighlighter.HighlightXml(activeFile.Code)</code></pre>
    </div>
</div>
```

---

## 블록 데모 작성 패턴

```csharp
// BlockDemos/Login/Login01Demo.razor
@code {
    public static BlockDemo GetDemo() => new(
        Name: "Login 01",
        Slug: "login-01",
        Description: "A simple login form with email and password.",
        Category: "login",
        Demo: @<LoginForm01 />,
        Thumbnail: @<div class="scale-[0.6] pointer-events-none">
            <LoginForm01 />
        </div>,
        Files:
        [
            new("login-form.razor", @"<RnCard Class=""w-full max-w-sm"">
    <RnCardHeader>
        <RnCardTitle>Login</RnCardTitle>
        ...
    </RnCardHeader>
    ...
</RnCard>"),
            new("page.razor", @"@page ""/login""

<div class=""flex min-h-svh items-center justify-center"">
    <LoginForm />
</div>")
        ]
    );
}
```

---

## 블록 작성 규칙

### RnUI 컴포넌트만 사용

블록은 기존 RnUI 컴포넌트를 조합만 한다. 새 기본 컴포넌트가 필요하면 먼저 라이브러리에 추가.

```razor
@* ✅ RnUI 컴포넌트 조합 *@
<RnCard><RnCardHeader>...</RnCardHeader></RnCard>

@* ❌ HTML 직접 작성 (RnUI 컴포넌트가 있는데) *@
<div class="rounded-xl border p-4">...</div>
```

### CSS는 Tailwind utility 직접 사용

블록의 레이아웃 CSS는 `shadcn.src.css`에 추가하지 않는다.
Tailwind utility 클래스를 razor에서 직접 사용:

```razor
@* ✅ 블록 레이아웃: Tailwind utility 직접 *@
<div class="flex min-h-svh items-center justify-center p-4">
    <LoginForm01 />
</div>

@* ❌ 블록 레이아웃을 .cn-* 클래스로 만들지 않음 *@
<div class="cn-login-layout">
```

### 반응형 필수

모든 블록은 모바일→데스크톱 반응형:

```razor
<div class="flex min-h-svh flex-col items-center justify-center gap-6 p-4 md:p-8">
    <div class="w-full max-w-sm">
        @* 모바일: 전체 너비, 데스크톱: 최대 sm *@
    </div>
</div>
```

### 상태 관리

블록 내 상태(form 입력, 로딩 등)는 블록 전용 조합 컴포넌트 내부에서 관리:

```razor
@* Components/LoginForm01.razor *@
@code {
    private string _email = "";
    private string _password = "";
    private bool _isLoading;

    private async Task HandleSubmit()
    {
        _isLoading = true;
        await Task.Delay(1000); // 데모용 딜레이
        _isLoading = false;
    }
}
```

---

## 금지 사항

- 블록 전용 컴포넌트를 `Daeha.RnUI` 라이브러리에 넣기 (데모 앱에만)
- 블록 레이아웃을 `shadcn.src.css`에 `.cn-*` 클래스로 추가
- 실제 API 호출 (데모이므로 목 데이터/딜레이만 사용)
- 하드코딩된 색상 (`bg-blue-500` 등) — 반드시 디자인 토큰 사용 (`bg-primary` 등)
