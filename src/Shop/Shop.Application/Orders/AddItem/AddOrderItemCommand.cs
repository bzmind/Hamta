using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using FluentValidation;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;

namespace Shop.Application.Orders.AddItem;

public record AddOrderItemCommand(long InventoryId, long UserId, int Quantity) : IBaseCommand;

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<OperationResult> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _inventoryRepository.GetAsTrackingAsync(request.InventoryId);

        if (inventory == null)
            return OperationResult.NotFound();

        if (inventory.Quantity < request.Quantity)
            return OperationResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        var order = await _orderRepository.GetOrderByCustomerIdAsTracking(request.UserId);

        if (order == null)
            order = new Order(request.UserId);

        await _orderRepository.AddAsync(order);

        order.AddOrderItem(new OrderItem(order.Id, request.InventoryId, request.Quantity, inventory.Price));

        if (order.Items.First(i => i.InventoryId == inventory.Id).Count > inventory.Quantity)
            return OperationResult.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(oi => oi.Quantity)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessages.FieldQuantityMinNumber("سفارشات", 0));
    }
}