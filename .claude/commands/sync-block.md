# Sync Block with shadcn/ui

shadcn/ui 원본 블록과 RnUI 블록 구현을 비교하여 차이점을 분석합니다.

## 입력

$ARGUMENTS 에 블록 이름이 들어옵니다 (예: `login-01`, `sidebar-05`).

## 실행 절차

1. **shadcn/ui 원본 fetch**: shadcn/ui GitHub에서 해당 블록 소스를 가져옵니다.
   - URL: `https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/new-york-v4/blocks/{block-name}`
   - `page.tsx`와 `components/` 폴더의 모든 파일을 읽습니다.

2. **RnUI 현재 구현 읽기**: 카테고리와 번호를 추출하여 해당 파일을 읽습니다.
   - `login-01` → `BlockDemos/Login/Login01Demo.razor` + `Components/LoginForm01.razor`
   - `sidebar-05` → `BlockDemos/Sidebar/Sidebar05Demo.razor` + `Components/AppSidebar05.razor`
   - 구현이 없으면 "미구현" 으로 표시합니다.

3. **차이점 리포트 생성**: 아래 항목을 비교하여 차이를 알려줍니다.
   - **레이아웃 구조**: 원본의 JSX 구조 vs RnUI의 Razor 구조
   - **사용 컴포넌트**: 원본에서 사용한 컴포넌트가 RnUI에 모두 매핑되었는지
   - **누락된 UI 요소**: 원본에 있지만 RnUI 블록에 없는 요소 (링크, 텍스트, 아이콘 등)
   - **Tailwind 클래스 차이**: 레이아웃/스타일 클래스 비교
   - **Form 처리**: 원본의 form 구조 vs Blazor EditForm 변환 확인
   - **반응형**: 모바일/데스크톱 반응형 클래스 누락 여부

4. **변환 가이드**: 차이가 있다면 Blazor로 어떻게 수정해야 하는지 코드를 제안합니다.

## 출력

차이점 분석 결과를 테이블 형식으로 보여주고, 수정이 필요한 경우 코드 변경 제안을 합니다.
코드를 직접 수정하지 않습니다 — 분석 결과만 제공합니다.
