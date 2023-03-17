namespace NKZSoft.Template.Application.TodoItems.Commands.CreateInRedis;

using Create;

public sealed class CreateTodoItemRedisCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateTodoItemRedisCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(250)
            .NotEmpty();
    }
}
