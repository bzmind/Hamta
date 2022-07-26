using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.Inventories.Add;

public class AddSellerInventoryCommand : IBaseCommand<long>
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public long ColorId { get; set; }
    public int DiscountPercentage { get; set; }
}

public class AddSellerInventoryCommandHandler : IBaseCommandHandler<AddSellerInventoryCommand, long>
{
    private readonly ISellerRepository _sellerRepository;

    public AddSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult<long>> Handle(AddSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult<long>.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        var inventory = new SellerInventory(seller.Id, request.ProductId, request.Quantity, request.Price,
            request.ColorId, request.DiscountPercentage);

        seller.AddInventory(inventory);

        await _sellerRepository.SaveAsync();
        return OperationResult<long>.Success(inventory.Id);
    }
}

public class AddSellerInventoryCommandValidator : AbstractValidator<AddSellerInventoryCommand>
{
    public AddSellerInventoryCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("محصولات"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("محصولات"))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldQuantityMinNumber("محصولات", 0));

        RuleFor(i => i.Price)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("قیمت"))
            .GreaterThan(0).WithMessage(ValidationMessages.TomanMinAmount("قیمت", 0));

        RuleFor(i => i.ColorId)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("رنگ"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("رنگ"));

        RuleFor(i => i.DiscountPercentage)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تخفیف"))
            .LessThanOrEqualTo(100).WithMessage(ValidationMessages.FieldPercentageMaximum("تخفیف", 100));
    }
}