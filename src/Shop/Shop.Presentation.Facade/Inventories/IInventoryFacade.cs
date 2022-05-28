using Common.Application;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DiscountByPercentage;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.RemoveDiscount;
using Shop.Query.Inventories._DTOs;

namespace Shop.Presentation.Facade.Inventories;

public interface IInventoryFacade
{
    Task<OperationResult<long>> Create(CreateInventoryCommand command);
    Task<OperationResult> Edit(EditInventoryCommand command);
    Task<OperationResult> DiscountByPercentage(DiscountByPercentageCommand command);
    Task<OperationResult> RemoveDiscount(RemoveInventoryDiscountCommand command);

    Task<InventoryDto?> GetInventoryById(long id);
    Task<InventoryFilterResult> GetInventoryByFilter(InventoryFilterParam filterParams);
}