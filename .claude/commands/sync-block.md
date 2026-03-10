# Sync Block with shadcn/ui

shadcn/ui 원본 블록과 RnUI 블록 구현을 비교하여 차이점을 분석합니다.

> **반드시 `.agents/knowledge/sync-strategy.md`의 Component-First Fix 원칙을 따릅니다.**

## 입력

$ARGUMENTS 에 블록 이름이 들어옵니다 (예: `login-01`, `sidebar-05`).

## 실행 절차

### Phase 1: 원본 수집

1. **shadcn/ui 원본 fetch**: shadcn/ui GitHub에서 해당 블록 소스를 가져옵니다.
   - URL: `https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/blocks/{block-name}`
   - `page.tsx`와 `components/` 폴더의 모든 파일을 읽습니다.

2. **원본에서 사용하는 컴포넌트 소스도 fetch**: 블록이 사용하는 shadcn/ui 컴포넌트(Card, Button, Input 등)의 원본 소스도 가져옵니다.
   - URL: `https://raw.githubusercontent.com/shadcn-ui/ui/main/apps/v4/registry/new-york-v4/ui/{component}.tsx`

3. **RnUI 현재 구현 읽기**: 카테고리와 번호를 추출하여 해당 파일을 읽습니다.
   - `login-01` → `BlockDemos/Login/Login01Demo.razor` + `Components/LoginForm01.razor`
   - `sidebar-05` → `BlockDemos/Sidebar/Sidebar05Demo.razor` + `Components/AppSidebar05.razor`
   - 구현이 없으면 "미구현" 으로 표시합니다.

4. **RnUI 컴포넌트 CSS 읽기**: `shadcn.src.css`에서 해당 컴포넌트의 `.cn-*` 클래스를 읽습니다.

### Phase 2: 차이점 분류 (Component-First Fix 원칙)

차이점을 발견하면 **반드시 레이어를 분류**합니다:

| 레이어 | 판별 기준 | 수정 대상 | 우선순위 |
|--------|-----------|-----------|----------|
| **L1 컴포넌트** | 이 컴포넌트를 사용하는 모든 블록에 영향 | `shadcn.src.css` 또는 `.razor` 컴포넌트 | 🔴 최우선 |
| **L2 블록** | 이 블록에만 영향 | `BlockDemos/` 하위 파일 | 🟡 컴포넌트 수정 후 |
| **L3 미구현** | 원본 컴포넌트가 RnUI에 없음 | 신규 컴포넌트 또는 div 대체 | 🟠 사용자 선택 |

### Phase 3: 리포트 출력

아래 항목을 비교하여 **레이어 태그와 함께** 차이를 알려줍니다:

- **레이아웃 구조**: 원본의 JSX 구조 vs RnUI의 Razor 구조
- **사용 컴포넌트**: 원본에서 사용한 컴포넌트가 RnUI에 모두 매핑되었는지
- **컴포넌트 CSS 차이**: 원본 컴포넌트의 className vs RnUI `.cn-*` 클래스 (L1)
- **누락된 UI 요소**: 원본에 있지만 RnUI 블록에 없는 요소 (L2)
- **Tailwind 클래스 차이**: 레이아웃/스타일 클래스 비교 (L1 또는 L2)
- **Form 처리**: 원본의 form 구조 vs Blazor EditForm 변환 확인
- **반응형**: 모바일/데스크톱 반응형 클래스 누락 여부

### Phase 4: 수정 가이드

1. **L1 차이가 있으면**: 컴포넌트 수정 코드를 제안하고, "이 컴포넌트를 먼저 수정해야 합니다" 명시
2. **L3 차이가 있으면**: 구현 vs 대체 선택지를 사용자에게 제시
3. **L2 차이만 남으면**: 블록 수정 코드를 제안

## 출력

차이점 분석 결과를 **레이어 분류 테이블** 형식으로 보여주고, 수정 순서를 명시합니다.
코드를 직접 수정하지 않습니다 — 분석 결과만 제공합니다.

## Phase 5: CSS 빌드 검증 (수정 적용 시 필수)

수정을 적용한 경우, **반드시 두 CSS 파이프라인을 모두 빌드**합니다:

```bash
# 1. 라이브러리 CSS (L1 컴포넌트 수정 시)
cd src/Daeha.RnUI && npm run build:css

# 2. 데모 CSS (L2 블록 수정 시 — 새 Tailwind utility 클래스 스캔 필수)
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css

# 3. dotnet build 확인
dotnet build
```

> ⚠️ **라이브러리 CSS만 빌드하면 데모에서 새 Tailwind 클래스가 적용되지 않습니다.**
> 참조: `.agents/knowledge/rnui-css-architecture.md` > "듀얼 빌드 필수"

## 금지

- L1 컴포넌트 차이를 블록의 `Class="..."` 오버라이드로 땜질하는 것
- 컴포넌트 원본 소스를 확인하지 않고 블록 차이만 분석하는 것
- 레이어 분류 없이 차이점을 나열하는 것
- **CSS/Razor 수정 후 라이브러리 CSS만 빌드하고 데모 CSS를 빌드하지 않는 것**
