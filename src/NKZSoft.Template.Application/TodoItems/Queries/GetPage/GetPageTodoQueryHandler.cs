namespace NKZSoft.Template.Application.TodoItems.Queries.GetPage;

using NKZSoft.Template.Application.Models;
using Common.Handlers;
using Common.Interfaces;
using Common.Paging;
using Common.Repositories;

public sealed class GetPageTodoQueryHandler :
    PagingQueryHandler<GetPageTodoItemsQuery, Result<CollectionViewModel<ToDoItemDto>>, ToDoItemDto>
{
    private readonly IToDoItemRepository _repository;

    public GetPageTodoQueryHandler(
        IToDoItemRepository repository,
        IApplicationDbContext context,
        ICurrentUserService currentUserService, IMapper mapper) : base(context, mapper, currentUserService) =>
        _repository = repository.ThrowIfNull();

    public override async Task<Result<CollectionViewModel<ToDoItemDto>>> Handle(GetPageTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = ToDoItemSpecification.Create(request.PageContext);

        var entities = await _repository
            .ListAsync(specification, cancellationToken)
            .ConfigureAwait(false);

        var dtoItems = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<ToDoItemDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<ToDoItemDto>(
            dtoItems, dtoItems.Count));
    }
}
