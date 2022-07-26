using Common.Api;
using Shop.API.ViewModels.Sellers.Inventories;
using Shop.Query.Sellers._DTOs;

namespace Shop.UI.Services.Sellers;

public interface ISellerService
{
    Task<ApiResult> Create(AddSellerInventoryViewModel model);
    Task<ApiResult> Edit(EditSellerInventoryViewModel model);
    Task<ApiResult> AddInventory(AddSellerInventoryViewModel model);
    Task<ApiResult> EditInventory(EditSellerInventoryViewModel model);
    Task<ApiResult> RemoveInventory(long inventoryId);
    Task<ApiResult> IncreaseInventoryQuantity(IncreaseSellerInventoryQuantityViewModel model);
    Task<ApiResult> DecreaseInventoryQuantity(DecreaseSellerInventoryQuantityViewModel model);
    Task<ApiResult> Remove(long inventoryId);

    Task<SellerDto?> GetById(long id);
    Task<SellerFilterResult> GetByFilter(SellerFilterParams filterParams);
    Task<SellerInventoryDto?> GetInventoryById(long id);
    Task<SellerInventoryFilterResult> GetInventoryByFilter(SellerInventoryFilterParams filterParams);
}