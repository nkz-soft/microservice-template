namespace NKZSoft.Template.Application.TodoItems.Commands.Update;

using Common.Exceptions;
using Common.Repositories.PostgreSql;

public sealed class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand>
{
    private readonly IToDoItemRepository _repository;

    public UpdateToDoItemCommandHandler(IToDoItemRepository repository) => _repository = repository.ThrowIfNull();

    public async Task Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository
            .SingleOrDefaultAsync(new ToDoItemByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ToDoItem), request.Id);
        }

        entity.Update(request.Title, request.Description);

        await _repository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
