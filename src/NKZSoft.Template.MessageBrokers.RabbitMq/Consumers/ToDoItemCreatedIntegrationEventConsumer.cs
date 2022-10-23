namespace NKZSoft.Template.MessageBrokers.RabbitMq.Consumers;

public sealed class ToDoItemCreatedIntegrationEventConsumer : IConsumer<ToDoItemCreatedIntegrationEvent>
{
    private readonly ILogger<ToDoItemCreatedIntegrationEventConsumer> _logger;

    public ToDoItemCreatedIntegrationEventConsumer(ILogger<ToDoItemCreatedIntegrationEventConsumer> logger) =>
        _logger = logger;

    public Task Consume(ConsumeContext<ToDoItemCreatedIntegrationEvent> context)
    {
        _logger.LogInformation($"Consume Integration Event: {context.Message}");
        return Task.CompletedTask;
    }
}
