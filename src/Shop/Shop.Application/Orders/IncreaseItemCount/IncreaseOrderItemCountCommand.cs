using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Orders.IncreaseItemCount;

public record IncreaseOrderItemCountCommand(long UserId, long OrderItemId) : IBaseCommand;

public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;

    public IncreaseOrderItemCountCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);
        if (order == null)
            return OperationResult.NotFound("سفارش یافت نشد");

        var itemToBeIncreased = order.Items.FirstOrDefault(oi => oi.Id == request.OrderItemId);
        if (itemToBeIncreased == null)
            return OperationResult.NotFound("محصول یافت نشد");

        var inventory = await _sellerRepository.GetInventoryByIdAsTrackingAsync(itemToBeIncreased.InventoryId);
        if (inventory == null)
            return OperationResult.NotFound("انبار یافت نشد");

        if (inventory.Quantity - 1 == 0)
            return OperationResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        order.IncreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}