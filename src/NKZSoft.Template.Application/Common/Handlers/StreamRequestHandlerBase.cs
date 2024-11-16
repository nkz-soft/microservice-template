namespace NKZSoft.Template.Application.Common.Handlers;

using Interfaces;
using NKZSoft.Template.Common.Extensions;

public abstract class StreamRequestHandlerBase<TRequest, TResponse> : IStreamRequestHandler<TRequest, TResponse>
    where TRequest : IStreamRequest<TResponse>
{
    protected ICurrentUserService CurrentUserService { get; }

    protected IMapper Mapper { get; }

    protected StreamRequestHandlerBase(ICurrentUserService currentUserService, IMapper autoMapper)
    {
        CurrentUserService = currentUserService.ThrowIfNull();
        Mapper = autoMapper.ThrowIfNull();
    }

    public abstract IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
