using Common.Api;
using Shop.API.ViewModels.Orders;
using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;

namespace Shop.UI.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> AddItem(AddOrderItemViewModel command);
    Task<ApiResult> Checkout(long shippingMethodId);
    Task<ApiResult> Finalize(long orderId);
    Task<ApiResult> IncreaseItemCount(long itemId);
    Task<ApiResult> DecreaseItemCount(long itemId);
    Task<ApiResult> SetStatus(SetOrderStatusViewModel model);
    Task<ApiResult> RemoveItem(long itemId);

    Task<ApiResult<OrderDto?>> GetById(long orderId);
    Task<ApiResult<OrderDto?>> GetByUserId(long userId);
    Task<ApiResult<OrderFilterResult>> GetByFilter(OrderFilterParams filterParams);
    Task<ApiResult<OrderFilterResult>> GetByFilterForUser(int pageId, int take, Order.OrderStatus? status);
}