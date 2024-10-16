namespace NKZSoft.Template.Application.TodoItems.Commands.CreateInRedis;

using Common.Repositories.Redis;

public sealed class CreateToDoItemRedisCommandHandler(IToDoItemRedisRepository repository)
    : IRequestHandler<CreateToDoItemRedisCommand, Result<Guid>>
{
    private readonly IToDoItemRedisRepository _repository = repository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateToDoItemRedisCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoItem(request.Title);

        await _repository
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
