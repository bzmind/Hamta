using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductListDto : BaseDto
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int AverageScore { get; set; }
    public int Quantity { get; set; }
    public List<string> ColorCodes { get; set; }
}