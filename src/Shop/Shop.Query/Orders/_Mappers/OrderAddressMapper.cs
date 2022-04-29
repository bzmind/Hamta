using Shop.Domain.OrderAggregate;
using Shop.Query.Orders._DTOs;

namespace Shop.Query.Orders._Mappers;

internal static class OrderAddressMapper
{
    public static OrderAddressDto MapToOrderAddressDto(this OrderAddress? orderAddress)
    {
        if (orderAddress == null)
            return null;

        return new OrderAddressDto
        {
            Id = orderAddress.Id,
            CreationDate = orderAddress.CreationDate,
            OrderId = orderAddress.OrderId,
            FullName = orderAddress.FullName,
            PhoneNumber = orderAddress.PhoneNumber,
            Province = orderAddress.Province,
            City = orderAddress.City,
            FullAddress = orderAddress.FullAddress,
            PostalCode = orderAddress.PostalCode
        };
    }

    public static List<OrderAddressDto> MapToOrderAddressDto(this List<OrderAddress> orderAddresses)
    {
        var dtoAddresses = new List<OrderAddressDto>();

        orderAddresses.ForEach(address =>
        {
            dtoAddresses.Add(new OrderAddressDto
            {
                Id = address.Id,
                CreationDate = address.CreationDate,
                OrderId = address.OrderId,
                FullName = address.FullName,
                PhoneNumber = address.PhoneNumber,
                Province = address.Province,
                City = address.City,
                FullAddress = address.FullAddress,
                PostalCode = address.PostalCode
            });
        });

        return dtoAddresses;
    }
}