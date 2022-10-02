using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Introduction { get; set; }
    public string? Review { get; set; }
    public string MainImage { get; set; }
    public float AverageScore { get; set; }
    public List<ProductGalleryImageDto> GalleryImages { get; set; } = new();
    public List<ProductSpecificationQueryDto> Specifications { get; set; } = new();
    public List<ProductCategorySpecificationQueryDto> CategorySpecifications { get; set; } = new();
}