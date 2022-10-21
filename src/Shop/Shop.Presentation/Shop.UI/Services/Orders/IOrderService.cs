using Common.Api;
using Shop.API.ViewModels.Orders;
using Shop.Query.Orders._DTOs;

namespace Shop.UI.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> AddItem(AddOrderItemViewModel command);
    Task<ApiResult> Checkout(CheckoutOrderViewModel model);
    Task<ApiResult> IncreaseItemCount(long itemId);
    Task<ApiResult> DecreaseItemCount(long itemId);
    Task<ApiResult> SetStatus(SetOrderStatusViewModel model);
    Task<ApiResult> RemoveItem(long itemId);

    Task<OrderDto?> GetById(long orderId);
    Task<OrderDto?> GetByUserId(long userId);
    Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams);
}