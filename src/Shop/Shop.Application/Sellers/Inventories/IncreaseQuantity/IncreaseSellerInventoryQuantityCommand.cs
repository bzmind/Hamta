using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.Inventories.IncreaseQuantity;

public class IncreaseSellerInventoryQuantityCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long InventoryId { get; set; }
    public int Quantity { get; set; }
}

public class IncreaseSellerInventoryQuantityCommandHandler :
    IBaseCommandHandler<IncreaseSellerInventoryQuantityCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public IncreaseSellerInventoryQuantityCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(IncreaseSellerInventoryQuantityCommand request,
        CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        seller.IncreaseInventoryQuantity(request.InventoryId, request.Quantity);

        await _sellerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class IncreaseSellerInventoryQuantityCommandValidator :
    AbstractValidator<IncreaseSellerInventoryQuantityCommand>
{
    public IncreaseSellerInventoryQuantityCommandValidator()
    {
        RuleFor(i => i.Quantity)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("تعداد"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("تعداد"))
            .GreaterThan(0).WithMessage(ValidationMessages.FieldMinLength("تعداد", 0));
    }
}