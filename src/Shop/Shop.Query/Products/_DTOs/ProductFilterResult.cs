using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Products._DTOs;

public class ProductFilterResult : BaseFilterResult<ProductDto, ProductFilterParam>
{
    
}

public class ProductFilterParam : BaseFilterParam
{
    public long? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? EnglishName { get; set; }
    public string? Slug { get; set; }
    public int? AverageScore { get; set; }
}