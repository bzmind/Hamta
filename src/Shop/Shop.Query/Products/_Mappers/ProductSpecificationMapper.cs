using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductSpecificationMapper
{
    public static ProductSpecificationDto MapToSpecificationDto(this ProductSpecification? specification)
    {
        if (specification == null)
            return null;

        return new ProductSpecificationDto
        {
            Id = specification.Id,
            CreationDate = specification.CreationDate,
            ProductId = specification.ProductId,
            Key = specification.Key,
            Value = specification.Value,
            IsImportantFeature = specification.IsImportantFeature
        };
    }

    public static List<ProductSpecificationDto> MapToSpecificationDto(this List<ProductSpecification> specifications)
    {
        var dtoSpecifications = new List<ProductSpecificationDto>();

        specifications.ForEach(specification =>
        {
            dtoSpecifications.Add(new ProductSpecificationDto
            {
                Id = specification.Id,
                CreationDate = specification.CreationDate,
                ProductId = specification.ProductId,
                Key = specification.Key,
                Value = specification.Value,
                IsImportantFeature = specification.IsImportantFeature
            });
        });

        return dtoSpecifications;
    }
}