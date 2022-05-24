using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Application.Inventories.Create;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.Edit;

public record EditInventoryCommand(long InventoryId, long ProductId, int Quantity, int Price, long ColorId)
    : IBaseCommand;

public class EditInventoryCommandHandler : IBaseCommandHandler<EditInventoryCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public EditInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound();

        inventory.Edit(request.ProductId, request.Quantity, request.Price, request.ColorId);
        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public EditInventoryCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تعداد محصولات"))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldQuantityMinNumber("تعداد محصولات", 0));

        RuleFor(i => i.Price)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .GreaterThan(0).WithMessage(ValidationMessages.PriceMinAmount("قیمت", 0));

        RuleFor(i => i.ColorId)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رنگ"));
    }
}