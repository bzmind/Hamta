using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductForShopAttributesDto : BaseDto
{
    public string Title { get; set; }
    public List<string> Values { get; set; } = new();
}