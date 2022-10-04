namespace NKZSoft.Template.Application.TodoItems.Commands.Delete;

public sealed record DeleteTodoItemCommand(Guid Id)  : IRequest;
