namespace NKZSoft.Template.Application.TodoItems.Specifications;

using System.Collections.Frozen;
using Common.Exceptions;
using Common.Filters;
using Common.Paging;
using Models;

internal sealed class ToDoItemSpecification : Specification<ToDoItem>
{
    private static readonly FrozenDictionary<string, Expression<Func<ToDoItem, object>>> _sortExpressions =
        new Dictionary<string, Expression<Func<ToDoItem, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(ToDoItemFilter.Id), item => item.Id },
            { nameof(ToDoItemFilter.Title), item => item.Title },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private ToDoItemSpecification()
    {
    }

    public static Specification<ToDoItem> Create()
    {
        var specification =  new ToDoItemSpecification();

        var specificationBuilder = specification.Query;

        specificationBuilder.AsNoTracking();
        return specification;
    }

    public static Specification<ToDoItem> Create(IPageContext<ToDoItemFilter> pageContext)
    {
        var specification = new ToDoItemSpecification();

        var specificationBuilder = specification.Query;

        Filter(specificationBuilder, pageContext.Filter);
        Sort(specificationBuilder, pageContext.ListSort);

        if (pageContext.PageIndex != 0)
        {
            specificationBuilder.Skip(pageContext.PageSize * (pageContext.PageIndex - 1));
        }

        if (pageContext.PageSize != 0)
        {
            specificationBuilder.Take(pageContext.PageSize);
        }

        specificationBuilder.AsNoTracking();
        return specification;
    }

    private static void Filter(ISpecificationBuilder<ToDoItem> specificationBuilder, ToDoItemFilter filter)
    {
        if (filter.Title.HasValue())
        {
            specificationBuilder.Where(item => item.Title == filter.Title.Value);
        }

        if (filter.Id.HasValue())
        {
            specificationBuilder.Where(item => item.Id == filter.Id.Value);
        }
    }

    private static ISpecificationBuilder<ToDoItem> Sort(ISpecificationBuilder<ToDoItem> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        return sortDescriptors.Length != 0 ? sortDescriptors.Aggregate(specificationBuilder, Sort) : Sort(specificationBuilder, new SortDescriptor(nameof(ToDoItemFilter.Id)));
    }

    private static ISpecificationBuilder<ToDoItem> Sort(ISpecificationBuilder<ToDoItem> specificationBuilder,
        SortDescriptor sort)
    {
        return _sortExpressions.TryGetValue(sort.Field, out var se)
            ? (ISpecificationBuilder<ToDoItem>)(sort.Direction == EnumSortDirection.Desc
                ? specificationBuilder.OrderByDescending(se!)
                : specificationBuilder.OrderBy(se!))
            : throw new BadRequestException($"Invalid field name {sort.Field}.");
    }
}
