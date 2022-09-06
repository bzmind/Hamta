using Shop.Domain.ColorAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Query.Sellers._DTOs;

namespace Shop.Query.Sellers._Mappers;

internal static class SellerInventoryMapper
{
    public static SellerInventoryDto MapToSellerInventoryDto(this SellerInventory? inventory, Color color,
        Product product)
    {
        if (inventory == null)
            return null;

        return new SellerInventoryDto
        {
            Id = inventory.Id,
            CreationDate = inventory.CreationDate,
            ProductId = inventory.ProductId,
            ColorId = color.Id,
            ProductName = product.Name,
            ProductEnglishName = product.EnglishName,
            ProductMainImage = product.MainImage,
            ColorName = color.Name,
            ColorCode = color.Code,
            Quantity = inventory.Quantity,
            Price = inventory.Price.Value,
            IsAvailable = inventory.IsAvailable,
            DiscountPercentage = inventory.DiscountPercentage,
            IsDiscounted = inventory.IsDiscounted
        };
    }
}