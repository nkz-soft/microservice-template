namespace NKZSoft.Template.Application.TodoItems.Features.Queries.GetItemFromRedis.v1;

using GetItem.v1;

internal sealed class GetTodoItemQueryFromRedisValidator : AbstractValidator<GetTodoItemQuery>
{
    public GetTodoItemQueryFromRedisValidator() =>
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
}
