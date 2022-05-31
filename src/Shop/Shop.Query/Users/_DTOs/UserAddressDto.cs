using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserAddressDto : BaseDto
{
    public long UserId { get; set; }
    public bool IsActive { get; set; }
    public string FullName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string FullAddress { get; set; }
    public string PostalCode { get; set; }
}