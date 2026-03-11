# Fix Demo

데모가 작동하지 않는 컴포넌트를 찾아 수정합니다.

## 입력

$ARGUMENTS 에 컴포넌트 이름이 들어옵니다 (예: `Sheet`, `Drawer`, `Toast`).
비어 있으면 **모든 데모**를 스캔하여 작동하지 않는 것을 찾습니다.

## 실행 절차

1. **데모 파일 읽기**: `src/Daeha.RnUI.Demo.Wasm/Pages/ComponentDemos/` 에서 해당 데모 파일을 찾습니다.

2. **문제 진단**: 아래 항목을 확인합니다.
   - `Demo:` RenderFragment에 실제 컴포넌트가 포함되어 있는지
   - 인터랙티브 컴포넌트(Sheet, Drawer, Dialog 등)에 상태(`@bind-Open`, `OnClick` 등)가 있는지
   - static `GetDemo()` 내에서 상태가 필요한 경우 별도 `{Name}DemoContent.razor` 래퍼 컴포넌트가 있는지

3. **수정 패턴**: 상태가 필요한 데모는 아래 패턴으로 수정합니다.

   **Before** (static 메서드에서 상태 불가):
   ```razor
   Demo: @<div>
       <RnButton>Open Sheet</RnButton>  @* 클릭해도 아무것도 안 됨 *@
   </div>,
   ```

   **After** (별도 래퍼 컴포넌트):
   ```razor
   // SheetDemoContent.razor (새 파일)
   <RnButton OnClick="() => _open = true">Open Sheet</RnButton>
   <RnSheet @bind-Open="_open">...</RnSheet>
   @code { private bool _open; }

   // SheetDemo.razor
   Demo: @<SheetDemoContent />,
   ```

4. **shadcn/ui 원본 데모 확인**: 필요 시 shadcn/ui 데모 소스를 가져와 Blazor로 변환합니다.
   - URL: `https://raw.githubusercontent.com/shadcn-ui/ui/main/apps/v4/registry/new-york-v4/examples/{name}-demo.tsx`

5. **빌드 확인**: `dotnet build` 실행하여 오류 없는지 확인합니다.

## 인터랙티브 데모가 필요한 컴포넌트 목록

별도 `{Name}DemoContent.razor`가 필요한 컴포넌트:
- Sheet, Drawer, Dialog, AlertDialog — `@bind-Open` 상태 필요
- Toast — `@inject ToastService` 필요
- Command (Palette 모드) — 키보드 단축키 상태 필요
- Popover, HoverCard, Tooltip — Open 상태 제어 필요 (자체 토글이면 불필요)

## 완료 후 문서 업데이트

데모 수정이 완료되면 아래 문서도 함께 확인·수정합니다.

1. **`README.md`** — 해당 컴포넌트의 사용 예제(Usage Examples)가 변경된 API와 일치하는지 확인. 불일치 시 업데이트.
2. **`src/Daeha.RnUI/NUGET_README.md`** — 동일하게 사용 예제 확인·업데이트.
3. **컴포넌트 API 변경 시** — 파라미터 추가/삭제/이름 변경이 있었다면 위 두 파일의 코드 예제를 새 API에 맞게 수정합니다.

## 금지

- `GetDemo()`를 non-static으로 변경하지 않습니다 (전체 데모 시스템에 영향).
- 데모 수정 후 빌드 확인 없이 완료 선언하지 않습니다.
