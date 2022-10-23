namespace NKZSoft.Template.Events.Event.ToDoItem.Create;

using Interfaces;

public sealed record ToDoItemCreatedIntegrationEvent(Guid Id, string Title, string? Note) : IIntegrationEvent;
