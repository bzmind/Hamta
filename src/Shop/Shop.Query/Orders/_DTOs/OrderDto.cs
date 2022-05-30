using Common.Query.BaseClasses;

namespace Shop.Query.Orders._DTOs;

public class OrderDto : BaseDto
{
    public long CustomerId { get; set; }
    public string Status { get; set; }
    public OrderAddressDto? Address { get; set; }
    public string ShippingMethod { get; set; }
    public int ShippingCost { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}