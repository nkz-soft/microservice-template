namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetItem.v1;

using Models;
using NKZSoft.Template.Application.Common.Exceptions;
using NKZSoft.Template.Application.Common.Handlers;
using NKZSoft.Template.Application.Common.Interfaces;
using Repositories.PostgreSql;

internal sealed class GetTodoItemDbQueryHandler : HandlerDbQueryBase<GetTodoItemQuery, Result<ToDoItemDto>>
{
    private readonly IToDoItemRepository _repository;

    public GetTodoItemDbQueryHandler(
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
