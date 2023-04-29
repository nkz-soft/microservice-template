namespace NKZSoft.Template.Application.TodoItems.Features.Commands.Create.v1;

public sealed record CreateToDoItemCommand(string Title, int? ListId) : IRequest<Result<Guid>>;
