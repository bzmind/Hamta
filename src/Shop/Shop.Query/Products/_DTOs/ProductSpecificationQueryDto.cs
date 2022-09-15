using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductSpecificationQueryDto : BaseDto
{
    public long ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}