# Demo Examples

컴포넌트의 상세 활용 예제 페이지를 생성합니다. shadcn/ui의 examples 패턴을 참고하여 실무에서 자주 쓰이는 조합과 시나리오를 보여줍니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름이 들어옵니다 (예: `Button`, `Select`, `Form`, `Card`).
비어 있으면 사용자에게 어떤 컴포넌트의 예제를 만들지 물어봅니다.

## 실행 절차

### 1. 현재 데모 파악

- `src/Daeha.RnUI.Demo.Wasm/Pages/ComponentDemos/` 에서 해당 `{Name}Demo.razor` 파일을 읽습니다.
- 기존 `GetDemo()`의 `Examples` 배열에 어떤 예제가 있는지 확인합니다.
- 해당 컴포넌트의 모든 Parameters, Enums, 서브 컴포넌트를 파악합니다.
  - `src/Daeha.RnUI/Components/UI/{Name}/` 폴더 전체 읽기

### 2. shadcn/ui 원본 examples 참조

shadcn/ui GitHub에서 해당 컴포넌트의 example 파일들을 확인합니다:
- URL: `https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/examples`
- `{name}-demo.tsx`, `{name}-*.tsx` 패턴의 파일들을 모두 참조합니다.
- fetch 도구로 원본 소스를 읽어 Blazor 변환의 기초로 삼습니다.

### 3. 예제 목록 설계

사용자에게 생성할 예제 목록을 제안합니다. 아래 카테고리를 기준으로:

| 카테고리 | 설명 | 예시 |
|---------|------|------|
| **Variants** | 모든 variant/size 조합 | 이미 `Examples`에 있으면 스킵 |
| **With Icons** | 아이콘과 함께 사용 | Button + Icon, Input + Icon |
| **Composition** | 다른 컴포넌트와 조합 | Card + Form, Dialog + Form |
| **Real-world** | 실무 시나리오 | 로그인 폼, 검색 필터, 설정 페이지 |
| **States** | 상태별 표시 | Loading, Disabled, Error, Empty |
| **Responsive** | 반응형 패턴 | 모바일/데스크톱 레이아웃 |

사용자 승인 후 다음 단계로 진행합니다.

### 4. 예제 파일 생성

예제 수에 따라 2가지 방식 중 선택:

#### A. 인라인 방식 (예제 5개 이하)

기존 `{Name}Demo.razor`의 `Examples` 배열에 직접 추가:

```razor
Examples:
[
    new("With Icon", "아이콘을 포함한 버튼",
        @<RnButton><RnIcon Icon="RnIcons.Mail" Size="16" /> Email</RnButton>,
        @"<RnButton><RnIcon Icon=""RnIcons.Mail"" Size=""16"" /> Email</RnButton>"),
    // ... 추가 예제
]
```

#### B. 폴더 방식 (예제 6개 이상 또는 상태가 필요한 복잡한 예제)

`DataTableExamples/` 패턴을 따라 별도 폴더 생성:

```
ComponentDemos/{Category}/{Name}Examples/
├── WithIconExample.razor
├── CompositionExample.razor
├── LoadingStateExample.razor
└── RealWorldExample.razor
```

각 예제 파일은 독립적인 Razor 컴포넌트:

```razor
@* WithIconExample.razor *@
<div class="flex flex-col gap-4">
    <h3 class="text-lg font-semibold">With Icons</h3>
    <p class="text-sm text-muted-foreground">버튼에 아이콘을 추가하는 다양한 방법</p>
    <div class="flex flex-wrap gap-2">
        @* 예제 내용 *@
    </div>
</div>

@code {
    // 상태가 필요한 경우 여기에
}
```

그리고 `{Name}Demo.razor`의 `Demo` 또는 `Examples`에서 참조:

```razor
Demo: @<{Name}DemoContent />,
```

### 5. 인터랙티브 예제 처리

상태가 필요한 예제(Dialog + Form, Toast 트리거 등)는 반드시 별도 래퍼 컴포넌트로 분리합니다:

```razor
// {Name}Examples/{ExampleName}Example.razor — 상태 포함 가능
<RnButton OnClick="() => _dialogOpen = true">Open Dialog</RnButton>
<RnDialog @bind-Open="_dialogOpen">
    <RnDialogContent>
        <RnDialogHeader>
            <RnDialogTitle>Edit Profile</RnDialogTitle>
        </RnDialogHeader>
        @* 폼 내용 *@
    </RnDialogContent>
</RnDialog>

@code {
    private bool _dialogOpen;
}
```

`GetDemo()`는 static 메서드이므로 내부에서 직접 상태를 관리할 수 없습니다.

### 6. CSS 빌드

```bash
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css   # 데모 CSS (새 Tailwind utility)
dotnet build                                         # 빌드 확인
```

### 7. 빌드 검증

`dotnet build` exit code 0 확인. 실패 시 수정 후 재빌드.

## 예제 품질 기준

- **복사해서 바로 쓸 수 있는 코드**: 예제 코드는 그대로 프로젝트에 붙여넣기 가능해야 함
- **실무 데이터 사용**: `Lorem ipsum` 대신 현실적인 데이터 (이름, 이메일, 금액 등)
- **접근성 포함**: aria-label, role 등 접근성 속성을 예제에도 포함
- **코드 문자열 동기화**: `Code` 파라미터의 문자열이 실제 `Demo` RenderFragment와 일치해야 함

## 금지

- `GetDemo()`를 non-static으로 변경하지 않습니다
- 컴포넌트 자체(라이브러리 코드)를 수정하지 않습니다 — 데모 코드만 추가
- `shadcn.src.css`(라이브러리 CSS)를 수정하지 않습니다
- 빌드 확인 없이 완료 선언하지 않습니다

## 완료 후

1. 추가한 예제 목록을 사용자에게 보고합니다
2. 데모 사이트에서 확인할 URL 경로를 안내합니다
