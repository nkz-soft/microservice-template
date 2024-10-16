namespace NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

using Common;
using Events;

public sealed class ToDoItem : BaseAuditableEntity<Guid, string>, IAggregateRoot
{
    /// <summary>
    /// for Hot Chocolate only
    /// see https://stackoverflow.com/questions/56995658/in-graphql-hotchocolate-can-you-have-optional-parameters-or-use-a-constructor
    /// </summary>
    public ToDoItem() : base(Guid.NewGuid())
    { }

    public ToDoItem(string title) : this(title, note: null) => Title = title;

    public ToDoItem(string title, string? note) : base(Guid.NewGuid())
    {
        Title = title;
        Note = note;

        AddCreateDomainEvent();
    }

    public string Title { get; set; } = default!;

    public string? Note { get; set; }

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
