using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductInventoryDto : BaseDto
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public string ColorName { get; set; }
    public string ColorCode { get; set; }
    public bool IsAvailable { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsDiscounted { get; set; }
}