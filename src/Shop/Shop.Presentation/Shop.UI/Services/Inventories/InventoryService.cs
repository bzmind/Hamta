using Common.Api;
using Shop.Query.Inventories._DTOs;
using System.Text.Json;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DecreaseQuantity;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.IncreaseQuantity;
using Shop.Application.Inventories.SetDiscountPercentage;

namespace Shop.UI.Services.Inventories;

public class InventoryService : BaseService, IInventoryService
{
    protected override string ApiEndpointName { get; set; } = "Inventory";

    public InventoryService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }
    
    public async Task<ApiResult> Create(CreateInventoryCommand model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> Edit(EditInventoryCommand model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ApiResult> IncreaseQuantity(IncreaseInventoryQuantityCommand model)
    {
        return await PutAsJsonAsync("IncreaseQuantity", model);
    }

    public async Task<ApiResult> DecreaseQuantity(DecreaseInventoryQuantityCommand model)
    {
        return await PutAsJsonAsync("DecreaseQuantity", model);
    }

    public async Task<ApiResult> SetDiscountPercentage(SetInventoryDiscountPercentageCommand model)
    {
        return await PutAsJsonAsync("SetDiscountPercentage", model);
    }

    public async Task<ApiResult> RemoveDiscount(long inventoryId)
    {
        return await PutAsync($"RemoveDiscount/{inventoryId}");
    }

    public async Task<ApiResult> Remove(long inventoryId)
    {
        return await DeleteAsync($"Remove/{inventoryId}");
    }

    public async Task<InventoryDto> GetById(long inventoryId)
    {
        var result = await GetFromJsonAsync<InventoryDto>($"GetById/{inventoryId}");
        return result.Data;
    }

    public async Task<InventoryFilterResult> GetByFilter(InventoryFilterParams filterParams)
    {
        var url = $"api/inventory/GetByFilterPageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&ProductId={filterParams.ProductId}&StartQuantity={filterParams.StartQuantity}" +
                  $"&EndQuantity={filterParams.EndQuantity}&StartPrice={filterParams.StartPrice}" +
                  $"&EndPrice={filterParams.EndPrice}" +
                  $"&StartDiscountPercentage={filterParams.StartDiscountPercentage}" +
                  $"&EndDiscountPercentage={filterParams.EndDiscountPercentage}" +
                  $"&IsAvailable={filterParams.IsAvailable}&IsDiscounted={filterParams.IsDiscounted}";
        var result = await GetFromJsonAsync<InventoryFilterResult>(url);
        return result.Data;
    }
}