namespace NKZSoft.Template.Presentation.Grpc.Services;
using Models;
using Models.ToDoItem;

[ServiceContract]
public interface IToDoItemService
{
    [OperationContract]
    ValueTask<ToDoItemResponse> GetToDoItemByIdAsync(GetTodoItemRequest request,
        CancellationToken cancellationToken = default);

    [OperationContract]
    IAsyncEnumerable<ToDoItemsResponse> GetRageToDoItemsAsync(GetPageTodoItemsRequest request,
        CancellationToken cancellationToken = default);

    [OperationContract]
    IAsyncEnumerable<ToDoItemResponse> GetToDoItemsAsync(CancellationToken cancellationToken = default);
}
