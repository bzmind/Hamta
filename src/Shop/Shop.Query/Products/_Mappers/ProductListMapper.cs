using Shop.Domain.ColorAggregate;
using Shop.Domain.InventoryAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductListMapper
{
    public static ProductListDto MapToProductListDto(this Product? product, Inventory inventory)
    {
        if (product == null)
            return null;

        return new ProductListDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            ColorId = inventory.ColorId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Price = inventory.Price.Value,
            AverageScore = product.AverageScore,
            Quantity = inventory.Quantity,
            Colors = new List<Color>()
        };
    }

    public static ProductListDto SetProductListDtoColors(this ProductListDto? product, Color color)
    {
        if (product == null)
            return null;

        return new ProductListDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            ColorId = product.ColorId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Price = product.Price,
            AverageScore = product.AverageScore,
            Quantity = product.Quantity,
            Colors = new List<Color>
            {
                color
            }
        };
    }
}