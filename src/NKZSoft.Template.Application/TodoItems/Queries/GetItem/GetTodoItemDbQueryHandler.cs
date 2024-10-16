namespace NKZSoft.Template.Application.TodoItems.Queries.GetItem;

using Common.Exceptions;
using Common.Handlers;
using Common.Interfaces;
using Common.Repositories.PostgreSql;
using NKZSoft.Template.Application.Models;

public sealed class GetTodoItemDbQueryHandler(
    IToDoItemRepository repository,
    IApplicationDbContext applicationDbContext,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : HandlerDbQueryBase<GetTodoItemQuery, Result<ToDoItemDto>>(applicationDbContext, mapper, currentUserService)
{
    private readonly IToDoItemRepository _repository = repository.ThrowIfNull();

    public override async Task<Result<ToDoItemDto>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository
            .SingleOrDefaultAsync(new ToDoItemByIdSpecification(request.Id, noTracking: true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoItem = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<ToDoItemDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoItem);
    }
}
