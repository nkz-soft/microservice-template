namespace NKZSoft.Template.Application.TodoItems.Commands.CreateInRedis;

using Create;

public sealed class CreateToDoItemRedisCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemRedisCommandValidator() =>
        RuleFor(v => v.Title)
            .MaximumLength(250)
            .NotEmpty();
}
