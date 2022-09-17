using MediatR;

namespace NKZSoft.Template.Domain.Notifications;

using AggregatesModel.ToDoAggregates.Entities;

public class ToDoItemCreatedNotification : INotification
{
    public ToDoItemCreatedNotification(ToDoItem item)
    {
        Item = item;
    }

    public ToDoItem Item { get; }
}
