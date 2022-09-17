using MediatR.Pipeline;

namespace NKZSoft.Template.Application.Common.Behaviours;

using Interfaces;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var user = _currentUserService.CurrentUser;

        _logger.LogInformation("LoggingBehaviour Request: {Name} {@UserId} {@Request}",
            requestName, user.Id, request);

        await Task.CompletedTask;
    }
}
