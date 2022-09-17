namespace NKZSoft.Template.Application.Common.Handlers;

using Interfaces;
using Paging;

public abstract class PagingQueryHandler<TQ, TCM, TM> : HandlerQueryBase<TQ, TCM>
    where TQ : IRequest<TCM>
    where TCM : Result<CollectionViewModel<TM>>, new()
{
    protected PagingQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper,
        ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }

    public abstract override Task<TCM> Handle(TQ request, CancellationToken cancellationToken);
}
