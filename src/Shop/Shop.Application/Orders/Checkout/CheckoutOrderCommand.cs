﻿using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Orders.Checkout;

public record CheckoutOrderCommand(long UserId, long ShippingMethodId) : IBaseCommand;

public class CheckoutOrderCommandHandler : IBaseCommandHandler<CheckoutOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ISellerRepository _sellerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IShippingRepository _shippingRepository; 

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, ISellerRepository sellerRepository,
        IUserRepository userRepository, IShippingRepository shippingRepository)
    {
        _orderRepository = orderRepository;
        _sellerRepository = sellerRepository;
        _userRepository = userRepository;
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsTracking(request.UserId);
        if (order == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("سفارش"));

        var user = await _userRepository.GetAsync(request.UserId);
        if (user == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        var userAddress = user.Addresses.FirstOrDefault(a => a.IsActive);
        if (userAddress == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("آدرس کاربر"));

        var orderAddress = new OrderAddress(order.Id, userAddress.FullName,
            new PhoneNumber(userAddress.PhoneNumber.Value), userAddress.Province, userAddress.City,
            userAddress.FullAddress, userAddress.PostalCode);

        var shipping = await _shippingRepository.GetAsync(request.ShippingMethodId);
        if (shipping == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("روش ارسال"));

        order.Checkout(orderAddress, shipping.Name, shipping.Cost.Value);

        await _orderRepository.SaveAsync();
        return OperationResult.Success();
    }
}