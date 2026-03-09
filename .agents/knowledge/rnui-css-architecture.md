# RnUI CSS 아키텍처

## 개요

RnUI는 두 개의 CSS 파이프라인이 존재한다. 아래 흐름을 반드시 이해하고 수정해야 한다.

---

## CSS 파일 역할 및 흐름

```
[라이브러리]
src/Daeha.RnUI/wwwroot/css/shadcn.src.css   ← 유일한 수정 대상
         │
         │  npm run build:css (Tailwind 컴파일)
         ▼
src/Daeha.RnUI/wwwroot/css/shadcn.css       ← 컴파일 결과물 (커밋 대상)
         │
         │  Blazor Static Web Asset (_content/Daeha.RnUI/css/shadcn.css)
         ▼
[데모 브라우저] + [NuGet 설치 사용자 브라우저]
```

```
[데모 프로젝트]
src/Daeha.RnUI.Demo.Wasm/wwwroot/input.css  ← Tailwind utility scan 용도만
         │
         │  npm run watch:css
         ▼
src/Daeha.RnUI.Demo.Wasm/wwwroot/css/tailwindcss.css
```

---

## 핵심 원칙

### ✅ 라이브러리가 Source of Truth

| 항목 | 위치 | 설명 |
|---|---|---|
| 디자인 토큰 (`@theme`) | `shadcn.src.css` | 라이트모드 기본 CSS 변수 |
| 다크모드 (`dark`) | `shadcn.src.css` | 다크모드 오버라이드 |
| 컴포넌트 CSS (`.cn-*`) | `shadcn.src.css` | 모든 컴포넌트 스타일 |

### ❌ 데모 `input.css`는 오버라이드 전용

`input.css`는 다음 역할만 담당한다:
1. `@import "tailwindcss"` — Tailwind utility 클래스 생성
2. `@source` — `.razor` 파일 클래스 스캔
3. **라이브러리 기본값을 바꾸고 싶을 때만** `@theme` 혹은 `.dark` 내용 추가

---

## 수정 규칙

### 테마 색상 수정 시
```
반드시 shadcn.src.css "만" 수정할 것
input.css는 절대 수정하지 않는다 (중복 발생)
```

### CSS 변수 추가 시

**라이트모드 기본값** → `shadcn.src.css`의 `@theme {}` 블록  
**다크모드 오버라이드** → `shadcn.src.css`의 `.dark {}` 블록

```css
/* ✅ 올바른 방법 - shadcn.src.css */
@theme {
  --color-border-subtle: color-mix(in oklch, var(--color-border) 50%, transparent);
}

.dark {
  --color-border: oklch(0.38 0 0);
  --color-border-subtle: color-mix(in oklch, var(--color-border) 100%, transparent);
}
```

### 컴포넌트 border에 subtle 색상 적용 시

```css
/* ✅ 올바른 방법 - var(--color-border-subtle) 사용 */
.cn-card {
  border: 1px solid var(--color-border-subtle);
}

/* ❌ 잘못된 방법 - Tailwind border-b는 --color-border 100% 적용 */
.cn-card {
  @apply border-b;
}
```

---

## NuGet 배포 동작 방식

NuGet 설치 사용자가 `_content/Daeha.RnUI/css/shadcn.css`를 로드하면:
- `:root`에 모든 `--color-*` 기본값 자동 적용
- `.dark` 클래스 시 다크모드 오버라이드 자동 적용
- 사용자가 별도 CSS 설정 없이도 컴포넌트가 동작함

사용자가 테마를 커스터마이징하려면 자신의 CSS에:
```css
@theme {
  --color-primary: oklch(0.6 0.2 250); /* 브랜드 색상 오버라이드 */
}
```

---

## 현재 정의된 공통 CSS 변수

| 변수 | 라이트모드 | 다크모드 | 용도 |
|---|---|---|---|
| `--color-border-subtle` | `color-mix(border 50%, transparent)` | `color-mix(border 100%, transparent)` | Accordion, Table, Card 구분선 |

---

## 자주 하는 실수

1. **`input.css`에서 색상 수정** → `shadcn.src.css`의 변경이 데모에 반영 안 됨
2. **`shadcn.css`를 직접 수정** → 빌드 시 덮어써짐. `.src.css`만 수정할 것
3. **`border-b` Tailwind 클래스 사용** → `--color-border` 100% 적용. `--color-border-subtle`이 필요하면 명시적 `border: 1px solid var(--color-border-subtle)` 사용
