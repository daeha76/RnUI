---
trigger: always_on
---

# CSS & Theming Rules

## 수정 대상 파일 (하나만 기억)

**`src/Daeha.RnUI/wwwroot/css/shadcn.src.css`** — 이것만 수정.

| 파일 | 역할 | 수정 가능 |
|------|------|-----------|
| `shadcn.src.css` | 디자인 토큰 + 컴포넌트 CSS (Source of Truth) | **YES** |
| `shadcn.css` | 컴파일 결과물 | **NO** (빌드 시 덮어씌워짐) |
| `input.css` (데모) | Tailwind utility 스캔 + 데모 전용 오버라이드 | 오버라이드만 |

상세 아키텍처: `.agents/knowledge/rnui-css-architecture.md` 참조.

---

## shadcn.src.css 파일 구조

```css
@import "tailwindcss";

/* 1. @theme {} — 디자인 토큰 (라이트모드 기본값) */
@theme {
  --color-background: oklch(1 0 0);
  --color-primary: oklch(0.205 0 0);
  /* ... */
}

/* 2. @keyframes — 애니메이션 정의 */
@keyframes accordion-down { ... }

/* 3. .dark — 다크모드 오버라이드 */
.dark, [data-theme="dark"], [data-theme="dark-sky"] {
  --color-background: oklch(0.11 0 0);
  /* ... */
}

/* 4. [data-theme="light-sky"] — 테마 변형 */

/* 5. @layer base — 전역 기본 스타일 */

/* 6. 컴포넌트 CSS — .cn-{name} 클래스들 */
.cn-button { ... }
.cn-button-variant-default { ... }
```

---

## 색상 시스템: oklch()

모든 색상은 **oklch(L C H)** 색공간 사용 (Tailwind v4 기본).

```
oklch(밝기  채도  색상각)
       0~1  0~0.4  0~360
```

- 무채색(gray 계열): `oklch(0.922 0 0)` — 채도 0, 색상각 0
- 유채색(primary 등): `oklch(0.588 0.158 241.966)` — sky-600
- 반투명: `oklch(0.205 0 0 / 0.9)` 또는 Tailwind `bg-primary/90`

### 라이트↔다크 변환 규칙

| 토큰 | 라이트 | 다크 | 패턴 |
|------|--------|------|------|
| background | 밝음 (L≈1) | 어두움 (L≈0.11) | 반전 |
| foreground | 어두움 (L≈0.145) | 밝음 (L≈0.985) | 반전 |
| primary | 어두움 | 밝음 | 반전 |
| muted | 밝음 (L≈0.965) | 어두움 (L≈0.269) | 반전 |
| border | 밝음 (L≈0.922) | 어두움 (L≈0.269) | 반전 |
| destructive | 진한 빨강 | 어두운 빨강 | 밝기만 조정 |

---

## 디자인 토큰 전체 목록

### 핵심 토큰 (모든 컴포넌트 공통)

| 토큰 | 용도 |
|------|------|
| `--color-background` / `--color-foreground` | 페이지 배경/텍스트 |
| `--color-card` / `--color-card-foreground` | 카드 배경/텍스트 |
| `--color-popover` / `--color-popover-foreground` | 팝오버/드롭다운 배경/텍스트 |
| `--color-primary` / `--color-primary-foreground` | 주요 액션 (버튼, 링크) |
| `--color-secondary` / `--color-secondary-foreground` | 보조 액션 |
| `--color-muted` / `--color-muted-foreground` | 비활성/보조 텍스트 |
| `--color-accent` / `--color-accent-foreground` | hover/focus 강조 |
| `--color-destructive` / `--color-destructive-foreground` | 위험/삭제 |
| `--color-border` | 기본 테두리 |
| `--color-border-subtle` | 50% 투명 테두리 (카드, 테이블 구분선) |
| `--color-input` | input/select 테두리 |
| `--color-ring` | focus ring |

### Sidebar 토큰

| 토큰 | 용도 |
|------|------|
| `--color-sidebar` / `--color-sidebar-foreground` | 사이드바 배경/텍스트 |
| `--color-sidebar-primary` / `--color-sidebar-primary-foreground` | 사이드바 주요 액션 |
| `--color-sidebar-accent` / `--color-sidebar-accent-foreground` | 사이드바 hover 강조 |
| `--color-sidebar-border` / `--color-sidebar-ring` | 사이드바 테두리/ring |

### 기타 토큰

| 토큰 | 용도 |
|------|------|
| `--radius-lg` / `--radius-md` / `--radius-sm` | border-radius |
| `--font-sans` / `--font-mono` | 폰트 패밀리 |
| `--animate-accordion-down` / `--animate-accordion-up` | 애니메이션 |

