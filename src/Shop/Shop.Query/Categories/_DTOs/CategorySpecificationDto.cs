using Common.Query.BaseClasses;

namespace Shop.Query.Categories.DTOs;

public class CategorySpecificationDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}