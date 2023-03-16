namespace NKZSoft.Template.Application.TodoItems.Queries.GetQueryable;

using Common.Handlers;
using Common.Interfaces;

public class GetQueryableDbQueryHandlerDb : HandlerDbQueryBase<GetQueryableQuery, IQueryable<ToDoItem>>
{
    public GetQueryableDbQueryHandlerDb(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public override async Task<IQueryable<ToDoItem>> Handle(GetQueryableQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(ContextDb.AppDbContext.Set<ToDoItem>()).ConfigureAwait(false);
}
