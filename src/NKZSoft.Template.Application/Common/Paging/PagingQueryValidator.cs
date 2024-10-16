namespace NKZSoft.Template.Application.Common.Paging;

public class PagingQueryValidator<T, TCM, TF> : AbstractValidator<T>
    where T : PagingQuery<TCM, TF> where TF : class, new()
{
    protected PagingQueryValidator()
    {
        RuleFor(pageContext => pageContext.PageContext).NotNull()
            .NotEmpty().WithMessage("PageContext is required.");

        RuleFor(pageContext => pageContext.PageContext.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("PageIndex at least greater than or equal to 1.");

        RuleFor(pageContext => pageContext.PageContext.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
