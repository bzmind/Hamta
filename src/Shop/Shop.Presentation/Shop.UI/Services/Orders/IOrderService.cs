using Common.Api;
using Shop.Query.Orders._DTOs;
using Shop.UI.Models.Comments;
using Shop.UI.Models.Orders;

namespace Shop.UI.Services.Orders;

public interface IOrderService
{
    Task<ApiResult?> Create(CreateCommentViewModel model);
    Task<ApiResult?> Checkout(CheckoutOrderViewModel model);
    Task<ApiResult?> IncreaseItemCount(long orderItemId);
    Task<ApiResult?> DecreaseItemCount(long orderItemId);
    Task<ApiResult?> SetStatus(SetOrderStatusViewModel model);
    Task<ApiResult?> Remove(long orderItemId);

    Task<OrderDto?> GetById(long orderId);
    Task<List<OrderDto>?> GetByFilter(OrderFilterParamsViewModel filterParams);
}