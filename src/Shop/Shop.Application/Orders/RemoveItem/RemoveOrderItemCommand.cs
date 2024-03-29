﻿using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.RemoveItem;

public record RemoveOrderItemCommand(long UserId, long OrderItemId) : IBaseCommand;

public class RemoveOrderItemCommandHandler : IBaseCommandHandler<RemoveOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;

    public RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        order.RemoveOrderItem(request.OrderItemId);

        if (!order.Items.Any())
            _orderRepository.Delete(order);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}