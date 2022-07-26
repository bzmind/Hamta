using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.Inventories.Edit;

public class EditSellerInventoryCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long InventoryId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public long ColorId { get; set; }
    public int DiscountPercentage { get; set; }
}

public class EditSellerInventoryCommandHandler : IBaseCommandHandler<EditSellerInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public EditSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(EditSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        seller.EditInventory(request.InventoryId, request.ProductId, request.Quantity, request.Price,
            request.ColorId, request.DiscountPercentage);

        await _sellerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditSellerInventoryCommandValidator : AbstractValidator<EditSellerInventoryCommand>
{
    public EditSellerInventoryCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تعداد محصولات"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تعداد محصولات"))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldQuantityMinNumber("تعداد محصولات", 0));

        RuleFor(i => i.Price)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .GreaterThan(0).WithMessage(ValidationMessages.TomanMinAmount("قیمت", 0));

        RuleFor(i => i.ColorId)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("آیدی رنگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("آیدی رنگ"));

        RuleFor(i => i.DiscountPercentage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تخفیف"))
            .LessThanOrEqualTo(100).WithMessage(ValidationMessages.FieldPercentageMaximum("تخفیف", 100));
    }
}