using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductCategorySpecificationMapper
{
    public static List<ProductCategorySpecificationQueryDto> MapToProductCategorySpecificationQueryDto
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
                Description = specification.Description,
            });
        });

        return dtoSpecifications;
    }
}