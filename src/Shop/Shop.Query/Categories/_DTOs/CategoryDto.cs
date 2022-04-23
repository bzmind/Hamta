using System.Collections.ObjectModel;
using Common.Query.BaseClasses;
using Shop.Domain.CategoryAggregate;

namespace Shop.Query.Categories.DTOs;

public class CategoryDto : BaseDto
{
    public long? ParentId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public List<CategoryDto> SubCategories { get; set; }
    public List<CategorySpecificationDto> Specifications { get; set; }
}