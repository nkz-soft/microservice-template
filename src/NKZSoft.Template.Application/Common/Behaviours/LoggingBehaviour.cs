namespace NKZSoft.Template.Application.Common.Behaviours;

using Interfaces;
using NKZSoft.Template.Common.Extensions;

public sealed class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService) => _logger = logger.ThrowIfNull();

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LoggingRequest(request.ToString());
        await Task.CompletedTask;
    }
}
