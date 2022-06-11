using Common.Api;
using Shop.Query.Inventories._DTOs;
using Shop.UI.Models.Inventories;

namespace Shop.UI.Services.Inventories;

public interface IInventoryService
{
    Task<ApiResult?> Create(CreateInventoryViewModel model);
    Task<ApiResult?> Edit(EditInventoryViewModel model);
    Task<ApiResult?> IncreaseQuantity(SetInventoryQuantityViewModel model);
    Task<ApiResult?> DecreaseQuantity(CreateInventoryViewModel model);
    Task<ApiResult?> DiscountByPercentage(SetDiscountByPercentageViewModel model);
    Task<ApiResult?> RemoveDiscount(long inventoryId);
    Task<ApiResult?> Remove(long inventoryId);

    Task<InventoryDto?> GetById(long inventoryId);
    Task<InventoryFilterResult?> GetByFilter(InventoryFilterParamsViewModel filterParams);
}