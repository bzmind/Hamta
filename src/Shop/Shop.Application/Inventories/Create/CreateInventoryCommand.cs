using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.Create;

public record CreateInventoryCommand(long ProductId, int Quantity, int Price, long ColorId) : IBaseCommand;

public class CreateInventoryCommandHandler : IBaseCommandHandler<CreateInventoryCommand>
{
    private readonly IInventoryRepository _inventoryRepository;

    public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = new Inventory(request.ProductId, request.Quantity, request.Price, request.ColorId);

        _inventoryRepository.Add(inventory);
        await _inventoryRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
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