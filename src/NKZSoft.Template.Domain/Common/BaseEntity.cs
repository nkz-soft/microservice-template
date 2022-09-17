using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace NKZSoft.Template.Domain.Common;

public abstract class BaseEntity : IEntity
{
    private readonly List<INotification> _domainEvents = new();
    
    public int Id { get; set; }

    [NotMapped]
    public bool IsNew { get; set; }

    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
    
    public void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(INotification domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
