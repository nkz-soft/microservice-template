namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

public sealed class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator() =>
        RuleFor(v => v.Title)
            .MaximumLength(250)
            .NotEmpty();
}
