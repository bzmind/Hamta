using Common.Query.BaseClasses;

namespace Shop.Query.Categories._DTOs;

public class QueryCategorySpecificationDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Title { get; set; }
    public bool IsImportantFeature { get; set; }
}