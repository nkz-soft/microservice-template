namespace NKZSoft.Template.Common.Extensions;

public static partial class LoggerExtension
{
    [LoggerMessage(1, LogLevel.Information, "Integration event has been consumed: {Message}.")]
    internal static partial void ConsumeIntegrationEvent(this ILogger logger, string message);

    [LoggerMessage(2, LogLevel.Information, "Domain event has been raised: {Message}.")]
    public static partial void RaiseIntegrationEvent(this ILogger logger, string message);

    [LoggerMessage(3, LogLevel.Warning, "Long running request: {ElapsedMilliseconds} milliseconds {Request}.")]
    public static partial void LongRunningRequest(this ILogger logger, long elapsedMilliseconds, string request);

    [LoggerMessage(4, LogLevel.Error, "Unhandled exception has occured for request: `{Request}.")]
    public static partial void UnhandledExceptionRequest(this ILogger logger, string? request);

    [LoggerMessage(5, LogLevel.Error, "Request has executed: `{Request}.")]
    public static partial void LoggingRequest(this ILogger logger, string? request);

    [LoggerMessage(6, LogLevel.Error, "An error occurred while migrating or initializing the database.")]
    public static partial void MigrationError(this ILogger logger, Exception ex);

    [LoggerMessage(7, LogLevel.Error, "Application: An unhandled exception has occurred.")]
    public static partial void ApplicationUnhandledException(this ILogger logger, Exception ex);
}
