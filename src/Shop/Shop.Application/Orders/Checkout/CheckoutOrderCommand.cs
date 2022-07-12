using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Orders.Checkout;

public class CheckoutOrderCommand : IBaseCommand
{
    public long UserId { get; set; }
    public long UserAddressId { get; set; }
    public long ShippingMethodId { get; set; }
}

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IShippingRepository _shippingRepository; 

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository,
        IUserRepository userRepository, IShippingRepository shippingRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
        _userRepository = userRepository;
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);

        if (order == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سفارش"));

        // TODO: Test it

        var user = await _userRepository.GetAsync(request.UserId);
        var userAddress = user.Addresses.FirstOrDefault(a => a.Id == request.UserAddressId);

        if (userAddress == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("آدرس کاربر"));

        var address = new OrderAddress(order.Id, userAddress.FullName,
            new PhoneNumber(userAddress.PhoneNumber.Value), userAddress.Province, userAddress.City,
            userAddress.FullAddress, userAddress.PostalCode);

        var shipping = await _shippingRepository.GetAsync(request.ShippingMethodId);

        if (shipping == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("روش ارسال"));

        order.Checkout(address, shipping.Name, shipping.Cost.Value);

        var inventories = await _inventoryRepository.GetInventoriesForOrderItems(order.Items.ToList());

        order.Items.ToList().ForEach(orderItem =>
        {
            var inventory = inventories.First(i => i.Id == orderItem.InventoryId);
            inventory.DecreaseQuantity(orderItem.Count);
        });

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}