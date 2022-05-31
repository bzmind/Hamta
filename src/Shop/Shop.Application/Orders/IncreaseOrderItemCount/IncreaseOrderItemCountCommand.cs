using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.IncreaseOrderItemCount;

public class IncreaseOrderItemCountCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long InventoryId { get; set; }
    public long OrderItemId { get; set; }

    public IncreaseOrderItemCountCommand(long userId, long inventoryId, long orderItemId)
    {
        UserId = userId;
        InventoryId = inventoryId;
        OrderItemId = orderItemId;
    }
}

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

        // TODO: Change status enums to strings
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound("انبار یافت نشد");

        if (inventory.Quantity - 1 == 0)
            return OperationResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        order.IncreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}