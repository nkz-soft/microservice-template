namespace NKZSoft.Template.Application.Common.Handlers;

using Interfaces;
using NKZSoft.Template.Common.Extensions;

public abstract class HandlerDbBase<TQ, TM> : HandlerBase<TQ, TM>
    where TQ : IRequest<TM>
{
    protected IApplicationDbContext ContextDb { get; }

    protected HandlerDbBase(ICurrentUserService currentUserService,
        IMapper mapper,
        IApplicationDbContext contextDb)
        : base(currentUserService, mapper) =>
        ContextDb = contextDb.ThrowIfNull();

    public abstract override Task<TM> Handle(TQ request, CancellationToken cancellationToken);
}
