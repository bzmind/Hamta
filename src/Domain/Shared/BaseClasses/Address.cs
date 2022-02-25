using Domain.Shared.Value_Objects;

namespace Domain.Shared.BaseClasses;

public abstract class Address : BaseEntity
{
    public string FullName { get; protected set; }
    public PhoneNumber PhoneNumber { get; protected set; }
    public string Province { get; protected set; }
    public string City { get; protected set; }
    public string FullAddress { get; protected set; }
    public string PostalCode { get; protected set; }
}