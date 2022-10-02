using Common.Query.BaseClasses;
using Shop.Query.Categories._DTOs;
using Shop.Query.Colors._DTOs;
using Shop.Query.Comments._DTOs;

namespace Shop.Query.Products._DTOs;

public class SingleProductDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string? Introduction { get; set; }
    public string? Review { get; set; }
    public string MainImage { get; set; }
    public float AverageScore { get; set; }
    public CategoryDto Category { get; set; }
    public List<ProductGalleryImageDto> GalleryImages { get; set; } = new();
    public List<ProductSpecificationQueryDto>? Specifications { get; set; } = new();
    public List<ProductCategorySpecificationQueryDto>? CategorySpecifications { get; set; } = new();
    public List<ProductInventoryDto>? Inventories { get; set; } = new();
    public List<CommentDto>? Comments { get; set; } = new();
    public List<ColorDto> Colors => Inventories.Select(i => new ColorDto
    {
        Id = i.ColorId,
        Name = i.ColorName,
        Code = i.ColorCode
    }).DistinctBy(c => c.Id).ToList();
}