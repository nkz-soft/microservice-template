namespace NKZSoft.Template.Application.Common.Handlers;

using Interfaces;

public abstract class HandlerDbQueryBase<TQ, TM> : HandlerDbBase<TQ, TM>
    where TQ : IRequest<TM>
{
    protected HandlerDbQueryBase(
        IApplicationDbContext contextDb,
        IMapper mapper,
        ICurrentUserService currentUserService)
        : base(currentUserService, mapper, contextDb)
    {
    }
}
