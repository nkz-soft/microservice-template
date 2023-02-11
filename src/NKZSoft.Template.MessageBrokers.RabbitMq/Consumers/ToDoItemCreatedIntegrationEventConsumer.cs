namespace NKZSoft.Template.MessageBrokers.RabbitMq.Consumers;

using Common.Extensions;
using Events.ToDoItem.Create;

public sealed class ToDoItemCreatedIntegrationEventConsumer : IConsumer<ToDoItemCreatedIntegrationEvent>
{
    private readonly ILogger<ToDoItemCreatedIntegrationEventConsumer> _logger;

    public ToDoItemCreatedIntegrationEventConsumer(ILogger<ToDoItemCreatedIntegrationEventConsumer> logger) =>
        _logger = logger;

    public Task Consume(ConsumeContext<ToDoItemCreatedIntegrationEvent> context)
    {
        _logger.ConsumeIntegrationEvent(context.Message.ToString());
        return Task.CompletedTask;
    }
}
