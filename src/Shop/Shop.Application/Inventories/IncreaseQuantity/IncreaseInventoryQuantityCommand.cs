using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.IncreaseQuantity;

public record IncreaseInventoryQuantityCommand(long InventoryId, int Amount) : IBaseCommand;

public class IncreaseInventoryQuantityCommandHandler : IBaseCommandHandler<IncreaseInventoryQuantityCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public IncreaseInventoryQuantityCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(IncreaseInventoryQuantityCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("انبار"));

        inventory.IncreaseQuantity(request.Amount);

        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class IncreaseInventoryQuantityCommandValidator : AbstractValidator<IncreaseInventoryQuantityCommand>
{
    public IncreaseInventoryQuantityCommandValidator()
    {
        RuleFor(i => i.Amount)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تعداد"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تعداد"))
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.FieldGreaterThanOrEqualTo("تعداد", 1));
    }
}