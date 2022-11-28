using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories._DTOs;

namespace Shop.Query.Categories._Mappers;

internal static class CategorySpecificationMapper
{
    public static CategorySpecificationQueryDto MapToCategorySpecificationQueryDto
        (this CategorySpecification? specification)
    {
        if (specification == null)
            return null;

        return new CategorySpecificationQueryDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            CategoryId = specification.CategoryId,
            Title = specification.Title,
            IsImportant = specification.IsImportant,
            IsOptional = specification.IsOptional,
            IsFilterable = specification.IsFilterable
        };
    }

    public static List<CategorySpecificationQueryDto> MapToCategorySpecificationQueryDto
        (this List<CategorySpecification> specifications)
    {
        var dtoSpecifications = new List<CategorySpecificationQueryDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new CategorySpecificationQueryDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                CategoryId = specification.CategoryId,
                Title = specification.Title,
                IsImportant = specification.IsImportant,
                IsOptional = specification.IsOptional,
                IsFilterable = specification.IsFilterable
            });
        });

        return dtoSpecifications;
    }
}