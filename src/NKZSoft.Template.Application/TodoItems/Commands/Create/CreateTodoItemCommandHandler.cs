using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

using Common.Interfaces;

public sealed class CreateTodoItemCommandHandler : IRequestHandler<CreateToВoItemCommand, IResult<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<IResult<int>> Handle(CreateToВoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoItem(request.Title);

        await _context.Set<ToDoItem>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(entity.Id);
    }
}
