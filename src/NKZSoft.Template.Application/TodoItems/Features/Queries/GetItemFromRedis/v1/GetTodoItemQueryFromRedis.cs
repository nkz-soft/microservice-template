namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetItemFromRedis.v1;

using Models;

public record GetTodoItemQueryFromRedis(Guid Id) : IRequest<Result<ToDoItemDto>>;
