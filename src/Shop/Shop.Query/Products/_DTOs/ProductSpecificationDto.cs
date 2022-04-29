using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class ProductSpecificationDto : BaseDto
{
    public long ProductId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public bool IsImportantFeature { get; set; }
}