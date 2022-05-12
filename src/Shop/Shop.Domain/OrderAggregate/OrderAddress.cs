using Common.Domain.BaseClasses;
using Common.Domain.ValueObjects;

namespace Shop.Domain.OrderAggregate;

public class OrderAddress : BaseAddress
{
    public long OrderId { get; private set; }

    private OrderAddress()
    {

    }

    public OrderAddress(long orderId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        Guard(fullName, province, city, fullAddress, postalCode);
        OrderId = orderId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
    }
}