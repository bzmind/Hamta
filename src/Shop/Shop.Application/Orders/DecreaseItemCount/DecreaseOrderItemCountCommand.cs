using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
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
        var order = await _orderRepository.GetOrderByCustomerIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        var item = order.Items.FirstOrDefault(oi => oi.Id == request.OrderItemId);

        if (item == null)
            return OperationResult.NotFound();

        if (item.Count - 1 <= 0)
            return OperationResult.Error(ValidationMessages.FieldQuantityMinNumber("آیتم های سفارش", 0));

        order.DecreaseItemCount(request.OrderItemId);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}