using MediatR;

namespace NKZSoft.Template.Domain.Common;

public interface IEntity
{
    Guid Id { get; set; }

    bool IsNew { get; set; }

    IReadOnlyCollection<INotification> DomainEvents { get; }

    void AddDomainEvent(INotification eventItem);

    void RemoveDomainEvent(INotification eventItem);

    void ClearDomainEvents();
}
