namespace NKZSoft.Template.MessageBrokers.RabbitMq.Consumers;

public sealed class ToDoItemUpdatedIntegrationEventConsumer : IConsumer<ToDoItemUpdatedIntegrationEvent>
{
    private readonly ILogger<ToDoItemUpdatedIntegrationEventConsumer> _logger;

    public ToDoItemUpdatedIntegrationEventConsumer(ILogger<ToDoItemUpdatedIntegrationEventConsumer> logger) =>
        _logger = logger;

    public Task Consume(ConsumeContext<ToDoItemUpdatedIntegrationEvent> context)
    {
        _logger.LogInformation($"Consume Integration Event: {context.Message}");
        return Task.CompletedTask;
    }
}
