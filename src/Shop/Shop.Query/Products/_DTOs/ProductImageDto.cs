using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductImageDto : BaseDto
{
    public long ProductId { get; set; }
    public string Name { get; set; }
}