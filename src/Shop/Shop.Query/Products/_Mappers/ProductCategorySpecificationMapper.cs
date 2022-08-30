using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductCategorySpecificationMapper
{
    public static QueryProductCategorySpecificationDto MapToQueryProductCategorySpecificationDto
        (this ProductCategorySpecification? specification)
    {
        if (specification == null)
            return null;

        return new QueryProductCategorySpecificationDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            ProductId = specification.ProductId,
            CategorySpecificationId = specification.CategorySpecificationId,
            Description = specification.Description
        };
    }

    public static List<QueryProductCategorySpecificationDto> MapToQueryProductCategorySpecificationDto
        (this List<ProductCategorySpecification> specifications)
    {
        var dtoSpecifications = new List<QueryProductCategorySpecificationDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new QueryProductCategorySpecificationDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                ProductId = specification.ProductId,
                CategorySpecificationId = specification.CategorySpecificationId,
                Description = specification.Description
            });
        });

        return dtoSpecifications;
    }
}