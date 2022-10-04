namespace NKZSoft.Template.Application.TodoItems.Models;

using Common.Filters;

public partial record ToDoItemFilter
{
    public FilterFieldDefinition<Guid>? Id { get; set; }

    public FilterFieldDefinition<string>? Title { get; set; }
}
