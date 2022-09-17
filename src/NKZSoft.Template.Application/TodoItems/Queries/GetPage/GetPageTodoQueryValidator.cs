namespace NKZSoft.Template.Application.TodoItems.Queries.GetPage;

using Application.Models;
using Common.Paging;
using Models;

public sealed class GetPageTodoQueryValidator
    : PagingQueryValidator<GetPageTodoItemsQuery, Result<CollectionViewModel<ToDoItemDto>>,ToDoItemFilter>
{
}
