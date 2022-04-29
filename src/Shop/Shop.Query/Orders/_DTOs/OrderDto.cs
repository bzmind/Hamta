using Common.Query.BaseClasses;
using Shop.Domain.OrderAggregate;
using Shop.Domain.OrderAggregate.ValueObjects;

namespace Shop.Query.Orders._DTOs;

public class OrderDto : BaseDto
{
    public long CustomerId { get; set; }
    public Order.OrderStatus Status { get; set; }
    public OrderAddressDto? Address { get; set; }
    public ShippingInfo? ShippingInfo { get; set; }
    public List<OrderItemDto> Items { get; set; }
}