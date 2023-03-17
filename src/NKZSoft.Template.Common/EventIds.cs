namespace NKZSoft.Template.Common;

internal static class EventIds
{
    public static readonly EventId ConsumeIntegrationEvent = new EventId(1, nameof(ConsumeIntegrationEvent));

    public static readonly EventId RaiseIntegrationEvent = new EventId(2, nameof(RaiseIntegrationEvent));

    public static readonly EventId LongRunningRequest = new EventId(3, nameof(LongRunningRequest));

    public static readonly EventId UnhandledExceptionRequest = new EventId(4, nameof(UnhandledExceptionRequest));

    public static readonly EventId LoggingRequest = new EventId(4, nameof(LoggingRequest));
}
