namespace NKZSoft.Template.Application.TodoItems.Features.Commands.Update.v1;

public sealed record UpdateTodoItemCommand(Guid Id, string Title, string Description) : IRequest;
