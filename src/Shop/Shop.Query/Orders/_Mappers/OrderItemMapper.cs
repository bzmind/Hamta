using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders._Mappers;

internal static class OrderItemMapper
{
    public static List<OrderItemDto> MapToOrderItemDto(this List<OrderItem> orderItems)
    {
        var dtoItems = new List<OrderItemDto>();

        orderItems.ForEach(oi =>
        {
            dtoItems.Add(new OrderItemDto
            {
                Id = oi.Id,
                CreationDate = oi.CreationDate,
                OrderId = oi.OrderId,
                InventoryId = oi.InventoryId,
                ProductName = null,
                Count = oi.Count,
                Price = oi.Price.Value,
                InventoryQuantity = null,
                ColorName = null,
                ColorCode = null
            });
        });

        return dtoItems;
    }
}