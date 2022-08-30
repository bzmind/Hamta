using Common.Query.BaseClasses;
using Shop.Domain.ColorAggregate;

namespace Shop.Query.Products._DTOs;

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
    public int AllQuantityInStock { get; set; }
    public List<Color> Colors { get; set; } = new();
}