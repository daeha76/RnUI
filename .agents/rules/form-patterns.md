---
trigger: always_on
---

# Form Patterns (Login/Signup Blocks)

## Blazor Form 기본 패턴

### EditForm + FormName (SSR 호환 필수)

```razor
<EditForm Model="_model" FormName="login-form" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />

    <div class="grid gap-4">
        <div class="grid gap-2">
            <RnLabel For="email">Email</RnLabel>
            <RnInput Id="email" Type="email" Placeholder="m@example.com"
                     @bind-Value="_model.Email" required />
        </div>
        <div class="grid gap-2">
            <RnLabel For="password">Password</RnLabel>
            <RnInput Id="password" Type="password"
                     @bind-Value="_model.Password" required />
        </div>
        <RnButton Type="submit" Class="w-full" Disabled="_isLoading">
            @if (_isLoading)
            {
                <RnSpinner Class="mr-2" /> <span>Loading...</span>
            }
            else
            {
                <span>Login</span>
            }
        </RnButton>
    </div>
</EditForm>
```

**필수**: `FormName` 없으면 SSR POST 바인딩 실패.

---

## Form 모델 패턴

```csharp
@code {
    private LoginModel _model = new();
    private bool _isLoading;

    private async Task HandleSubmit()
    {
        _isLoading = true;
        StateHasChanged();

        // 데모: 실제 API 호출 대신 딜레이
        await Task.Delay(1500);

        _isLoading = false;
        StateHasChanged();
    }

    private sealed class LoginModel
    {
        [Required(ErrorMessage = "이메일을 입력해주세요.")]
        [EmailAddress(ErrorMessage = "유효한 이메일 주소를 입력해주세요.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "비밀번호를 입력해주세요.")]
        public string Password { get; set; } = "";
    }
}
```

- Form 모델은 블록 컴포넌트 내부에 `private sealed class`로 정의
- 데모에서는 실제 API 호출하지 않음 — `Task.Delay`로 로딩 시뮬레이션

---

## Form 레이아웃 패턴

### Login 레이아웃 변형

```razor
@* 1. 중앙 카드 (login-01) *@
<div class="flex min-h-svh items-center justify-center p-4">
    <RnCard Class="w-full max-w-sm">...</RnCard>
</div>

@* 2. 이미지 + 카드 분할 (login-02) *@
<div class="grid min-h-svh lg:grid-cols-2">
    <div class="flex items-center justify-center p-4">
        <RnCard Class="w-full max-w-sm">...</RnCard>
    </div>
    <div class="hidden lg:block bg-muted">
        @* 이미지 또는 브랜딩 영역 *@
    </div>
</div>

@* 3. 전체 배경 (login-03) *@
<div class="flex min-h-svh flex-col items-center justify-center gap-6 bg-muted p-4">
    <div class="w-full max-w-sm">...</div>
</div>
```

### Signup 레이아웃

Login과 동일 구조 + 추가 필드 (이름, 비밀번호 확인):

```razor
<div class="grid gap-4">
    <div class="grid grid-cols-2 gap-4">
        <div class="grid gap-2">
            <RnLabel For="first-name">이름</RnLabel>
            <RnInput Id="first-name" @bind-Value="_model.FirstName" required />
        </div>
        <div class="grid gap-2">
            <RnLabel For="last-name">성</RnLabel>
            <RnInput Id="last-name" @bind-Value="_model.LastName" required />
        </div>
    </div>
    @* ... email, password, confirm password *@
</div>
```

---

## 소셜 로그인 버튼

```razor
@* 구분선 *@
<div class="relative text-center text-sm after:absolute after:inset-0 after:top-1/2 after:z-0 after:flex after:items-center after:border-t after:border-border">
    <span class="relative z-10 bg-card px-2 text-muted-foreground">
        Or continue with
    </span>
</div>

@* 소셜 버튼 그리드 *@
<div class="grid grid-cols-2 gap-4">
    <RnButton Variant="ButtonVariant.Outline" Class="w-full">
        <RnIcon Icon="RnIcons.Github" /> GitHub
    </RnButton>
    <RnButton Variant="ButtonVariant.Outline" Class="w-full">
        <RnIcon Icon="RnIcons.Google" /> Google
    </RnButton>
</div>
```

---

## 에러 메시지 표시

```razor
@* Validation Summary — 폼 상단 *@
<ValidationSummary class="text-sm text-destructive" />

@* 필드별 에러 — 각 입력 아래 *@
<ValidationMessage For="() => _model.Email" class="text-sm text-destructive" />

@* 서버 에러 — 수동 표시 *@
@if (!string.IsNullOrEmpty(_errorMessage))
{
    <RnAlert Variant="AlertVariant.Destructive">
        <RnAlertDescription>@_errorMessage</RnAlertDescription>
    </RnAlert>
}
```

---

## 링크 패턴 (Login ↔ Signup 전환)

```razor
<div class="text-center text-sm">
    Don't have an account?
    <a href="/signup" class="underline underline-offset-4">Sign up</a>
</div>
```

데모에서는 실제 라우팅 없이 텍스트만 표시 (또는 `#`으로 링크).

---

## 로딩 상태 패턴

```razor
<RnButton Type="submit" Class="w-full" Disabled="_isLoading">
    @if (_isLoading)
    {
        <RnSpinner /> <span>Signing in...</span>
    }
    else
    {
        <span>Sign in</span>
    }
</RnButton>
```

로딩 중: 버튼 `Disabled` + Spinner + 텍스트 변경.
