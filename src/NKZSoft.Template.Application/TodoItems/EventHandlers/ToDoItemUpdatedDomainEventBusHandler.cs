namespace NKZSoft.Template.Application.TodoItems.EventHandlers;

public sealed class ToDoItemUpdatedDomainEventBusHandler : INotificationHandler<ToDoItemUpdatedDomainEvent>
{
    private readonly ILogger<ToDoItemUpdatedDomainEventBusHandler> _logger;

    public ToDoItemUpdatedDomainEventBusHandler(ILogger<ToDoItemUpdatedDomainEventBusHandler> logger) => _logger = logger;

    public async Task Handle(ToDoItemUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        await Task.CompletedTask;
    }
}
