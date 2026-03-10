# Build CSS

라이브러리 CSS를 빌드하고 결과를 확인합니다.

## 실행 절차

1. **CSS 컴파일**: 라이브러리 CSS를 빌드합니다.
   ```bash
   cd src/Daeha.RnUI && npm run build:css
   ```

2. **빌드 결과 확인**: `dotnet build`로 전체 빌드가 성공하는지 확인합니다.

3. **변경사항 확인**: `git diff src/Daeha.RnUI/wwwroot/css/shadcn.css`로 컴파일 결과 변경사항을 보여줍니다.

4. **프리뷰 (선택)**: Claude Preview가 가능한 경우:
   - 데모 앱을 시작하여 시각적으로 확인합니다.
   - 라이트모드와 다크모드 모두 스크린샷을 캡처합니다.

## 주의

- `shadcn.css`는 컴파일 결과물입니다. 직접 수정하지 마세요.
- CSS 소스 수정은 반드시 `shadcn.src.css`에서만 합니다.
