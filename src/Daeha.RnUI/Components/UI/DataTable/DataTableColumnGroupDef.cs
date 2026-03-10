namespace RnUI.Components.UI.DataTable;

public class DataTableColumnGroupDef
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public List<string> ColumnIds { get; set; } = [];
    public int Order { get; set; }
}
