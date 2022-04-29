using Common.Query.BaseClasses;

namespace Shop.Query.Customers._DTOs;

public class CustomerFavoriteItemDto : BaseDto
{
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductMainImage { get; set; }
    public int ProductPrice { get; set; }
    public double AverageScore { get; set; }
    public bool IsAvailable { get; set; }
}