using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Customers._DTOs;

public class CustomerDto : BaseDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<CustomerAddressDto> Addresses { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string AvatarName { get; set; }
    public bool IsSubscribedToNews { get; set; }
    public List<CustomerFavoriteItemDto> FavoriteItems { get; set; }
}