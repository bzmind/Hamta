using Common.Query.BaseClasses;

namespace Shop.Query.Products._DTOs;

public class QueryProductExtraDescriptionDto : BaseDto
{
    public long ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}