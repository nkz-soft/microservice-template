namespace NKZSoft.Template.Application.Common.Paging;

public sealed class SortDescriptor(string field, EnumSortDirection direction = EnumSortDirection.Asc)
{
    public string Field { get; init; } = field;

    public EnumSortDirection Direction { get; init; } = direction;

    public override string ToString() => $"{Field}:{Direction}";
}
