using Shop.Domain.ColorAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class SingleProductMapper
{
    public static SingleProductDto? MapToSingleProductDto(this Product product, SellerInventory inventory, Color color,
        Seller seller)
    {
        if (product == null)
            return null;

        var productDto = new SingleProductDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Introduction = product.Introduction,
            Review = product.Review,
            MainImage = product.MainImage,
            GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
            CategorySpecifications = product.CategorySpecifications.ToList()
                .MapToProductCategorySpecificationQueryDto(),
            Specifications = product.Specifications.ToList().MapToQueryProductSpecificationDto(),
            Inventories = new List<ProductInventoryDto>()
        };

        if (inventory != null)
            productDto.Inventories.Add(new()
            {
                Id = inventory.Id,
                CreationDate = inventory.CreationDate,
                SellerId = seller.Id,
                ProductId = inventory.Id,
                Quantity = inventory.Quantity,
                Price = inventory.Price.Value,
                ShopName = seller.ShopName,
                ColorId = color.Id,
                ColorName = color.Name,
                ColorCode = color.Code,
                IsAvailable = inventory.IsAvailable,
                DiscountPercentage = inventory.DiscountPercentage,
                IsDiscounted = inventory.IsDiscounted
            });

        return productDto;
    }
}