using Common.Api;
using Shop.Query.Inventories._DTOs;
using System.Text.Json;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DecreaseQuantity;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.IncreaseQuantity;
using Shop.Application.Inventories.SetDiscountPercentage;

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

    // This Commands are actually used only to send Data through HttpClient, they're just DTO not Command
    // They might be the same as their command records, this 👇 was the same as it's command (CreateInventoryCommand)
    // IDK about others, but Ashrafi had named these "...Command" as well.
    public async Task<ApiResult?> Create(CreateInventoryCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/inventory/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditInventoryCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> IncreaseQuantity(IncreaseInventoryQuantityCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/increasequantity", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> DecreaseQuantity(DecreaseInventoryQuantityCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/decreasequantity", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetDiscountPercentage(SetInventoryDiscountPercentageCommand model)
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

    public async Task<InventoryFilterResult?> GetByFilter(InventoryFilterParams filterParams)
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