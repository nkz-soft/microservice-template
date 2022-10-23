namespace NKZSoft.Template.Application.TodoItems.Specifications;

using Common.Exceptions;
using Common.Filters;
using Common.Paging;
using Models;

public sealed class ToDoItemSpecification : Specification<ToDoItem>
{
    private static readonly IDictionary<string, Expression<Func<ToDoItem, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<ToDoItem, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(ToDoItemFilter.Id), c => c.Id },
            { nameof(ToDoItemFilter.Title), c => c.Title },
        };

    private ToDoItemSpecification()
    {
    }

    public static Specification<ToDoItem> Create(IPageContext<ToDoItemFilter> pageContext)
    {
        var specification =  new ToDoItemSpecification();

        var specificationBuilder = specification.Query;

        specification.Filter(specificationBuilder, pageContext.Filter);
        specification.Sort(specificationBuilder, pageContext.ListSort);

        if (pageContext.PageIndex != 0)
        {
            specificationBuilder.Skip(pageContext.PageSize * (pageContext.PageIndex - 1));
        }

        if (pageContext.PageSize != 0)
        {
            specificationBuilder.Take(pageContext.PageSize);
        }

        return specification;
    }

    private void Filter(ISpecificationBuilder<ToDoItem> specificationBuilder, ToDoItemFilter filter)
    {
        if (filter.Title.HasValue())
        {
            specificationBuilder.Where(x => x.Title == filter.Title.Value);
        }

        if (filter.Id.HasValue())
        {
            specificationBuilder.Where(x => x.Id == filter.Id.Value);
        }
    }

    private ISpecificationBuilder<ToDoItem> Sort(ISpecificationBuilder<ToDoItem> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Any())
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(ToDoItemFilter.Id)));
    }

    private ISpecificationBuilder<ToDoItem> Sort(ISpecificationBuilder<ToDoItem> specificationBuilder,
        SortDescriptor sort)
    {
        if (SortExpressions.TryGetValue(sort.Field, out var se))
        {
            return sort.Direction == EnumSortDirection.Desc
                ? specificationBuilder.OrderByDescending(se!)
                : specificationBuilder.OrderBy(se!);
        }
        throw new BadRequestException($"Invalid field name {sort.Field}.");
    }
}
