namespace NKZSoft.Template.Persistence.PostgreSQL.Repositories;

using Application.Common.Repositories.PostgreSql;

public class ToDoItemRepository : RepositoryBase<ToDoItem>, IToDoItemRepository
{
    public ToDoItemRepository(IApplicationDbContext dbContext) : base(dbContext.AppDbContext)
    {
    }

    public ToDoItemRepository(IApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext.AppDbContext, specificationEvaluator)
    {
    }
}
