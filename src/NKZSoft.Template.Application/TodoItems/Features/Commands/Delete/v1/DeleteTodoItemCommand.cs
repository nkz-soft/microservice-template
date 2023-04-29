namespace NKZSoft.Template.Application.TodoItems.Features.Commands.Delete.v1;

public sealed record DeleteTodoItemCommand(Guid Id)  : IRequest;
