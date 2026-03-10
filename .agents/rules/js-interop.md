---
trigger: always_on
---

# JS Interop Rules

## 원칙

JS Interop은 **최소한으로** 사용한다. Blazor로 구현 가능한 것은 Blazor로 한다.
JS가 필요한 경우 반드시 `RnUIInteropService`를 통해 호출한다.

---

## JS가 필요한 경우 (허용)

| 기능 | 이유 | 메서드 |
|------|------|--------|
| Body scroll lock | Blazor에서 `document.body.style` 직접 제어 불가 | `LockBodyScrollAsync` / `UnlockBodyScrollAsync` |
| Focus trap | Tab 키 가로채기는 JS만 가능 | `EnableFocusTrapAsync` / `DisableFocusTrapAsync` |
| Escape 키 리스너 | 전역 keydown 이벤트 | `AddEscapeListenerAsync` / `RemoveEscapeListenerAsync` |
| Click outside 감지 | 컴포넌트 외부 클릭 감지 | `AddClickOutsideListenerAsync` / `RemoveClickOutsideListenerAsync` |

### JS가 불필요한 경우 (Blazor로 처리)

- 조건부 렌더링 (`@if`)
- CSS 클래스 토글 (ComputedClass)
- 이벤트 핸들링 (`@onclick`, `@onkeydown`)
- 상태 관리 (Parameter, CascadingValue)
- 타이머 (`System.Threading.Timer`, `PeriodicTimer`)
- 애니메이션 (CSS `transition`, `@keyframes`, `data-state` 기반)

---

## RnUIInteropService 사용 패턴

### 오버레이 컴포넌트 전체 흐름 (Dialog, Sheet, Drawer)

```csharp
@implements IAsyncDisposable
@inject RnUIInteropService Interop

@code {
    private readonly string _id = Guid.NewGuid().ToString("N")[..8];
    private ElementReference _contentRef;
    private DotNetObjectReference<RnDialog>? _dotNetRef;
    private bool _previousOpen;

    // 1. firstRender에서 DotNetRef 생성
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _dotNetRef = DotNetObjectReference.Create(this);

        // 2. Open → true: 모든 interop 활성화
        if (Open && !_previousOpen)
        {
            _previousOpen = true;
            await Interop.LockBodyScrollAsync();
            await Interop.EnableFocusTrapAsync(_id, _contentRef);
            await Interop.AddEscapeListenerAsync(_id, _dotNetRef!, nameof(Close));
        }
        // 3. Open → false: 모든 interop 해제
        else if (!Open && _previousOpen)
        {
            _previousOpen = false;
            await Interop.DisposeAllAsync(_id);
            await Interop.UnlockBodyScrollAsync();
        }
    }

    // 4. JS에서 호출 가능한 메서드
    [JSInvokable]
    public async Task Close() => await OpenChanged.InvokeAsync(false);

    // 5. 컴포넌트 소멸 시 정리
    public async ValueTask DisposeAsync()
    {
        if (_previousOpen)
        {
            await Interop.DisposeAllAsync(_id);
            await Interop.UnlockBodyScrollAsync();
        }
        _dotNetRef?.Dispose();
    }
}
```

---

## 핵심 규칙

### 1. OnAfterRenderAsync에서만 호출

```csharp
// ❌ 금지: OnInitializedAsync에서 JS 호출 → SSR 런타임 에러
protected override async Task OnInitializedAsync()
{
    await Interop.LockBodyScrollAsync(); // 에러!
}

// ✅ 올바른: OnAfterRenderAsync에서만
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (Open && !_previousOpen)
        await Interop.LockBodyScrollAsync();
}
```

### 2. _previousOpen 패턴으로 중복 호출 방지

`OnAfterRenderAsync`는 매 렌더링마다 호출된다. `_previousOpen` 플래그로 상태 전환 시에만 interop 실행.

### 3. IAsyncDisposable 필수 구현

JS 리스너를 등록하는 모든 컴포넌트는 반드시 `IAsyncDisposable` 구현.
미구현 시 **메모리 누수** + **이벤트 리스너 누적**.

### 4. JSDisconnectedException 처리

`RnUIInteropService` 내부에서 이미 `catch (JSDisconnectedException) { }`로 처리됨.
컴포넌트에서 추가 예외 처리 불필요.

### 5. DisposeAllAsync로 일괄 정리

개별 Remove 호출 대신 `DisposeAllAsync(_id)`로 해당 ID의 모든 리스너를 한번에 정리.

---

## IJSRuntime 직접 사용 금지

```csharp
// ❌ 금지
@inject IJSRuntime JS
await JS.InvokeVoidAsync("eval", "document.body.style.overflow = 'hidden'");

// ✅ 올바른
@inject RnUIInteropService Interop
await Interop.LockBodyScrollAsync();
```

새로운 JS 기능이 필요하면:
1. `wwwroot/js/rnui-interop.js`에 함수 추가
2. `RnUIInteropService.cs`에 C# 래퍼 메서드 추가
3. 컴포넌트에서 서비스 메서드 호출

---

## JS 모듈 (rnui-interop.js) 수정 규칙

- ESM 모듈 (`export function`)
- 각 함수는 `id` 파라미터로 리스너 등록/해제를 관리
- `disposeAll(id)`에서 해당 id의 모든 리스너 일괄 해제
- ref counting: scroll lock은 중첩 모달 대비 카운터 사용
- 이벤트 리스너 등록 시 반드시 해제 함수도 함께 저장
