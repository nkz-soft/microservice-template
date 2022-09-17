namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

public sealed record CreateToВoItemCommand(string Title, int? ListId) : IRequest<IResult<int>>;
