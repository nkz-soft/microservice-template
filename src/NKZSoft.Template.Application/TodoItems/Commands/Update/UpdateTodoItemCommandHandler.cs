namespace NKZSoft.Template.Application.TodoItems.Commands.Update;

using Common.Exceptions;
using Common.Interfaces;

public sealed class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ToDoItem>()
            .FindAsync(new object[] { request.Id }, cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ToDoItem), request.Id);
        }

        entity.Update(request.Title, request.Description);

        await _context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
