namespace NKZSoft.Template.Application.TodoItems.Commands.Update;

public sealed class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator() =>
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
}
