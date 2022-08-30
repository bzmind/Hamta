using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class QueryProductCategorySpecificationDto : BaseDto
{
    public long ProductId { get; set; }
    public long CategorySpecificationId { get; set; }
    public string Description { get; set; }
}