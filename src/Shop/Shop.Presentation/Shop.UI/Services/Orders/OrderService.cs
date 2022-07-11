using Common.Api;
using Shop.Query.Orders._DTOs;
using System.Text.Json;
using Shop.Application.Orders.SetStatus;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Comments;

namespace Shop.UI.Services.Orders;

public class OrderService : BaseService, IOrderService
{
    protected override string ApiEndpointName { get; set; } = "Order";

    public OrderService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateCommentViewModel model)
    {
        return await PostAsJsonAsync("AddItem", model);
    }

    public async Task<ApiResult> Checkout(CheckoutOrderViewModel model)
    {
        return await PutAsJsonAsync("Checkout", model);
    }

    public async Task<ApiResult> IncreaseItemCount(long orderItemId)
    {
        return await PutAsync($"IncreaseItemCount/{orderItemId}");
    }

    public async Task<ApiResult> DecreaseItemCount(long orderItemId)
    {
        return await PutAsync($"DecreaseItemCount/{orderItemId}");
    }

    public async Task<ApiResult> SetStatus(SetOrderStatusCommand model)
    {
        return await PutAsJsonAsync("SetStatus", model);
    }

    public async Task<ApiResult> Remove(long orderItemId)
    {
        return await DeleteAsync($"Remove/{orderItemId}");
    }

    public async Task<OrderDto> GetById(long orderId)
    {
        var result = await GetFromJsonAsync<OrderDto>($"GetById/{orderId}");
        return result.Data;
    }

    public async Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams)
    {
        var url = $"api/order/GetByFilterPageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&UserId={filterParams.UserId}&StartDate={filterParams.StartDate}" +
                  $"&EndDate={filterParams.EndDate}&Status={filterParams.Status}";
        var result = await GetFromJsonAsync<OrderFilterResult>(url);
        return result.Data;
    }
}