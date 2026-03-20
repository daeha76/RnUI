# RnUI

**文档语言**: [English](../README.md) | [한국어](README.ko.md) | **中文** | [Español](README.es.md) | [Deutsch](README.de.md) | [日本語](README.ja.md)

**[shadcn/ui](https://ui.shadcn.com) 的 Blazor 移植版**

精心设计、无障碍友好的 UI 组件库，基于 Tailwind CSS 构建，专为 .NET Blazor 应用打造。

[**在线演示**](https://daeha76.github.io/RnUI/)

---

## 为什么选择 RnUI？

- **56 个组件类别** — 包含 195+ 个 Razor 组件文件的综合 UI 库
- **基于 shadcn/ui** — 源自 Web 端久经验证的设计系统，直接移植到 Blazor
- **Tailwind CSS** — 采用 oklch 色彩空间，基于 CSS 自定义属性的主题系统
- **深色模式** — 内置亮色/深色模式支持
- **无障碍访问** — 组件设计充分考虑无障碍性
- **零外部依赖** — 仅依赖 ASP.NET Core 框架引用
- **多目标支持** — 支持 .NET 8.0、9.0、10.0 | 兼容 Blazor Server 和 WebAssembly

---

## 快速开始

### 1. 安装包

```bash
dotnet add package Daeha.RnUI
```

### 2. 添加引用

在 `_Imports.razor` 中添加以下内容：

```razor
@using Daeha.RnUI.Components.UI
```

### 3. 链接样式表

在 `App.razor` 或 `_Host.cshtml` 中添加 CSS：

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. 配置 Tailwind CSS

RnUI 使用 Tailwind CSS v4。您需要在 Blazor 项目中配置 Tailwind CSS 以扫描和编译工具类。

#### 4-1. 安装 Tailwind CSS

在项目根目录安装 Tailwind CSS：

```bash
npm init -y
npm install -D tailwindcss @tailwindcss/cli
```

#### 4-2. 创建 CSS 入口文件

创建 `wwwroot/input.css` 文件，并将 Razor 文件添加为扫描目标：

```css
@import "tailwindcss";
@source "../**/*.razor";
```

> **使用 NuGet 包时**：如果通过 NuGet 安装，库的 `.razor` 文件不在本地。但 RnUI 组件基础样式（`.cn-*` 类）已包含在 `shadcn.css` 中，无需额外的 `@source` 配置即可使用。您只需扫描自己 `.razor` 文件中使用的 Tailwind 工具类。

#### 4-3. 添加构建脚本

在 `package.json` 中添加构建脚本：

```json
{
  "scripts": {
    "build:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css",
    "watch:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css --watch"
  }
}
```

#### 4-4. 链接样式表

在 `App.razor` 或 `_Host.cshtml` 中同时链接 RnUI 样式和 Tailwind CSS 输出文件：

```html
<!-- RnUI 组件样式 -->
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
<!-- Tailwind 工具类 -->
<link rel="stylesheet" href="css/tailwindcss.css" />
```

#### 4-5. 构建 CSS

开发时使用监听模式，生产环境执行构建：

```bash
npm run watch:css   # 开发环境（监听文件变更）
npm run build:css   # 生产环境（一次性构建）
```

---

## 组件

> 在 [**演示站点**](https://daeha76.github.io/RnUI/components) 查看每个组件的在线演示和详细用法。

### 按钮与输入

| 组件 | 描述 | 演示 |
|---|---|---|
| `RnButton` | 支持 Default、Secondary、Outline、Ghost、Destructive、Link 变体 | [查看](https://daeha76.github.io/RnUI/components/button) |
| `RnInput` | 文本输入框 | [查看](https://daeha76.github.io/RnUI/components/input) |
| `RnTextarea` | 多行文本输入框 | [查看](https://daeha76.github.io/RnUI/components/textarea) |
| `RnLabel` | 表单标签 | [查看](https://daeha76.github.io/RnUI/components/label) |
| `RnCheckbox` | 复选框 | [查看](https://daeha76.github.io/RnUI/components/checkbox) |
| `RnSwitch` | 切换开关 | [查看](https://daeha76.github.io/RnUI/components/switch) |
| `RnRadioGroup` | 单选按钮组 | [查看](https://daeha76.github.io/RnUI/components/radio-group) |
| `RnSelect` | 下拉选择框 | [查看](https://daeha76.github.io/RnUI/components/select) |
| `RnToggle` | 切换按钮 | [查看](https://daeha76.github.io/RnUI/components/toggle) |
| `RnToggleGroup` | 切换按钮组 | [查看](https://daeha76.github.io/RnUI/components/toggle-group) |
| `RnCombobox` | 可搜索的下拉选择框 | [查看](https://daeha76.github.io/RnUI/components/combobox) |
| `RnInputOTP` | OTP 输入框 | [查看](https://daeha76.github.io/RnUI/components/input-otp) |
| `RnField` | 字段容器（集成 Label、Description、Error） | [查看](https://daeha76.github.io/RnUI/components/field) |
| `RnTextField` | RnField + RnFieldLabel + RnInput 的便捷封装 | [查看](https://daeha76.github.io/RnUI/components/text-field) |
| `RnDateField` | RnField + RnFieldLabel + RnDatePicker 的便捷封装（string? 绑定） | [查看](https://daeha76.github.io/RnUI/components/date-field) |
| `RnForm` | 表单验证 | [查看](https://daeha76.github.io/RnUI/components/form) |

### 卡片与容器

| 组件 | 描述 | 演示 |
|---|---|---|
| `RnCard` | 由 Header、Title、Description、Content、Footer、Action 组合而成 | [查看](https://daeha76.github.io/RnUI/components/card) |
| `RnAlert` | 警告消息（Default、Destructive） | [查看](https://daeha76.github.io/RnUI/components/alert) |
| `RnBadge` | 状态徽章 | [查看](https://daeha76.github.io/RnUI/components/badge) |
| `RnAspectRatio` | 宽高比容器 | [查看](https://daeha76.github.io/RnUI/components/aspect-ratio) |
| `RnScrollArea` | 自定义滚动区域 | [查看](https://daeha76.github.io/RnUI/components/scroll-area) |
| `RnSeparator` | 分隔线 | [查看](https://daeha76.github.io/RnUI/components/separator) |
| `RnResizable` | 可调整大小的面板 | [查看](https://daeha76.github.io/RnUI/components/resizable) |

### 数据展示

| 组件 | 描述 | 演示 |
|---|---|---|
| `RnTable` | 基础表格（Head、Body、Row、Header、Cell） | [查看](https://daeha76.github.io/RnUI/components/table) |
| `RnDataTable` | 高级数据表格（排序、筛选、分页、选择、行展开） | [查看](https://daeha76.github.io/RnUI/components/data-table) |
| `RnAvatar` | 用户头像（支持头像组） | [查看](https://daeha76.github.io/RnUI/components/avatar) |
| `RnProgress` | 进度条 | [查看](https://daeha76.github.io/RnUI/components/progress) |
| `RnSlider` | 滑块 | [查看](https://daeha76.github.io/RnUI/components/slider) |
| `RnSkeleton` | 加载骨架屏 | [查看](https://daeha76.github.io/RnUI/components/skeleton) |
| `RnSpinner` | 加载旋转器 | [查看](https://daeha76.github.io/RnUI/components/spinner) |
| `RnKbd` | 键盘快捷键展示 | [查看](https://daeha76.github.io/RnUI/components/kbd) |
| `RnCalendar` | 日历 | [查看](https://daeha76.github.io/RnUI/components/calendar) |

### 导航

| 组件 | 描述 | 演示 |
|---|---|---|
| `RnTabs` | 选项卡组件（Default、Line 变体） | [查看](https://daeha76.github.io/RnUI/components/tabs) |
| `RnBreadcrumb` | 面包屑导航 | [查看](https://daeha76.github.io/RnUI/components/breadcrumb) |
| `RnPagination` | 分页器 | [查看](https://daeha76.github.io/RnUI/components/pagination) |
| `RnNavigationMenu` | 导航菜单 | [查看](https://daeha76.github.io/RnUI/components/navigation-menu) |
| `RnSidebar` | 侧边栏（Header、Content、Footer、Group、Menu） | [查看](https://daeha76.github.io/RnUI/components/sidebar) |
| `RnMainLayout01` | 三插槽布局（Header/Content/Footer），用于 RnSidebarInset 内部 | [查看](https://daeha76.github.io/RnUI/components/main-layout-01) |
| `RnMenubar` | 菜单栏 | [查看](https://daeha76.github.io/RnUI/components/menubar) |

### 覆盖层

| 组件 | 描述 | 演示 |
|---|---|---|
| `RnDialog` | 模态对话框 | [查看](https://daeha76.github.io/RnUI/components/dialog) |
| `RnAlertDialog` | 确认对话框 | [查看](https://daeha76.github.io/RnUI/components/alert-dialog) |
| `RnSheet` | 侧边抽屉 | [查看](https://daeha76.github.io/RnUI/components/sheet) |
| `RnPopover` | 弹出框 | [查看](https://daeha76.github.io/RnUI/components/popover) |
| `RnTooltip` | 工具提示 | [查看](https://daeha76.github.io/RnUI/components/tooltip) |
| `RnHoverCard` | 悬停卡片 | [查看](https://daeha76.github.io/RnUI/components/hover-card) |
| `RnDropdownMenu` | 下拉菜单 | [查看](https://daeha76.github.io/RnUI/components/dropdown-menu) |
| `RnContextMenu` | 右键菜单 | [查看](https://daeha76.github.io/RnUI/components/context-menu) |
| `RnDrawer` | 抽屉（移动端底部弹出） | [查看](https://daeha76.github.io/RnUI/components/drawer) |
| `RnToast` | 消息提示 | [查看](https://daeha76.github.io/RnUI/components/toast) |

### 折叠与其他

| 组件 | 描述 | 演示 |
|---|---|---|
| `RnAccordion` | 手风琴 | [查看](https://daeha76.github.io/RnUI/components/accordion) |
| `RnCollapsible` | 折叠面板 | [查看](https://daeha76.github.io/RnUI/components/collapsible) |
| `RnEmpty` | 空状态 | [查看](https://daeha76.github.io/RnUI/components/empty) |
| `RnCarousel` | 轮播 / 滑动组件 | [查看](https://daeha76.github.io/RnUI/components/carousel) |
| `RnCommand` | 命令面板 | [查看](https://daeha76.github.io/RnUI/components/command) |
| `RnDatePicker` | 日期选择器 | [查看](https://daeha76.github.io/RnUI/components/date-picker) |
| `RnGantt` | 甘特图 | [查看](https://daeha76.github.io/RnUI/components/gantt) |
| `RnButtonGroup` | 按钮组 | [查看](https://daeha76.github.io/RnUI/components/button-group) |

---

## 使用示例

### Button

支持 6 种变体和 8 种尺寸。

```razor
@* 变体 *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* 尺寸 *@
<RnButton Size="ButtonSize.Sm">Small</RnButton>
<RnButton Size="ButtonSize.Default">Default</RnButton>
<RnButton Size="ButtonSize.Lg">Large</RnButton>
<RnButton Size="ButtonSize.Icon">🔔</RnButton>
```

### Card

```razor
<RnCard>
    <RnCardHeader>
        <RnCardTitle>Card Title</RnCardTitle>
        <RnCardDescription>Card description goes here.</RnCardDescription>
    </RnCardHeader>
    <RnCardContent>
        <p>Card content with some example text.</p>
    </RnCardContent>
    <RnCardFooter>
        <RnButton>Save</RnButton>
        <RnButton Variant="ButtonVariant.Outline">Cancel</RnButton>
    </RnCardFooter>
</RnCard>
```

### Dialog

```razor
<RnButton OnClick="() => _dialogOpen = true">Open Dialog</RnButton>

<RnDialog @bind-Open="_dialogOpen">
    <RnDialogHeader>
        <RnDialogTitle>Edit Profile</RnDialogTitle>
        <RnDialogDescription>Make changes to your profile here.</RnDialogDescription>
    </RnDialogHeader>
    <div class="space-y-4 py-4">
        <RnInput Placeholder="Name" @bind-Value="_name" />
        <RnInput Type="email" Placeholder="Email" @bind-Value="_email" />
    </div>
    <RnDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">Cancel</RnButton>
        <RnButton OnClick="Save">Save Changes</RnButton>
    </RnDialogFooter>
</RnDialog>
```

### TextField

将 RnField + RnFieldLabel + RnInput 组合为单个组件的便捷封装。

```razor
<RnTextField Label="姓名" @bind-Value="_name" Placeholder="请输入姓名" />
<RnTextField Label="邮箱" @bind-Value="_email" Type="email" Placeholder="请输入邮箱" />
```

### DateField

将 RnField + RnFieldLabel + RnDatePicker 组合为单个组件的便捷封装。支持 `string?` 绑定（yyyy-MM-dd 格式），便于迁移。

```razor
<RnDateField Label="出生日期" @bind-Value="_birthDate" />
<RnDateField Label="开始日期" @bind-Value="_startDate" Placeholder="选择日期..." />
```

### DataTable

```razor
<RnDataTable TItem="PaymentRecord"
             Items="_payments"
             SelectionMode="SelectionMode.Multiple"
             PageSize="10">
    <Columns>
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Id" Title="ID" Sortable />
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Name" Title="Name" Sortable Filterable />
        <RnDataTableColumn TItem="PaymentRecord" Property="x => x.Amount" Title="Amount" Sortable>
            <CellTemplate Context="item">
                <div class="text-right font-medium">@item.Amount.ToString("C")</div>
            </CellTemplate>
        </RnDataTableColumn>
    </Columns>
</RnDataTable>
```

> 更多示例请访问 [演示站点](https://daeha76.github.io/RnUI/)。

---

## 自定义主题

RnUI 使用 CSS 自定义属性进行主题定制。通过覆盖以下变量来自定义颜色：

```css
:root {
    --background: 0 0% 100%;
    --foreground: 0 0% 3.9%;
    --primary: 0 0% 9%;
    --primary-foreground: 0 0% 98%;
    --secondary: 0 0% 96.1%;
    --destructive: 0 84.2% 60.2%;
    --muted: 0 0% 96.1%;
    --accent: 0 0% 96.1%;
    --border: 0 0% 89.8%;
    --ring: 0 0% 3.9%;
    --radius-lg: 0.5rem;
}
```

---

## 演示

**在线演示**：[https://daeha76.github.io/RnUI/](https://daeha76.github.io/RnUI/)

如需在本地运行演示，请使用 `Daeha.RnUI.Demo.Wasm` 项目：

```bash
# 构建演示 CSS
cd src/Daeha.RnUI.Demo.Wasm && npm install && npm run build:css

# 运行演示
dotnet run --project src/Daeha.RnUI.Demo.Wasm
```

在浏览器中打开 `https://localhost:7256` 即可浏览所有组件。

---

## 参与贡献

欢迎贡献代码！请随时提交 Issue 或 Pull Request。

1. Fork 本仓库
2. 创建功能分支（`git checkout -b feature/amazing-feature`）
3. 提交更改（`git commit -m 'feat: Add amazing feature'`）
4. 推送到分支（`git push origin feature/amazing-feature`）
5. 创建 Pull Request

---

## 许可证

[MIT](../LICENSE.md) 许可证

---

<div align="center">

用心为 Blazor 社区打造

</div>
