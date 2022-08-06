using Shop.Domain.CategoryAggregate;
using Shop.Query.Categories._DTOs;

namespace Shop.Query.Categories._Mappers;

internal static class CategorySpecificationMapper
{
    public static QueryCategorySpecificationDto MapToQueryCategorySpecificationDto
        (this CategorySpecification? specification)
    {
        if (specification == null)
            return null;

        return new QueryCategorySpecificationDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            CategoryId = specification.CategoryId,
            Title = specification.Title,
            IsImportantFeature = specification.IsImportantFeature
        };
    }

    public static List<QueryCategorySpecificationDto> MapToQueryCategorySpecificationDto
        (this List<CategorySpecification> specifications)
    {
        var dtoSpecifications = new List<QueryCategorySpecificationDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new QueryCategorySpecificationDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                CategoryId = specification.CategoryId,
                Title = specification.Title,
            IsImportantFeature = specification.IsImportantFeature
            });
        });

        return dtoSpecifications;
    }
}