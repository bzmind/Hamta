using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;

namespace Shop.Presentation.Facade.Orders;

internal class OrderFacade : IOrderFacade
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

    public async Task<OperationResult> Checkout(CheckoutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OrderDto?> GetById(long id)
    {
        return await _mediator.Send(new GetOrderByIdQuery(id));
    }

    public async Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParams));
    }
}