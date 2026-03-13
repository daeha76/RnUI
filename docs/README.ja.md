# RnUI

**Documentation**: [English](../README.md) | [한국어](README.ko.md) | [中文](README.zh-CN.md) | [Español](README.es.md) | [Deutsch](README.de.md) | **日本語**

**[shadcn/ui](https://ui.shadcn.com) の Blazor 移植版**

Tailwind CSS を使用して構築された、美しくアクセシブルな .NET Blazor アプリケーション向け UI コンポーネントライブラリです。

[**ライブデモ**](https://daeha76.github.io/RnUI/)

---

## RnUI を選ぶ理由

- **54 のコンポーネントカテゴリ** — 194 の Razor コンポーネントファイルを含む包括的な UI ライブラリ
- **shadcn/ui ベース** — Web で実績のあるデザインシステムを Blazor に直接移植
- **Tailwind CSS** — oklch カラーシステムと CSS カスタムプロパティベースのテーマ機能
- **ダークモード** — ライト/ダークモードの組み込みサポート
- **アクセシブル** — アクセシビリティを考慮して設計されたコンポーネント
- **外部依存関係ゼロ** — ASP.NET Core フレームワーク参照のみ
- **マルチターゲット** — .NET 8.0, 9.0, 10.0 対応 | Blazor Server & WebAssembly 互換

---

## クイックスタート

### 1. パッケージのインストール

```bash
dotnet add package Daeha.RnUI
```

### 2. インポートの追加

`_Imports.razor` に以下を追加してください：

```razor
@using Daeha.RnUI.Components.UI
```

### 3. スタイルシートのリンク

`App.razor` または `_Host.cshtml` に CSS を追加してください：

```html
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
```

### 4. Tailwind CSS のセットアップ

RnUI は Tailwind CSS v4 を使用しています。Blazor プロジェクトで Tailwind CSS をセットアップし、ユーティリティクラスをスキャン・コンパイルする必要があります。

#### 4-1. Tailwind CSS のインストール

プロジェクトルートに Tailwind CSS をインストールします：

```bash
npm init -y
npm install -D tailwindcss @tailwindcss/cli
```

#### 4-2. CSS エントリファイルの作成

`wwwroot/input.css` ファイルを作成し、Razor ファイルをスキャン対象に含めます：

```css
@import "tailwindcss";
@source "../**/*.razor";
```

> **NuGet パッケージ使用時**: NuGet 経由でインストールした場合、ライブラリの `.razor` ファイルはローカルに存在しません。ただし、RnUI コンポーネントの基本スタイル（`.cn-*` クラス）は `shadcn.css` に既に含まれているため、追加の `@source` 設定なしで動作します。スキャンが必要なのは、あなた自身の `.razor` ファイルで使用する Tailwind ユーティリティクラスのみです。

#### 4-3. ビルドスクリプトの追加

`package.json` にビルドスクリプトを追加します：

```json
{
  "scripts": {
    "build:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css",
    "watch:css": "npx @tailwindcss/cli -i wwwroot/input.css -o wwwroot/css/tailwindcss.css --watch"
  }
}
```

#### 4-4. スタイルシートのリンク

`App.razor` または `_Host.cshtml` に RnUI スタイルと Tailwind CSS 出力ファイルの両方をリンクします：

```html
<!-- RnUI コンポーネントスタイル -->
<link rel="stylesheet" href="_content/Daeha.RnUI/css/shadcn.css" />
<!-- Tailwind ユーティリティクラス -->
<link rel="stylesheet" href="css/tailwindcss.css" />
```

#### 4-5. CSS のビルド

開発時はウォッチモードを、本番環境ではビルドを使用します：

```bash
npm run watch:css   # 開発用（ファイル変更を監視）
npm run build:css   # 本番用（一回限りのビルド）
```

---

## コンポーネント

> 各コンポーネントのライブデモと詳細な使い方は [**デモサイト**](https://daeha76.github.io/RnUI/components) をご覧ください。

### ボタン & 入力

| コンポーネント | 説明 | デモ |
|---|---|---|
| `RnButton` | Default, Secondary, Outline, Ghost, Destructive, Link バリアントをサポート | [Live](https://daeha76.github.io/RnUI/components/button) |
| `RnInput` | テキスト入力フィールド | [Live](https://daeha76.github.io/RnUI/components/input) |
| `RnTextarea` | 複数行テキスト入力 | [Live](https://daeha76.github.io/RnUI/components/textarea) |
| `RnLabel` | フォームラベル | [Live](https://daeha76.github.io/RnUI/components/label) |
| `RnCheckbox` | チェックボックス | [Live](https://daeha76.github.io/RnUI/components/checkbox) |
| `RnSwitch` | トグルスイッチ | [Live](https://daeha76.github.io/RnUI/components/switch) |
| `RnRadioGroup` | ラジオボタングループ | [Live](https://daeha76.github.io/RnUI/components/radio-group) |
| `RnSelect` | ドロップダウン選択 | [Live](https://daeha76.github.io/RnUI/components/select) |
| `RnToggle` | トグルボタン | [Live](https://daeha76.github.io/RnUI/components/toggle) |
| `RnToggleGroup` | トグルボタングループ | [Live](https://daeha76.github.io/RnUI/components/toggle-group) |
| `RnCombobox` | 検索可能なドロップダウン選択 | [Live](https://daeha76.github.io/RnUI/components/combobox) |
| `RnInputOTP` | OTP 入力フィールド | [Live](https://daeha76.github.io/RnUI/components/input-otp) |
| `RnField` | フィールドコンテナ（Label, Description, Error を統合） | [Live](https://daeha76.github.io/RnUI/components/field) |
| `RnForm` | フォームバリデーション | [Live](https://daeha76.github.io/RnUI/components/form) |

### カード & コンテナ

| コンポーネント | 説明 | デモ |
|---|---|---|
| `RnCard` | Header, Title, Description, Content, Footer, Action で構成 | [Live](https://daeha76.github.io/RnUI/components/card) |
| `RnAlert` | アラートメッセージ（Default, Destructive） | [Live](https://daeha76.github.io/RnUI/components/alert) |
| `RnBadge` | ステータスバッジ | [Live](https://daeha76.github.io/RnUI/components/badge) |
| `RnAspectRatio` | アスペクト比コンテナ | [Live](https://daeha76.github.io/RnUI/components/aspect-ratio) |
| `RnScrollArea` | カスタムスクロールエリア | [Live](https://daeha76.github.io/RnUI/components/scroll-area) |
| `RnSeparator` | 区切り線 | [Live](https://daeha76.github.io/RnUI/components/separator) |
| `RnResizable` | リサイズ可能なパネル | [Live](https://daeha76.github.io/RnUI/components/resizable) |

### データ表示

| コンポーネント | 説明 | デモ |
|---|---|---|
| `RnTable` | 基本テーブル（Head, Body, Row, Header, Cell） | [Live](https://daeha76.github.io/RnUI/components/table) |
| `RnDataTable` | 高機能データテーブル（ソート、フィルタリング、ページネーション、選択、行展開） | [Live](https://daeha76.github.io/RnUI/components/data-table) |
| `RnAvatar` | ユーザーアバター（グループ対応） | [Live](https://daeha76.github.io/RnUI/components/avatar) |
| `RnProgress` | プログレスバー | [Live](https://daeha76.github.io/RnUI/components/progress) |
| `RnSlider` | スライダー | [Live](https://daeha76.github.io/RnUI/components/slider) |
| `RnSkeleton` | ローディングスケルトン | [Live](https://daeha76.github.io/RnUI/components/skeleton) |
| `RnSpinner` | ローディングスピナー | [Live](https://daeha76.github.io/RnUI/components/spinner) |
| `RnKbd` | キーボードショートカット表示 | [Live](https://daeha76.github.io/RnUI/components/kbd) |
| `RnCalendar` | カレンダー | [Live](https://daeha76.github.io/RnUI/components/calendar) |

### ナビゲーション

| コンポーネント | 説明 | デモ |
|---|---|---|
| `RnTabs` | タブコンポーネント（Default, Line バリアント） | [Live](https://daeha76.github.io/RnUI/components/tabs) |
| `RnBreadcrumb` | パンくずナビゲーション | [Live](https://daeha76.github.io/RnUI/components/breadcrumb) |
| `RnPagination` | ページネーション | [Live](https://daeha76.github.io/RnUI/components/pagination) |
| `RnNavigationMenu` | ナビゲーションメニュー | [Live](https://daeha76.github.io/RnUI/components/navigation-menu) |
| `RnSidebar` | サイドバー（Header, Content, Footer, Group, Menu） | [Live](https://daeha76.github.io/RnUI/components/sidebar) |
| `RnMainLayout01` | 3スロットレイアウト（Header/Content/Footer）、RnSidebarInset 内で使用 | [Live](https://daeha76.github.io/RnUI/components/main-layout-01) |
| `RnMenubar` | メニューバー | [Live](https://daeha76.github.io/RnUI/components/menubar) |

### オーバーレイ

| コンポーネント | 説明 | デモ |
|---|---|---|
| `RnDialog` | モーダルダイアログ | [Live](https://daeha76.github.io/RnUI/components/dialog) |
| `RnAlertDialog` | 確認ダイアログ | [Live](https://daeha76.github.io/RnUI/components/alert-dialog) |
| `RnSheet` | サイドシート | [Live](https://daeha76.github.io/RnUI/components/sheet) |
| `RnPopover` | ポップオーバー | [Live](https://daeha76.github.io/RnUI/components/popover) |
| `RnTooltip` | ツールチップ | [Live](https://daeha76.github.io/RnUI/components/tooltip) |
| `RnHoverCard` | ホバーカード | [Live](https://daeha76.github.io/RnUI/components/hover-card) |
| `RnDropdownMenu` | ドロップダウンメニュー | [Live](https://daeha76.github.io/RnUI/components/dropdown-menu) |
| `RnContextMenu` | コンテキストメニュー | [Live](https://daeha76.github.io/RnUI/components/context-menu) |
| `RnDrawer` | ドロワー（モバイル用ボトムシート） | [Live](https://daeha76.github.io/RnUI/components/drawer) |
| `RnToast` | トースト通知 | [Live](https://daeha76.github.io/RnUI/components/toast) |

### ディスクロージャー & その他

| コンポーネント | 説明 | デモ |
|---|---|---|
| `RnAccordion` | アコーディオン | [Live](https://daeha76.github.io/RnUI/components/accordion) |
| `RnCollapsible` | 折りたたみ | [Live](https://daeha76.github.io/RnUI/components/collapsible) |
| `RnEmpty` | 空状態 | [Live](https://daeha76.github.io/RnUI/components/empty) |
| `RnCarousel` | カルーセル / スライダー | [Live](https://daeha76.github.io/RnUI/components/carousel) |
| `RnCommand` | コマンドパレット | [Live](https://daeha76.github.io/RnUI/components/command) |
| `RnDatePicker` | 日付選択 | [Live](https://daeha76.github.io/RnUI/components/date-picker) |
| `RnGantt` | ガントチャート | [Live](https://daeha76.github.io/RnUI/components/gantt) |
| `RnButtonGroup` | ボタングループ | [Live](https://daeha76.github.io/RnUI/components/button-group) |

---

## 使用例

### Button

6 つのバリアントと 8 つのサイズをサポートしています。

```razor
@* バリアント *@
<RnButton>Default</RnButton>
<RnButton Variant="ButtonVariant.Secondary">Secondary</RnButton>
<RnButton Variant="ButtonVariant.Outline">Outline</RnButton>
<RnButton Variant="ButtonVariant.Ghost">Ghost</RnButton>
<RnButton Variant="ButtonVariant.Destructive">Destructive</RnButton>
<RnButton Variant="ButtonVariant.Link">Link</RnButton>

@* サイズ *@
<RnButton Size="ButtonSize.Sm">Small</RnButton>
<RnButton Size="ButtonSize.Default">Default</RnButton>
<RnButton Size="ButtonSize.Lg">Large</RnButton>
<RnButton Size="ButtonSize.Icon">🔔</RnButton>
```

### Card

```razor
<RnCard>
    <RnCardHeader>
        <RnCardTitle>カードタイトル</RnCardTitle>
        <RnCardDescription>カードの説明文をここに記載します。</RnCardDescription>
    </RnCardHeader>
    <RnCardContent>
        <p>カードのコンテンツとサンプルテキストです。</p>
    </RnCardContent>
    <RnCardFooter>
        <RnButton>保存</RnButton>
        <RnButton Variant="ButtonVariant.Outline">キャンセル</RnButton>
    </RnCardFooter>
</RnCard>
```

### Dialog

```razor
<RnButton OnClick="() => _dialogOpen = true">ダイアログを開く</RnButton>

<RnDialog @bind-Open="_dialogOpen">
    <RnDialogHeader>
        <RnDialogTitle>プロフィール編集</RnDialogTitle>
        <RnDialogDescription>プロフィール情報を変更できます。</RnDialogDescription>
    </RnDialogHeader>
    <div class="space-y-4 py-4">
        <RnInput Placeholder="名前" @bind-Value="_name" />
        <RnInput Type="email" Placeholder="メールアドレス" @bind-Value="_email" />
    </div>
    <RnDialogFooter>
        <RnButton Variant="ButtonVariant.Outline" OnClick="() => _dialogOpen = false">キャンセル</RnButton>
        <RnButton OnClick="Save">変更を保存</RnButton>
    </RnDialogFooter>
</RnDialog>
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

> その他の例については、[デモサイト](https://daeha76.github.io/RnUI/) をご覧ください。

---

## カスタマイズ

RnUI はテーマ設定に CSS カスタムプロパティを使用しています。以下の変数をオーバーライドすることで色をカスタマイズできます：

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

## デモ

**オンラインデモ**: [https://daeha76.github.io/RnUI/](https://daeha76.github.io/RnUI/)

ローカルでデモを実行するには、`Daeha.RnUI.Demo.Wasm` プロジェクトを使用します：

```bash
# デモ CSS のビルド
cd src/Daeha.RnUI.Demo.Wasm && npm install && npm run build:css

# デモの実行
dotnet run --project src/Daeha.RnUI.Demo.Wasm
```

ブラウザで `https://localhost:7256` を開くと、すべてのコンポーネントを確認できます。

---

## コントリビューション

コントリビューションを歓迎します！Issue や Pull Request をお気軽にお送りください。

1. リポジトリをフォーク
2. フィーチャーブランチを作成（`git checkout -b feature/amazing-feature`）
3. 変更をコミット（`git commit -m 'feat: Add amazing feature'`）
4. ブランチにプッシュ（`git push origin feature/amazing-feature`）
5. Pull Request を作成

---

## ライセンス

[MIT](../LICENSE.md) ライセンス
