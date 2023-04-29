namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetItem.v1;

using Models;

public sealed record GetTodoItemQuery(Guid Id) : IRequest<Result<ToDoItemDto>>;
