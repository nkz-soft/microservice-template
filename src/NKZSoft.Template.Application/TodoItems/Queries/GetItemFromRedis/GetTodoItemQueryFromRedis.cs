namespace NKZSoft.Template.Application.TodoItems.Queries.GetItemFromRedis;

using Application.Models;

public record GetTodoItemQueryFromRedis(Guid Id) : IRequest<Result<ToDoItemDto>>;
