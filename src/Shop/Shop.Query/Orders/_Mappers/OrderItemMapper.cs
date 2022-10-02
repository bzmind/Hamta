using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders._Mappers;

public static class OrderItemMapper
{
    public static OrderItemDto MapToOrderItemDto(this OrderItem orderItem)
    {
        if (orderItem == null)
            return null;

        return new OrderItemDto
        {
            Id = orderItem.Id,
            CreationDate = orderItem.CreationDate,
            OrderId = orderItem.OrderId,
            InventoryId = orderItem.InventoryId,
            Count = orderItem.Count,
            Price = orderItem.Price.Value
        };
    }

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
                Count = oi.Count,
                Price = oi.Price.Value
            });
        });

        return dtoItems;
    }
}