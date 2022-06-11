namespace Shop.API.ViewModels.Users;

public class CreateUserAddressCommandViewModel
{
    public string FullName { get; init; }
    public string PhoneNumber { get; init; }
    public string Province { get; init; }
    public string City { get; init; }
    public string FullAddress { get; init; }
    public string PostalCode { get; init; }
}