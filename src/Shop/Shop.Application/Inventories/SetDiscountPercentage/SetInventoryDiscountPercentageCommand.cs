using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.SetDiscountPercentage;

public record SetInventoryDiscountPercentageCommand(long InventoryId, int DiscountPercentage) : IBaseCommand;

public class SetInventoryDiscountPercentageCommandHandler : IBaseCommandHandler<SetInventoryDiscountPercentageCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public SetInventoryDiscountPercentageCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(SetInventoryDiscountPercentageCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound();

        inventory.SetDiscountPercentage(request.DiscountPercentage);
        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetInventoryDiscountPercentageCommandValidator : AbstractValidator<SetInventoryDiscountPercentageCommand>
{
    public SetInventoryDiscountPercentageCommandValidator()
    {
        RuleFor(i => i.DiscountPercentage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تخفیف"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تخفیف"))
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.DiscountMinPercentage("تخفیف", 1))
            .LessThanOrEqualTo(100).WithMessage(ValidationMessages.DiscountMaxPercentage("تخفیف", 100));
    }
}