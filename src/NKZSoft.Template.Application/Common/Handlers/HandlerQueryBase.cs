namespace NKZSoft.Template.Application.Common.Handlers;

using Interfaces;

public abstract class HandlerQueryBase<TQ, TM> : HandlerBase<TQ, TM>
    where TQ : IRequest<TM>
{
    protected HandlerQueryBase(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }
}
