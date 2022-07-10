using Common.Api;
using Shop.API.CommandViewModels.Comments;
using Shop.API.CommandViewModels.Orders;
using Shop.Application.Orders.SetStatus;
using Shop.Query.Orders._DTOs;

namespace Shop.UI.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> Create(CreateCommentCommandViewModel model);
    Task<ApiResult> Checkout(CheckoutOrderCommandViewModel model);
    Task<ApiResult> IncreaseItemCount(long orderItemId);
    Task<ApiResult> DecreaseItemCount(long orderItemId);
    Task<ApiResult> SetStatus(SetOrderStatusCommand model);
    Task<ApiResult> Remove(long orderItemId);

    Task<OrderDto> GetById(long orderId);
    Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams);
}