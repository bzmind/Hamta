using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.IncreaseOrderItemCount;

public record IncreaseOrderItemCountCommand(long UserId, long OrderItemId) : IBaseCommand;

public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;

    public IncreaseOrderItemCountCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByCustomerIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        order.IncreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}