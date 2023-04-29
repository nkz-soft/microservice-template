namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetPage.v1;

using NKZSoft.Template.Application.Common.Paging;
using NKZSoft.Template.Application.TodoItems.Models;

public sealed class GetPageTodoItemsQuery : PagingQuery<Result<CollectionViewModel<ToDoItemDto>>, ToDoItemFilter>
{
    public GetPageTodoItemsQuery(PageContext<ToDoItemFilter> pageContext) : base(pageContext)
    {
    }

    public static GetPageTodoItemsQuery Create(PageContext<ToDoItemFilter> pageContext) => new(pageContext);
}
