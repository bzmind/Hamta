using Common.Api;
using Shop.API.ViewModels.Comments;
using Shop.API.ViewModels.Orders;
using Shop.Application.Orders.SetStatus;
using Shop.Query.Orders._DTOs;

namespace Shop.UI.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> Create(CreateCommentViewModel model);
    Task<ApiResult> Checkout(CheckoutOrderViewModel model);
    Task<ApiResult> IncreaseItemCount(long orderItemId);
    Task<ApiResult> DecreaseItemCount(long orderItemId);
    Task<ApiResult> SetStatus(SetOrderStatusCommand model);
    Task<ApiResult> Remove(long orderItemId);

    Task<OrderDto> GetById(long orderId);
    Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams);
}