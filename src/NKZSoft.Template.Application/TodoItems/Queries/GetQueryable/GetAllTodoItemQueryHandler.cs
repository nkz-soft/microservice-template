namespace NKZSoft.Template.Application.TodoItems.Queries.GetQueryable;

using Common.Handlers;
using Common.Interfaces;

public class GetQueryableQueryHandler : HandlerQueryBase<GetQueryableQuery, IQueryable<ToDoItem>>
{
    public GetQueryableQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public override async Task<IQueryable<ToDoItem>> Handle(GetQueryableQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(ContextDb.AppDbContext.Set<ToDoItem>()).ConfigureAwait(false);
}
