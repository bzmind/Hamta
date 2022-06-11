namespace Shop.UI.Models.Products;

public class ProductFilterParamsViewModel : BaseFilterParamsViewModel
{
    public long? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? EnglishName { get; set; }
    public string? Slug { get; set; }
    public int? AverageScore { get; set; }
}