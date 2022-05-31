using Common.Application;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DecreaseQuantity;
using Shop.Application.Inventories.DiscountByPercentage;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.IncreaseQuantity;
using Shop.Application.Inventories.RemoveDiscount;
using Shop.Query.Inventories._DTOs;

namespace Shop.Presentation.Facade.Inventories;

public interface IInventoryFacade
{
    Task<OperationResult<long>> Create(CreateInventoryCommand command);
    Task<OperationResult> Edit(EditInventoryCommand command);
    Task<OperationResult> IncreaseQuantity(IncreaseInventoryQuantityCommand command);
    Task<OperationResult> DecreaseQuantity(DecreaseInventoryQuantityCommand command);
    Task<OperationResult> DiscountByPercentage(DiscountByPercentageCommand command);
    Task<OperationResult> RemoveDiscount(RemoveInventoryDiscountCommand command);
    Task<OperationResult> Remove(long inventoryId);

    Task<InventoryDto?> GetById(long id);
    Task<InventoryFilterResult> GetByFilter(InventoryFilterParam filterParams);
}