namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetPage.v1;

using Models;
using NKZSoft.Template.Application.Common.Handlers;
using NKZSoft.Template.Application.Common.Interfaces;
using NKZSoft.Template.Application.Common.Paging;
using Repositories.PostgreSql;

internal sealed class GetPageTodoDbQueryHandler :
    PagingDbQueryHandlerDb<GetPageTodoItemsQuery, Result<CollectionViewModel<ToDoItemDto>>, ToDoItemDto>
{
    private readonly IToDoItemRepository _repository;

    public GetPageTodoDbQueryHandler(
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
