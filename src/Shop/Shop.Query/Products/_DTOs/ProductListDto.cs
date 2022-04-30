using Common.Query.BaseClasses;
using Shop.Domain.ColorAggregate;

namespace Shop.Query.Products._DTOs;

public class ProductListDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string EnglishName { get; set; }
    public string Slug { get; set; }
    public int Price { get; set; }
    public int AverageScore { get; set; }
    public int Quantity { get; set; }
    public List<Color> Colors { get; set; }
}