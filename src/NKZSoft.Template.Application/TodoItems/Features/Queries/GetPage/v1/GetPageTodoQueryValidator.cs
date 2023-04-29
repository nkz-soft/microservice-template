namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetPage.v1;

using Common.Paging;
using Models;

internal sealed class GetPageTodoQueryValidator
    : PagingQueryValidator<GetPageTodoItemsQuery, Result<CollectionViewModel<ToDoItemDto>>,ToDoItemFilter>
{
}
