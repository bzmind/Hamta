using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.ShippingAggregate;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Create;

public record CreateShippingCommand(string ShippingMethod, int ShippingCost) : IBaseCommand;

public class CreateShippingCommandHandler : IBaseCommandHandler<CreateShippingCommand>
{
    private readonly IShippingRepository _shippingRepository;

    public CreateShippingCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = new Shipping(request.ShippingMethod, request.ShippingCost);

        _shippingRepository.Add(shipping);
        await _shippingRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CreateShippingCommandValidator : AbstractValidator<CreateShippingCommand>
{
    public CreateShippingCommandValidator()
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