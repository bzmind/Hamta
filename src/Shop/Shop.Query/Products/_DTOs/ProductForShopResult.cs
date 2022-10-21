using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.ColorAggregate;

namespace Shop.Query.Products._DTOs;

public class ProductForShopResult : BaseFilterResult<ProductForShopDto, ProductForShopFilterParams>
{
    public int HighestPriceInCategory { get; set; }
    public List<ProductForShopAttributesDto> Attributes { get; set; } = new();
}

public class ProductForShopDto : BaseDto
{
    public long InventoryId { get; set; }
    public string Name { get; set; }
    public string? EnglishName { get; set; }
    public string Slug { get; set; }
    public string MainImage { get; set; }
    public int Price { get; set; }
    public int DiscountPercentage { get; set; }
    public int AverageScore { get; set; }
    public int AllQuantityInStock { get; set; }
    public List<Color> Colors { get; set; } = new();
    public int TotalDiscountedPrice => Price - (int)Math.Round(Price * (double)DiscountPercentage / 100);
}

public class ProductForShopFilterParams : BaseFilterParams
{
    public long? CategoryId { get; set; }
    public long? OldCategoryId { get; set; }
    public string? CategorySlug { get; set; }
    public List<string>? Attributes { get; set; }
    public string? Name { get; set; }
    public int? AverageScore { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDiscountPercentage { get; set; }
    public int? MaxDiscountPercentage { get; set; }
    public bool? OnlyAvailable { get; set; }
    public bool? OnlyDiscounted { get; set; }
    public OrderBy OrderBy { get; set; } = OrderBy.MostPopular;
}

public enum OrderBy
{
    MostPopular,
    Cheapest,
    MostExpensive,
    Latest
}