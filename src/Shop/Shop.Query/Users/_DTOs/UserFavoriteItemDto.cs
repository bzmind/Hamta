using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserFavoriteItemDto : BaseDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public long InventoryId { get; set; }
    public string ProductName { get; set; }
    public string ProductSlug { get; set; }
    public string ProductMainImage { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
    public float AverageScore { get; set; }
    public bool IsAvailable { get; set; }

    public int TotalDiscountedPrice => Price - (int)Math.Round(Price * (double)DiscountPercentage / 100);
}