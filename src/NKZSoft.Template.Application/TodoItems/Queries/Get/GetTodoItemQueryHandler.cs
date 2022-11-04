namespace NKZSoft.Template.Application.TodoItems.Queries.Get;

using Application.Models;
using Common.Exceptions;
using Common.Handlers;
using Common.Interfaces;
using Common.Repositories;

public sealed class GetTodoItemQueryHandler : HandlerQueryBase<GetTodoItemQuery, Result<ToDoItemDto>>
{
    private readonly IToDoItemRepository _repository;

    public GetTodoItemQueryHandler(
        IToDoItemRepository repository,
        IApplicationDbContext applicationDbContext,
        ICurrentUserService currentUserService,
        IMapper mapper)
        : base(applicationDbContext, mapper, currentUserService) =>
        _repository = repository.ThrowIfNull();

    public override async Task<Result<ToDoItemDto>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository
            .SingleOrDefaultAsync(new ToDoItemByIdSpecification(request.Id, true), cancellationToken)
            .ConfigureAwait(false);

        entity.ThrowIfNull(new NotFoundException());

        var dtoItem = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<ToDoItemDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoItem);
    }
}
