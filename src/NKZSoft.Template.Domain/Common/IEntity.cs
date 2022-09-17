using MediatR;

namespace NKZSoft.Template.Domain.Common;

public interface IEntity
{
    int Id { get; set; }
    
    bool IsNew { get; set; }

    IReadOnlyCollection<INotification> DomainEvents { get; }

    void AddDomainEvent(INotification eventItem);

    void RemoveDomainEvent(INotification eventItem);

    void ClearDomainEvents();
}