using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Customers._DTOs;

public class CustomerAddressDto : BaseDto
{
    public long CustomerId { get; set; }
    public bool IsActive { get; set; }
    public string FullName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string FullAddress { get; set; }
    public string PostalCode { get; set; }
}