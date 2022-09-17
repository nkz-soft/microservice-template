namespace NKZSoft.Template.Application.TodoItems.Queries.Get;

public sealed class GetTodoItemQueryValidator : AbstractValidator<GetTodoItemQuery>
{
    public GetTodoItemQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}