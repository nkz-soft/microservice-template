namespace NKZSoft.Template.Application.TodoItems.Queries.GetStream;

using Application.Models;

public sealed record GetStreamTodoItemsQuery : IStreamRequest<ToDoItemDto>
{
}
