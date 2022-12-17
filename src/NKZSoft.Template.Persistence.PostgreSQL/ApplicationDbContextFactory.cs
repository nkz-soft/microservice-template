namespace NKZSoft.Template.Persistence.PostgreSQL;

public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDbInitializer _dbInitializer;
    private readonly IMediator _mediator;
    private readonly IDateTime _dateTime;

    public ApplicationDbContextFactory(
            IDbContextFactory<ApplicationDbContext> pooledFactory,
            ICurrentUserService currentUserService,
            IDbInitializer dbInitializer,
            IMediator mediator,
            IDateTime dateTime)
    {
        _pooledFactory = pooledFactory.ThrowIfNull();
        _currentUserService = currentUserService.ThrowIfNull();
        _dbInitializer = dbInitializer.ThrowIfNull();
        _mediator = mediator.ThrowIfNull();
        _dateTime = dateTime.ThrowIfNull();
    }

    public ApplicationDbContext CreateDbContext()
    {
        var context = _pooledFactory.CreateDbContext();
        context.InitContext(_currentUserService, _dbInitializer, _dateTime, _mediator);
        return context;
    }
}
