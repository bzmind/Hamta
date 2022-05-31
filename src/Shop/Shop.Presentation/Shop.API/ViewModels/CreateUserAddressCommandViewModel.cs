namespace Shop.API.ViewModels;

public record CreateUserAddressCommandViewModel(string FullName, string PhoneNumber, string Province,
    string City, string FullAddress, string PostalCode);