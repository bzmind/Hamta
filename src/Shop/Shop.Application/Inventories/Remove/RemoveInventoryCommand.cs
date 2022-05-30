using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.Remove;

public record RemoveInventoryCommand(long InventoryId) : IBaseCommand;

public class RemoveInventoryHandler : IBaseCommandHandler<RemoveInventoryCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public RemoveInventoryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(RemoveInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("انبار"));

        _inventoryRepository.Delete(inventory);

        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}