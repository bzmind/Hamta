﻿using Common.Application;
using MediatR;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.IncreaseOrderItemCount;
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

    public async Task<OperationResult> IncreaseItemCount(IncreaseOrderItemCountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseItemCount(DecreaseOrderItemCountCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetStatus(SetOrderStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Checkout(CheckoutOrderCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OrderDto?> GetOrderById(long id)
    {
        return await _mediator.Send(new GetOrderByIdQuery(id));
    }

    public async Task<OrderFilterResult> GetOrderByFilter(OrderFilterParam filterParams)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParams));
    }
}