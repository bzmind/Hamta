using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Products._DTOs;

public class ProductFilterResult : BaseFilterResult<ProductFilterDto, ProductFilterParams>
{
    public int HighestPriceInCategory { get; set; }
}

public class ProductFilterParams : BaseFilterParams
{
    public long? CategoryId { get; set; }
    public long? OldCategoryId { get; set; }
    public string? Name { get; set; }
    public string? EnglishName { get; set; }
    public string? Slug { get; set; }
    public int? AverageScore { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinDiscountPercentage { get; set; }
    public int? MaxDiscountPercentage { get; set; }
}