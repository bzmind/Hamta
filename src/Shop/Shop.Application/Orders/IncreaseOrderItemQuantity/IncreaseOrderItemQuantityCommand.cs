using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.IncreaseOrderItemQuantity;

public record IncreaseOrderItemQuantityCommand(long UserId, long OrderItemId) : IBaseCommand;

public class IncreaseOrderItemQuantityCommandHandler : IBaseCommandHandler<IncreaseOrderItemQuantityCommand>
{
    private readonly IOrderRepository _orderRepository;

    public IncreaseOrderItemQuantityCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByCustomerIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        order.IncreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}