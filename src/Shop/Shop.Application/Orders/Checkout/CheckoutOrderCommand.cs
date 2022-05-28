using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Application.Validation.CustomFluentValidations;
using Common.Domain.ValueObjects;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.Checkout;

public record CheckoutOrderCommand(long UserId, string FullName, string PhoneNumber, string Province,
    string City, string FullAddress, string PostalCode, string ShippingMethod, int ShippingCost) : IBaseCommand;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByCustomerIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound();

        var address = new OrderAddress(order.Id, request.FullName, new PhoneNumber(request.PhoneNumber),
            request.Province, request.City, request.FullAddress, request.PostalCode);

        order.Checkout(address, request.ShippingMethod, request.ShippingCost);

        var inventories = await _inventoryRepository.GetInventoriesForOrderItems(order.Items.ToList());

        order.Items.ToList().ForEach(orderItem =>
        {
            var inventory = inventories.First(i => i.Id == orderItem.InventoryId);
            inventory.RemoveFromQuantity(orderItem.Count);
        });

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
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