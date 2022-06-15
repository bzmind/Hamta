using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.DiscountByPercentage;

public record DiscountByPercentageCommand(long InventoryId, int DiscountPercentage) : IBaseCommand;

public class DiscountByPercentageCommandHandler : IBaseCommandHandler<DiscountByPercentageCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public DiscountByPercentageCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(DiscountByPercentageCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound();

        inventory.DiscountByPercentage(request.DiscountPercentage);
        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class DiscountByPercentageCommandValidator : AbstractValidator<DiscountByPercentageCommand>
{
    public DiscountByPercentageCommandValidator()
    {
        RuleFor(i => i.DiscountPercentage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تخفیف"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تخفیف"))
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.DiscountMinPercentage("تخفیف", 1))
            .LessThanOrEqualTo(100).WithMessage(ValidationMessages.DiscountMaxPercentage("تخفیف", 100));
    }
}