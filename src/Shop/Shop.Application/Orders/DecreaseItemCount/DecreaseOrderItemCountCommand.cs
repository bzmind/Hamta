using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.DecreaseItemCount;

public record DecreaseOrderItemCountCommand(long UserId, long OrderItemId) : IBaseCommand;

public class DecreaseOrderItemCountCommandHandler : IBaseCommandHandler<DecreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DecreaseOrderItemCountCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(DecreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        var itemToDecrease = order.Items.FirstOrDefault(oi => oi.Id == request.OrderItemId);
        if (itemToDecrease == null)
            return OperationResult.NotFound();

        order.DecreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}