---
trigger: always_on
---

# Component Development Rules

## 컴포넌트 분류 (4가지 패턴)

### 1. Simple — 단일 요소 (RnCard, RnBadge, RnLabel)

HTML 요소 하나에 CSS 클래스만 적용. 상태 없음.

```razor
<div data-slot="{name}" class="@CssUtils.Cn("cn-{name}", Class)" @attributes="AdditionalAttributes">
    @ChildContent
</div>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
```

### 2. Variant — 열거형 변형 (RnButton, RnAlert, RnBadge)

Variant/Size enum으로 스타일 분기. `ComputedClass` + `switch` 패턴.

```razor
<button data-slot="button" data-variant="@Variant.ToString().ToLowerInvariant()"
        class="@ComputedClass" @attributes="AdditionalAttributes">
    @ChildContent
</button>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public {Name}Variant Variant { get; set; } = {Name}Variant.Default;
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string ComputedClass => CssUtils.Cn("cn-{name}", VariantClass, Class);

    private string VariantClass => Variant switch
    {
        {Name}Variant.Default => "cn-{name}-variant-default",
        {Name}Variant.Outline => "cn-{name}-variant-outline",
        _ => "cn-{name}-variant-default"
    };
}
```

### 3. Composite — 부모+자식 조합 (RnCard, RnTable, RnDialog)

부모가 구조를 제공하고, 자식이 슬롯을 채움. 자식은 Simple 패턴.

```
RnCard (부모)
├── RnCardHeader
├── RnCardTitle
├── RnCardDescription
├── RnCardContent
├── RnCardFooter
└── RnCardAction
```

자식 컴포넌트 패턴:
```razor
<div data-slot="card-header" class="@CssUtils.Cn("cn-card-header", Class)" @attributes="AdditionalAttributes">
    @ChildContent
</div>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
```

### 4. Stateful — 상태 관리 필요 (RnAccordion, RnDialog, RnSwitch)

내부 상태 + 이벤트 콜백 + (필요시) CascadingValue.

#### 4a. Two-way binding (RnSwitch, RnCheckbox)
```csharp
[Parameter] public bool Checked { get; set; }
[Parameter] public EventCallback<bool> CheckedChanged { get; set; }

private async Task Toggle()
{
    Checked = !Checked;
    await CheckedChanged.InvokeAsync(Checked);
}
```

#### 4b. CascadingValue (RnAccordion → RnAccordionItem)
```razor
@* 부모: CascadingValue로 자신을 전달 *@
<CascadingValue Value="this" IsFixed="false">
    <div data-slot="accordion">@ChildContent</div>
</CascadingValue>

@* 자식: CascadingParameter로 부모 접근 *@
@code {
    [CascadingParameter] public RnAccordion? Accordion { get; set; }
    internal bool IsExpanded => Accordion?.IsItemExpanded(Value) ?? false;
}
```

#### 4c. Overlay (RnDialog, RnSheet, RnDrawer)
```csharp
@implements IAsyncDisposable
@inject RnUIInteropService Interop

// Open/Close 상태 추적
private bool _previousOpen;

// OnAfterRenderAsync에서 interop 호출
// DisposeAsync에서 반드시 정리
```

---

## 필수 체크리스트 (새 컴포넌트 생성 시)

### 파일 생성
- [ ] `Components/UI/{Name}/Rn{Name}.razor` — 메인 컴포넌트
- [ ] `Components/UI/{Name}/{Name}Enums.cs` — enum 파일 (variant/size가 있을 때만)
- [ ] `Components/UI/{Name}/Rn{Name}{Sub}.razor` — 서브 컴포넌트 (composite일 때만)
- [ ] `tests/.../Rn{Name}Tests.cs` — bunit 테스트
- [ ] `Demo.Wasm/Pages/ComponentDemos/{Category}/Rn{Name}Demo.razor` — 데모 페이지

### Parameter 필수 3종
- [ ] `[Parameter] public RenderFragment? ChildContent { get; set; }` — 내용이 없는 컴포넌트도 포함 (void 컴포넌트 제외)
- [ ] `[Parameter] public string? Class { get; set; }` — 사용자 CSS 오버라이드
- [ ] `[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }`

### HTML 속성 필수
- [ ] `data-slot="{name}"` — 컴포넌트 식별 (kebab-case)
- [ ] `data-variant` — variant가 있을 경우
- [ ] `data-size` — size가 있을 경우
- [ ] `data-state` — 상태가 있을 경우 (`"open"` / `"closed"`, `"checked"` / `"unchecked"`)

### CSS 클래스
- [ ] `shadcn.src.css`에 `.cn-{name}` 베이스 클래스 추가
- [ ] variant가 있으면 `.cn-{name}-variant-{variant}` 추가
- [ ] size가 있으면 `.cn-{name}-size-{size}` 추가

### 접근성
- [ ] 적절한 `role` 속성 (`dialog`, `switch`, `menu`, `tab` 등)
- [ ] `aria-*` 속성 (checked, expanded, modal, labelledby 등)
- [ ] disabled 처리: `disabled="@(Disabled ? true : (object?)null)"`
- [ ] focus visible ring 스타일
- [ ] sr-only 텍스트 (아이콘 전용 버튼 등)

---

## Enum 작성 규칙

```csharp
// 파일: Components/UI/{Name}/{Name}Enums.cs
// 네임스페이스: RnUI.Components.UI (항상 동일)
namespace RnUI.Components.UI;

/// <summary>Visual variant of the {name}.</summary>
public enum {Name}Variant
{
    Default,    // 항상 첫 번째
    // ... shadcn/ui 원본 variant 순서 따름
}
```

- Enum 이름: `{Name}Variant`, `{Name}Size`
- 공통 Size가 이미 있으면 `ComponentSize` 재사용 (`Utils/SharedEnums.cs`)
- 컴포넌트별 고유 Size만 전용 enum 생성

---

## disabled 처리 패턴

```razor
@* disabled 속성: bool → nullable object 변환 *@
disabled="@(Disabled ? true : (object?)null)"

@* CSS: CssUtils.CnDisabled() 사용 가능 *@
class="@CssUtils.Cn("cn-button", CssUtils.CnDisabled(Disabled), Class)"

@* 이벤트 핸들러에서도 가드 *@
private async Task Toggle()
{
    if (Disabled) return;
    // ...
}
```

`Disabled`가 `false`일 때 `disabled` 속성 자체가 렌더링되지 않아야 함 → `(object?)null` 패턴.

---

## ID 생성 패턴

오버레이, aria-labelledby 등에 고유 ID가 필요할 때:

```csharp
private readonly string _id = Guid.NewGuid().ToString("N")[..8];
// aria-labelledby="@(_id + "-title")"
```

---

## 금지 사항

- 컴포넌트 내 `Console.WriteLine` / `Debug.WriteLine`
- 인라인 `style` 속성 사용 (모든 스타일은 CSS 클래스로)
- `IJSRuntime` 직접 주입 (반드시 `RnUIInteropService` 경유)
- Simple/Composite 자식 컴포넌트에 불필요한 상태 추가
- `@code` 블록 없이 코드비하인드(.razor.cs) 분리 — 현재 프로젝트는 inline `@code` 패턴 통일
