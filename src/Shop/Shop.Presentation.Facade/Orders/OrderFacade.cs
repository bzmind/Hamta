using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.DecreaseItemCount;
using Shop.Application.Orders.Finalize;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Orders._DTOs;
using Shop.Query.Orders.GetByFilter;
using Shop.Query.Orders.GetById;
using Shop.Query.Orders.GetByUserId;

namespace Shop.Presentation.Facade.Orders;

public class OrderFacade : IOrderFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public OrderFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult<long>> AddItem(AddOrderItemCommand command)
    {
        var order = await GetByUserId(command.UserId);
        if (order != null)
        {
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
            await _cache.RemoveAsync(CacheKeys.Order(order.Id));
        }
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveItem(RemoveOrderItemCommand command)
    {
        var order = await GetByUserId(command.UserId);
        if (order != null)
        {
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
            await _cache.RemoveAsync(CacheKeys.Order(order.Id));
        }
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseItemCount(long userId, long orderItemId)
    {
        var order = await GetByUserId(userId);
        if (order != null)
        {
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
            await _cache.RemoveAsync(CacheKeys.Order(order.Id));
        }
        return await _mediator.Send(new IncreaseOrderItemCountCommand(userId, orderItemId));
    }

    public async Task<OperationResult> DecreaseItemCount(long userId, long orderItemId)
    {
        var order = await GetByUserId(userId);
        if (order != null)
        {
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
            await _cache.RemoveAsync(CacheKeys.Order(order.Id));
        }
        return await _mediator.Send(new DecreaseOrderItemCountCommand(userId, orderItemId));
    }

    public async Task<OperationResult> SetStatus(SetOrderStatusCommand command)
    {
        var order = await GetByUserId(command.UserId);
        if (order != null)
        {
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
            await _cache.RemoveAsync(CacheKeys.Order(order.Id));
        }
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Checkout(long userId, long shippingMethodId)
    {
        var order = await GetByUserId(userId);
        if (order != null)
        {
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
            await _cache.RemoveAsync(CacheKeys.Order(order.Id));
        }
        return await _mediator.Send(new CheckoutOrderCommand(userId, shippingMethodId));
    }

    public async Task<OperationResult> Finalize(long orderId)
    {
        var order = await GetById(orderId);
        if (order != null)
            await _cache.RemoveAsync(CacheKeys.UserOrders(order.UserId));
        await _cache.RemoveAsync(CacheKeys.Order(orderId));
        return await _mediator.Send(new FinalizeOrderCommand(orderId));
    }

    public async Task<OrderDto?> GetById(long id)
    {
        return await _cache.GetOrSet(CacheKeys.Order(id),
            async () => await _mediator.Send(new GetOrderByIdQuery(id)));
    }

    public async Task<OrderDto?> GetByUserId(long userId)
    {
        return await _cache.GetOrSet(CacheKeys.UserOrders(userId),
            async () => await _mediator.Send(new GetOrderByUserIdQuery(userId)));
    }

    public async Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams)
    {
        return await _mediator.Send(new GetOrderByFilterQuery(filterParams));
    }
}