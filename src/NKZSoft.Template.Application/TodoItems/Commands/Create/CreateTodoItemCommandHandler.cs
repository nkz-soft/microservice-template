namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

using Common.Repositories;

public sealed class CreateTodoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Result<Guid>>
{
    private readonly IToDoItemRepository _repository;

    public CreateTodoItemCommandHandler(IToDoItemRepository repository) => _repository = repository.ThrowIfNull();

    public async Task<Result<Guid>> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoItem(request.Title);

        await _repository
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        await _repository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
