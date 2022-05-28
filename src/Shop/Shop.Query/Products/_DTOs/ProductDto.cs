using Common.Query.BaseClasses;
using Shop.Domain.ProductAggregate.Value_Objects;
using Shop.Query.Categories._DTOs;

namespace Shop.Query.Products._DTOs;

public class ProductDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Description { get; set; }
    public string MainImage { get; set; }
    public float AverageScore { get; set; }
    public List<ProductImageDto> GalleryImages { get; set; }
    public List<ProductSpecificationDto> CustomSpecifications { get; set; }
    public List<CategorySpecificationDto> CategorySpecifications { get; set; }
    public List<ProductExtraDescriptionDto> ExtraDescriptions { get; set; }
    public List<ProductInventoryDto> ProductInventories { get; set; }
}