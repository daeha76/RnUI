# Design Review

컴포넌트의 디자인 일관성, 접근성, CSS 품질을 리뷰합니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름이 들어옵니다 (예: `Button`, `Timeline`).

## 실행 절차

1. **소스 파일 읽기**: 해당 컴포넌트의 모든 파일을 읽습니다.
   - `src/Daeha.RnUI/Components/UI/{Name}/` 폴더 전체
   - `src/Daeha.RnUI/wwwroot/css/shadcn.src.css`에서 `.cn-{name}` 관련 CSS
   - `tests/Daeha.RnUI.Tests/Components/Rn{Name}Tests.cs`

2. **디자인 일관성 체크**: 아래 항목을 검사하고 결과를 보고합니다.

### Razor 체크리스트

| 항목 | 확인 |
|------|------|
| `data-slot` 속성 존재 | |
| `Rn` 접두사 네이밍 | |
| 필수 3 Parameter (ChildContent, Class, AdditionalAttributes) | |
| `@attributes="AdditionalAttributes"` 적용 | |
| `ComputedClass` 패턴 사용 | |
| `CssUtils.Cn()` 사용 | |
| Variant enum → switch 매핑 정확성 | |
| disabled 처리 패턴 (`(object?)null`) | |
| data-state 동적 바인딩 (상태 컴포넌트) | |

### CSS 체크리스트

| 항목 | 확인 |
|------|------|
| `.cn-{name}` 베이스 클래스 존재 | |
| variant/size 네이밍 규칙 준수 | |
| 디자인 토큰 사용 (하드코딩 색상 없음) | |
| `@apply` 활용 | |
| focus-visible ring 스타일 (인터랙티브 요소) | |
| disabled 스타일 | |
| 다크모드 대응 (`dark:` 접두사 또는 토큰 자동 전환) | |
| `border-subtle` 적절한 사용 | |
| 불필요한 `!important` 없음 | |

### 접근성 체크리스트

| 항목 | 확인 |
|------|------|
| 적절한 `role` 속성 | |
| `aria-*` 동적 업데이트 | |
| 키보드 네비게이션 가능 | |
| sr-only 텍스트 (아이콘 전용) | |
| focus trap (모달 컴포넌트) | |
| Escape 닫기 (오버레이 컴포넌트) | |

### 테스트 체크리스트

| 항목 | 확인 |
|------|------|
| 테스트 파일 존재 | |
| 기본 렌더링 테스트 | |
| data-slot 검증 | |
| ChildContent 렌더링 검증 | |
| Class 머징 검증 | |
| Variant 적용 검증 (해당 시) | |
| 이벤트 콜백 검증 (해당 시) | |

3. **결과 보고**: 체크리스트 결과를 표 형태로 보여주고, 문제가 있는 항목은 수정 제안을 합니다.

## 출력 형식

```
## 🔍 Design Review: Rn{Name}

### 요약
- ✅ 통과: N개
- ⚠️ 경고: N개
- ❌ 실패: N개

### 상세 결과
(체크리스트 표)

### 수정 제안
(문제 항목별 구체적 수정 방법)
```
