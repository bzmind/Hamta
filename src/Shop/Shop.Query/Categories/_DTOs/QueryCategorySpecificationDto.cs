using Common.Query.BaseClasses;

namespace Shop.Query.Categories._DTOs;

public class QueryCategorySpecificationDto : BaseDto
{
    public long CategoryId { get; set; }
    public string Title { get; set; }
    public bool IsImportant { get; set; }
    public bool IsOptional { get; set; }
}