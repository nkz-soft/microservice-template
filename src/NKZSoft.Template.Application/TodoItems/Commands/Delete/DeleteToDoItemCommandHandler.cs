namespace NKZSoft.Template.Application.TodoItems.Commands.Delete;

using Common.Exceptions;
using Common.Repositories;
using Common.Repositories.PostgreSql;

public sealed class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
{
    private readonly IToDoItemRepository _repository;

    public DeleteToDoItemCommandHandler(IToDoItemRepository repository) => _repository = repository;

    public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository
            .SingleOrDefaultAsync(new ToDoItemByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ToDoItem), request.Id);
        }

        await _repository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await _repository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
