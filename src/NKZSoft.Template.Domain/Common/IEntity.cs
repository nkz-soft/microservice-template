namespace NKZSoft.Template.Domain.Common;

public interface IEntity
{
    Guid Id { get; set; }

    bool IsNew { get; set; }

    IReadOnlyCollection<INotification> DomainEvents { get; }

    void AddDomainEvent(INotification domainEvent);

    void RemoveDomainEvent(INotification domainEvent);

    void ClearDomainEvents();
}
