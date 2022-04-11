using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.UseCases.RemoveDiscount;

public record RemoveInventoryDiscountCommand(long InventoryId) : IBaseCommand;

public class RemoveInventoryDiscountCommandHandler : IBaseCommandHandler<RemoveInventoryDiscountCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public RemoveInventoryDiscountCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(RemoveInventoryDiscountCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound();

        inventory.RemoveDiscount();
        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}