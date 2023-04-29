namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetQueryable.v1;

using NKZSoft.Template.Application.Common.Handlers;
using NKZSoft.Template.Application.Common.Interfaces;

internal sealed class GetQueryableDbQueryHandlerDb : HandlerDbQueryBase<GetQueryableQuery, IQueryable<ToDoItem>>
{
    public GetQueryableDbQueryHandlerDb(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public override async Task<IQueryable<ToDoItem>> Handle(GetQueryableQuery request, CancellationToken cancellationToken) =>
        await Task.FromResult(ContextDb.AppDbContext.Set<ToDoItem>()).ConfigureAwait(false);
}
