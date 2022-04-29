using Shop.Domain.CustomerAggregate;
using Shop.Query.Customers._DTOs;

namespace Shop.Query.Customers._Mappers;

internal static class CustomerAddressMapper
{
    public static CustomerAddressDto MapToCustomerAddressDto(this CustomerAddress? address)
    {
        if (address == null)
            return null;

        return new CustomerAddressDto
        {
            Id = address.Id,
            CreationDate = address.CreationDate,
            CustomerId = address.CustomerId,
            IsActive = address.IsActive,
            FullName = address.FullName,
            PhoneNumber = address.PhoneNumber,
            Province = address.Province,
            City = address.City,
            FullAddress = address.FullAddress,
            PostalCode = address.PostalCode
        };
    }

    public static List<CustomerAddressDto> MapToCustomerAddressDto(this List<CustomerAddress> addresses)
    {
        var dtoAddresses = new List<CustomerAddressDto>();

        addresses.ForEach(address =>
        {
            dtoAddresses.Add(new CustomerAddressDto
            {
                Id = address.Id,
                CreationDate = address.CreationDate,
                CustomerId = address.CustomerId,
                IsActive = address.IsActive,
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