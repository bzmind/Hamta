using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.Finalize;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetByUserId;

namespace Shop.Presentation.Facade.Orders;

public class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;

    public OrderFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> AddItem(AddOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveItem(RemoveOrderItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseItemCount(long userId, long orderItemId)
    {
        return await _mediator.Send(new IncreaseOrderItemCountCommand(userId, orderItemId));
    }

    public async Task<OperationResult> DecreaseItemCount(long userId, long orderItemId)
    {
        return await _mediator.Send(new DecreaseOrderItemCountCommand(userId, orderItemId));
    }

    public async Task<OperationResult> SetStatus(SetOrderStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Checkout(long userId, long shippingMethodId)
    {
        return await _mediator.Send(new CheckoutOrderCommand(userId, shippingMethodId));
    }

    public async Task<OperationResult> Finalize(long orderId)
    {
        return await _mediator.Send(new FinalizeOrderCommand(orderId));
    }

    public async Task<OrderDto?> GetById(long id)
    {
        return await _mediator.Send(new GetOrderByIdQuery(id));
    }

    public async Task<OrderDto?> GetByUserId(long userId)
    {
        return await _mediator.Send(new GetOrderByUserIdQuery(userId));
    }

    public async Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParams));
    }
}