namespace NKZSoft.Template.Application.TodoItems.Queries.GetStream;

using System.Runtime.CompilerServices;
using Application.Models;
using Common.Handlers;
using Common.Interfaces;
using Common.Repositories.PostgreSql;
using NKZSoft.Template.Common.Extensions;

public class GetStreamTodoItemsQueryHandler : StreamRequestHandlerBase<GetStreamTodoItemsQuery, ToDoItemDto>
{
    private readonly IToDoItemRepository _repository;

    public GetStreamTodoItemsQueryHandler(
        IToDoItemRepository repository,
        ICurrentUserService currentUserService,
        IMapper autoMapper) : base(currentUserService, autoMapper) =>
        _repository = repository.ThrowIfNull();

    public override async IAsyncEnumerable<ToDoItemDto> Handle(GetStreamTodoItemsQuery request,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var specification = ToDoItemSpecification.Create();

        await foreach (var entity in _repository
                           .AsAsyncEnumerable(specification)
                           .WithCancellation(cancellationToken)
                           .ConfigureAwait(false))
        {
            yield return await entity
                .BuildAdapter(Mapper.Config)
                .AdaptToTypeAsync<ToDoItemDto>()
                .ConfigureAwait(false);
        }
    }
}
