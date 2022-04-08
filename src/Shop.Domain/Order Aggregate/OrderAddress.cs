using Common.Domain.Base_Classes;
using Common.Domain.Value_Objects;

namespace Shop.Domain.Order_Aggregate;

public class OrderAddress : Address
{
    public long OrderId { get; private set; }

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