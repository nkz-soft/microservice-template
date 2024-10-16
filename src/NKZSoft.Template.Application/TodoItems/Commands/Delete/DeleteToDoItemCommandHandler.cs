namespace NKZSoft.Template.Application.TodoItems.Commands.Delete;

using Common.Exceptions;
using Common.Repositories.PostgreSql;

public sealed class DeleteToDoItemCommandHandler(IToDoItemRepository repository)
    : IRequestHandler<DeleteToDoItemCommand>
{
    public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository
            .SingleOrDefaultAsync(new ToDoItemByIdSpecification(request.Id), cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ToDoItem), request.Id);
        }

        await repository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
        await repository.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
