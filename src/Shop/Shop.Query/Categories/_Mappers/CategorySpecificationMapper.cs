using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories._DTOs;

namespace Shop.Query.Categories._Mappers;

internal static class CategorySpecificationMapper
{
    public static CategorySpecificationDto MapToCategorySpecificationDto(this CategorySpecification? specification)
    {
        if (specification == null)
            return null;

        return new CategorySpecificationDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            CategoryId = specification.CategoryId,
            Title = specification.Title,
            Description = specification.Description,
            IsImportantFeature = specification.IsImportantFeature
        };
    }

    public static List<CategorySpecificationDto> MapToCategorySpecificationDto(this List<CategorySpecification> specifications)
    {
        var dtoSpecifications = new List<CategorySpecificationDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new CategorySpecificationDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                CategoryId = specification.CategoryId,
                Title = specification.Title,
                Description = specification.Description,
            IsImportantFeature = specification.IsImportantFeature
            });
        });

        return dtoSpecifications;
    }
}