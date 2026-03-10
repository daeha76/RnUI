# Sync Component with shadcn/ui

shadcn/ui 원본과 RnUI 컴포넌트를 비교하여 차이점을 분석합니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름이 들어옵니다 (예: `button`, `dialog`).

## 실행 절차

1. **shadcn/ui 원본 fetch**: shadcn/ui GitHub에서 해당 컴포넌트 소스를 가져옵니다.
   - URL: `https://github.com/shadcn-ui/ui/tree/main/apps/v4/registry/bases/base/ui/{name}.tsx`
   - 또는 raw URL로 직접 fetch

2. **RnUI 현재 구현 읽기**: `src/Daeha.RnUI/Components/UI/{Name}/` 폴더의 모든 파일을 읽습니다.

3. **CSS 비교**: shadcn/ui 원본의 cva()/className과 `shadcn.src.css`의 `.cn-*` 클래스를 비교합니다.

4. **차이점 리포트 생성**: 아래 항목을 비교하여 차이를 알려줍니다.
   - **누락된 variant/size**: 원본에 있지만 RnUI에 없는 것
   - **CSS 차이**: Tailwind 클래스 차이 (추가/변경/제거)
   - **HTML 구조 차이**: data-slot, aria-* 속성 차이
   - **기능 차이**: 원본에 있지만 미구현된 props/기능
   - **추가 구현**: RnUI에만 있는 확장 기능

5. **변환 가이드**: 차이가 있다면 Blazor로 어떻게 변환해야 하는지 구체적 코드를 제안합니다.

## 출력

차이점 분석 결과를 테이블 형식으로 보여주고, 수정이 필요한 경우 코드 변경 제안을 합니다.
코드를 직접 수정하지 않습니다 — 분석 결과만 제공합니다.
