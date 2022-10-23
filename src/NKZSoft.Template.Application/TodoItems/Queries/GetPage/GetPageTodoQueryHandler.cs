namespace NKZSoft.Template.Application.TodoItems.Queries.GetPage;

using NKZSoft.Template.Application.Models;
using Specifications;
using Common.Handlers;
using Common.Interfaces;
using Common.Paging;

public sealed class GetPageTodoQueryHandler :
    PagingQueryHandler<GetPageTodoItemsQuery, Result<CollectionViewModel<ToDoItemDto>>, ToDoItemDto>
{
    public GetPageTodoQueryHandler(IApplicationDbContext context,
        ICurrentUserService currentUserService, IMapper mapper) : base(context, mapper, currentUserService)
    {
    }

    public override async Task<Result<CollectionViewModel<ToDoItemDto>>> Handle(GetPageTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = ToDoItemSpecification.Create(request.PageContext);

        var entities = await ContextDb.Set<ToDoItem>()
            .AsNoTracking()
            .WithSpecification(specification)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var dtoItems = await entities
            .BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<List<ToDoItemDto>>()
            .ConfigureAwait(false);

        return Result.Ok(new CollectionViewModel<ToDoItemDto>(
            dtoItems, dtoItems.Count));
    }
}
