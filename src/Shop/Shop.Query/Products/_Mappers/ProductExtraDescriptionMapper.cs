using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductExtraDescriptionMapper
{
    public static ProductExtraDescriptionDto MapToExtraDescriptionDto(this ProductExtraDescription? description)
    {
        if (description == null)
            return null;

        return new ProductExtraDescriptionDto
        {
            Id = description.Id,
            CreationDate = description.CreationDate,
            ProductId = description.ProductId,
            Title = description.Title,
            Description = description.Description
        };
    }

    public static List<ProductExtraDescriptionDto> MapToExtraDescriptionDto(this List<ProductExtraDescription> descriptions)
    {
        var dtoDescriptions = new List<ProductExtraDescriptionDto>();
        
        descriptions.ForEach(description =>
        {
            dtoDescriptions.Add(new ProductExtraDescriptionDto
            {
                Id = description.Id,
                CreationDate = description.CreationDate,
                ProductId = description.ProductId,
                Title = description.Title,
                Description = description.Description
            });
        });

        return dtoDescriptions;
    }
}