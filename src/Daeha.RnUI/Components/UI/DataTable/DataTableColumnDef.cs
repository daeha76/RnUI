using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace RnUI.Components.UI.DataTable;

public class DataTableColumnDef<TItem>
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public Expression<Func<TItem, object?>>? PropertyExpression { get; set; }
    public Func<TItem, object?>? PropertyAccessor { get; set; }
    public Func<TItem, string>? StringAccessor { get; set; }
    public bool Sortable { get; set; }
    public bool Filterable { get; set; }
    public bool Visible { get; set; } = true;
    public string? Width { get; set; }
    public bool MergeRows { get; set; }
    public RenderFragment<TItem>? CellTemplate { get; set; }
    public RenderFragment<TItem>? HeaderTemplate { get; set; }
    public Func<TItem, string?>? CellClass { get; set; }
    public int Order { get; set; }
    public string? GroupId { get; set; }

    public string GetStringValue(TItem item)
    {
        if (StringAccessor != null) return StringAccessor(item);
        if (PropertyAccessor != null) return PropertyAccessor(item)?.ToString() ?? "";
        return "";
    }

    public IComparable? GetSortValue(TItem item)
    {
        if (PropertyAccessor == null) return null;
        return PropertyAccessor(item) as IComparable;
    }

    internal static string ExtractMemberName(Expression<Func<TItem, object?>> expression)
    {
        var body = expression.Body;
        if (body is UnaryExpression unary)
            body = unary.Operand;
        if (body is MemberExpression member)
            return member.Member.Name;
        return "";
    }

    internal static Func<TItem, object?> CompileAccessor(Expression<Func<TItem, object?>> expression)
    {
        return expression.Compile();
    }
}
