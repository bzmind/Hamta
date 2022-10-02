using Common.Query.BaseClasses;

namespace Shop.Query.Sellers._DTOs;

public class SellerInventoryDto : BaseDto
{
    public long ProductId { get; set; }
    public long ColorId { get; set; }
    public string ProductName { get; set; }
    public string? ProductEnglishName { get; set; }
    public string ProductMainImage { get; set; }
    public string ColorName { get; set; }
    public string ColorCode { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public bool IsAvailable { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsDiscounted { get; set; }
    public int TotalPrice => Price - (int)Math.Round(Price * (double)DiscountPercentage / 100);
    public int DiscountAmount => Price - TotalPrice;
}