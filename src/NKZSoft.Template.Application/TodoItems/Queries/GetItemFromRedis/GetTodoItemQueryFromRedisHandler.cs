namespace NKZSoft.Template.Application.TodoItems.Queries.GetItemFromRedis;

using Application.Models;
using Common.Exceptions;
using Common.Handlers;
using Common.Interfaces;
using Common.Repositories;
using Common.Repositories.Redis;
using GetItem;

public class GetTodoItemQueryFromRedisHandler : HandlerBase<GetTodoItemQueryFromRedis, Result<ToDoItemDto>>
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
