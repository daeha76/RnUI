---
trigger: always_on
---

# shadcn/ui Reference Rules

## 원칙

RnUI는 shadcn/ui의 **디자인, CSS, HTML 구조**를 충실히 따른다.
React/Radix 구현은 **순수 Blazor**로 재작성한다.

---

## shadcn/ui 원본 확인 방법

새 컴포넌트를 구현하거나 기존 컴포넌트를 수정할 때, 반드시 원본을 먼저 확인한다.

### 1. GitHub 소스 (최우선)

shadcn/ui 컴포넌트 소스:
```
https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/ui
```

파일 예시:
- `button.tsx` → RnButton.razor
- `card.tsx` → RnCard.razor + 서브 컴포넌트
- `dialog.tsx` → RnDialog.razor + 서브 컴포넌트

### 2. 공식 문서

```
https://ui.shadcn.com/docs/components/{component-name}
```

API, 사용 예시, 접근성 사양 확인.

### 3. Tailwind CSS v4 소스 (CSS 클래스 확인)

shadcn/ui가 사용하는 Tailwind 유틸리티 최신 문법 확인:
```
https://tailwindcss.com/docs
```

---

## React → Blazor 변환 규칙

### 기본 매핑

| React (shadcn/ui) | Blazor (RnUI) | 비고 |
|---|---|---|
| `React.forwardRef((props, ref) => ...)` | `@ref` + `ElementReference` | ref forwarding |
| `className={cn("base", variant, className)}` | `class="@ComputedClass"` + `CssUtils.Cn()` | 클래스 병합 |
| `{children}` | `@ChildContent` (`RenderFragment`) | 자식 콘텐츠 |
| `{...props}` | `@attributes="AdditionalAttributes"` | 나머지 속성 전달 |
| `const [open, setOpen] = useState(false)` | `[Parameter] bool Open` + `EventCallback<bool> OpenChanged` | 상태 관리 |
| `useEffect(() => {}, [deps])` | `OnAfterRenderAsync(bool firstRender)` | 사이드 이펙트 |
| `useContext(ThemeContext)` | `[CascadingParameter]` | 컨텍스트 |
| `useRef()` | `ElementReference` + `@ref` | DOM 참조 |
| `useCallback` | 일반 메서드 (불필요) | Blazor는 자동 관리 |
| `useMemo` | 계산 프로퍼티 또는 필드 캐싱 | 드물게 필요 |

### Radix UI 프리미티브 → Blazor 변환

shadcn/ui는 Radix UI를 기반으로 한다. RnUI는 Radix를 **사용하지 않고** 순수 Blazor로 구현한다.

| Radix 패턴 | Blazor 구현 |
|---|---|
| `<Dialog.Root>` / `<Dialog.Portal>` | `@if (Open)` 조건부 렌더링 |
| `<Dialog.Overlay>` | `<div class="cn-dialog-overlay" @onclick="HandleOverlayClick">` |
| `<Dialog.Content>` | `<div @ref="_contentRef" role="dialog" aria-modal="true">` |
| `<Dialog.Close>` | `<button @onclick="Close">` |
| `<Dialog.Trigger>` | 부모에서 `Open` 바인딩으로 처리 |
| `<Accordion.Root>` | `<CascadingValue Value="this">` |
| `<Accordion.Item>` | `[CascadingParameter] RnAccordion?` |
| `<Tooltip.Provider>` | 전역 서비스 또는 CascadingValue |
| `asChild` prop | `AdditionalAttributes` 패턴 또는 `RenderFragment` |
| `data-state="open"` | `data-state="@(Open ? "open" : "closed")"` |
| `onOpenChange` | `EventCallback<bool> OpenChanged` |

### Portal 처리

React Portal은 DOM 트리 밖에 렌더링한다. Blazor에서는:

```razor
@* Blazor에서 Portal 대체: 높은 z-index + fixed positioning *@
@if (Open)
{
    <div class="fixed inset-0 z-50"> @* overlay *@ </div>
    <div class="fixed z-50 ..."> @* content *@ </div>
}
```

별도 Portal 메커니즘 불필요 — `z-50` + `fixed`로 충분.

---

## cva() → enum + switch 변환

shadcn/ui의 `cva()` (class-variance-authority):

```tsx
// React 원본
const buttonVariants = cva("inline-flex items-center ...", {
  variants: {
    variant: {
      default: "bg-primary text-primary-foreground ...",
      destructive: "bg-destructive text-white ...",
      outline: "border bg-background ...",
    },
    size: {
      default: "h-9 px-4 py-2",
      sm: "h-8 px-3",
      lg: "h-10 px-6",
    },
  },
  defaultVariants: { variant: "default", size: "default" },
})
```

Blazor 변환:

```csharp
// 1. Enum 정의
public enum ButtonVariant { Default, Destructive, Outline }
public enum ButtonSize { Default, Sm, Lg }

// 2. switch 매핑 (variant → .cn-* CSS 클래스)
private string VariantClass => Variant switch
{
    ButtonVariant.Default => "cn-button-variant-default",
    ButtonVariant.Destructive => "cn-button-variant-destructive",
    ButtonVariant.Outline => "cn-button-variant-outline",
    _ => "cn-button-variant-default"
};

// 3. CSS는 shadcn.src.css에 정의
// .cn-button-variant-default { @apply bg-primary text-primary-foreground ...; }
```

**핵심**: cva의 variant 문자열은 Blazor에서 CSS 클래스로 분리.
인라인 Tailwind 유틸리티를 razor에 넣지 말고, `shadcn.src.css`의 `.cn-*` 클래스에 `@apply`로 정의.

---

## cn() → CssUtils.Cn() 변환

```tsx
// React
<div className={cn("p-4", className)} />
```

```razor
@* Blazor *@
<div class="@CssUtils.Cn("p-4", Class)" />
```

조건부:
```tsx
// React
cn("base", isActive && "active", disabled && "opacity-50")
```

```razor
@* Blazor *@
CssUtils.Cn("base", CssUtils.CnIf(isActive, "active"), CssUtils.CnIf(disabled, "opacity-50"))
```

---

## 새 컴포넌트 구현 워크플로우

1. **원본 확인**: shadcn/ui GitHub에서 `{component}.tsx` 소스 읽기
2. **구조 분석**: 어떤 Radix 프리미티브를 사용하는지 파악
3. **CSS 추출**: `cva()` 정의와 Tailwind 클래스를 `shadcn.src.css`에 `.cn-*`로 옮기기
4. **HTML 구조 복제**: JSX → Razor 마크업 변환 (data-slot, aria-*, role 유지)
5. **상태 로직 변환**: React hooks → Blazor Parameter/EventCallback/CascadingValue
6. **JS 필요 여부 판단**: focus trap, scroll lock 등 → `RnUIInteropService` 사용
7. **테스트 작성**: bunit으로 렌더링/상태/접근성 검증

---

## 금지 사항

- shadcn/ui에 없는 variant/size 임의 추가 (사용자 요청 시에만)
- React 패턴 그대로 옮기기 (hooks, JSX 조건부 렌더링 패턴 등)
- Radix UI npm 패키지 의존성 추가
- shadcn/ui의 `@/lib/utils`의 `cn()` 함수를 JS로 구현 (C# `CssUtils.Cn()` 사용)
