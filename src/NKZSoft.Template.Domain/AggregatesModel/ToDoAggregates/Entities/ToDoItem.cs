namespace NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

using Common;
using Events;

public sealed class ToDoItem : BaseAuditableEntity<Guid, string>, IAggregateRoot
{
    public ToDoItem()
    {
    }

    public ToDoItem(string title) : this(title, null)
    {
        Title = title;
    }

    public ToDoItem(string title, string? note)
    {
        Id = Guid.NewGuid();
        Title = title;
        Note = note;

        AddCreateDomainEvent();
    }

    public string Title { get; private set; }  = default!;

    public string? Note { get; private set; }

    public void Update(string title, string note)
    {
        Title = title;
        Note = note;
    }

    private void AddCreateDomainEvent()
    {
        var createEvent = new ToDoItemCreatedDomainEvent(Id, Title, Note);
        AddDomainEvent(createEvent);
    }
}
