namespace NKZSoft.Template.Application.TodoItems.Queries.GetItem;

using NKZSoft.Template.Application.Models;

public sealed record GetTodoItemQuery(Guid Id) : IRequest<Result<ToDoItemDto>>;
