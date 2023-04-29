namespace NKZSoft.Template.Application.Common.Behaviours;

using Interfaces;

public sealed class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviour<TRequest>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger, ICurrentUserService currentUserService)
        => _logger = logger.ThrowIfNull();

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LoggingRequest(request.ToString());
        await Task.CompletedTask;
    }
}
