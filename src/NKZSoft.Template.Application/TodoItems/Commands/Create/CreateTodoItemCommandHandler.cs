namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

using Common.Interfaces;

public sealed class CreateTodoItemCommandHandler : IRequestHandler<CreateToВoItemCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Result<Guid>> Handle(CreateToВoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoItem(request.Title);

        await _context.Set<ToDoItem>()
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok(entity.Id);
    }
}
