﻿using System.Text.Json;
using Common.Api;
using Shop.Query.Orders._DTOs;
using Shop.UI.Models.Comments;
using Shop.UI.Models.Orders;

namespace Shop.UI.Services.Orders;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public OrderService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateCommentViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/order/additem", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Checkout(CheckoutOrderViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/order/checkout", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> IncreaseItemCount(long orderItemId)
    {
        var result = await _client.PutAsync($"api/order/increaseitemcount/{orderItemId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> DecreaseItemCount(long orderItemId)
    {
        var result = await _client.PutAsync($"api/order/decreaseitemcount/{orderItemId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> SetStatus(SetOrderStatusViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/order/setstatus", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Remove(long orderItemId)
    {
        var result = await _client.DeleteAsync($"api/order/remove/{orderItemId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<OrderDto?> GetById(long orderId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<OrderDto>>($"api/order/getbyid/{orderId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<OrderFilterResult?> GetByFilter(OrderFilterParamsViewModel filterParams)
    {
        var url = $"api/order/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&UserId={filterParams.UserId}&StartDate={filterParams.StartDate}" +
                  $"&EndDate={filterParams.EndDate}&Status={filterParams.Status}";

        var result = await _client.GetFromJsonAsync<ApiResult<OrderFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}