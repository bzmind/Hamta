using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate;
using Shop.Domain.InventoryAggregate.Repository;

namespace Shop.Application.Inventories.Create;

public record CreateInventoryCommand(long ProductId, int Quantity, int Price, long ColorId) : IBaseCommand<long>;

public class CreateInventoryCommandHandler : IBaseCommandHandler<CreateInventoryCommand, long>
{
    private readonly IInventoryRepository _inventoryRepository;

    public CreateInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var inventory = new Inventory(request.ProductId, request.Quantity, request.Price, request.ColorId);

        _inventoryRepository.Add(inventory);
        await _inventoryRepository.SaveAsync();
        return OperationResult<long>.Success(inventory.Id);
    }
}

public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تعداد محصولات"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تعداد محصولات"))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldQuantityMinNumber("تعداد محصولات", 0));

        RuleFor(i => i.Price)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .GreaterThan(0).WithMessage(ValidationMessages.PriceMinAmount("قیمت", 0));

        RuleFor(i => i.ColorId)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("رنگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رنگ"));
    }
}