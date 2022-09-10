namespace Shop.Application.Categories._DTOs;

public class CategorySpecificationDto
{
    public long? Id { get; set; }
    public string Title { get; set; }
    public bool IsImportant { get; set; }
    public bool IsOptional { get; set; }
}