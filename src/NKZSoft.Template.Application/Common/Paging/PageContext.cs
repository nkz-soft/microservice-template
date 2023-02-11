namespace NKZSoft.Template.Application.Common.Paging;

using System.Numerics;

public sealed record PageContext<T> : IPageContext<T>,
    IIncrementOperators<PageContext<T>>, IDecrementOperators<PageContext<T>>
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

    public int PageIndex { get; private set; }

    public int PageSize { get; }

    public T Filter { get; }

    public IEnumerable<SortDescriptor> ListSort { get; }

    public IEnumerable<GroupDescriptor> ListGroup { get; }

    public bool IsValid() => PageIndex > 0 && PageSize > 0;

    public static PageContext<T> operator ++(PageContext<T> obj)
    {
        obj.PageIndex++;
        return obj;
    }

    public static PageContext<T> operator --(PageContext<T> obj)
    {
        obj.PageIndex--;
        return obj;
    }
}
