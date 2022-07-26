using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.Inventories.DecreaseQuantity;

public record DecreaseSellerInventoryQuantityCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long InventoryId { get; set; }
    public int Quantity { get; set; }
}

public class DecreaseSellerInventoryQuantityCommandHandler :
    IBaseCommandHandler<DecreaseSellerInventoryQuantityCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public DecreaseSellerInventoryQuantityCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(DecreaseSellerInventoryQuantityCommand request,
        CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        seller.DecreaseInventoryQuantity(request.InventoryId, request.Quantity);

        await _sellerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class DecreaseSellerInventoryQuantityCommandValidator :
    AbstractValidator<DecreaseSellerInventoryQuantityCommand>
{
    public DecreaseSellerInventoryQuantityCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull().WithMessage(ValidationMessages.QuantityRequired)
            .NotEmpty().WithMessage(ValidationMessages.QuantityRequired)
            .GreaterThan(0).WithMessage(ValidationMessages.FieldMinLength("تعداد", 0));
    }
}