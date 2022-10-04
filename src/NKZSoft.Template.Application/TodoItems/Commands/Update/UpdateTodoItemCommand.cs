namespace NKZSoft.Template.Application.TodoItems.Commands.Update;

public sealed record UpdateTodoItemCommand(Guid Id, string Title, string Description) : IRequest;
