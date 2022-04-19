using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.DecreaseItemQuantity;

public record DecreaseOrderItemQuantityCommand(long UserId, long OrderItemId) : IBaseCommand;

public class DecreaseOrderItemQuantityCommandHandler : IBaseCommandHandler<DecreaseOrderItemQuantityCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DecreaseOrderItemQuantityCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(DecreaseOrderItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        order.DecreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}