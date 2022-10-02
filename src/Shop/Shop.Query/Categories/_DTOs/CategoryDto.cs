using Common.Query.BaseClasses;

namespace Shop.Query.Categories._DTOs;

public class CategoryDto : BaseDto
{
    public long? ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public bool ShowInMenu { get; set; }
    public List<CategoryDto> SubCategories { get; set; } = new();
    public List<CategorySpecificationQueryDto> Specifications { get; set; }
}