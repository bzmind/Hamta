using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.IncreaseOrderItemCount;

public record IncreaseOrderItemCountCommand(long UserId, long OrderItemId) : IBaseCommand;

public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public IncreaseOrderItemCountCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound("سفارش یافت نشد");

        var itemToBeIncreased = order.Items.FirstOrDefault(oi => oi.Id == request.OrderItemId);

        if (itemToBeIncreased == null)
            return OperationResult.NotFound("محصول یافت نشد");

        var inventory = await _inventoryRepository.GetAsTrackingAsync(itemToBeIncreased.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound("انبار یافت نشد");

        if (inventory.Quantity - 1 == 0)
            return OperationResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        order.IncreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}