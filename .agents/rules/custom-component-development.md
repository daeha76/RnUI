---
trigger: always_on
---

# Custom Component Development Rules

shadcn/ui에 존재하지 않는 커스텀 컴포넌트를 만들 때의 규칙.
기존 RnUI 디자인 시스템과의 일관성을 유지하면서 독자적인 컴포넌트를 설계한다.

---

## 기본 원칙

1. **디자인 토큰 준수**: 커스텀 컴포넌트도 기존 `@theme` 토큰(`--color-*`, `--radius-*`)을 사용한다. 새 토큰은 최소한으로 추가.
2. **기존 패턴 일관성**: Simple / Variant / Composite / Stateful 4가지 패턴 중 하나를 따른다.
3. **CSS 네이밍 동일**: `.cn-{name}`, `.cn-{name}-variant-{variant}` 등 기존 네이밍 그대로.
4. **접근성 독립 설계**: shadcn/ui 참조 없이 WAI-ARIA 1.2 패턴을 직접 조사하여 적용한다.
5. **최소 기능 원칙**: 첫 구현은 핵심 기능만. 사용하면서 확장한다.

---

## shadcn/ui 참조 없는 컴포넌트 설계 워크플로우

### 1. 요구사항 정의

구현 전에 반드시 아래를 명확히 한다:

```
- 컴포넌트의 목적과 사용 시나리오
- 필요한 variants (시각적 변형)
- 필요한 sizes (크기 변형)
- 상태 관리 필요 여부 (stateless vs stateful)
- 자식 컴포넌트 필요 여부 (단일 vs composite)
- 인터랙션 패턴 (클릭, 호버, 키보드 등)
- 접근성 요구사항 (role, aria-*, 키보드)
```

### 2. 유사 컴포넌트 참조

shadcn/ui에 동일 컴포넌트가 없더라도, **유사한 기존 컴포넌트**를 참조한다:

| 커스텀 컴포넌트 유형 | 참조할 기존 컴포넌트 |
|---|---|
| 상태 표시/인디케이터 | RnBadge, RnProgress |
| 데이터 입력 | RnInput, RnSelect, RnSlider |
| 오버레이/팝업 | RnDialog, RnPopover, RnTooltip |
| 리스트/그리드 | RnTable, RnCommand |
| 네비게이션 | RnTabs, RnBreadcrumb, RnPagination |
| 피드백/알림 | RnAlert, RnToast |
| 레이아웃/컨테이너 | RnCard, RnCollapsible |
| 토글/선택 | RnSwitch, RnCheckbox, RnToggle |

**참조 시 확인할 것**: HTML 구조, CSS 패턴, Parameter 설계, 접근성 구현.

### 3. API 설계 (Parameter 결정)

```csharp
// 필수 3종 (모든 컴포넌트)
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter] public string? Class { get; set; }
[Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

// Variant/Size (필요 시)
[Parameter] public {Name}Variant Variant { get; set; } = {Name}Variant.Default;
[Parameter] public ComponentSize Size { get; set; } = ComponentSize.Default;

// 상태 (필요 시) — Two-way binding 패턴
[Parameter] public bool Value { get; set; }
[Parameter] public EventCallback<bool> ValueChanged { get; set; }

// 이벤트 (필요 시)
[Parameter] public EventCallback OnClick { get; set; }
[Parameter] public EventCallback<string> OnChange { get; set; }
```

#### Parameter 네이밍 규칙

| 패턴 | 네이밍 | 예시 |
|------|--------|------|
| On/Off 상태 | `bool` + `Changed` 접미사 | `Checked`/`CheckedChanged`, `Open`/`OpenChanged` |
| 값 선택 | 타입 + `Changed` | `Value`/`ValueChanged`, `SelectedItem`/`SelectedItemChanged` |
| 비활성화 | `Disabled` (bool) | 모든 인터랙티브 컴포넌트 |
| 텍스트 | `string` 직접 | `Title`, `Placeholder`, `Label` |
| 아이콘/커스텀 영역 | `RenderFragment` | `Icon`, `Prefix`, `Suffix` |

### 4. CSS 설계

커스텀 컴포넌트의 CSS도 `shadcn.src.css`에 작성하며, 기존 토큰을 활용한다:

```css
/* ✅ 기존 토큰 활용 */
.cn-custom {
  @apply rounded-lg bg-card text-card-foreground;
  border: 1px solid var(--color-border-subtle);
}

/* ✅ 기존 패턴과 일관된 variant */
.cn-custom-variant-default {
  @apply bg-primary text-primary-foreground;
}
.cn-custom-variant-outline {
  @apply border bg-background;
}

/* ❌ 임의의 하드코딩 색상 */
.cn-custom {
  background: #f0f0f0;  /* 토큰 사용해야 함 */
  border-radius: 8px;   /* --radius-* 사용해야 함 */
}
```

