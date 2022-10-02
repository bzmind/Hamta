using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductCategorySpecificationQueryDto : BaseDto
{
    public long ProductId { get; set; }
    public long CategorySpecificationId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsImportant { get; set; }
    public bool IsOptional { get; set; }
}