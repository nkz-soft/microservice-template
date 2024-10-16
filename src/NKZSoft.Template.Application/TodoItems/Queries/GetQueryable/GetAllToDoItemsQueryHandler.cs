namespace NKZSoft.Template.Application.TodoItems.Queries.GetQueryable;

using Common.Handlers;
using Common.Interfaces;

public class GetAllToDoItemsQueryHandler(
    IApplicationDbContext applicationDbContext,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : HandlerDbQueryBase<GetAllToDoItemsQuery, IQueryable<ToDoItem>>(applicationDbContext, mapper, currentUserService)
{
    public override async Task<IQueryable<ToDoItem>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(ContextDb.AppDbContext.Set<ToDoItem>()).ConfigureAwait(false);
}
