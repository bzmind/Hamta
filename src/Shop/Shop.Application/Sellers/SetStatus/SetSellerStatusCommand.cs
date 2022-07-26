using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.SetStatus;

public record SetSellerStatusCommand(long Id, Seller.SellerStatus Status) : IBaseCommand;

public class SetSellerStatusCommandHandler : IBaseCommandHandler<SetSellerStatusCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public SetSellerStatusCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(SetSellerStatusCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetAsTrackingAsync(request.Id);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        seller.SetStatus(request.Status);

        await _sellerRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class SetSellerStatusCommandValidator : AbstractValidator<SetSellerStatusCommand>
{
    public SetSellerStatusCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("آیدی فروشنده"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("آیدی فروشنده"));

        RuleFor(c => c.Status)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("وضعیت"))
            .IsInEnum().WithMessage(ValidationMessages.FieldInvalid("وضعیت"));
    }
}