---

## 컴포넌트 CSS 작성 패턴

### 네이밍 규칙

```css
/* 베이스 클래스 */
.cn-{name} { ... }

/* Variant (있을 때만) */
.cn-{name}-variant-{variant} { ... }

/* Size (있을 때만) */
.cn-{name}-size-{size} { ... }

/* 서브 컴포넌트 */
.cn-{name}-{sub} { ... }
/* 예: .cn-card-header, .cn-card-title, .cn-dialog-overlay */
```

### @apply 사용 원칙

```css
/* ✅ 올바른 사용: Tailwind 유틸리티를 @apply로 조합 */
.cn-badge {
  @apply inline-flex items-center justify-center rounded-full px-2 py-0.5 text-xs font-medium;
}

/* ✅ 다크모드 변형: dark: 접두사 사용 */
.cn-button-variant-outline {
  @apply border bg-background hover:bg-accent dark:border-input dark:bg-input/30;
}

/* ✅ CSS 변수가 필요한 경우: var() 직접 사용 */
.cn-card {
  @apply rounded-xl bg-card shadow-sm;
  border: 1px solid var(--color-border-subtle);
}

/* ❌ 잘못된 사용: 인라인 스타일처럼 과도한 커스텀 값 */
.cn-card {
  border-color: rgba(0, 0, 0, 0.1); /* oklch 토큰 사용해야 함 */
}
```

### 자주 사용하는 Tailwind 패턴

```css
/* focus-visible ring (모든 인터랙티브 요소) */
focus-visible:border-ring focus-visible:ring-[3px] focus-visible:ring-ring/50

/* disabled 상태 */
disabled:pointer-events-none disabled:opacity-50

/* aria-invalid (form 요소) */
aria-invalid:border-destructive aria-invalid:ring-destructive/20
dark:aria-invalid:ring-destructive/40

/* SVG 아이콘 크기 자동 조정 */
[&_svg]:pointer-events-none [&_svg]:shrink-0 [&_svg:not([class*='size-'])]:size-4

/* data-state 기반 스타일링 */
data-[state=open]:animate-in data-[state=closed]:animate-out
```

---

## 테마 변형 추가 방법

새 색상 테마를 추가할 때 `shadcn.src.css`에:

```css
/* 라이트 변형 — 기본 라이트 위에 primary 계열만 오버라이드 */
[data-theme="light-{color}"] {
  --color-primary: oklch(...);
  --color-primary-foreground: oklch(...);
  --color-ring: oklch(...);
  --color-sidebar-primary: oklch(...);
  --color-sidebar-primary-foreground: oklch(...);
}

/* 다크 변형 — .dark 블록의 셀렉터에 추가 + 별도 블록으로 오버라이드 */
/* 1. .dark 셀렉터에 [data-theme="dark-{color}"] 추가 (다크 기본값 상속) */
/* 2. 아래에 [data-theme="dark-{color}"] 블록으로 primary 계열 오버라이드 */
[data-theme="dark-{color}"] {
  --color-primary: oklch(...);
  --color-primary-foreground: oklch(...);
  --color-ring: oklch(...);
  --color-sidebar-primary: oklch(...);
  --color-sidebar-primary-foreground: oklch(...);
}
```

---

## border-subtle vs border 사용 기준

| 상황 | 사용 | 이유 |
|------|------|------|
| Card 외곽선 | `var(--color-border-subtle)` | 은은한 구분 |
| Table 행 구분 | `var(--color-border-subtle)` | 과하지 않은 구분선 |
| Accordion 구분 | `var(--color-border-subtle)` | 은은한 구분 |
| Input/Select 테두리 | `border-input` (Tailwind) | 명확한 입력 영역 표시 |
| Separator | `border-border` (Tailwind) | 명확한 섹션 구분 |

```css
/* ✅ subtle: var() 직접 사용 */
.cn-card { border: 1px solid var(--color-border-subtle); }

/* ✅ 일반: Tailwind @apply 사용 */
.cn-input { @apply border border-input; }

/* ❌ 잘못된 방법: Tailwind border-b로 subtle 효과 시도 */
.cn-card { @apply border; }  /* border-border 100% 적용됨 */
```

---

## 빌드 후 확인 사항

CSS 수정 후 반드시:
1. `cd src/Daeha.RnUI && npm run build:css` 실행
2. 라이트모드 / 다크모드 모두 확인
3. 테마 변형(sky 등)이 있으면 해당 테마도 확인
