using Shop.Domain.UserAggregate;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users._Mappers;

internal static class UserAddressMapper
{
    public static UserAddressDto MapToUserAddressDto(this UserAddress? address)
    {
        if (address == null)
            return null;

        return new UserAddressDto
        {
            Id = address.Id,
            CreationDate = address.CreationDate,
            UserId = address.UserId,
            IsActive = address.IsActive,
            FullName = address.FullName,
            PhoneNumber = address.PhoneNumber,
            Province = address.Province,
            City = address.City,
            FullAddress = address.FullAddress,
            PostalCode = address.PostalCode
        };
    }

    public static List<UserAddressDto> MapToUserAddressDto(this List<UserAddress> addresses)
    {
        var dtoAddresses = new List<UserAddressDto>();

        addresses.ForEach(address =>
        {
            dtoAddresses.Add(new UserAddressDto
            {
                Id = address.Id,
                CreationDate = address.CreationDate,
                UserId = address.UserId,
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