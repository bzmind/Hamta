using Common.Application;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.Inventories.Add;
using Shop.Application.Sellers.Inventories.DecreaseQuantity;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Application.Sellers.Inventories.IncreaseQuantity;
using Shop.Application.Sellers.Inventories.Remove;
using Shop.Application.Sellers.SetStatus;
using Shop.Query.Sellers._DTOs;

namespace Shop.Presentation.Facade.Sellers;

public interface ISellerFacade
{
    Task<OperationResult<long>> Create(CreateSellerCommand command);
    Task<OperationResult> Edit(EditSellerCommand command);
    Task<OperationResult> SetStatus(SetSellerStatusCommand command);
    Task<OperationResult<long>> AddInventory(AddSellerInventoryCommand command);
    Task<OperationResult> EditInventory(EditSellerInventoryCommand command);
    Task<OperationResult> RemoveInventory(RemoveSellerInventoryCommand command);
    Task<OperationResult> IncreaseInventoryQuantity(IncreaseSellerInventoryQuantityCommand command);
    Task<OperationResult> DecreaseInventoryQuantity(DecreaseSellerInventoryQuantityCommand command);
    Task<OperationResult> Remove(long sellerId);

    Task<SellerDto?> GetCurrentSeller(long userId);
    Task<SellerFilterResult> GetByFilter(SellerFilterParams filterParams);
    Task<SellerInventoryDto?> GetInventoryById(long id);
    Task<SellerInventoryFilterResult> GetInventoriesByFilter(SellerInventoryFilterParams filterParams);
}