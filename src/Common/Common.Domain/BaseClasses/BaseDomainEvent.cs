using MediatR;

namespace Common.Domain.BaseClasses;

public class BaseDomainEvent : INotification
{
    public DateTime CreationDate { get; protected set; }

    public BaseDomainEvent()
    {
        CreationDate = DateTime.Now;
    }
}