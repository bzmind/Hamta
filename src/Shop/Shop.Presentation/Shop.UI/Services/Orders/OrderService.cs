using Common.Api;
using Shop.Query.Orders._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Orders;
using Shop.Domain.OrderAggregate;

namespace Shop.UI.Services.Orders;

public class OrderService : BaseService, IOrderService
{
    protected override string ApiEndpointName { get; set; } = "Order";

    public OrderService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> AddItem(AddOrderItemViewModel model)
    {
        return await PostAsJsonAsync("AddItem", model);
    }

    public async Task<ApiResult> Checkout(long shippingMethodId)
    {
        return await PutAsync($"Checkout/{shippingMethodId}");
    }

    public async Task<ApiResult> Finalize(long orderId)
    {
        return await PutAsync($"Finalize/{orderId}");
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

    public async Task<ApiResult<OrderDto?>> GetById(long orderId)
    {
        var result = await GetFromJsonAsync<OrderDto>($"GetById/{orderId}");
        return result;
    }

    public async Task<ApiResult<OrderDto?>> GetByUserId(long userId)
    {
        var result = await GetFromJsonAsync<OrderDto>($"GetByUserId/{userId}");
        return result;
    }

    public async Task<ApiResult<OrderFilterResult>> GetByFilter(OrderFilterParams filterParams)
    {
        var url = MakeQueryUrl("GetByFilter", filterParams);
        var result = await GetFromJsonAsync<OrderFilterResult>(url);
        return result;
    }

    public async Task<ApiResult<OrderFilterResult>> GetByFilterForUser(int pageId, int take,
        Order.OrderStatus? status)
    {
        var filterParams = new OrderFilterParams
        {
            PageId = pageId,
            Take = take,
            Status = status,
            OrderId = null,
            UserId = null,
            StartDate = null,
            EndDate = null
        };
        var url = MakeQueryUrl("GetByFilterForUser", filterParams);
        var result = await GetFromJsonAsync<OrderFilterResult>(url);
        return result;
    }
}