namespace NKZSoft.Template.Application.TodoItems.Features.Commands.CreateInRedis.v1;

using Create.v1;

public sealed class CreateTodoItemRedisCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateTodoItemRedisCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(250)
            .NotEmpty();
    }
}
