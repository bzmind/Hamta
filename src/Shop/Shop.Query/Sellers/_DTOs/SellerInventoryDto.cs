using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Sellers._DTOs;

public class SellerInventoryDto : BaseDto
{
    public long ProductId { get; set; }
    public long ColorId { get; set; }
    public int Quantity { get; set; }
    public Money Price { get; set; }
    public bool IsAvailable { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsDiscounted { get; set; }
}