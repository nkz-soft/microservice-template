namespace NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

using Common;

public sealed class ToDoList : BaseAuditableEntity
{
    private readonly List<ToDoItem> _toDoItems = new();

    public ToDoList(string title)
    {
        Title = title;
    }

    public string Title { get; }
    
    public IReadOnlyCollection<ToDoItem> ToDoItems => _toDoItems;
    
    public void AddToDoItem(string title)
    {
        var orderItem = new ToDoItem(title);
        _toDoItems.Add(orderItem);
    }
}
