# Sync Strategy: Component-First Fix 원칙

## 핵심 원칙

> **블록의 디자인 차이를 발견하면, 그 원인이 컴포넌트인지 블록인지 먼저 분류하라.**
> **컴포넌트 원인이면 컴포넌트를 먼저 고치고, 블록은 그 다음이다.**

비유: 벽지(블록)에 금이 갔을 때, 벽 구조(컴포넌트)가 문제인지 벽지 시공(블록 코드)이 문제인지 먼저 파악한다.
벽 구조가 틀어졌는데 벽지만 고치면 다른 방(다른 블록)에서도 같은 문제가 반복된다.

---

## 차이점 분류 기준

### Layer 1: 컴포넌트 레벨 차이 (먼저 수정)

RnUI 컴포넌트(`RnCard`, `RnButton` 등)의 CSS/HTML 구조가 shadcn/ui 원본 컴포넌트와 다른 경우.

**판별법**: 해당 차이가 이 컴포넌트를 사용하는 **모든 블록**에 영향을 주는가?
- YES → 컴포넌트 레벨 차이

**예시**:
- Card 루트에 `flex flex-col gap-6 py-6`이 빠져 있음 → 모든 Card 사용 블록에 영향
- Button의 variant CSS 클래스 차이 → 모든 Button 사용 블록에 영향
- Input의 focus ring 스타일 차이 → 모든 Input 사용 블록에 영향

**수정 위치**: `shadcn.src.css`의 `.cn-*` 클래스 또는 컴포넌트 `.razor` 파일

### Layer 2: 블록 레벨 차이 (컴포넌트 수정 후)

블록 자체의 레이아웃, 구성, Tailwind 클래스가 원본과 다른 경우.

**판별법**: 해당 차이가 **이 블록에만** 영향을 주는가?
- YES → 블록 레벨 차이

**예시**:
- 필드 간격 `gap-6` vs 원본 `gap-7`
- CardTitle에 불필요한 `text-2xl` 추가
- "Sign up" 링크의 텍스트 색상 누락
- 원본에 없는 Loading 스피너 추가

**수정 위치**: `BlockDemos/` 하위의 `.razor` 파일

### Layer 3: 미구현 컴포넌트로 인한 차이 (별도 판단)

원본이 사용하는 컴포넌트가 RnUI에 아직 없어서 `<div>` + Tailwind로 대체한 경우.

**판별법**: 원본의 `<FieldGroup>`, `<Field>` 등이 RnUI에서 `<div class="...">`로 대체되었는가?
- YES → 미구현 컴포넌트 차이

**선택지**:
- A) 컴포넌트를 먼저 구현 후 블록에 적용 (원본 충실도 높음)
- B) `<div>` + Tailwind로 동일 시각 결과 달성 (빠른 구현)
- 사용자에게 선택지를 제시하고 결정을 받는다

---

## 실행 순서 (반드시 준수)

```
1. 차이점 전체 목록 작성
2. 각 차이를 Layer 1 / Layer 2 / Layer 3으로 분류
3. Layer 1 (컴포넌트) 차이가 있으면:
   → /sync-shadcn {component} 로 컴포넌트 먼저 동기화
   → 컴포넌트 수정 후 블록을 다시 확인 (컴포넌트 수정으로 자동 해결된 차이 제외)
4. Layer 3 (미구현 컴포넌트) 차이가 있으면:
   → 사용자에게 구현 vs 대체 선택지 제시
5. Layer 2 (블록) 차이만 남으면:
   → 블록 코드 수정
```

---

## 금지 사항

- **컴포넌트 CSS 문제를 블록에서 `Class="..."` 오버라이드로 땜질하지 마라**
  - 다른 블록에서 같은 문제가 반복된다
  - 예: Card에 `gap-6`이 없는데 블록마다 `Class="flex flex-col gap-6"` 추가 → 금지

- **컴포넌트 수정 없이 블록만 고치고 "완료" 선언하지 마라**
  - 근본 원인을 남겨둔 채 증상만 치료하는 것

- **컴포넌트 수정의 파급 효과를 무시하지 마라**
  - Card CSS를 고치면 Card를 사용하는 모든 블록이 영향받음
  - 수정 전 영향 범위를 확인하고 사용자에게 알린다

---

## 리포트 출력 형식

차이점 리포트에서 각 항목 옆에 레이어를 명시:

```markdown
| 차이점 | 원본 | RnUI | 레이어 | 영향 범위 |
|--------|------|------|--------|-----------|
| Card 루트 flex/gap/py 누락 | `flex flex-col gap-6 py-6` | 없음 | **L1 컴포넌트** | 모든 Card 블록 |
| CardTitle text-2xl 추가 | 없음 | `Class="text-2xl"` | **L2 블록** | login-01만 |
| Field 컴포넌트 미구현 | `<Field>` | `<div>` | **L3 미구현** | Field 사용 블록 |
```

---

## ⚠️ 수정 후 빌드 검증 체크리스트 (필수)

CSS 또는 Razor 파일을 수정한 후 **반드시 아래 두 빌드를 모두 실행**한다.
하나라도 빠지면 브라우저에서 스타일이 적용되지 않는 버그가 발생한다.

```bash
# ✅ 라이브러리 CSS (.cn-* 컴포넌트 스타일)
cd src/Daeha.RnUI && npm run build:css

# ✅ 데모 CSS (Tailwind utility 클래스: gap-7, flex, text-sm 등)
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css

# ✅ .NET 빌드
dotnet build
```

### 왜 두 번 빌드하는가?

두 파이프라인은 **완전히 독립적**이다. 라이브러리 CSS에는 `.cn-card`, `.cn-button` 같은 컴포넌트 클래스가 들어가고, 데모 CSS에는 `gap-7`, `flex`, `text-center` 같은 Tailwind utility 클래스가 들어간다. Razor 파일에 새 utility 클래스를 추가하면 데모 Tailwind가 이를 스캔해서 포함시켜야 한다.

> 상세: `.agents/knowledge/rnui-css-architecture.md` > "듀얼 빌드 필수"
