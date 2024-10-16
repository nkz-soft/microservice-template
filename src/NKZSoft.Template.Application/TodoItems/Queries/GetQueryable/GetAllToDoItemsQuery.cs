namespace NKZSoft.Template.Application.TodoItems.Queries.GetQueryable;

public sealed record GetAllToDoItemsQuery : IRequest<IQueryable<ToDoItem>>
{
}
