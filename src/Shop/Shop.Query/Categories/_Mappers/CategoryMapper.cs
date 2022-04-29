using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories._DTOs;

namespace Shop.Query.Categories._Mappers;

internal static class CategoryMapper
{
    public static CategoryDto MapToCategoryDto(this Category? category)
    {
        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            ParentId = category.ParentId,
            Title = category.Title,
            Slug = category.Slug,
            SubCategories = category.SubCategories.ToList().MapToCategoryDto(),
            Specifications = category.Specifications.ToList().MapToSpecificationDto()
        };
    }
    
    public static List<CategoryDto> MapToCategoryDto(this List<Category> categories)
    {
        var dtoCategories = new List<CategoryDto>();

        categories.ForEach(category =>
        {
            dtoCategories.Add(new CategoryDto
            {
                Id = category.Id,
                CreationDate = category.CreationDate,
                ParentId = category.ParentId,
                Title = category.Title,
                Slug = category.Slug,
                SubCategories = category.SubCategories.ToList().MapToCategoryDto(),
                Specifications = category.Specifications.ToList().MapToSpecificationDto()
            });
        });

        return dtoCategories;
    }
}