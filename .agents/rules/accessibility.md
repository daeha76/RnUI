---
trigger: always_on
---

# Accessibility Rules

## 원칙

shadcn/ui가 Radix UI 위에서 제공하는 접근성을 RnUI에서도 동일하게 보장한다.
WAI-ARIA 1.2 패턴을 따른다.

---

## 컴포넌트별 필수 ARIA

### Dialog / AlertDialog / Sheet / Drawer (모달 오버레이)

```html
<div role="dialog" aria-modal="true" aria-labelledby="{id}-title">
```

| 속성 | 값 | 필수 |
|------|---|------|
| `role` | `"dialog"` 또는 `"alertdialog"` | YES |
| `aria-modal` | `"true"` | YES |
| `aria-labelledby` | title 요소의 id | YES |
| `aria-describedby` | description 요소의 id | 권장 |

Close 버튼: `<span class="sr-only">Close</span>` 필수.

### Switch / Checkbox

```html
<button role="switch" aria-checked="true" data-state="checked">
```

| 속성 | 값 |
|------|---|
| `role` | `"switch"` 또는 `"checkbox"` |
| `aria-checked` | `"true"` / `"false"` (bool → 소문자 문자열) |
| `data-state` | `"checked"` / `"unchecked"` |

### Accordion

```html
<button aria-expanded="true" aria-controls="{item-id}-content">
<div id="{item-id}-content" role="region" aria-labelledby="{item-id}-trigger">
```

### Tabs

```html
<div role="tablist">
  <button role="tab" aria-selected="true" aria-controls="panel-{id}">
</div>
<div role="tabpanel" id="panel-{id}" aria-labelledby="tab-{id}">
```

### DropdownMenu / ContextMenu

```html
<div role="menu">
  <div role="menuitem">
  <div role="menuitemcheckbox" aria-checked="true">
  <div role="menuitemradio" aria-checked="true">
  <div role="separator">
</div>
```

### Alert

```html
<div role="alert">  <!-- 또는 role="status" -->
```

### Progress

```html
<div role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">
```

### Tooltip

```html
<div role="tooltip" id="{id}">
<!-- 트리거 요소에 aria-describedby="{id}" -->
```

---

## data-state 패턴

상태를 가진 컴포넌트는 CSS 애니메이션/스타일링을 위해 `data-state`를 사용한다.

```razor
data-state="@(Open ? "open" : "closed")"
data-state="@(Checked ? "checked" : "unchecked")"
data-state="@(IsExpanded ? "open" : "closed")"
data-state="@(Active ? "active" : "inactive")"
```

CSS에서 활용:
```css
[data-state="open"] { ... }
[data-state="closed"] { ... }
```

---

## 키보드 네비게이션

### 모든 인터랙티브 요소

- **Tab**: 다음 포커스 가능 요소로 이동
- **Shift+Tab**: 이전 포커스 가능 요소로 이동
- **Enter/Space**: 활성화

### 모달 (Dialog, Sheet, Drawer)

- **Escape**: 닫기 (`RnUIInteropService.AddEscapeListenerAsync`)
- **Tab**: 모달 내부에서만 순환 (`RnUIInteropService.EnableFocusTrapAsync`)
- 열릴 때: 첫 번째 포커스 가능 요소로 자동 포커스
- 닫힐 때: 트리거 요소로 포커스 복원 (가능하면)

### Accordion

- **Enter/Space**: 열기/닫기
- **ArrowDown/ArrowUp**: 다음/이전 item으로 포커스 (선택적)

### Tabs

- **ArrowLeft/ArrowRight**: 탭 간 이동
- **Home/End**: 첫/마지막 탭

---

## Focus Visible Ring

모든 인터랙티브 요소에 focus-visible 스타일 필수:

```css
focus-visible:border-ring focus-visible:ring-[3px] focus-visible:ring-ring/50
```

마우스 클릭 시에는 ring이 보이지 않고, 키보드 Tab 시에만 보인다.

---

## sr-only (Screen Reader Only)

시각적으로 숨기지만 스크린 리더는 읽는 텍스트:

```razor
<span class="sr-only">Close</span>
<span class="sr-only">Toggle navigation</span>
<span class="sr-only">Loading...</span>
```

사용 시점:
- 아이콘 전용 버튼 (X 닫기, 햄버거 메뉴 등)
- 장식용 아이콘 옆 설명
- 진행 상태 알림

---

## disabled 접근성

```razor
@* HTML disabled 속성: null이면 렌더링 안 됨 *@
disabled="@(Disabled ? true : (object?)null)"

@* CSS disabled 스타일 *@
disabled:pointer-events-none disabled:opacity-50

@* 이벤트 핸들러 가드 *@
if (Disabled) return;

@* 그룹 disabled 전파 *@
data-disabled="@Disabled.ToString().ToLower()"
group-data-[disabled=true]:pointer-events-none
group-data-[disabled=true]:opacity-50
```

---

## 체크리스트 (컴포넌트 완성 전 확인)

- [ ] 적절한 `role` 속성이 있는가
- [ ] `aria-*` 속성이 동적으로 업데이트되는가
- [ ] `data-state`가 상태 변화를 반영하는가
- [ ] 키보드만으로 모든 기능을 사용할 수 있는가
- [ ] focus-visible ring이 보이는가
- [ ] 아이콘 전용 버튼에 `sr-only` 텍스트가 있는가
- [ ] disabled 상태에서 인터랙션이 차단되는가
- [ ] 모달은 focus trap과 Escape 닫기가 동작하는가
