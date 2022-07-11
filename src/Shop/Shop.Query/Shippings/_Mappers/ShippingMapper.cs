using Shop.Domain.ShippingAggregate;
using Shop.Query.Shippings._DTOs;

namespace Shop.Query.Shippings._Mappers;

internal static class ShippingMapper
{
    public static ShippingDto MapToShippingDto(this Shipping? shipping)
    {
        if (shipping == null)
            return null;

        return new ShippingDto
        {
            Id = shipping.Id,
            CreationDate = shipping.CreationDate,
            ShippingMethod = shipping.Name,
            ShippingCost = shipping.Cost.Value
        };
    }

    public static List<ShippingDto> MapToShippingDto(this List<Shipping> shippings)
    {
        var dtoShippings = new List<ShippingDto>();

        shippings.ForEach(shipping =>
        {
            dtoShippings.Add(new ShippingDto
            {
                Id = shipping.Id,
                CreationDate = shipping.CreationDate,
                ShippingMethod = shipping.Name,
                ShippingCost = shipping.Cost.Value
            });
        });

        return dtoShippings;
    }
}