namespace NKZSoft.Template.Presentation.Grpc.Services;

using Application.TodoItems.Queries.GetItem;
using Models;
using Models.ToDoItem;
using NKZSoft.Template.Common;

public class ToDoItemService : IToDoItemService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ToDoItemService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator.ThrowIfNull();
        _mapper = mapper.ThrowIfNull();
    }

    public async ValueTask<ToDoItemResponse> GetToDoItemById(GetTodoItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetTodoItemQuery(request.Id), cancellationToken);

        return await result
            .BuildAdapter(_mapper.Config)
            .AdaptToTypeAsync<ToDoItemResponse>()
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<ToDoItemsResponse> GetToDoItems(GetTodoItemsRequest request, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var pageContext = new PageContext<ToDoItemFilter>(request.PageIndex, request.PageSize);

        while (!cancellationToken.IsCancellationRequested)
        {
            var items = await _mediator.Send(GetPageTodoItemsQuery.Create(pageContext), cancellationToken);

            if (items.IsFailed || !items.Value.Data.Any())
            {
                yield break;
            }

            yield return  await items.BuildAdapter(_mapper.Config)
                .AdaptToTypeAsync<ToDoItemsResponse>()
                .ConfigureAwait(false);

            ++pageContext;
        }
    }
}
