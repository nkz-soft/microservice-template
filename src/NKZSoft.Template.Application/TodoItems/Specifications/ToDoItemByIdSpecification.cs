namespace NKZSoft.Template.Application.TodoItems.Specifications;

public sealed class ToDoItemByIdSpecification : Specification<ToDoItem>, ISingleResultSpecification<ToDoItem>
{
    public ToDoItemByIdSpecification(Guid id, bool noTracking = false)
    {
        Query.Where(item => item.Id == id);
        if (noTracking)
        {
            Query.AsNoTracking();
        }
    }
}
