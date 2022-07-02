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
    
    public async Task<ApiResult?> Create(CreateInventoryCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/inventory/Create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditInventoryCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/Edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> IncreaseQuantity(IncreaseInventoryQuantityCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/IIncreaseQuantity", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> DecreaseQuantity(DecreaseInventoryQuantityCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/DecreaseQuantity", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> SetDiscountPercentage(SetInventoryDiscountPercentageCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/inventory/SetDiscountPercentage", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> RemoveDiscount(long inventoryId)
    {
        var result = await _client.PutAsync($"api/inventory/RemoveDiscount/{inventoryId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long inventoryId)
    {
        var result = await _client.DeleteAsync($"api/inventory/Remove/{inventoryId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<InventoryDto?> GetById(long inventoryId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<InventoryDto>>($"api/inventory/GetById/{inventoryId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<InventoryFilterResult?> GetByFilter(InventoryFilterParams filterParams)
    {
        var url = $"api/inventory/GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
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