# Block Structure: shadcn → Blazor 매핑

## shadcn/ui 원본 구조

```
apps/v4/registry/new-york-v4/blocks/
├── login-01/
│   ├── page.tsx                    ← Next.js 페이지 (라우트)
│   └── components/
│       └── login-form.tsx          ← 조합 컴포넌트
├── sidebar-01/
│   ├── page.tsx
│   └── components/
│       └── app-sidebar.tsx
└── dashboard-01/
    ├── page.tsx
    └── components/
        ├── app-sidebar.tsx
        ├── chart-area-interactive.tsx
        ├── data-table.tsx
        ├── section-cards.tsx
        └── site-header.tsx
```

---

## Blazor (RnUI Demo) 구조

```
src/Daeha.RnUI.Demo.Wasm/
├── Pages/
│   └── Blocks/
│       └── Blocks.razor              ← /blocks, /blocks/{Slug} 라우트
├── BlockDemos/
│   ├── Login/
│   │   ├── Login01Demo.razor         ← GetDemo() — BlockDemo 반환
│   │   ├── Login02Demo.razor
│   │   ├── Login04Demo.razor
│   │   └── Components/
│   │       ├── LoginForm01.razor     ← 블록 전용 조합 컴포넌트
│   │       ├── LoginForm02.razor
│   │       └── LoginForm04.razor
│   ├── Signup/
│   │   ├── Signup01Demo.razor
│   │   └── Components/
│   │       └── SignupForm01.razor
│   ├── Sidebar/
│   │   ├── Sidebar01Demo.razor
│   │   ├── Sidebar05Demo.razor
│   │   └── Components/
│   │       ├── AppSidebar01.razor
│   │       └── AppSidebar05.razor
│   └── Dashboard/
│       ├── Dashboard01Demo.razor
│       └── Components/
│           ├── DashAppSidebar.razor
│           ├── DashSiteHeader.razor
│           └── DashSectionCards.razor
├── Shared/
│   ├── ComponentPreview.razor        ← 기존 (컴포넌트 데모용)
│   ├── BlockPreview.razor            ← NEW (블록 데모용 — 파일 탭)
│   ├── CodeBlock.razor               ← 기존
│   └── SyntaxHighlighter.cs          ← 기존
└── Models/
    ├── ComponentDemo.cs              ← 기존
    └── BlockDemo.cs                  ← NEW
```

---

## 파일 역할 매핑

| shadcn (React) | RnUI (Blazor) | 역할 |
|---|---|---|
| `page.tsx` | 페이지 코드는 `BlockDemo.Files`의 Code 문자열로 포함 | 라우트 페이지 코드 보여주기용 |
| `components/login-form.tsx` | `Components/LoginForm01.razor` | 실제 렌더링되는 조합 컴포넌트 |
| `__blocks__.json` (registry) | `BlockDemo` record의 속성들 | 메타데이터 (이름, 설명, 카테고리) |

---

## BlockPreview 컴포넌트 (NEW — 생성 필요)

기존 `ComponentPreview`와 다른 점:

| | ComponentPreview | BlockPreview |
|---|---|---|
| 프리뷰 영역 | `min-h-[350px]`, 중앙 정렬, 패딩 | `min-h-[500px]`, 패딩 없음 (전체 레이아웃) |
| 코드 표시 | 단일 코드 블록 | **파일 탭** (여러 파일 전환) |
| 코드 접기 | 4줄 초과 시 접기 | 항상 접기 (블록 코드는 길음) |

```
┌─────────────────────────────────────────────┐
│  라이브 프리뷰 (전체 너비, 실제 페이지처럼) │
│  min-h-[500px], 패딩 없음                   │
├─────────────────────────────────────────────┤
│ [login-form.razor] [page.razor]  📋 Copy    │ ← 파일 탭
├─────────────────────────────────────────────┤
│  선택된 파일의 syntax highlighted 코드       │
│  (접기/펼치기)                              │
└─────────────────────────────────────────────┘
```

---

## Blocks 페이지 라우팅

```razor
@* Pages/Blocks/Blocks.razor *@
@page "/blocks"
@page "/blocks/{Slug}"
@layout DocsLayout

@if (string.IsNullOrEmpty(Slug))
{
    @* 카테고리 탭 + 블록 그리드 목록 *@
}
else
{
    @* 블록 상세: BlockPreview *@
}

@code {
    [Parameter] public string? Slug { get; set; }
}
```

---

## GetDemo() 패턴 (블록용)

```csharp
// BlockDemos/Login/Login01Demo.razor
@code {
    public static BlockDemo GetDemo() => new(
        Name: "Login 01",
        Slug: "login-01",
        Description: "A simple login form with card layout.",
        Category: "login",
        Demo: @<div class="flex min-h-[500px] items-center justify-center bg-background">
            <LoginForm01 />
        </div>,
        Thumbnail: @<div class="scale-[0.5] origin-top-left pointer-events-none w-[200%] h-[300px] overflow-hidden">
            <div class="flex items-center justify-center h-full">
                <LoginForm01 />
            </div>
        </div>,
        Files:
        [
            new("login-form.razor",
                """
                <RnCard Class="w-full max-w-sm">
                    <RnCardHeader>
                        <RnCardTitle>Login</RnCardTitle>
                        <RnCardDescription>Enter your email to login.</RnCardDescription>
                    </RnCardHeader>
                    <RnCardContent>
                        <EditForm Model="_model" FormName="login" OnValidSubmit="HandleSubmit">
                            ...
                        </EditForm>
                    </RnCardContent>
                </RnCard>
                """),
            new("page.razor",
                """
                @page "/login"

                <div class="flex min-h-svh items-center justify-center">
                    <LoginForm />
                </div>
                """)
        ]
    );
}
```

---

## 네이밍 컨벤션

| 대상 | 패턴 | 예시 |
|------|------|------|
| 데모 파일 | `{Block}{Num}Demo.razor` | `Login01Demo.razor` |
| 조합 컴포넌트 | `{Block}Form{Num}.razor` 또는 `App{Block}{Num}.razor` | `LoginForm01.razor`, `AppSidebar01.razor` |
| Slug | `{block}-{num}` (kebab-case) | `login-01`, `sidebar-05` |
| 카테고리 | 소문자 | `login`, `signup`, `sidebar`, `dashboard` |
