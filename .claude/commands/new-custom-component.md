# New Custom Component Scaffolding

shadcn/ui에 존재하지 않는 커스텀 RnUI 컴포넌트를 스캐폴딩합니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름이 들어옵니다 (예: `Timeline`, `Stepper`, `Rating`).
접두사 `Rn`은 자동으로 붙이므로 이름만 제공합니다.

## 실행 절차

1. **요구사항 확인**: 사용자에게 컴포넌트의 목적, 필요한 variant/size, 상태 관리 필요 여부를 확인합니다.

2. **유사 컴포넌트 조사**: 기존 RnUI 컴포넌트 중 가장 유사한 것을 찾아 참조합니다.
   - 유사 컴포넌트의 HTML 구조, CSS 패턴, Parameter 설계를 분석합니다.
   - `.agents/rules/custom-component-development.md`의 유사 컴포넌트 참조 표를 활용합니다.

3. **다른 UI 라이브러리 참고**: 필요 시 다른 UI 라이브러리의 동일 컴포넌트를 웹 검색합니다.
   - Material UI, Ant Design, Chakra UI 등에서 API/구조 참고
   - 단, CSS와 구현은 RnUI 패턴을 따릅니다.

4. **접근성 조사**: WAI-ARIA Authoring Practices에서 해당 패턴을 확인합니다.
   - URL: `https://www.w3.org/WAI/ARIA/apg/patterns/`
   - 적절한 role, aria-*, 키보드 네비게이션 정의

5. **컴포넌트 분류 결정**: 4가지 패턴 중 선택합니다.
   - Simple / Variant / Composite / Stateful
   - 사용자에게 선택 결과와 이유를 공유합니다.

6. **파일 생성**: 아래 파일을 순서대로 생성합니다.
   - `src/Daeha.RnUI/Components/UI/{Name}/Rn{Name}.razor` — 메인 컴포넌트
   - `src/Daeha.RnUI/Components/UI/{Name}/{Name}Enums.cs` — enum (필요 시)
   - 서브 컴포넌트 파일들 (composite일 때)
   - `tests/Daeha.RnUI.Tests/Components/Rn{Name}Tests.cs` — bunit 테스트

7. **CSS 추가**: `src/Daeha.RnUI/wwwroot/css/shadcn.src.css`에 `.cn-{name}` 관련 클래스를 추가합니다.
   - 기존 디자인 토큰을 최대한 활용
   - 라이트/다크 모드 모두 고려

8. **CSS 빌드**: `cd src/Daeha.RnUI && npm run build:css` 실행

9. **빌드 확인**: `dotnet build` 실행

10. **테스트 실행**: `dotnet test` 실행

## 규칙

- CLAUDE.md와 `.agents/rules/` 의 모든 규칙을 준수합니다.
- **특히** `.agents/rules/custom-component-development.md`를 반드시 따릅니다.
- data-slot, AdditionalAttributes, Class Parameter는 반드시 포함합니다.
- 접근성(ARIA)을 독립적으로 조사하여 적용합니다 (shadcn/ui 참조 없음).
- 기존 디자인 토큰 활용을 우선하고, 새 토큰 추가는 최소화합니다.
- 데모 페이지는 별도 요청 시에만 생성합니다.

## `/new-component`와의 차이점

| 항목 | `/new-component` | `/new-custom-component` |
|------|-------------------|--------------------------|
| shadcn/ui 원본 | 있음 (tsx 참조) | 없음 (독자 설계) |
| CSS 변환 | cva() → .cn-* | 직접 설계 → .cn-* |
| 접근성 | Radix UI 참조 | WAI-ARIA 직접 조사 |
| Variant | shadcn/ui 그대로 | 기존 패턴 기반 독자 설계 |
| 다른 라이브러리 참고 | 불필요 | 필요 시 참고 |
