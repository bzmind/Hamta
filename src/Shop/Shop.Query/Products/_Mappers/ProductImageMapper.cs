using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductImageMapper
{
    public static ProductImageDto MapToProductImageDto(this ProductImage? productImage)
    {
        if (productImage == null)
            return null;

        return new ProductImageDto
        {
            Id = productImage.Id,
            CreationDate = productImage.CreationDate,
            ProductId = productImage.ProductId,
            Name = productImage.Name
        };
    }

    public static List<ProductImageDto> MapToProductImageDto(this List<ProductImage> productImages)
    {
        var dtoProducts = new List<ProductImageDto>();

        productImages.ForEach(productImage =>
        {
            dtoProducts.Add(new ProductImageDto
            {
                Id = productImage.Id,
                CreationDate = productImage.CreationDate,
                Name = productImage.Name
            });
        });

        return dtoProducts;
    }
}