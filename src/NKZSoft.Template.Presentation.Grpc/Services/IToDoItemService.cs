namespace NKZSoft.Template.Presentation.Grpc.Services;

using Models;
using Models.ToDoItem;

[ServiceContract]
public interface IToDoItemService
{
    [OperationContract]
    ValueTask<ToDoItemResponse> GetToDoItemById(GetTodoItemRequest request,
        CancellationToken cancellationToken = default);

    [OperationContract]
    IAsyncEnumerable<ToDoItemsResponse> GetRageToDoItems(GetPageTodoItemsRequest request,
        CancellationToken cancellationToken = default);

    [OperationContract]
    IAsyncEnumerable<ToDoItemResponse> GetToDoItems(CancellationToken cancellationToken = default);
}
