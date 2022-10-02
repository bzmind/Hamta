using Common.Application;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.RemoveItem;
using Shop.Application.Orders.SetStatus;
using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;

namespace Shop.Presentation.Facade.Orders;

public interface IOrderFacade
{
    Task<OperationResult<long>> AddItem(AddOrderItemCommand command);
    Task<OperationResult> RemoveItem(RemoveOrderItemCommand command);
    Task<OperationResult> IncreaseItemCount(long userId, long orderItemId);
    Task<OperationResult> DecreaseItemCount(long userId, long orderItemId);
    Task<OperationResult> SetStatus(SetOrderStatusCommand command);
    Task<OperationResult> Checkout(CheckoutOrderCommand command);

    Task<OrderDto?> GetById(long id);
    Task<OrderDto?> GetByUserId(long userId);
    Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams);
}