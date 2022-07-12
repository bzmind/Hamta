using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.DecreaseQuantity;

public record DecreaseInventoryQuantityCommand(long InventoryId, int Quantity) : IBaseCommand;

public class DecreaseInventoryQuantityCommandHandler : IBaseCommandHandler<DecreaseInventoryQuantityCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public DecreaseInventoryQuantityCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(DecreaseInventoryQuantityCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);
        if (inventory == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("انبار"));

        inventory.DecreaseQuantity(request.Quantity);

        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class DecreaseInventoryQuantityCommandValidator : AbstractValidator<DecreaseInventoryQuantityCommand>
{
    public DecreaseInventoryQuantityCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull().WithMessage(ValidationMessages.FieldRequired(""))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired(""))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldQuantityMinNumber("", 0));
    }
}