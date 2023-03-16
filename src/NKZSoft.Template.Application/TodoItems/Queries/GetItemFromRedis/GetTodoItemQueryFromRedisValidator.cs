namespace NKZSoft.Template.Application.TodoItems.Queries.GetItemFromRedis;

using GetItem;

public class GetTodoItemQueryFromRedisValidator : AbstractValidator<GetTodoItemQuery>
{
    public GetTodoItemQueryFromRedisValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
