using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.ShippingAggregate;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Create;

public record CreateShippingCommand(string ShippingName, int ShippingCost) : IBaseCommand<long>;

public class CreateShippingCommandHandler : IBaseCommandHandler<CreateShippingCommand, long>
{
    private readonly IShippingRepository _shippingRepository;

    public CreateShippingCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult<long>> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = new Shipping(request.ShippingName, request.ShippingCost);

        _shippingRepository.Add(shipping);
        await _shippingRepository.SaveAsync();
        return OperationResult<long>.Success(shipping.Id);
    }
}

public class CreateShippingCommandValidator : AbstractValidator<CreateShippingCommand>
{
    public CreateShippingCommandValidator()
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