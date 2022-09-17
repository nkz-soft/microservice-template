namespace NKZSoft.Template.Application.TodoItems.Commands.Update;

public sealed record UpdateTodoItemCommand(int Id, string Title, string Description) : IRequest;