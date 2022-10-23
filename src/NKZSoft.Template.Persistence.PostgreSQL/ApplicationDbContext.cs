using NKZSoft.Template.Persistence.PostgreSQL.Common;
using NKZSoft.Template.Persistence.PostgreSQL.Extensions;

namespace NKZSoft.Template.Persistence.PostgreSQL;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService = null!;
    private readonly IDateTime _dateTime = null!;
    private readonly IMediator _mediator = null!;
    private readonly IDbInitializer _dbInitializer = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTime dateTime,
        IMediator mediator,
        IDbInitializer dbInitializer)
        : base(options)
    {
        _dbInitializer = dbInitializer.ThrowIfNull();
        _currentUserService = currentUserService.ThrowIfNull();
        _dateTime = dateTime.ThrowIfNull();
        _mediator = mediator.ThrowIfNull();
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
    public DbSet<ToDoList> ToDoLists => Set<ToDoList>();

    public DbContext AppDbContext => this;

    public IDbInitializer DbInitializer => _dbInitializer;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUser = _currentUserService.CurrentUser;

        UpdateEntities(currentUser);

        var result = await base.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync();

        return result;
    }

    public async Task MigrateAsync() => await AppDbContext.Database.MigrateAsync();

    public async Task SeedAsync() => await  _dbInitializer.SeedAsync(this);

    private static void UpdateState<T>(EntityEntry<T> entry)
        where T : class, IEntity
    {
        if (entry.Entity.IsNew)
        {
            entry.State = EntityState.Added;
        }
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());
        await _mediator.DispatchDomainEventsAsync(domainEntities);
    }

    private void UpdateEntities(ICurrentUser currentUser)
    {
        foreach (var entry in ChangeTracker.Entries<IBaseAuditableEntity>())
        {
            UpdateState(entry);

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = currentUser.Id.ToString();
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = currentUser.Id.ToString();
                    entry.Entity.Modified = _dateTime.Now;
                    break;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.DataTimeConfigure();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
