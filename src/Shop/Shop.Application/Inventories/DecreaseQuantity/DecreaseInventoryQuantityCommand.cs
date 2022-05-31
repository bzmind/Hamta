using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.DecreaseQuantity;

public record DecreaseInventoryQuantityCommand(long InventoryId, int Amount) : IBaseCommand;

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

        inventory.DecreaseQuantity(request.Amount);

        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class DecreaseInventoryQuantityCommandValidator : AbstractValidator<DecreaseInventoryQuantityCommand>
{
    public DecreaseInventoryQuantityCommandValidator()
    {
        RuleFor(i => i.Amount)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تعداد"))
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.FieldGreaterThanOrEqualTo("تعداد", 1));
    }
}