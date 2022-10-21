using Common.Api;
using Shop.Query.Orders._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Orders;

namespace Shop.UI.Services.Orders;

public class OrderService : BaseService, IOrderService
{
    protected override string ApiEndpointName { get; set; } = "Order";

    public OrderService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> AddItem(AddOrderItemViewModel model)
    {
        return await PostAsJsonAsync("AddItem", model);
    }

    public async Task<ApiResult> Checkout(CheckoutOrderViewModel model)
    {
        return await PutAsJsonAsync("Checkout", model);
    }

    public async Task<ApiResult> IncreaseItemCount(long itemId)
    {
        return await PutAsync($"IncreaseItemCount/{itemId}");
    }

    public async Task<ApiResult> DecreaseItemCount(long itemId)
    {
        return await PutAsync($"DecreaseItemCount/{itemId}");
    }

    public async Task<ApiResult> SetStatus(SetOrderStatusViewModel model)
    {
        return await PutAsJsonAsync("SetStatus", model);
    }

    public async Task<ApiResult> RemoveItem(long itemId)
    {
        return await DeleteAsync($"RemoveItem/{itemId}");
    }

    public async Task<OrderDto?> GetById(long orderId)
    {
        var result = await GetFromJsonAsync<OrderDto>($"GetById/{orderId}");
        return result.Data;
    }

    public async Task<OrderDto?> GetByUserId(long userId)
    {
        var result = await GetFromJsonAsync<OrderDto>($"GetByUserId/{userId}");
        return result.Data;
    }

    public async Task<OrderFilterResult> GetByFilter(OrderFilterParams filterParams)
    {
        var url = MakeQueryUrl("GetByFilter", filterParams);
        var result = await GetFromJsonAsync<OrderFilterResult>(url);
        return result.Data;
    }
}