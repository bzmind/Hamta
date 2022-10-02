using System.Text.Json;
using Common.Api;
using Shop.API.ViewModels.Sellers.Inventories;
using Shop.Query.Sellers._DTOs;

namespace Shop.UI.Services.Sellers;

public class SellerService : BaseService, ISellerService
{
    protected override string ApiEndpointName { get; set; } = "Seller";

    public SellerService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }
    
    public async Task<ApiResult> Create(AddSellerInventoryViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> Edit(EditSellerInventoryViewModel model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ApiResult> AddInventory(AddSellerInventoryViewModel model)
    {
        return await PostAsJsonAsync("AddInventory", model);
    }

    public async Task<ApiResult> EditInventory(EditSellerInventoryViewModel model)
    {
        return await PutAsJsonAsync("EditInventory", model);
    }

    public async Task<ApiResult> RemoveInventory(long inventoryId)
    {
        return await DeleteAsync($"RemoveInventory/{inventoryId}");
    }

    public async Task<ApiResult> IncreaseInventoryQuantity(IncreaseSellerInventoryQuantityViewModel model)
    {
        return await PutAsJsonAsync("IncreaseInventoryQuantity", model);
    }

    public async Task<ApiResult> DecreaseInventoryQuantity(DecreaseSellerInventoryQuantityViewModel model)
    {
        return await PutAsJsonAsync("DecreaseInventoryQuantity", model);
    }

    public async Task<ApiResult> Remove(long sellerId)
    {
        return await DeleteAsync($"Remove/{sellerId}");
    }

    public async Task<SellerDto?> GetCurrentSeller()
    {
        var result = await GetFromJsonAsync<SellerDto>("GetCurrentSeller");
        return result.Data;
    }

    public async Task<SellerFilterResult> GetByFilter(SellerFilterParams filterParams)
    {
        var url = MakeQueryUrl("GetByFilter", filterParams);
        var result = await GetFromJsonAsync<SellerFilterResult>(url);
        return result.Data;
    }

    public async Task<SellerInventoryDto?> GetInventoryById(long inventoryId)
    {
        var result = await GetFromJsonAsync<SellerInventoryDto>($"GetInventoryById/{inventoryId}");
        return result.Data;
    }

    public async Task<SellerInventoryFilterResult> GetInventoryByFilter(SellerInventoryFilterParams filterParams)
    {
        var url = $"GetInventoriesByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&UserId={filterParams.UserId}&ProductName={filterParams.ProductName}" +
                  $"&MinQuantity={filterParams.MinQuantity}&MaxQuantity={filterParams.MaxQuantity}" +
                  $"&MinPrice={filterParams.MinPrice}&MaxPrice={filterParams.MaxPrice}" +
                  $"&MinDiscountPercentage={filterParams.MinDiscountPercentage}" +
                  $"&MaxDiscountPercentage={filterParams.MaxDiscountPercentage}" +
                  $"&OnlyAvailable={filterParams.OnlyAvailable}&OnlyDiscounted={filterParams.OnlyDiscounted}";
        var result = await GetFromJsonAsync<SellerInventoryFilterResult>(url);
        return result.Data;
    }
}