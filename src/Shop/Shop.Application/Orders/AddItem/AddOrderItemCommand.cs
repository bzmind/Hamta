using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using FluentValidation;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Orders.AddItem;

public class AddOrderItemCommand : IBaseCommand<long>
{
    public long UserId { get; set; }
    public long InventoryId { get; set; }
    public int Quantity { get; set; }

    private AddOrderItemCommand()
    {

    }
}

public class AddOrderItemCommandHandler : IBaseCommandHandler<AddOrderItemCommand, long>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult<long>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _sellerRepository.GetInventoryByIdAsTrackingAsync(request.InventoryId);
        if (inventory == null)
            return OperationResult<long>.NotFound(ValidationMessages.FieldNotFound("انبار"));

        if (inventory.Quantity < request.Quantity)
            return OperationResult<long>.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);
        if (order == null)
        {
            order = new Order(request.UserId);
            await _orderRepository.AddAsync(order);
        }

        order.AddOrderItem(new OrderItem(order.Id, request.InventoryId, request.Quantity, inventory.Price));

        if (order.Items.First(i => i.InventoryId == inventory.Id).Count > inventory.Quantity)
            return OperationResult<long>.Error("تعداد محصولات سفارش داده شده بیشتر از موجودی است");

        await _orderRepository.SaveAsync();
        return OperationResult<long>.Success(order.Id);
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