namespace NKZSoft.Template.Application.Common.Paging;

public interface IPageContext<out T>
{
    int PageIndex { get; }

    int PageSize { get; }

    T Filter { get; }

    IEnumerable<SortDescriptor> ListSort { get; }

    IEnumerable<GroupDescriptor> ListGroup { get; }

    bool IsValid();
}
