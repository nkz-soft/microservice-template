namespace NKZSoft.Template.Domain.Common;

public abstract class BaseEntity<TKey> : IPkEntity<TKey>
{
    private readonly List<INotification> _domainEvents = new();

    public TKey Id { get; protected set; }

    [NotMapped]
    public bool IsNew { get; set; }

    [NotMapped]
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(INotification domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
