namespace NKZSoft.Template.Persistence.PostgreSQL;
using NKZSoft.Template.Common.Extensions;
using Common;
using Extensions;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private ICurrentUserService _currentUserService = null!;
    private IDateTime _dateTime = null!;
    private IMediator _mediator = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public void InitContext(ICurrentUserService currentUserService, IDbInitializer dbInitializer, IDateTime dateTime, IMediator mediator)
    {
        DbInitializer = dbInitializer.ThrowIfNull();
        _currentUserService = currentUserService.ThrowIfNull();
        _dateTime = dateTime.ThrowIfNull();
        _mediator = mediator.ThrowIfNull();
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
    public DbSet<ToDoList> ToDoLists => Set<ToDoList>();

    public DbContext AppDbContext => this;

    public IDbInitializer? DbInitializer { get; private set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUser = _currentUserService.CurrentUser;

        UpdateEntities(currentUser);

        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await DispatchDomainEventsAsync().ConfigureAwait(false);

        return result;
    }

    public async Task MigrateAsync() => await AppDbContext.Database.MigrateAsync().ConfigureAwait(false);

    public async Task SeedAsync() => await DbInitializer!.SeedAsync(this).ConfigureAwait(false);

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<IEntity>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0);
        await _mediator.DispatchDomainEventsAsync(domainEntities)
            .ConfigureAwait(false);
    }

    private void UpdateEntities(ICurrentUser currentUser)
    {
        foreach (var entry in ChangeTracker.Entries<IBaseAuditableEntity<string>>())
        {
            if (entry.Entity.IsNew)
            {
                entry.State = EntityState.Added;
            }

            switch (entry)
            {
                case { State: EntityState.Added,}:
                    entry.Entity.CreatedBy = currentUser?.Id?.ToString(CultureInfo.InvariantCulture);
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case { State: EntityState.Modified,}:
                    entry.Entity.ModifiedBy = currentUser?.Id?.ToString(CultureInfo.InvariantCulture);
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
