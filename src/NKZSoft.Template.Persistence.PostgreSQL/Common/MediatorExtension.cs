using Microsoft.EntityFrameworkCore.ChangeTracking;
using NKZSoft.Template.Domain.Common;

namespace NKZSoft.Template.Persistence.PostgreSQL.Common;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(
        this IMediator mediator, IEnumerable<EntityEntry<IEntity>> entityEntries)
    {
        var enumerable = entityEntries.ToList();

        var domainEvents = enumerable
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
        
        foreach (var domainEvent in domainEvents)
        { 
            await mediator.Publish(domainEvent);
        }

        enumerable
            .ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}