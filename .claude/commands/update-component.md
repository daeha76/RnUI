# Update Component

기존 RnUI 컴포넌트를 수정합니다. shadcn/ui 동기화, 버그 수정, 기능 추가 등 모든 컴포넌트 변경에 사용합니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름과 수정 내용이 들어옵니다.

형식: `{컴포넌트명} {수정 내용}`

예시:
- `Button variant 추가: link`
- `Card shadcn/ui 최신 동기화`
- `Sheet 닫힘 애니메이션 버그 수정`
- `Dialog 접근성 개선`

## 실행 절차

### 1단계: 현재 상태 파악

1. **RnUI 현재 구현 읽기**: `src/Daeha.RnUI/Components/UI/{Name}/` 폴더의 모든 파일을 읽습니다.
2. **관련 CSS 읽기**: `shadcn.src.css`에서 `.cn-{name}` 관련 클래스를 확인합니다.
3. **테스트 읽기**: `tests/Daeha.RnUI.Tests/Components/Rn{Name}Tests.cs` 파일을 읽습니다.

### 2단계: shadcn/ui 원본 확인 (동기화 요청 시)

수정 내용에 "동기화", "sync", "최신" 등이 포함되면:
1. shadcn/ui 원본을 fetch합니다.
   - URL: `https://raw.githubusercontent.com/shadcn-ui/ui/main/apps/v4/registry/new-york-v4/ui/{name}.tsx`
2. 원본과 현재 구현의 차이를 분석합니다.
3. 차이점을 사용자에게 요약 보고하고 수정 범위를 확인받습니다.

### 3단계: 파급 효과 확인 (수정 전 필수)

1. **사용처 검색**: 이 컴포넌트를 사용하는 모든 파일을 검색합니다.
   - `src/Daeha.RnUI/Components/` (다른 컴포넌트에서 사용)
   - `src/Daeha.RnUI.Demo.Wasm/` (데모 페이지)
   - `tests/` (테스트)
2. **영향 범위 보고**: 수정으로 영향받는 파일 목록을 사용자에게 알립니다.
3. **Breaking Change 여부**: Parameter 이름 변경, 삭제, enum 값 변경 등이 있으면 경고합니다.

### 4단계: 수정 실행

1. **컴포넌트 Razor 수정**: `Rn{Name}.razor` 및 서브 컴포넌트 파일 수정
2. **CSS 수정**: `shadcn.src.css`의 `.cn-{name}` 관련 클래스 수정
3. **Enum 수정**: variant/size enum 변경이 필요하면 `{Name}Enums.cs` 수정
4. **테스트 업데이트**: 변경에 맞게 기존 테스트를 수정하고, 새 테스트를 추가합니다.

### 5단계: 빌드 검증 (필수)

```bash
# 1. 라이브러리 CSS 빌드
cd src/Daeha.RnUI && npm run build:css

# 2. 데모 CSS 빌드
cd src/Daeha.RnUI.Demo.Wasm && npm run build:css

# 3. .NET 빌드
dotnet build

# 4. 테스트
dotnet test
```

4개 모두 성공해야 완료 선언 가능합니다.

### 6단계: 데모 페이지 수정 (필요 시)

컴포넌트 API가 변경되었으면 데모 페이지도 함께 수정합니다.
- `src/Daeha.RnUI.Demo.Wasm/Pages/ComponentDemos/{Name}Demo.razor`

## 규칙

- CLAUDE.md의 모든 컴포넌트 규칙을 준수합니다.
- **수정 전 파급 효과를 반드시 확인**합니다 — 확인 없이 수정 금지.
- **Breaking Change가 있으면 사용자 확인 후 진행**합니다.
- `data-slot`, `AdditionalAttributes`, `Class` Parameter가 유지되는지 확인합니다.
- 접근성(ARIA) 속성이 제거되지 않았는지 확인합니다.
- CSS 변경 시 `shadcn.src.css`만 수정합니다 — `shadcn.css` 직접 수정 금지.
- 테두리는 `var(--color-border-subtle)` 사용 규칙을 준수합니다.

## 금지

- 파급 효과 확인 없이 컴포넌트를 수정하는 것
- 빌드/테스트 실행 없이 완료를 선언하는 것
- `shadcn.css`를 직접 수정하는 것
- Breaking Change를 사용자에게 알리지 않는 것
- 데모 CSS 빌드를 누락하는 것
