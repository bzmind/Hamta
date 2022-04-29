using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Orders._DTOs;

public class OrderAddressDto : BaseDto
{
    public long OrderId { get; set; }
    public string FullName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string FullAddress { get; set; }
    public string PostalCode { get; set; }
}