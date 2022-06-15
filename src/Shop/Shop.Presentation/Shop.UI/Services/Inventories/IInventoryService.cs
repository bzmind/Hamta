using Common.Api;
using Shop.Query.Inventories._DTOs;
using Shop.UI.Models.Inventories;

namespace Shop.UI.Services.Inventories;

public interface IInventoryService
{
    Task<ApiResult?> Create(CreateInventoryCommandViewModel model);
    Task<ApiResult?> Edit(EditInventoryCommandViewModel model);
    Task<ApiResult?> IncreaseQuantity(SetInventoryQuantityCommandViewModel model);
    Task<ApiResult?> DecreaseQuantity(CreateInventoryCommandViewModel model);
    Task<ApiResult?> SetDiscountPercentage(SetDiscountPercentageCommandViewModel model);
    Task<ApiResult?> RemoveDiscount(long inventoryId);
    Task<ApiResult?> Remove(long inventoryId);

    Task<InventoryDto?> GetById(long inventoryId);
    Task<InventoryFilterResult?> GetByFilter(InventoryFilterParamsViewModel filterParams);
}