namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetItem.v1;

internal sealed class GetTodoItemQueryValidator : AbstractValidator<GetTodoItemQuery>
{
    public GetTodoItemQueryValidator() =>
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
}
