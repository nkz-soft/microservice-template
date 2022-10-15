namespace NKZSoft.Template.Application.TodoItems.Queries.GetQueryable;

public class GetQueryableQueryHandler : HandlerQueryBase<GetQueryableQuery, IQueryable<ToDoItem>>
{
    public GetQueryableQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public override async Task<IQueryable<ToDoItem>> Handle(GetQueryableQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(ContextDb.AppDbContext.Set<ToDoItem>()).ConfigureAwait(false);
}
