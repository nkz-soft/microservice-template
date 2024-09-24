namespace NKZSoft.Template.Application.TodoItems.Queries.GetQueryable;

using Common.Handlers;
using Common.Interfaces;

public class GetAllToDoItemsQueryHandler : HandlerDbQueryBase<GetAllToDoItemsQuery, IQueryable<ToDoItem>>
{
    public GetAllToDoItemsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public override async Task<IQueryable<ToDoItem>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(ContextDb.AppDbContext.Set<ToDoItem>()).ConfigureAwait(false);
}
