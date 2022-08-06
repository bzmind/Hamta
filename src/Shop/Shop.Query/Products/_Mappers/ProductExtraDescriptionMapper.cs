using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductExtraDescriptionMapper
{
    public static QueryProductExtraDescriptionDto MapToQueryExtraDescriptionDto
        (this ProductExtraDescription? description)
    {
        if (description == null)
            return null;

        return new QueryProductExtraDescriptionDto
        {
            Id = description.Id,
            CreationDate = description.CreationDate,
            ProductId = description.ProductId,
            Title = description.Title,
            Description = description.Description
        };
    }

    public static List<QueryProductExtraDescriptionDto> MapToQueryExtraDescriptionDto
        (this List<ProductExtraDescription> descriptions)
    {
        var dtoDescriptions = new List<QueryProductExtraDescriptionDto>();
        
        descriptions.ForEach(description =>
        {
            dtoDescriptions.Add(new QueryProductExtraDescriptionDto
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