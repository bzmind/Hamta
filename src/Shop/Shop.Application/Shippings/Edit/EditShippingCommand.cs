using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Edit;

public record EditShippingCommand(long Id, string Name, int Cost) : IBaseCommand;

public class EditShippingCommandHandler : IBaseCommandHandler<EditShippingCommand>
{
    private readonly IShippingRepository _shippingRepository;

    public EditShippingCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(EditShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _shippingRepository.GetAsTrackingAsync(request.Id);

        if (shipping == null)
            return OperationResult.NotFound();

        shipping.Edit(request.Name, request.Cost);

        await _shippingRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class EditShippingCommandValidator : AbstractValidator<EditShippingCommand>
{
    public EditShippingCommandValidator()
    {
        RuleFor(s => s.Name)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("نام"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام"));

        RuleFor(s => s.Cost)
            .NotNull().WithMessage(ValidationMessages.FieldRequired("هزینه"))
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("هزینه"))
            .GreaterThan(0).WithMessage(ValidationMessages.PriceMinAmount("هزینه", 0));
    }
}