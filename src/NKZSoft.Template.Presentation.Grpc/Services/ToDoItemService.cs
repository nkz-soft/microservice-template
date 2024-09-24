namespace NKZSoft.Template.Presentation.Grpc.Services;

using Application.TodoItems.Queries.GetStream;
using Models;
using Models.ToDoItem;
using Common;

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
        var result = await _mediator.Send(new GetTodoItemQuery(request.Id), cancellationToken)
            .ConfigureAwait(false);

        return await result
            .BuildAdapter(_mapper.Config)
            .AdaptToTypeAsync<ToDoItemResponse>()
            .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<ToDoItemResponse> GetToDoItems(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var stream =  _mediator
            .CreateStream(new GetStreamTodoItemsQuery(), cancellationToken);

        await foreach (var item in stream.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return  await item.BuildAdapter(_mapper.Config)
                .AdaptToTypeAsync<ToDoItemResponse>()
                .ConfigureAwait(false);
        }
    }

    public async IAsyncEnumerable<ToDoItemsResponse> GetRageToDoItems(GetPageTodoItemsRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var pageContext = new PageContext<ToDoItemFilter>(request.PageIndex, request.PageSize);

        while (!cancellationToken.IsCancellationRequested)
        {
            var items = await _mediator
                .Send(GetPageTodoItemsQuery.Create(pageContext), cancellationToken)
                .ConfigureAwait(false);

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
