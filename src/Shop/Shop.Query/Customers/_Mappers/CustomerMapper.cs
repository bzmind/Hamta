using Shop.Domain.CustomerAggregate;
using Shop.Query.Customers._DTOs;

namespace Shop.Query.Customers._Mappers;

internal static class CustomerMapper
{
    public static CustomerDto MapToCustomerDto(this Customer? customer)
    {
        if (customer == null)
            return null;

        var customerDto = new CustomerDto
        {
            Id = customer.Id,
            CreationDate = customer.CreationDate,
            FullName = customer.FullName,
            Email = customer.Email,
            Password = customer.Password,
            Addresses = customer.Addresses.ToList().MapToCustomerAddressDto(),
            PhoneNumber = customer.PhoneNumber,
            AvatarName = customer.AvatarName,
            IsSubscribedToNews = customer.IsSubscribedToNews,
            FavoriteItems = new List<CustomerFavoriteItemDto>()
        };

        customer.FavoriteItems.ToList().ForEach(fi =>
        {
            customerDto.FavoriteItems.Add(new CustomerFavoriteItemDto()
            {
                Id = fi.Id,
                CreationDate = fi.CreationDate,
                CustomerId = fi.CustomerId,
                ProductId = fi.ProductId,
                ProductName = null,
                ProductMainImage = null,
                ProductPrice = null,
                AverageScore = null,
                IsAvailable = null
            });
        });

        return customerDto;
    }
}