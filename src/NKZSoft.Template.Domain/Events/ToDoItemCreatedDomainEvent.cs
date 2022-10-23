namespace NKZSoft.Template.Domain.Events;

public sealed record ToDoItemCreatedDomainEvent(Guid Id, string Title, string? Note) : INotification;
