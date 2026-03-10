# New Block Scaffolding

shadcn/ui 블록을 RnUI 데모 앱에 스캐폴딩합니다.

## 입력

$ARGUMENTS 에 블록 이름이 들어옵니다 (예: `login-01`, `sidebar-05`).

## 실행 절차

1. **shadcn/ui 원본 확인**: shadcn/ui GitHub에서 해당 블록 소스를 가져옵니다.
   - URL: `https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/blocks/{block-name}`
   - `page.tsx`와 `components/` 폴더의 모든 파일을 읽습니다.

2. **필요 컴포넌트 확인**: `.agents/knowledge/block-catalog.md`에서 해당 블록의 필요 컴포넌트가 모두 구현되어 있는지 확인합니다. 미구현 컴포넌트가 있으면 경고합니다.

3. **카테고리 판단**: 블록 이름에서 카테고리를 추출합니다.
   - `login-XX` → `login`
   - `signup-XX` → `signup`
   - `sidebar-XX` → `sidebar`
   - `dashboard-XX` → `dashboard`

4. **파일 생성**: `.agents/knowledge/block-structure.md`의 구조에 따라:
   - `src/Daeha.RnUI.Demo.Wasm/BlockDemos/{Category}/{Block}Demo.razor` — GetDemo() 정의
   - `src/Daeha.RnUI.Demo.Wasm/BlockDemos/{Category}/Components/{Component}.razor` — 조합 컴포넌트

5. **React → Blazor 변환**: `.agents/rules/shadcn-reference.md` 규칙에 따라 변환합니다.
   - JSX → Razor 마크업
   - shadcn 컴포넌트 → RnUI 컴포넌트 (Button→RnButton 등)
   - cva variants → CssUtils.Cn()
   - React hooks → Blazor Parameter/EventCallback
   - Form: React form → Blazor EditForm + FormName

6. **레이아웃 적용**: 블록 카테고리에 맞는 레이아웃 패턴 적용:
   - Login/Signup → `.agents/rules/form-patterns.md`
   - Sidebar/Dashboard → `.agents/rules/layout-patterns.md`

7. **빌드 확인**: `dotnet build` 실행

## 규칙

- `.agents/rules/block-development.md`의 모든 규칙을 준수합니다.
- RnUI 기존 컴포넌트만 사용합니다 (새 HTML 직접 작성 금지).
- 레이아웃 CSS는 Tailwind utility 직접 사용 (shadcn.src.css에 추가 금지).
- 모든 블록은 반응형 (모바일→데스크톱).
- 데모에서 실제 API 호출하지 않습니다 (목 데이터 + Task.Delay).
- BlockDemo.Files에 소스 코드 문자열을 반드시 포함합니다 (코드 보기용).
