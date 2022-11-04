namespace NKZSoft.Template.Persistence.PostgreSQL.Repositories;

using Application.Common.Repositories;

public class ToDoListRepository : RepositoryBase<ToDoList>, IToDoListRepository
{
    public ToDoListRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
    }

    public ToDoListRepository(IApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        : base(dbContext.AppDbContext, specificationEvaluator)
    {
    }
}
