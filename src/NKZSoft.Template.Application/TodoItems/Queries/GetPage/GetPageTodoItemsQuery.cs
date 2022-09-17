using NKZSoft.Template.Application.Models;
using NKZSoft.Template.Application.TodoItems.Models;
using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.TodoItems.Queries.GetPage;

using Application.Models;
using Common.Paging;
using Models;

public sealed class GetPageTodoItemsQuery : PagingQuery<Result<CollectionViewModel<ToDoItemDto>>, ToDoItemFilter>
{
    public GetPageTodoItemsQuery(IPageContext<ToDoItemFilter> pageContext) : base(pageContext)
    {
    }

    public static GetPageTodoItemsQuery Create(PageContext<ToDoItemFilter> pageContext) => new(pageContext);
}