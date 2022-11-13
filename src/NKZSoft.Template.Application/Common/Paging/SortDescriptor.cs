namespace NKZSoft.Template.Application.Common.Paging;

public sealed class SortDescriptor
{
    public SortDescriptor(string field, EnumSortDirection direction = EnumSortDirection.Asc)
    {
        Field = field;
        Direction = direction;
    }

    public string Field { get; init; }

    public EnumSortDirection Direction { get; init; }

    public override string ToString() => $"{Field}:{Direction.ToString()}";
}
