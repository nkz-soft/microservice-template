using MediatR;

namespace NKZSoft.Template.Domain.Notifications;

using AggregatesModel.ToDoAggregates.Entities;

public class TodoItemCompletedEvent : INotification
{
    public TodoItemCompletedEvent(ToDoItem item)
    {
        Item = item;
    }

    public ToDoItem Item { get; }
}
