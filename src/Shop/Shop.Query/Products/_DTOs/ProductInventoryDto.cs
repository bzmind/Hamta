using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductInventoryDto : BaseDto
{
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public long ColorId { get; set; }
    public string ShopName { get; set; }
    public string ColorName { get; set; }
    public string ColorCode { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsDiscounted { get; set; }
    public int TotalDiscountedPrice => Price - (int)Math.Round(Price * (double)DiscountPercentage / 100);
}