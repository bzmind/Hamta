using Shop.Domain.ColorAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductListMapper
{
    public static ProductFilterDto MapToProductFilterDto(this Product? product, SellerInventory inventory,
        Color color)
    {
        if (product == null)
            return null;
        
        return new ProductFilterDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            InventoryId = inventory.Id,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            MainImage = product.MainImage,
            LowestInventoryPrice = inventory.Price.Value,
            AverageScore = product.AverageScore,
            AllQuantityInStock = inventory.Quantity,
            Colors = new List<Color>
            {
                color
            }
        };
    }
}