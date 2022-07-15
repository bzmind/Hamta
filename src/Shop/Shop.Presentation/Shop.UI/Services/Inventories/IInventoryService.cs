using Common.Api;
using Shop.API.ViewModels.Inventories;
using Shop.Query.Inventories._DTOs;

namespace Shop.UI.Services.Inventories;

public interface IInventoryService
{
    Task<ApiResult> Create(CreateInventoryViewModel model);
    Task<ApiResult> Edit(EditInventoryViewModel model);
    Task<ApiResult> IncreaseQuantity(IncreaseInventoryQuantityViewModel model);
    Task<ApiResult> DecreaseQuantity(DecreaseInventoryQuantityViewModel model);
    Task<ApiResult> SetDiscountPercentage(SetInventoryDiscountPercentageViewModel model);
    Task<ApiResult> RemoveDiscount(long inventoryId);
    Task<ApiResult> Remove(long inventoryId);

    Task<InventoryDto?> GetById(long inventoryId);
    Task<InventoryFilterResult> GetByFilter(InventoryFilterParams filterParams);
}