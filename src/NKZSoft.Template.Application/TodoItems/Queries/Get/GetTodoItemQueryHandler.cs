using NKZSoft.Template.Application.Models;
using NKZSoft.Template.Domain.AggregatesModel.ToDoAggregates.Entities;

namespace NKZSoft.Template.Application.TodoItems.Queries.Get;

using Application.Models;
using Common.Exceptions;
using Common.Handlers;
using Common.Interfaces;

public sealed class GetTodoItemQueryHandler : HandlerQueryBase<GetTodoItemQuery, Result<ToDoItemDto>>
{
    public GetTodoItemQueryHandler(IApplicationDbContext applicationDbContext, 
        ICurrentUserService currentUserService,
        IMapper mapper)
        : base(applicationDbContext, mapper, currentUserService)
    {
    }
    
    public override async Task<Result<ToDoItemDto>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var entity = await ContextDb.Set<ToDoItem>()
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        entity.ThrowIfNull(new NotFoundException());

        return Result.Ok(await entity.BuildAdapter(Mapper.Config)
            .AdaptToTypeAsync<ToDoItemDto>());
    }
}