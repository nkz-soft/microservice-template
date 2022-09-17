using MediatR;

namespace NKZSoft.Template.Domain.Notifications;

using AggregatesModel.ToDoAggregates.Entities;

public class TodoItemDeletedEvent : INotification
{
    public TodoItemDeletedEvent(ToDoItem item)
    {
        Item = item;
    }

    public ToDoItem Item { get; }
}
