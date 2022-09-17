using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NKZSoft.Template.Application.Models;
using NKZSoft.Template.Application.TodoItems.Models;
using NKZSoft.Template.Application.TodoItems.Specifications;
using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.TodoItems.Queries.GetPage;

using Application.Models;
using Common.Handlers;
using Common.Interfaces;
using Common.Paging;
using Specifications;

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
            .WithSpecification(specification)
            .ToListAsync(cancellationToken);

        return Result.Ok(new CollectionViewModel<ToDoItemDto>(
            entities.Adapt<IEnumerable<ToDoItemDto>>(), entities.Count));
    }
}
