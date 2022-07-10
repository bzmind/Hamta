using Common.Api;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DecreaseQuantity;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.IncreaseQuantity;
using Shop.Application.Inventories.SetDiscountPercentage;
using Shop.Query.Inventories._DTOs;

namespace Shop.UI.Services.Inventories;

public interface IInventoryService
{
    Task<ApiResult> Create(CreateInventoryCommand model);
    Task<ApiResult> Edit(EditInventoryCommand model);
    Task<ApiResult> IncreaseQuantity(IncreaseInventoryQuantityCommand model);
    Task<ApiResult> DecreaseQuantity(DecreaseInventoryQuantityCommand model);
    Task<ApiResult> SetDiscountPercentage(SetInventoryDiscountPercentageCommand model);
    Task<ApiResult> RemoveDiscount(long inventoryId);
    Task<ApiResult> Remove(long inventoryId);

    Task<InventoryDto> GetById(long inventoryId);
    Task<InventoryFilterResult> GetByFilter(InventoryFilterParams filterParams);
}