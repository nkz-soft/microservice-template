namespace NKZSoft.Template.MessageBrokers.RabbitMq.Consumers;

using Common.Extensions;
using Events.ToDoItem.Update;

public sealed class ToDoItemUpdatedIntegrationEventConsumer(ILogger<ToDoItemUpdatedIntegrationEventConsumer> logger)
    : IConsumer<ToDoItemUpdatedIntegrationEvent>
{
    public Task Consume(ConsumeContext<ToDoItemUpdatedIntegrationEvent> context)
    {
        logger.ConsumeIntegrationEvent($"Consume Integration Event: {context.Message}");
        return Task.CompletedTask;
    }
}
