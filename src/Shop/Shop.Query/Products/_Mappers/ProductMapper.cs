using Shop.Domain.CategoryAggregate;
using Shop.Domain.ColorAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Query.Categories._Mappers;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductMapper
{
    public static ProductDto? MapToProductDto(this Product product, Category category,
        SellerInventory inventory, Color color)
    {
        if (product == null)
            return null;

        var productDto = new ProductDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Introduction = product.Introduction,
            Review = product.Review,
            AverageScore = product.AverageScore,
            MainImage = product.MainImage,
            GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
            Specifications = product.Specifications.ToList().MapToQueryProductSpecificationDto(),
            CategorySpecifications = product.CategorySpecifications
                .ToList().MapToQueryProductCategorySpecificationDto(),
            Inventories = new List<ProductInventoryDto>()
        };

        if (inventory != null)
            productDto.Inventories.Add(new()
            {
                Id = inventory.Id,
                CreationDate = inventory.CreationDate,
                ProductId = inventory.Id,
                Quantity = inventory.Quantity,
                Price = inventory.Price.Value,
                ColorName = color.Name,
                ColorCode = color.Code,
                IsAvailable = inventory.IsAvailable,
                DiscountPercentage = inventory.DiscountPercentage,
                IsDiscounted = inventory.IsDiscounted
            });

        return productDto;
    }
}