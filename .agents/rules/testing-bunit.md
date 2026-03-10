---
trigger: always_on
---

# Testing Rules (xUnit + bunit)

## 테스트 프레임워크

- **xUnit** — 테스트 러너
- **bunit 2.6.2** — Blazor 컴포넌트 테스트
- **FluentAssertions** — 어서션
- **Moq** — JS Interop 등 목킹

---

## 파일 위치 및 네이밍

```
tests/Daeha.RnUI.Tests/
├── Components/
│   ├── RnButtonTests.cs        # Components/UI/Button/ 대응
│   ├── RnCardTests.cs          # Components/UI/Card/ 대응
│   ├── RnDialogTests.cs        # Components/UI/Dialog/ 대응
│   └── ...
└── Utils/
    └── CssUtilsTests.cs        # Utils/ 대응
```

- 테스트 파일명: `Rn{Name}Tests.cs`
- 네임스페이스: `Daeha.RnUI.Tests.Components`
- 클래스명: `Rn{Name}Tests`

---

## 테스트 클래스 구조

```csharp
using Bunit;
using FluentAssertions;
using RnUI.Components.UI;
using RnUI.Components.UI.{Name};
using Xunit;

namespace Daeha.RnUI.Tests.Components;

[Trait("Category", "Unit")]
public class Rn{Name}Tests : BunitContext
{
    // 테스트 메서드
}
```

`BunitContext`를 상속하여 `Render<T>()` 메서드 사용.

---

## 테스트 항목 (모든 컴포넌트 공통)

### 1. 기본 렌더링

```csharp
[Fact]
public void Rn{Name}_DefaultRender_HasBaseClass()
{
    var cut = Render<Rn{Name}>(p => p.AddChildContent("content"));

    cut.Find("[data-slot=\"{name}\"]").ClassList.Should().Contain("cn-{name}");
}
```

### 2. data-slot 속성

```csharp
[Fact]
public void Rn{Name}_HasDataSlotAttribute()
{
    var cut = Render<Rn{Name}>(p => p.AddChildContent("content"));

    cut.Find("[data-slot]").GetAttribute("data-slot").Should().Be("{name}");
}
```

### 3. ChildContent 렌더링

```csharp
[Fact]
public void Rn{Name}_WithChildContent_RendersText()
{
    var cut = Render<Rn{Name}>(p => p.AddChildContent("Hello"));

    cut.Find("[data-slot=\"{name}\"]").TextContent.Trim().Should().Contain("Hello");
}
```

### 4. 커스텀 Class 병합

```csharp
[Fact]
public void Rn{Name}_WithCustomClass_MergesClasses()
{
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.Class, "my-class")
        .AddChildContent("content"));

    var el = cut.Find("[data-slot=\"{name}\"]");
    el.ClassList.Should().Contain("cn-{name}");
    el.ClassList.Should().Contain("my-class");
}
```

### 5. Variant 클래스 (Variant 컴포넌트만)

```csharp
[Fact]
public void Rn{Name}_WithVariant_AppliesCorrectClass()
{
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.Variant, {Name}Variant.Destructive)
        .AddChildContent("content"));

    var el = cut.Find("[data-slot=\"{name}\"]");
    el.ClassList.Should().Contain("cn-{name}-variant-destructive");
    el.ClassList.Should().NotContain("cn-{name}-variant-default");
}
```

### 6. Size 클래스 (Size 컴포넌트만)

```csharp
[Fact]
public void Rn{Name}_WithSize_AppliesCorrectClass()
{
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.Size, ButtonSize.Sm)
        .AddChildContent("content"));

    cut.Find("[data-slot=\"{name}\"]").ClassList.Should().Contain("cn-{name}-size-sm");
}
```

### 7. Disabled 속성

```csharp
[Fact]
public void Rn{Name}_Disabled_HasDisabledAttribute()
{
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.Disabled, true)
        .AddChildContent("content"));

    cut.Find("[data-slot=\"{name}\"]").HasAttribute("disabled").Should().BeTrue();
}

[Fact]
public void Rn{Name}_NotDisabled_NoDisabledAttribute()
{
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.Disabled, false)
        .AddChildContent("content"));

    cut.Find("[data-slot=\"{name}\"]").HasAttribute("disabled").Should().BeFalse();
}
```

### 8. 이벤트 콜백

```csharp
[Fact]
public void Rn{Name}_Click_InvokesCallback()
{
    var clicked = false;
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, _ => clicked = true))
        .AddChildContent("content"));

    cut.Find("[data-slot=\"{name}\"]").Click();
    clicked.Should().BeTrue();
}
```

### 9. Two-way Binding (Stateful 컴포넌트만)

```csharp
[Fact]
public void Rn{Name}_Toggle_InvokesCheckedChanged()
{
    var newValue = false;
    var cut = Render<Rn{Name}>(p => p
        .Add(c => c.Checked, false)
        .Add(c => c.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => newValue = v)));

    cut.Find("[data-slot=\"{name}\"]").Click();
    newValue.Should().BeTrue();
}
```

---

## JS Interop 테스트 (Overlay 컴포넌트)

bunit에서 JS Interop은 기본적으로 목(mock)됨. 추가 설정 불필요:

```csharp
[Fact]
public void RnDialog_WhenOpen_RendersContent()
{
    // bunit은 JSInterop을 자동으로 loose mode로 처리
    var cut = Render<RnDialog>(p => p
        .Add(c => c.Open, true)
        .AddChildContent("Dialog content"));

    cut.Find("[data-slot=\"dialog-content\"]").TextContent.Should().Contain("Dialog content");
}

[Fact]
public void RnDialog_WhenClosed_DoesNotRender()
{
    var cut = Render<RnDialog>(p => p
        .Add(c => c.Open, false)
        .AddChildContent("Dialog content"));

    cut.FindAll("[data-slot=\"dialog-content\"]").Should().BeEmpty();
}
```

---

## 테스트 실행

```bash
# 전체 테스트
dotnet test

# Unit 테스트만
dotnet test --filter "Category=Unit"

# 특정 컴포넌트 테스트
dotnet test --filter "FullyQualifiedName~RnButtonTests"
```

---

## 테스트 작성 원칙

- 컴포넌트 생성 시 테스트도 함께 생성 (TDD 권장)
- 모든 variant/size 조합 테스트
- 기본값(default) 테스트는 반드시 포함
- disabled 상태 테스트
- ChildContent 렌더링 테스트
- 커스텀 Class 병합 테스트
- data-slot 속성 테스트
- 이벤트 콜백 테스트 (인터랙티브 컴포넌트)
