using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Common.Domain.ValueObjects;
using Shop.Domain.CustomerAggregate.Repository;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Orders.Checkout;

public record CheckoutOrderCommand(long CustomerId, long CustomerAddressId, long ShippingMethodId) : IBaseCommand;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IShippingRepository _shippingRepository; 

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IInventoryRepository inventoryRepository, ICustomerRepository customerRepository, IShippingRepository shippingRepository)
    {
        _orderRepository = orderRepository;
        _inventoryRepository = inventoryRepository;
        _customerRepository = customerRepository;
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByCustomerIdAsTracking(request.CustomerId);

        if (order == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سفارش"));

        // TODO: Test it

        var customer = await _customerRepository.GetAsync(request.CustomerId);
        var customerAddress = customer.Addresses.FirstOrDefault(a => a.Id == request.CustomerAddressId);

        if (customerAddress == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("آدرس کاربر"));

        var address = new OrderAddress(order.Id, customerAddress.FullName,
            new PhoneNumber(customerAddress.PhoneNumber.Value), customerAddress.Province, customerAddress.City,
            customerAddress.FullAddress, customerAddress.PostalCode);

        var shipping = await _shippingRepository.GetAsync(request.ShippingMethodId);

        if (shipping == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("روش ارسال"));

        order.Checkout(address, shipping.Method, shipping.Cost.Value);

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