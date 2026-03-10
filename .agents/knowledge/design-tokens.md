# Design Tokens Reference

## 색상 토큰 (oklch 색공간)

### Light Mode (기본값, @theme 블록)

| 토큰 | oklch 값 | 용도 |
|------|---------|------|
| `--color-background` | `oklch(1 0 0)` | 페이지 배경 (흰색) |
| `--color-foreground` | `oklch(0.145 0 0)` | 기본 텍스트 (거의 검정) |
| `--color-card` | `oklch(1 0 0)` | 카드 배경 |
| `--color-card-foreground` | `oklch(0.145 0 0)` | 카드 텍스트 |
| `--color-popover` | `oklch(1 0 0)` | 팝오버 배경 |
| `--color-popover-foreground` | `oklch(0.145 0 0)` | 팝오버 텍스트 |
| `--color-primary` | `oklch(0.205 0 0)` | 주요 액션 (어두운 회색) |
| `--color-primary-foreground` | `oklch(0.985 0 0)` | 주요 액션 텍스트 |
| `--color-secondary` | `oklch(0.965 0 0)` | 보조 액션 배경 |
| `--color-secondary-foreground` | `oklch(0.205 0 0)` | 보조 액션 텍스트 |
| `--color-muted` | `oklch(0.965 0 0)` | 비활성 배경 |
| `--color-muted-foreground` | `oklch(0.556 0 0)` | 비활성 텍스트 |
| `--color-accent` | `oklch(0.965 0 0)` | hover/focus 강조 배경 |
| `--color-accent-foreground` | `oklch(0.205 0 0)` | hover/focus 텍스트 |
| `--color-destructive` | `oklch(0.577 0.245 27.325)` | 위험/삭제 (빨강) |
| `--color-destructive-foreground` | `oklch(0.985 0 0)` | 위험 텍스트 |
| `--color-border` | `oklch(0.922 0 0)` | 기본 테두리 |
| `--color-border-subtle` | `color-mix(border 50%, transparent)` | 은은한 테두리 |
| `--color-input` | `oklch(0.922 0 0)` | input 테두리 |
| `--color-ring` | `oklch(0.708 0 0)` | focus ring |

### Dark Mode (.dark 블록)

| 토큰 | oklch 값 | 변화 |
|------|---------|------|
| `--color-background` | `oklch(0.11 0 0)` | 밝기 1→0.11 |
| `--color-foreground` | `oklch(0.985 0 0)` | 밝기 0.145→0.985 |
| `--color-card` | `oklch(0.145 0 0)` | |
| `--color-primary` | `oklch(0.985 0 0)` | 반전 |
| `--color-primary-foreground` | `oklch(0.205 0 0)` | 반전 |
| `--color-secondary` | `oklch(0.269 0 0)` | |
| `--color-muted` | `oklch(0.269 0 0)` | |
| `--color-muted-foreground` | `oklch(0.708 0 0)` | |
| `--color-accent` | `oklch(0.269 0 0)` | |
| `--color-destructive` | `oklch(0.396 0.141 25.723)` | 어두운 빨강 |
| `--color-destructive-foreground` | `oklch(0.637 0.237 25.331)` | |
| `--color-border` | `oklch(0.269 0 0)` | |
| `--color-border-subtle` | `color-mix(border 100%, transparent)` | 다크에서는 100% |
| `--color-input` | `oklch(0.269 0 0)` | |
| `--color-ring` | `oklch(0.439 0 0)` | |

### Sidebar 토큰

| 토큰 | Light | Dark |
|------|-------|------|
| `--color-sidebar` | `oklch(0.985 0 0)` | `oklch(0.175 0 0)` |
| `--color-sidebar-foreground` | `oklch(0.145 0 0)` | `oklch(0.985 0 0)` |
| `--color-sidebar-primary` | `oklch(0.205 0 0)` | `oklch(0.985 0 0)` |
| `--color-sidebar-primary-foreground` | `oklch(0.985 0 0)` | `oklch(0.205 0 0)` |
| `--color-sidebar-accent` | `oklch(0.965 0 0)` | `oklch(0.269 0 0)` |
| `--color-sidebar-accent-foreground` | `oklch(0.205 0 0)` | `oklch(0.985 0 0)` |
| `--color-sidebar-border` | `oklch(0.922 0 0)` | `oklch(0.269 0 0)` |
| `--color-sidebar-ring` | `oklch(0.708 0 0)` | `oklch(0.439 0 0)` |

---

## 테마 변형

### light-sky (Sky Blue 라이트)

| 토큰 | 값 | Tailwind 대응 |
|------|---|---------------|
| `--color-primary` | `oklch(0.588 0.158 241.966)` | sky-600 |
| `--color-primary-foreground` | `oklch(1 0 0)` | white |
| `--color-ring` | `oklch(0.685 0.169 237.323)` | sky-500 |
| `--color-sidebar-primary` | `oklch(0.588 0.158 241.966)` | sky-600 |
| `--color-sidebar-primary-foreground` | `oklch(1 0 0)` | white |

### dark-sky (Sky Blue 다크)

| 토큰 | 값 | Tailwind 대응 |
|------|---|---------------|
| `--color-primary` | `oklch(0.755 0.152 232.661)` | sky-400 |
| `--color-primary-foreground` | `oklch(0.145 0 0)` | near-black |
| `--color-ring` | `oklch(0.685 0.169 237.323)` | sky-500 |
| `--color-sidebar-primary` | `oklch(0.685 0.169 237.323)` | sky-500 |
| `--color-sidebar-primary-foreground` | `oklch(0.145 0 0)` | near-black |

---

## 기타 토큰

### Border Radius

| 토큰 | 값 |
|------|---|
| `--radius-lg` | `0.5rem` |
| `--radius-md` | `calc(var(--radius-lg) - 2px)` |
| `--radius-sm` | `calc(var(--radius-lg) - 4px)` |

### Font

| 토큰 | 값 |
|------|---|
| `--font-sans` | `"Geist", ui-sans-serif, system-ui, sans-serif` |
| `--font-mono` | `"Geist Mono", ui-monospace, monospace` |

### Animation

| 토큰 | 값 |
|------|---|
| `--animate-accordion-down` | `accordion-down 0.2s ease-out` |
| `--animate-accordion-up` | `accordion-up 0.2s ease-out` |

---

## 다크모드 적용 방식

```html
<!-- 방법 1: class -->
<html class="dark">

<!-- 방법 2: data-theme -->
<html data-theme="dark">
<html data-theme="dark-sky">
<html data-theme="light-sky">
```

CSS 셀렉터 우선순위:
```css
.dark,
[data-theme="dark"],
[data-theme="dark-sky"] {
  /* 다크 기본값 */
}

[data-theme="dark-sky"] {
  /* sky 오버라이드 — 위보다 나중에 선언 → 동일 specificity에서 승리 */
}
```
