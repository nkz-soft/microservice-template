namespace NKZSoft.Template.MessageBrokers.RabbitMq.Consumers;

using Common.Extensions;
using Events.ToDoItem.Update;
using Extensions;

public sealed class ToDoItemUpdatedIntegrationEventConsumer : IConsumer<ToDoItemUpdatedIntegrationEvent>
{
    private readonly ILogger<ToDoItemUpdatedIntegrationEventConsumer> _logger;

    public ToDoItemUpdatedIntegrationEventConsumer(ILogger<ToDoItemUpdatedIntegrationEventConsumer> logger) =>
        _logger = logger;

    public Task Consume(ConsumeContext<ToDoItemUpdatedIntegrationEvent> context)
    {
        _logger.ConsumeIntegrationEvent($"Consume Integration Event: {context.Message}");
        return Task.CompletedTask;
    }
}
