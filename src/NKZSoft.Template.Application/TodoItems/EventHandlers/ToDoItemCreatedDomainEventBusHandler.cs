namespace NKZSoft.Template.Application.TodoItems.EventHandlers;

using NKZSoft.Template.Common.Extensions;

public sealed class ToDoItemCreatedDomainEventBusHandler : INotificationHandler<ToDoItemCreatedDomainEvent>
{
    private readonly ILogger<ToDoItemCreatedDomainEventBusHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public ToDoItemCreatedDomainEventBusHandler(ILogger<ToDoItemCreatedDomainEventBusHandler> logger,
        IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _mapper = mapper.ThrowIfNull();
        _logger = logger.ThrowIfNull();
        _publishEndpoint = publishEndpoint.ThrowIfNull();
    }

    public async Task Handle(ToDoItemCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.RaiseIntegrationEvent(notification.GetType().Name);

        var createEvent = await notification
            .BuildAdapter(_mapper.Config)
            .AdaptToTypeAsync<ToDoItemCreatedIntegrationEvent>()
            .ConfigureAwait(false);

        await _publishEndpoint.Publish(createEvent, cancellationToken)
            .ConfigureAwait(false);
    }
}
