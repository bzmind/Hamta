using Common.Domain.BaseClasses;
using Common.Domain.Value_Objects;

namespace Domain.Order_Aggregate;

public class OrderAddress : Address
{
    public long OrderId { get; private set; }

    public OrderAddress(long orderId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        Validate(fullName, province, city, fullAddress, postalCode);
        OrderId = orderId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
    }
}