namespace NKZSoft.Template.Application.Common.Behaviours;

using NKZSoft.Template.Common.Extensions;

public sealed class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest>> logger) => _logger = logger.ThrowIfNull();

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LoggingRequest(request.ToString());
        await Task.CompletedTask.ConfigureAwait(false);
    }
}