### 5. 접근성 독립 설계

shadcn/ui 참조가 없으므로 직접 WAI-ARIA 패턴을 확인한다:

- **WAI-ARIA Authoring Practices**: https://www.w3.org/WAI/ARIA/apg/patterns/
- 적절한 `role` 선택
- `aria-*` 속성 설계
- 키보드 네비게이션 정의
- focus 관리 계획

---

## Variant 설계 가이드라인

커스텀 Variant를 만들 때 기존 shadcn/ui 패턴과 일관성을 유지:

### 시각적 Variant (일반적인 세트)

```csharp
public enum {Name}Variant
{
    Default,      // 가장 일반적인 모습 (항상 첫 번째)
    Secondary,    // 덜 강조
    Outline,      // 테두리만
    Ghost,        // 배경 없음, hover 시 배경
    Destructive,  // 위험/삭제 액션 (빨간색 계열)
    // 컴포넌트 특화 variant는 뒤에 추가
}
```

### 시각적 Variant 선택 기준

| 필요한 느낌 | 사용할 Variant | CSS 패턴 |
|---|---|---|
| 주요 액션 / 강조 | Default | `bg-primary text-primary-foreground` |
| 보조 액션 | Secondary | `bg-secondary text-secondary-foreground` |
| 테두리 스타일 | Outline | `border bg-background` |
| 최소한의 스타일 | Ghost | `hover:bg-accent hover:text-accent-foreground` |
| 위험/삭제 | Destructive | `bg-destructive text-destructive-foreground` |
| 링크처럼 보임 | Link | `text-primary underline-offset-4 hover:underline` |

### Size Variant

기존 `ComponentSize` enum 재사용 가능. 컴포넌트 고유 size만 별도 enum:

```csharp
// 공통 ComponentSize 재사용 (Utils/SharedEnums.cs)
[Parameter] public ComponentSize Size { get; set; } = ComponentSize.Default;

// 또는 컴포넌트 고유 size
public enum {Name}Size
{
    Default,
    Sm,
    Lg,
    // 컴포넌트 특화 size
}
```

---

## 새 디자인 토큰 추가 기준

커스텀 컴포넌트에서 새 토큰이 필요한 경우 **매우 신중하게** 추가:

### 추가해야 하는 경우

- 3개 이상의 컴포넌트에서 공유할 색상/값
- 라이트/다크 모드 전환이 필요한 색상
- 테마 변형(sky 등)에서도 오버라이드가 필요한 값

### 추가하지 말아야 하는 경우

- 단일 컴포넌트에서만 사용하는 값 → 기존 토큰 조합으로 해결
- opacity 변형 → `bg-primary/90` 같은 Tailwind 패턴 사용
- 한두 픽셀 차이 → 기존 `--radius-*` 등 근사값 사용

### 추가 시 규칙

```css
@theme {
  /* 기존 토큰들 ... */

  /* 새 토큰: 컴포넌트 이름 접두사 사용 */
  --color-{component}: oklch(...);
  --color-{component}-foreground: oklch(...);
}

/* 다크모드 오버라이드도 반드시 추가 */
.dark {
  --color-{component}: oklch(...);
  --color-{component}-foreground: oklch(...);
}
```

---

## 복합 커스텀 컴포넌트 설계

### 부모-자식 관계 설계

```
Rn{Name} (부모)
├── Rn{Name}Header   — 헤더 영역
├── Rn{Name}Content  — 메인 콘텐츠
├── Rn{Name}Footer   — 하단 영역
└── Rn{Name}Action   — 액션 버튼 영역
```

### 부모→자식 데이터 전달

상태 공유가 필요하면 `CascadingValue` 사용:

```razor
@* 부모 *@
<CascadingValue Value="this" IsFixed="false">
    <div data-slot="{name}" class="@ComputedClass" @attributes="AdditionalAttributes">
        @ChildContent
    </div>
</CascadingValue>

@* 자식 *@
@code {
    [CascadingParameter] public Rn{Name}? Parent { get; set; }
}
```

상태 공유가 불필요하면 자식은 독립 Simple 컴포넌트로 구현.

---

## 금지 사항

- 기존 디자인 토큰을 무시하고 하드코딩 색상 사용
- 유사 기존 컴포넌트를 참조하지 않고 완전히 다른 패턴 사용
- WAI-ARIA 확인 없이 접근성 생략
- 너무 많은 variant/size를 한 번에 추가 (필요 시 확장)
- `@theme`에 불필요한 토큰 남발
- CSS에서 `!important` 사용
- 다크모드 미고려
