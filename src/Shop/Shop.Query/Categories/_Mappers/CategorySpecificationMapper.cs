using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories.DTOs;

namespace Shop.Query.Categories.Mappers;

internal static class CategorySpecificationMapper
{
    public static CategorySpecificationDto MapToSpecificationDto(this CategorySpecification? specification)
    {
        if (specification == null)
            return null;

        return new CategorySpecificationDto
        {
            CategoryId = specification.CategoryId,
            Title = specification.Title,
            Description = specification.Description
        };
    }

    public static List<CategorySpecificationDto> MapToSpecificationDto(this List<CategorySpecification> specifications)
    {
        var model = new List<CategorySpecificationDto>();

        specifications.ForEach(specification =>
        {
            model.Add(new CategorySpecificationDto
            {
                CategoryId = specification.CategoryId,
                Title = specification.Title,
                Description = specification.Description
            });
        });

        return model;
    }
}