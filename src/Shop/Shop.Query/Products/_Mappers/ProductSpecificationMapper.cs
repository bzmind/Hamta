using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductSpecificationMapper
{
    public static ProductSpecificationQueryDto MapToQueryProductSpecificationDto
        (this ProductSpecification? specification)
    {
        if (specification == null)
            return null;

        return new ProductSpecificationQueryDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            ProductId = specification.ProductId,
            Title = specification.Title,
            Description = specification.Description
        };
    }

    public static List<ProductSpecificationQueryDto> MapToQueryProductSpecificationDto
        (this List<ProductSpecification> specifications)
    {
        var dtoSpecifications = new List<ProductSpecificationQueryDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new ProductSpecificationQueryDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                ProductId = specification.ProductId,
                Title = specification.Title,
                Description = specification.Description
            });
        });

        return dtoSpecifications;
    }
}