using Shop.Domain.SellerAggregate;
using Shop.Query.Sellers._DTOs;

namespace Shop.Query.Sellers._Mappers;

internal static class SellerInventoryMapper
{
    public static SellerInventoryDto MapToSellerInventoryDto(this SellerInventory? inventory)
    {
        if (inventory == null)
            return null;

        return new SellerInventoryDto
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

    public static List<SellerInventoryDto> MapToSellerInventoryDto(this List<SellerInventory> inventories)
    {
        var dtoInventories = new List<SellerInventoryDto>();

        inventories.ForEach(inventory =>
        {
            dtoInventories.Add(new SellerInventoryDto
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