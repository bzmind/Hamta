namespace Shop.API.ViewModels.Users;

public record CreateUserAddressCommandViewModel(string FullName, string PhoneNumber, string Province,
    string City, string FullAddress, string PostalCode);