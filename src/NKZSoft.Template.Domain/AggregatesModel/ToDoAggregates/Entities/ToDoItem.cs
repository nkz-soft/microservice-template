namespace NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

using Common;

public sealed class ToDoItem : BaseAuditableEntity, IAggregateRoot
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
        Title = title;
        Note = note;
    }

    public string Title { get; set; }

    public string? Note { get; set; }

    public void Update(string title, string note)
    {
        Title = title;
        Note = note;
    }
}
