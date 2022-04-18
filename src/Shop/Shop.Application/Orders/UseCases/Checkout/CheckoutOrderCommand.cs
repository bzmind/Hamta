using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.OrderAggregate.Services;

namespace Shop.Application.Orders.UseCases.Checkout;

public record CheckoutOrderCommand(long UserId, string FullName, string PhoneNumber, string Province,
    string City, string FullAddress, string PostalCode, int ShippingMethodId) : IBaseCommand;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDomainService _orderDomainService;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IOrderDomainService orderDomainService)
    {
        _orderRepository = orderRepository;
        _orderDomainService = orderDomainService;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        var address = new OrderAddress(order.Id, request.FullName, new PhoneNumber(request.PhoneNumber),
            request.Province, request.City, request.FullAddress, request.PostalCode);

        order.Checkout(address, request.ShippingMethodId, _orderDomainService);

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
    }
}