using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductImageMapper
{
    public static ProductGalleryImageDto MapToProductImageDto(this ProductGalleryImage? productImage)
    {
        if (productImage == null)
            return null;

        return new ProductGalleryImageDto
        {
            Id = productImage.Id,
            CreationDate = productImage.CreationDate,
            ProductId = productImage.ProductId,
            Name = productImage.Name,
            Sequence = productImage.Sequence
        };
    }

    public static List<ProductGalleryImageDto> MapToProductImageDto(this List<ProductGalleryImage> productImages)
    {
        var dtoProducts = new List<ProductGalleryImageDto>();

        productImages.ForEach(productImage =>
        {
            dtoProducts.Add(new ProductGalleryImageDto
            {
                Id = productImage.Id,
                CreationDate = productImage.CreationDate,
                ProductId = productImage.ProductId,
                Name = productImage.Name,
                Sequence = productImage.Sequence
            });
        });

        return dtoProducts;
    }
}