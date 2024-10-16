namespace NKZSoft.Template.MessageBrokers.RabbitMq.Consumers;

using Common.Extensions;
using Events.ToDoItem.Create;

public sealed class ToDoItemCreatedIntegrationEventConsumer(ILogger<ToDoItemCreatedIntegrationEventConsumer> logger)
    : IConsumer<ToDoItemCreatedIntegrationEvent>
{
    public Task Consume(ConsumeContext<ToDoItemCreatedIntegrationEvent> context)
    {
        logger.ConsumeIntegrationEvent(context.Message.ToString());
        return Task.CompletedTask;
    }
}
