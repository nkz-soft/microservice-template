using NKZSoft.Template.Persistence.PostgreSQL.Common;
using NKZSoft.Template.Persistence.PostgreSQL.Extensions;

namespace NKZSoft.Template.Persistence.PostgreSQL;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private ICurrentUserService _currentUserService = null!;
    private IDateTime _dateTime = null!;
    private IMediator _mediator = null!;
    private IDbInitializer _dbInitializer = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public void InitContext(ICurrentUserService currentUserService, IDbInitializer dbInitializer, IDateTime dateTime, IMediator mediator)
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

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.DomainEvents.Any());
        await _mediator.DispatchDomainEventsAsync(domainEntities);
    }

    private void UpdateEntities(ICurrentUser currentUser)
    {
        foreach (var entry in ChangeTracker.Entries<IBaseAuditableEntity<string>>())
        {
            if (entry.Entity.IsNew)
            {
                entry.State = EntityState.Added;
            }

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
}
