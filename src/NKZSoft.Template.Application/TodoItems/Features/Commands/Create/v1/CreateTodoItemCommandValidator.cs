namespace NKZSoft.Template.Application.TodoItems.Features.Commands.Create.v1;

public sealed class CreateTodoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(250)
            .NotEmpty();
    }
}
