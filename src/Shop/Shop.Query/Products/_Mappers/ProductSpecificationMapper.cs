using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductSpecificationMapper
{
    public static ProductSpecificationDto MapToProductSpecificationDto(this ProductSpecification? specification)
    {
        if (specification == null)
            return null;

        return new ProductSpecificationDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            ProductId = specification.ProductId,
            Key = specification.Title,
            Value = specification.Description,
            IsImportantFeature = specification.IsImportantFeature
        };
    }

    public static List<ProductSpecificationDto> MapToProductSpecificationDto(this List<ProductSpecification> specifications)
    {
        var dtoSpecifications = new List<ProductSpecificationDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new ProductSpecificationDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                ProductId = specification.ProductId,
                Key = specification.Title,
                Value = specification.Description,
                IsImportantFeature = specification.IsImportantFeature
            });
        });

        return dtoSpecifications;
    }
}