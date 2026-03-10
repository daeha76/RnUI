# shadcn/ui → Blazor 변환 치트시트

## React → Blazor 기본 매핑

### JSX → Razor

```tsx
// React JSX
<Button variant="destructive" size="sm" className="ml-2" onClick={handleClick}>
  Delete
</Button>
```

```razor
@* Blazor Razor *@
<RnButton Variant="ButtonVariant.Destructive" Size="ButtonSize.Sm" Class="ml-2" OnClick="HandleClick">
    Delete
</RnButton>
```

### Props → Parameters

```tsx
// React: props destructuring
interface ButtonProps {
  variant?: "default" | "destructive" | "outline";
  size?: "default" | "sm" | "lg";
  className?: string;
  disabled?: boolean;
  children: React.ReactNode;
}
```

```csharp
// Blazor: Parameter attributes
[Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Default;
[Parameter] public ButtonSize Size { get; set; } = ButtonSize.Default;
[Parameter] public string? Class { get; set; }
[Parameter] public bool Disabled { get; set; }
[Parameter] public RenderFragment? ChildContent { get; set; }
[Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

### State Management

```tsx
// React: useState
const [open, setOpen] = useState(false);
<Dialog open={open} onOpenChange={setOpen}>
```

```csharp
// Blazor: Parameter + EventCallback (two-way binding)
[Parameter] public bool Open { get; set; }
[Parameter] public EventCallback<bool> OpenChanged { get; set; }

// 사용 시:
<RnDialog @bind-Open="isOpen">
```

### Effects

```tsx
// React: useEffect
useEffect(() => {
  document.addEventListener("keydown", handler);
  return () => document.removeEventListener("keydown", handler);
}, []);
```

```csharp
// Blazor: OnAfterRenderAsync + IAsyncDisposable
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
        await Interop.AddEscapeListenerAsync(_id, _dotNetRef!, nameof(Close));
}

public async ValueTask DisposeAsync()
{
    await Interop.DisposeAllAsync(_id);
}
```

### Context

```tsx
// React: createContext + useContext
const AccordionContext = createContext<AccordionContextValue>(null);
const { value } = useContext(AccordionContext);
```

```razor
@* Blazor: CascadingValue + CascadingParameter *@

@* 부모 (Provider) *@
<CascadingValue Value="this" IsFixed="false">
    @ChildContent
</CascadingValue>

@* 자식 (Consumer) *@
@code {
    [CascadingParameter] public RnAccordion? Accordion { get; set; }
}
```

### Ref

```tsx
// React: useRef + forwardRef
const ref = useRef<HTMLDivElement>(null);
const Component = forwardRef<HTMLDivElement>((props, ref) => (
  <div ref={ref} />
));
```

```razor
@* Blazor: ElementReference + @ref *@
<div @ref="_contentRef">

@code {
    private ElementReference _contentRef;
}
```

### Conditional Rendering

```tsx
// React: && 연산자
{open && <div>Content</div>}
{isActive ? <Active /> : <Inactive />}
```

```razor
@* Blazor: @if 문 *@
@if (Open)
{
    <div>Content</div>
}
@if (isActive) { <Active /> } else { <Inactive /> }
```

### List Rendering

```tsx
// React: map
{items.map(item => <Item key={item.id} {...item} />)}
```

```razor
@* Blazor: @foreach *@
@foreach (var item in items)
{
    <Item @key="item.Id" Data="item" />
}
```

---

## Radix UI → 순수 Blazor

### Dialog

```tsx
// Radix: Portal + Overlay + Content
<Dialog.Root open={open} onOpenChange={setOpen}>
  <Dialog.Trigger asChild>
    <Button>Open</Button>
  </Dialog.Trigger>
  <Dialog.Portal>
    <Dialog.Overlay className="..." />
    <Dialog.Content className="...">
      <Dialog.Title>Title</Dialog.Title>
      <Dialog.Description>Desc</Dialog.Description>
      <Dialog.Close />
    </Dialog.Content>
  </Dialog.Portal>
</Dialog.Root>
```

```razor
@* Blazor: 조건부 렌더링 + z-index *@
<RnButton OnClick="() => isOpen = true">Open</RnButton>

<RnDialog @bind-Open="isOpen">
    <RnDialogHeader>
        <RnDialogTitle>Title</RnDialogTitle>
        <RnDialogDescription>Desc</RnDialogDescription>
    </RnDialogHeader>
    @* Close 버튼은 RnDialog 내부에서 자동 렌더링 *@
</RnDialog>
```

### Accordion

```tsx
// Radix: Root + Item + Trigger + Content
<Accordion.Root type="single" collapsible>
  <Accordion.Item value="item-1">
    <Accordion.Trigger>Title</Accordion.Trigger>
    <Accordion.Content>Content</Accordion.Content>
  </Accordion.Item>
</Accordion.Root>
```

```razor
@* Blazor: CascadingValue 기반 *@
<RnAccordion Type="AccordionType.Single" Collapsible>
    <RnAccordionItem Value="item-1">
        <RnAccordionTrigger>Title</RnAccordionTrigger>
        <RnAccordionContent>Content</RnAccordionContent>
    </RnAccordionItem>
</RnAccordion>
```

---

## cn() → CssUtils.Cn()

```tsx
// React: cn() from @/lib/utils (clsx + twMerge)
className={cn("base-class", variant === "default" && "variant-default", className)}
```

```csharp
// Blazor: CssUtils.Cn() (string.Join 기반)
class="@CssUtils.Cn("cn-button", VariantClass, SizeClass, Class)"

// 조건부
CssUtils.Cn("base", CssUtils.CnIf(isActive, "active"), CssUtils.CnIf(disabled, "opacity-50"))
```

> 주의: CssUtils.Cn()은 tailwind-merge 기능이 없다. 클래스 충돌 시 .cn-* 클래스에서 @apply로 해결.

---

## cva() → Enum + switch + shadcn.src.css

```tsx
// React: cva() — 인라인 variant 정의
const buttonVariants = cva("inline-flex items-center ...", {
  variants: {
    variant: { default: "bg-primary ...", ghost: "hover:bg-accent ..." },
    size: { default: "h-9 px-4", sm: "h-8 px-3" }
  }
});
// 사용: <button className={buttonVariants({ variant, size, className })} />
```

```csharp
// Blazor: 3단계 분리

// 1. Enum (ButtonEnums.cs)
public enum ButtonVariant { Default, Ghost }
public enum ButtonSize { Default, Sm }

// 2. switch 매핑 (RnButton.razor @code)
private string VariantClass => Variant switch
{
    ButtonVariant.Default => "cn-button-variant-default",
    ButtonVariant.Ghost => "cn-button-variant-ghost",
    _ => "cn-button-variant-default"
};

// 3. CSS 정의 (shadcn.src.css)
// .cn-button-variant-default { @apply bg-primary text-primary-foreground ...; }
// .cn-button-variant-ghost { @apply hover:bg-accent ...; }
```
