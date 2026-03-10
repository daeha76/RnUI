# New Component Scaffolding

새 RnUI 컴포넌트를 스캐폴딩합니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름이 들어옵니다 (예: `Sonner`, `Toggle`).
접두사 `Rn`은 자동으로 붙이므로 이름만 제공합니다.

## 실행 절차

1. **shadcn/ui 원본 확인**: 먼저 shadcn/ui GitHub에서 해당 컴포넌트 소스를 확인합니다.
   - URL: `https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/ui`
   - fetch 도구 또는 웹 검색으로 원본 tsx 파일을 읽어옵니다.

2. **컴포넌트 분류 판단**: `.agents/rules/component-development.md`의 4가지 패턴 중 어디에 해당하는지 결정합니다.
   - Simple / Variant / Composite / Stateful

3. **파일 생성**: 아래 파일을 순서대로 생성합니다.
   - `src/Daeha.RnUI/Components/UI/{Name}/Rn{Name}.razor` — 메인 컴포넌트
   - `src/Daeha.RnUI/Components/UI/{Name}/{Name}Enums.cs` — enum (variant/size가 있을 때만)
   - 서브 컴포넌트 파일들 (composite일 때)
   - `tests/Daeha.RnUI.Tests/Components/Rn{Name}Tests.cs` — bunit 테스트

4. **CSS 추가**: `src/Daeha.RnUI/wwwroot/css/shadcn.src.css`에 `.cn-{name}` 관련 클래스를 추가합니다.
   - shadcn/ui 원본의 cva() 정의를 `.cn-*` 클래스로 변환

5. **CSS 빌드**: `cd src/Daeha.RnUI && npm run build:css` 실행

6. **빌드 확인**: `dotnet build` 실행

7. **테스트 실행**: `dotnet test` 실행

## 규칙

- CLAUDE.md와 `.agents/rules/` 의 모든 규칙을 준수합니다.
- data-slot, AdditionalAttributes, Class Parameter는 반드시 포함합니다.
- 접근성(ARIA)을 빠뜨리지 않습니다.
- 데모 페이지는 별도 요청 시에만 생성합니다.
