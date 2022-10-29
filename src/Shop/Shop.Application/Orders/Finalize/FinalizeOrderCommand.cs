using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Orders.Finalize;

public record FinalizeOrderCommand(long OrderId) : IBaseCommand;

public class FinalizeOrderCommandHandler : IBaseCommandHandler<FinalizeOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;

    public FinalizeOrderCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(FinalizeOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsTrackingAsync(request.OrderId);
        if (order == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سفارش"));

        order.Finalize();

        var inventories = await _sellerRepository.GetOrderItemInventoriesAsync(order.Items.ToList());
        order.Items.ToList().ForEach(orderItem =>
        {
            var inventory = inventories.First(inventory => inventory.Id == orderItem.InventoryId);
            inventory.DecreaseQuantity(orderItem.Count);
        });

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}