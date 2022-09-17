namespace NKZSoft.Template.Application.Common.Paging;

public class PagingQueryValidator<T, TCM, TF> : AbstractValidator<T>
    where T : PagingQuery<TCM, TF> where TF : class, new()
{
    protected PagingQueryValidator()
    {
        RuleFor(x => x.PageContext).NotNull()
            .NotEmpty().WithMessage("PageContext is required.");

        RuleFor(x => x.PageContext.PageIndex)
            .GreaterThanOrEqualTo(1).WithMessage("PageIndex at least greater than or equal to 1.");

        RuleFor(x => x.PageContext.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
