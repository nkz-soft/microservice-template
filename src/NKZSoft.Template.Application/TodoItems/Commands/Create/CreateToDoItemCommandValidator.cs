namespace NKZSoft.Template.Application.TodoItems.Commands.Create;

public sealed class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator() =>
        RuleFor(command => command.Title)
            .MaximumLength(250)
            .NotEmpty();
}
