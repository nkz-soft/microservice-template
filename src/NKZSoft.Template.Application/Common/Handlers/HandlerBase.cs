namespace NKZSoft.Template.Application.Common.Handlers;

using Interfaces;

public abstract class HandlerBase<TQ, TM> : IRequestHandler<TQ, TM>
    where TQ : IRequest<TM>
{
    protected IApplicationDbContext ContextDb { get; }

    protected ICurrentUserService CurrentUserService { get; }

    protected IMapper Mapper { get; }

    protected HandlerBase(IApplicationDbContext applicationDbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        ContextDb = applicationDbContext.ThrowIfNull();
        CurrentUserService = currentUserService.ThrowIfNull();
        Mapper = mapper.ThrowIfNull();
    }

    public abstract Task<TM> Handle(TQ request, CancellationToken cancellationToken);
}
