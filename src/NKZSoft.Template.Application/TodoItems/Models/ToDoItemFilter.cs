namespace NKZSoft.Template.Application.TodoItems.Models;

using Common.Filters;

public partial record ToDoItemFilter
{
    public FilterFieldDefinition<int>? Id { get; set; }

    public FilterFieldDefinition<string>? Title { get; set; }
}
