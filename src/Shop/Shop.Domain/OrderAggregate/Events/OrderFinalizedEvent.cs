using Common.Domain.BaseClasses;

namespace Shop.Domain.OrderAggregate.Events;

public class OrderFinalizedEvent : BaseDomainEvent
{
    public long OrderId { get; private set; }

    public OrderFinalizedEvent(long orderId)
    {
        OrderId = orderId;
    }
}