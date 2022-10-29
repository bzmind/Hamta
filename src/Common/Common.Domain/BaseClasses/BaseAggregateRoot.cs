using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Domain.BaseClasses;

public class BaseAggregateRoot : BaseEntity
{
    private readonly List<BaseDomainEvent> _domainEvents = new();

    [NotMapped]
    public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents.ToList();

    public void AddDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(BaseDomainEvent eventItem)
    {
        _domainEvents.Remove(eventItem);
    }
}