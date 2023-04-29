namespace NKZSoft.Template.Application.TodoItems.Features.Commands.CreateInRedis.v1;

public sealed record CreateToDoItemRedisCommand(string Title, int? ListId) : IRequest<Result<Guid>>;
