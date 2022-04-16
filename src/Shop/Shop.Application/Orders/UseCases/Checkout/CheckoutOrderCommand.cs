using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.UseCases.Checkout;

public record CheckoutOrderCommand(long UserId, string FullName, string PhoneNumber, string Province,
    string City, string FullAddress, string PostalCode, string ShippingMethod) : IBaseCommand;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        var address = new OrderAddress(order.Id, request.FullName, new PhoneNumber(request.PhoneNumber),
            request.Province, request.City, request.FullAddress, request.PostalCode);

        Order.OrderShippingMethod shippingMethod;
        if (Enum.TryParse(request.ShippingMethod, out shippingMethod) == false)
            return OperationResult.Error("روش ارسال نامعتبر است");

        order.Checkout(address, shippingMethod);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}

internal class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(o => o.FullName)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("نام و نام خانوادگی"));

        RuleFor(o => o.PhoneNumber).ValidPhoneNumber();

        RuleFor(o => o.Province)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("استان"));

        RuleFor(o => o.City)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("شهر"));

        RuleFor(o => o.FullAddress)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("آدرس کامل"));

        RuleFor(o => o.PostalCode)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("کد پستی"));

        RuleFor(o => o.ShippingMethod)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.FieldRequired("روش ارسال"));
    }
}