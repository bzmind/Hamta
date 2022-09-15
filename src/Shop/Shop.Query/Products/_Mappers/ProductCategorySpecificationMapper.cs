using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductCategorySpecificationMapper
{
    public static ProductCategorySpecificationQueryDto MapToQueryProductCategorySpecificationDto
        (this ProductCategorySpecification? specification)
    {
        if (specification == null)
            return null;

        return new ProductCategorySpecificationQueryDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            ProductId = specification.ProductId,
            CategorySpecificationId = specification.CategorySpecificationId,
            Description = specification.Description
        };
    }

    public static List<ProductCategorySpecificationQueryDto> MapToQueryProductCategorySpecificationDto
        (this List<ProductCategorySpecification> specifications)
    {
        var dtoSpecifications = new List<ProductCategorySpecificationQueryDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new ProductCategorySpecificationQueryDto
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