using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.TodoItems.Commands.Delete;

using Common.Exceptions;
using Common.Interfaces;

public sealed class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ToDoItem>()
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ToDoItem), request.Id);
        }

        _context.Set<ToDoItem>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
