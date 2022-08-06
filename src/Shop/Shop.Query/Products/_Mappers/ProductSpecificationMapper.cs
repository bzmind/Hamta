using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductSpecificationMapper
{
    public static QueryProductSpecificationDto MapToQueryProductSpecificationDto
        (this ProductSpecification? specification)
    {
        if (specification == null)
            return null;

        return new QueryProductSpecificationDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            ProductId = specification.ProductId,
            Title = specification.Title,
            Description = specification.Description
        };
    }

    public static List<QueryProductSpecificationDto> MapToQueryProductSpecificationDto
        (this List<ProductSpecification> specifications)
    {
        var dtoSpecifications = new List<QueryProductSpecificationDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new QueryProductSpecificationDto
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