namespace NKZSoft.Template.Application.TodoItems.Models;

using Common.Filters;

public sealed partial record ToDoItemFilter
{
    public sealed class ToDoItemFilterBuilder
    {
        private readonly ToDoItemFilter _filter;

        public ToDoItemFilterBuilder() => _filter = new ToDoItemFilter();

        public ToDoItemFilterBuilder Id(Guid id)
        {
            _filter.Id = new FilterFieldDefinition<Guid> { Value = id };
            return this;
        }

        public ToDoItemFilterBuilder Title(string title)
        {
            _filter.Title = new FilterFieldDefinition<string> { Value = title };
            return this;
        }

        public ToDoItemFilter Build() => _filter;
    }

    public static ToDoItemFilterBuilder CreateBuilder() => new();
}
