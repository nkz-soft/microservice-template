namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetItemFromRedis.v1;

using Models;
using NKZSoft.Template.Application.Common.Exceptions;
using NKZSoft.Template.Application.Common.Handlers;
using NKZSoft.Template.Application.Common.Interfaces;
using Repositories.Redis;

internal sealed class GetTodoItemQueryFromRedisHandler : HandlerBase<GetTodoItemQueryFromRedis, Result<ToDoItemDto>>
{
    private readonly IToDoItemRedisRepository _repository;

    public GetTodoItemQueryFromRedisHandler(ICurrentUserService currentUserService,
        IMapper mapper,
        IToDoItemRedisRepository repository)
        : base(currentUserService, mapper) =>
        _repository = repository.ThrowIfNull();

    public override async Task<Result<ToDoItemDto>> Handle(GetTodoItemQueryFromRedis request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsyncById(request.Id, cancellationToken)
            .ConfigureAwait(false);
        entity.ThrowIfNull(new NotFoundException());

        var dtoItem = await entity
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<ToDoItemDto>()
            .ConfigureAwait(false);

        return Result.Ok(dtoItem);
    }
}
