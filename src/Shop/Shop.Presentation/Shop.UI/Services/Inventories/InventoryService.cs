﻿using Common.Api;
using Shop.Query.Inventories._DTOs;
using Shop.UI.Models.Inventories;
using System.Text.Json;

namespace Shop.UI.Services.Inventories;

public class InventoryService : IInventoryService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public InventoryService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateInventoryCommandViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/inventory/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditInventoryCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> IncreaseQuantity(SetInventoryQuantityCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/increasequantity", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> DecreaseQuantity(CreateInventoryCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/decreasequantity", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetDiscountPercentage(SetDiscountPercentageCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/setdiscountpercentage", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> RemoveDiscount(long inventoryId)
    {
        var result = await _client.PutAsync($"api/inventory/removediscount/{inventoryId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long inventoryId)
    {
        var result = await _client.DeleteAsync($"api/inventory/remove/{inventoryId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<InventoryDto?> GetById(long inventoryId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<InventoryDto>>($"api/inventory/getbyid/{inventoryId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<InventoryFilterResult?> GetByFilter(InventoryFilterParamsViewModel filterParams)
    {
        var url = $"api/inventory/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&ProductId={filterParams.ProductId}&StartQuantity={filterParams.StartQuantity}" +
                  $"&EndQuantity={filterParams.EndQuantity}&StartPrice={filterParams.StartPrice}" +
                  $"&EndPrice={filterParams.EndPrice}" +
                  $"&StartDiscountPercentage={filterParams.StartDiscountPercentage}" +
                  $"&EndDiscountPercentage={filterParams.EndDiscountPercentage}" +
                  $"&IsAvailable={filterParams.IsAvailable}&IsDiscounted={filterParams.IsDiscounted}";

        var result = await _client.GetFromJsonAsync<ApiResult<InventoryFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}