# Build CSS

라이브러리 CSS와 데모 CSS를 **둘 다** 빌드하고 결과를 확인합니다.

> **⚠️ 두 파이프라인은 독립적이므로 반드시 둘 다 빌드해야 합니다.**
> 참조: `.agents/knowledge/rnui-css-architecture.md` > "듀얼 빌드 필수"

## 실행 절차

1. **라이브러리 CSS 컴파일**: 컴포넌트 `.cn-*` 클래스를 빌드합니다.
   ```bash
   cd src/Daeha.RnUI && npm run build:css
   ```

2. **데모 CSS 컴파일**: Razor 파일의 Tailwind utility 클래스를 스캔하여 빌드합니다.
   ```bash
   cd src/Daeha.RnUI.Demo.Wasm && npm run build:css
   ```

3. **빌드 결과 확인**: `dotnet build`로 전체 빌드가 성공하는지 확인합니다.

4. **변경사항 확인**: 컴파일 결과 변경사항을 보여줍니다.
   ```bash
   git diff src/Daeha.RnUI/wwwroot/css/shadcn.css
   git diff src/Daeha.RnUI.Demo.Wasm/wwwroot/css/tailwindcss.css
   ```

5. **프리뷰 (선택)**: Claude Preview가 가능한 경우:
   - 데모 앱을 시작하여 시각적으로 확인합니다.
   - 라이트모드와 다크모드 모두 스크린샷을 캡처합니다.

## 주의

- `shadcn.css`는 컴파일 결과물입니다. 직접 수정하지 마세요.
- CSS 소스 수정은 반드시 `shadcn.src.css`에서만 합니다.
- **라이브러리 CSS만 빌드하면 데모에서 새 Tailwind 클래스가 적용되지 않습니다.**
  예: Razor에 `gap-7`을 추가했는데 데모 CSS를 빌드하지 않으면 간격이 적용되지 않음.
