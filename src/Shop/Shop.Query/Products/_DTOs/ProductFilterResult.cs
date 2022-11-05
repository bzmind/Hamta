using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.ColorAggregate;

namespace Shop.Query.Products._DTOs;

public class ProductFilterResult : BaseFilterResult<ProductFilterDto, ProductFilterParams>
{
    public int HighestPriceInCategory { get; set; }
}

public class ProductFilterDto : BaseDto
{
    public long InventoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string MainImage { get; set; }
    public int LowestInventoryPrice { get; set; }
    public int HighestInventoryPrice { get; set; }
    public float AverageScore { get; set; }
    public int InventoryQuantity { get; set; }
    public List<Color> Colors { get; set; } = new();
}

public class ProductFilterParams : BaseFilterParams
{
    public long? CategoryId { get; set; }
    public long? OldCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public int? AverageScore { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDiscountPercentage { get; set; }
    public int? MaxDiscountPercentage { get; set; }
    public OrderBy OrderBy { get; set; } = OrderBy.MostPopular;
}