namespace NKZSoft.Template.Application.Common.Paging;

using System.Numerics;

public sealed record PageContext<T> : IPageContext<T>,
    IIncrementOperators<PageContext<T>>, IDecrementOperators<PageContext<T>>
    where T : class, new()
{
    public PageContext(
        int pageIndex,
        int pageSize) : this(pageIndex, pageSize, new T(), [], [])
    {}

    public PageContext(
        int pageIndex,
        int pageSize,
        T filter) : this(pageIndex, pageSize, filter, [], [])
    {}

    public PageContext(
        int pageIndex,
        int pageSize,
        T filter,
        IEnumerable<SortDescriptor> listSort)
            : this(pageIndex, pageSize, filter, listSort, [])
    {}

    public PageContext(
        int pageIndex,
        int pageSize,
        IEnumerable<SortDescriptor> listSort)
        : this(pageIndex, pageSize, new T(), listSort, [])
    {}

    public PageContext(
        int pageIndex,
        int pageSize,
        T filter,
        IEnumerable<SortDescriptor> listSort,
        IEnumerable<GroupDescriptor> listGroup)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Filter = filter;
        ListSort = listSort;
        ListGroup = listGroup;
    }

    public int PageIndex { get; private set; }

    public int PageSize { get; }

    public T Filter { get; }

    public IEnumerable<SortDescriptor> ListSort { get; }

    public IEnumerable<GroupDescriptor> ListGroup { get; }

    public bool IsValid() => PageIndex > 0 && PageSize > 0;

    public static PageContext<T> operator ++(PageContext<T> value)
    {
        value.PageIndex++;
        return value;
    }

    public static PageContext<T> operator --(PageContext<T> value)
    {
        value.PageIndex--;
        return value;
    }
}
