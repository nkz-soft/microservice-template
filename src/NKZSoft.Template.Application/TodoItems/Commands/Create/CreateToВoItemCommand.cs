namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

public sealed record CreateToВoItemCommand(string Title, int? ListId) : IRequest<Result<Guid>>;
