namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

public sealed class CreateTodoItemCommandValidator : AbstractValidator<CreateToВoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(250)
            .NotEmpty();
    }
}
