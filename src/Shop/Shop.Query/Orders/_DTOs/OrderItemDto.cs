using Common.Query.BaseClasses;

namespace Shop.Query.Orders._DTOs;

public class OrderItemDto : BaseDto
{
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
    public string? ProductName { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int? InventoryQuantity { get; set; }
    public string? ColorName { get; set; }
    public string? ColorCode { get; set; }
    public int TotalPrice => Price * Count;
}