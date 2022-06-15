using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Edit;

public record EditShippingCommand(long ShippingId, string ShippingName, int ShippingCost) : IBaseCommand;

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

        shipping.Edit(request.ShippingName, request.ShippingCost);

        await _shippingRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditShippingCommandValidator : AbstractValidator<EditShippingCommand>
{
    public EditShippingCommandValidator()
    {
        RuleFor(s => s.ShippingName)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام"));

        RuleFor(s => s.ShippingCost)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("هزینه"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("هزینه"))
            .GreaterThan(0).WithMessage(ValidationMessages.PriceMinAmount("هزینه", 0));
    }
}