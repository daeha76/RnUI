# DataTable Architecture Patterns

## 개요

`RnDataTable<TItem>`은 정렬, 필터, 페이지네이션, 선택, 행 확장을 지원하는 고급 데이터 테이블.
TanStack Table의 구조를 Blazor CascadingValue 패턴으로 구현.

## State 관리 — DataTableState<TItem>

모든 테이블 상태는 `DataTableState<TItem>` 단일 객체에서 관리:

```
RnDataTable<TItem>
  └── CascadingValue: DataTableState<TItem>
        ├── RnDataTableSearch (검색)
        ├── RnDataTableColumnToggle (컬럼 토글)
        ├── RnDataTableColumn (컬럼 정의)
        ├── RnDataTablePagination (페이지네이션)
        └── RnDataTableRowActions (행 액션)
```

State 주요 책임:
- `Items` 원본 데이터 보관
- 필터/정렬/페이지네이션 적용된 `PagedItems` 계산
- 컬럼 정의 (`ColumnDefinitions`)
- 선택 상태 (`SelectedItems`)
- 검색어 (`SearchTerm`)
- 현재 페이지, 정렬 컬럼/방향

## OnChange 이벤트 패턴 (핵심)

자식 컴포넌트가 상태를 변경하면 부모(RnDataTable)에 알려야 re-render가 발생.
CascadingParameter는 단방향이므로, `DataTableState.OnChange` 이벤트로 역방향 알림:

```csharp
// DataTableState.cs
public event Action? OnChange;

public void SetSearchTerm(string term)
{
    SearchTerm = term;
    ApplyFilters();
    OnChange?.Invoke(); // 부모에게 알림
}

// RnDataTable.razor — OnChange 구독
protected override void OnInitialized()
{
    State.OnChange += StateHasChanged;
}
```

**규칙**: 자식 컴포넌트는 `State.Set*()`만 호출. 직접 `StateHasChanged()` 호출 금지.

## 리스트 불변성 (소비자 규칙)

Blazor는 `OnParametersSet`에서 파라미터 참조가 변경되었는지 확인.
같은 List 참조를 변경(mutate)하면 변경이 감지되지 않을 수 있음.

### 권장: 불변 리스트 패턴

```csharp
// 새 리스트 참조 생성 → Blazor가 변경 감지
_payments = [.._payments, newPayment];
```

### 대안: RefreshData() 호출

리스트를 직접 변경(mutate)한 경우 `RefreshData()`로 강제 갱신:

```csharp
// @ref로 테이블 참조 획득
<RnDataTable @ref="_table" TItem="Payment" Items="_payments" ...>

@code {
    private RnDataTable<Payment>? _table;

    private void AddPayment()
    {
        _payments.Add(newPayment); // 같은 참조 변경 (mutation)
        _table?.RefreshData();     // 강제 갱신
    }
}
```

**권장 순위**: 불변 패턴 > RefreshData(). 불변 패턴이 Blazor의 변경 감지에 자연스럽게 맞음.

## RnDataTable 주요 Parameter

| Parameter | 타입 | 설명 |
|-----------|------|------|
| `Items` | `IEnumerable<TItem>` | 데이터 소스 |
| `SelectionMode` | `SelectionMode` | None, Single, Multiple |
| `PageSize` | `int` | 기본 페이지 크기 |
| `PageSizeOptions` | `int[]` | 페이지 크기 선택지 |
| `RowClass` | `Func<TItem, string?>` | 행별 조건부 CSS 클래스 |
| `Toolbar` | `RenderFragment` | 검색/필터 영역 |
| `Columns` | `RenderFragment` | 컬럼 정의 영역 |
| `EmptyContent` | `RenderFragment` | 데이터 없을 때 표시 |

## RnDataTableColumn 주요 Parameter

| Parameter | 타입 | 설명 |
|-----------|------|------|
| `Property` | `Expression<Func<TItem, object>>` | 바인딩 속성 |
| `Title` | `string` | 컬럼 헤더 텍스트 |
| `Sortable` | `bool` | 정렬 가능 여부 |
| `Filterable` | `bool` | 필터 가능 여부 |
| `CellTemplate` | `RenderFragment<TItem>` | 셀 커스텀 렌더링 |
| `Id` | `string` | 컬럼 고유 ID (액션 컬럼 등) |

## 파일 구조

```
Components/UI/DataTable/
├── RnDataTable.razor              # 메인 컴포넌트 (CascadingValue 제공)
├── RnDataTableColumn.razor        # 컬럼 정의
├── RnDataTableSearch.razor        # 검색 입력
├── RnDataTableColumnToggle.razor  # 컬럼 표시/숨김 토글
├── RnDataTablePagination.razor    # 페이지네이션
├── RnDataTableRowActions.razor    # 행 액션 (DropdownMenu)
├── RnDataTableColumnHeader.razor  # 컬럼 헤더 (정렬 표시)
├── DataTableState.cs              # 상태 관리 클래스
└── DataTableEnums.cs              # SelectionMode enum
```
