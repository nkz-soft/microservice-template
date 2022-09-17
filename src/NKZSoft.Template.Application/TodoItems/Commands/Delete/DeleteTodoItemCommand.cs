namespace NKZSoft.Template.Application.TodoItems.Commands.Delete;

public sealed record DeleteTodoItemCommand(int Id)  : IRequest;