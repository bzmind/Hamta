using Shop.Domain.InventoryAggregate;
using Shop.Query.Inventories._DTOs;

namespace Shop.Query.Inventories._Mappers;

internal static class InventoryMapper
{
    public static InventoryDto MapToInventoryDto(this Inventory? inventory)
    {
        if (inventory == null)
            return null;

        return new InventoryDto
        {
            Id = inventory.Id,
            CreationDate = inventory.CreationDate,
            ProductId = inventory.ProductId,
            ColorId = inventory.ColorId,
            Quantity = inventory.Quantity,
            Price = inventory.Price,
            IsAvailable = inventory.IsAvailable,
            DiscountPercentage = inventory.DiscountPercentage,
            IsDiscounted = inventory.IsDiscounted
        };
    }

    public static List<InventoryDto> MapToInventoryDto(this List<Inventory> inventories)
    {
        var dtoInventories = new List<InventoryDto>();

        inventories.ForEach(inventory =>
        {
            dtoInventories.Add(new InventoryDto
            {
                Id = inventory.Id,
                CreationDate = inventory.CreationDate,
                ProductId = inventory.ProductId,
                ColorId = inventory.ColorId,
                Quantity = inventory.Quantity,
                Price = inventory.Price,
                IsAvailable = inventory.IsAvailable,
                DiscountPercentage = inventory.DiscountPercentage,
                IsDiscounted = inventory.IsDiscounted
            });
        });

        return dtoInventories;
    }
}