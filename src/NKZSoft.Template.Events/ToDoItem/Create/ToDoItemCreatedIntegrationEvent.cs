namespace NKZSoft.Template.Events.ToDoItem.Create;

using Interfaces;

public sealed record ToDoItemCreatedIntegrationEvent(Guid Id, string Title, string? Note) : IIntegrationEvent;
