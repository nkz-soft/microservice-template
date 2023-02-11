namespace NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

using Common;

public sealed class ToDoList : BaseAuditableEntity<Guid, string>
{
    private readonly List<ToDoItem> _toDoItems = new();

    //for Hot Chocolate only
    //see https://stackoverflow.com/questions/56995658/in-graphql-hotchocolate-can-you-have-optional-parameters-or-use-a-constructor
    public ToDoList(): base(Guid.NewGuid())
    {}

    public ToDoList(string title) : base(Guid.NewGuid())
    {
        Title = title;
    }

    public string Title { get; } = default!;

    public IReadOnlyCollection<ToDoItem> ToDoItems => _toDoItems;

    public void AddToDoItem(string title)
    {
        var orderItem = new ToDoItem(title);
        _toDoItems.Add(orderItem);
    }
}
