namespace NKZSoft.Template.Presentation.GraphQL.Services;

public sealed class Query
{
    /// <summary>
    /// Gets a Queryable collection of <see cref="ToDoItem"/>.
    /// </summary>
    /// <param name="mediator">The <see cref="IMediator"/> to add the route to.</param>
    /// <param name="token">The <see cref="IMediator"/> to add the route to.</param>
    /// <returns>Returns collection of <see cref="ToDoItem"/>.</returns>
    [UseProjection]
    [UseFiltering]
    [UseSorting]
#pragma warning disable CA1822
    public async Task<IQueryable<ToDoItem>> GetTodoItems([Service] IMediator mediator, CancellationToken token) =>
        await mediator.Send(new GetAllToDoItemsQuery(), token).ConfigureAwait(false);
#pragma warning restore CA1822
}
