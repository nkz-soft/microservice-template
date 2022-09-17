namespace NKZSoft.Template.Application.Common.Paging;

public sealed record PageContext<T> : IPageContext<T>
    where T : class, new()
{
    public PageContext(
        int pageIndex,
        int pageSize,
        T? filter = null,
        IEnumerable<SortDescriptor>? listSort = null,
        IEnumerable<GroupDescriptor>? listGroup = null)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Filter = filter ?? new T();
        ListSort = listSort ?? Enumerable.Empty<SortDescriptor>();
        ListGroup = listGroup ?? Enumerable.Empty<GroupDescriptor>();
    }

    public int PageIndex { get; }

    public int PageSize { get; }

    public T Filter { get; }

    public IEnumerable<SortDescriptor> ListSort { get; init; }

    public IEnumerable<GroupDescriptor> ListGroup { get; init; }

    public bool IsValid()
    {
        return PageIndex > 0 && PageSize > 0 &&
               Filter != null && ListSort != null;
    }
}
