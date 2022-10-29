using MediatR;
using Shop.Domain.OrderAggregate.Events;

namespace Shop.Application.Orders._EventHandlers;

public class OrderFinalizedEventHandler : INotificationHandler<OrderFinalizedEvent>
{
    public async Task Handle(OrderFinalizedEvent notification, CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        Console.WriteLine("Order has been finalized.");
    }
}