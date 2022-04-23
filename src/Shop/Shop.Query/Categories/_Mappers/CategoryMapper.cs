using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.Mappers;

internal static class CategoryMapper
{
    public static CategoryDto MapToCategoryDto(this Category? category)
    {
        if (category == null)
            return null;

        return new CategoryDto
        {
            ParentId = category.ParentId,
            Title = category.Title,
            Slug = category.Slug,
            SubCategories = category.SubCategories.ToList().MapToCategoryDto(),
            Specifications = category.Specifications.ToList().MapToSpecificationDto()
        };
    }
    
    public static List<CategoryDto> MapToCategoryDto(this List<Category> categories)
    {
        var model = new List<CategoryDto>();

        categories.ForEach(category =>
        {
            model.Add(new CategoryDto
            {
                ParentId = category.ParentId,
                Title = category.Title,
                Slug = category.Slug,
                SubCategories = category.SubCategories.ToList().MapToCategoryDto(),
                Specifications = category.Specifications.ToList().MapToSpecificationDto()
            });
        });

        return model;
    }
}