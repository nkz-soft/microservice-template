namespace NKZSoft.Template.Application.TodoItems.Queries.Get;

using Application.Models;

public sealed record GetTodoItemQuery(Guid Id) : IRequest<Result<ToDoItemDto>>;
