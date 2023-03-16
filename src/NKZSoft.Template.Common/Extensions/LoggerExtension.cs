namespace NKZSoft.Template.Common.Extensions;

internal static class LoggerExtension
{
    private static readonly Action<ILogger, string, Exception?> _consumeIntegrationEvent = LoggerMessage.Define<string>(
        LogLevel.Information,
        EventIds.ConsumeIntegrationEvent,
        "Integration event has been consumed: `{Message}`.");

    private static readonly Action<ILogger, string, Exception?> _raiseIntegrationEvent = LoggerMessage.Define<string>(
        LogLevel.Information,
        EventIds.RaiseIntegrationEvent,
        "Domain event has been raised: `{Message}`.");

    private static readonly Action<ILogger, long, string, Exception?> _longRunningRequest = LoggerMessage.Define<long, string>(
        LogLevel.Warning,
        EventIds.LongRunningRequest,
        "Long running request: `{ElapsedMilliseconds}` milliseconds `{Request}.`");

    private static readonly Action<ILogger, string?, Exception?> _unhandledExceptionRequest = LoggerMessage.Define<string?>(
        LogLevel.Error,
        EventIds.UnhandledExceptionRequest,
        "Unhandled exception has occured for request: `{Request}.`");

    private static readonly Action<ILogger, string?, Exception?> _loggingRequest = LoggerMessage.Define<string?>(
        LogLevel.Error,
        EventIds.LoggingRequest,
        "Request has executed: `{Request}.`");

    public static void ConsumeIntegrationEvent(this ILogger logger, string message)
        => _consumeIntegrationEvent(logger, message, null);

    public static void RaiseIntegrationEvent(this ILogger logger, string message)
        => _raiseIntegrationEvent(logger, message, null);

    public static void LongRunningRequest(this ILogger logger, long elapsedMilliseconds, string request)
        => _longRunningRequest(logger, elapsedMilliseconds, request, null);

    public static void UnhandledExceptionRequest(this ILogger logger, string? request, Exception? ex)
        => _unhandledExceptionRequest(logger, request, ex);

    public static void LoggingRequest(this ILogger logger, string? request)
        => _loggingRequest(logger, request, null);
}
