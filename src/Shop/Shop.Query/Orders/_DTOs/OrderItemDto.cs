using Common.Query.BaseClasses;

namespace Shop.Query.Orders._DTOs;

public class OrderItemDto : BaseDto
{
    public long OrderId { get; set; }
    public long InventoryId { get; set; }
    public string ProductName { get; set; }
    public string ProductMainImage { get; set; }
    public string ProductSlug { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    public int InventoryQuantity { get; set; }
    public int InventoryDiscountPercentage { get; set; }
    public string InventoryShopName { get; set; }
    public string ColorName  { get; set; }
    public string ColorCode  { get; set; }

    public int EachItemDiscountedPrice => Price - Price * InventoryDiscountPercentage / 100;
    public int DiscountAmount => Price - EachItemDiscountedPrice;
}