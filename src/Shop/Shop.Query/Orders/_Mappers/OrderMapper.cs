using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders._Mappers;

internal static class OrderMapper
{
    public static OrderDto MapToOrderDto(this Order? order)
    {
        if (order == null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            CustomerId = order.CustomerId,
            Status = order.Status,
            Address = order.Address.MapToOrderAddressDto(),
            ShippingMethod = order.ShippingInfo.ShippingMethod,
            ShippingCost = order.ShippingInfo.ShippingCost.Value,
            Items = new List<OrderItemDto>()
        };
    }

    public static List<OrderDto> MapToOrderDto(this List<Order> orders)
    {
        var dtoOrders = new List<OrderDto>();

        orders.ForEach(order =>
        {
            dtoOrders.Add(new OrderDto
            {
                Id = order.Id,
                CreationDate = order.CreationDate,
                CustomerId = order.CustomerId,
                Status = order.Status,
                Address = order.Address.MapToOrderAddressDto(),
                ShippingMethod = order.ShippingInfo.ShippingMethod,
                ShippingCost = order.ShippingInfo.ShippingCost.Value,
                Items = new List<OrderItemDto>()
            });
        });

        return dtoOrders;
    }
}