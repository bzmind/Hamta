using Common.Query.BaseClasses;

namespace Shop.Query.Orders._DTOs;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }
    public string Status { get; set; }
    public OrderAddressDto? Address { get; set; }
    public string ShippingName { get; set; }
    public int ShippingCost { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}