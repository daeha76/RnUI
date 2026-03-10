---
trigger: always_on
---

# Animation & Transition Patterns

커스텀 컴포넌트에서 애니메이션/트랜지션을 추가할 때의 규칙.
기존 shadcn/ui 애니메이션 패턴과 일관성을 유지한다.

---

## 기본 원칙

1. **CSS 애니메이션 우선**: JS 애니메이션 대신 CSS `@keyframes` + `animation` 사용.
2. **Tailwind 유틸리티 활용**: `transition-all`, `duration-200`, `ease-in-out` 등.
3. **`data-state` 기반 트리거**: `data-[state=open]:animate-in`, `data-[state=closed]:animate-out`.
4. **최소 움직임 존중**: `prefers-reduced-motion` 미디어 쿼리 고려.

---

## 기존 애니메이션 정의 (@keyframes)

`shadcn.src.css`에 이미 정의된 애니메이션:

```css
/* Accordion 열기/닫기 */
@keyframes accordion-down { from { height: 0; } to { height: var(--radix-accordion-content-height); } }
@keyframes accordion-up { from { height: var(--radix-accordion-content-height); } to { height: 0; } }

/* Collapsible 열기/닫기 */
@keyframes collapsible-down { from { height: 0; } to { height: var(--radix-collapsible-content-height); } }
@keyframes collapsible-up { from { height: var(--radix-collapsible-content-height); } to { height: 0; } }
```

---

## 트랜지션 패턴

### 1. 단순 호버/포커스 트랜지션

```css
.cn-{name} {
  @apply transition-colors;  /* 색상만 트랜지션 */
  /* 또는 */
  @apply transition-all;     /* 모든 속성 트랜지션 */
}
```

### 2. 열기/닫기 애니메이션 (data-state 기반)

```css
/* Tailwind animate 유틸리티 사용 */
.cn-{name}-content {
  @apply data-[state=open]:animate-in data-[state=closed]:animate-out
         data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0;
}
```

사용 가능한 animate 유틸리티:

| 클래스 | 효과 |
|------|------|
| `animate-in` / `animate-out` | 진입/퇴장 애니메이션 컨테이너 |
| `fade-in-0` / `fade-out-0` | 투명도 0에서/으로 |
| `zoom-in-95` / `zoom-out-95` | 스케일 95%에서/으로 |
| `slide-in-from-top-2` | 위에서 슬라이드 진입 |
| `slide-in-from-bottom-2` | 아래에서 슬라이드 진입 |
| `slide-in-from-left-2` | 왼쪽에서 슬라이드 진입 |
| `slide-in-from-right-2` | 오른쪽에서 슬라이드 진입 |
| `slide-out-to-*` | 해당 방향으로 슬라이드 퇴장 |

### 3. 높이 애니메이션 (접기/펼치기)

커스텀 `@keyframes` + CSS 변수:

```css
@keyframes {name}-expand {
  from { height: 0; opacity: 0; }
  to { height: var(--{name}-content-height); opacity: 1; }
}
@keyframes {name}-collapse {
  from { height: var(--{name}-content-height); opacity: 1; }
  to { height: 0; opacity: 0; }
}
```

Blazor에서 JS Interop으로 높이 변수 설정:

```csharp
// OnAfterRenderAsync에서 실제 높이를 CSS 변수로 설정
await Interop.SetCssVariableAsync(_contentRef, "--{name}-content-height", $"{height}px");
```

### 4. 오버레이 애니메이션 (Dialog, Sheet, Drawer)

```css
/* 오버레이 배경 */
.cn-{name}-overlay {
  @apply data-[state=open]:animate-in data-[state=closed]:animate-out
         data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0;
}

/* 콘텐츠 — 방향에 따라 슬라이드 */
.cn-{name}-content {
  @apply data-[state=open]:animate-in data-[state=closed]:animate-out
         data-[state=closed]:fade-out-0 data-[state=open]:fade-in-0
         data-[state=closed]:zoom-out-95 data-[state=open]:zoom-in-95;
}
```

---

## 새 @keyframes 추가 규칙

`shadcn.src.css`에 추가할 때:

```css
/* 1. @keyframes는 파일 상단 (@theme 블록 아래)에 모아둔다 */
@keyframes {name}-{action} {
  from { /* 시작 상태 */ }
  to { /* 끝 상태 */ }
}

/* 2. @theme에 커스텀 animate 토큰 등록 */
@theme {
  --animate-{name}-{action}: {name}-{action} 0.2s ease-out;
}

/* 3. 컴포넌트 CSS에서 사용 */
.cn-{name}[data-state="open"] {
  animation: var(--animate-{name}-{action});
}
```

### 타이밍 가이드라인

| 용도 | 시간 | easing |
|------|------|--------|
| 호버 효과 | `150ms` | `ease-in-out` |
| 토글/스위치 | `150ms` | `ease-in-out` |
| 드롭다운 열기 | `200ms` | `ease-out` |
| 모달 열기 | `200ms` | `ease-out` |
| 모달 닫기 | `150ms` | `ease-in` |
| 높이 확장 | `200ms-300ms` | `ease-out` |
| 페이지 트랜지션 | `300ms` | `ease-in-out` |

---

## Reduced Motion 대응

```css
/* 움직임 감소 설정이 켜진 사용자를 위해 */
@media (prefers-reduced-motion: reduce) {
  .cn-{name} {
    animation-duration: 0.01ms !important;
    transition-duration: 0.01ms !important;
  }
}
```

Tailwind v4에서는 `motion-reduce:` 접두사 사용 가능:

```css
.cn-{name} {
  @apply transition-all motion-reduce:transition-none;
}
```

---

## 스피너/로딩 애니메이션

```css
@keyframes spin {
  to { transform: rotate(360deg); }
}

.cn-spinner {
  @apply animate-spin;  /* Tailwind 내장 */
}
```

### 펄스 (Skeleton 로딩)

```css
.cn-skeleton {
  @apply animate-pulse;  /* Tailwind 내장 */
}
```

---

## 금지 사항

- JS `setInterval`/`requestAnimationFrame`으로 CSS 애니메이션 대체
- 500ms 이상의 긴 애니메이션 (사용자 경험 저하)
- `transform: scale()` + `position: fixed` 조합 (레이아웃 버그 유발)
- 애니메이션에 `!important` 사용
- `prefers-reduced-motion` 미고려
