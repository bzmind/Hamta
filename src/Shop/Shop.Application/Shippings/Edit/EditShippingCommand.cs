using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Edit;

public record EditShippingCommand(long ShippingId, string ShippingMethod, int ShippingCost) : IBaseCommand;

public class EditShippingCommandHandler : IBaseCommandHandler<EditShippingCommand>
{
    private readonly IShippingRepository _shippingRepository;

    public EditShippingCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(EditShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _shippingRepository.GetAsTrackingAsync(request.ShippingId);

        if (shipping == null)
            return OperationResult.NotFound();

        shipping.Edit(request.ShippingMethod, request.ShippingCost);

        await _shippingRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class EditShippingCommandValidator : AbstractValidator<EditShippingCommand>
{
    public EditShippingCommandValidator()
    {
        RuleFor(s => s.ShippingMethod)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام روش ارسال"));

        RuleFor(s => s.ShippingCost)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("هزینه ارسال"))
            .GreaterThan(0).WithMessage(ValidationMessages.PriceMinAmount("هزینه ارسال", 0));
    }
}