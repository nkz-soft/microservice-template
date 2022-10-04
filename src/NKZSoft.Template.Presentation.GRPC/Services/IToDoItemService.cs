namespace NKZSoft.Template.Presentation.GRPC.Services;

using Models;
using Models.ToDoItem;

[ServiceContract]
public interface IToDoItemService
{
    [OperationContract]
    ValueTask<ToDoItemResponse> GetToDoItemById(GetTodoItemRequest request, CancellationToken cancellationToken = default);

    [OperationContract]
    IAsyncEnumerable<ToDoItemsResponse> GetToDoItems(GetTodoItemsRequest request, CancellationToken cancellationToken = default);
}
