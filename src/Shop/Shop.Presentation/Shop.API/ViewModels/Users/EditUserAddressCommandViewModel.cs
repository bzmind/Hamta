namespace Shop.API.ViewModels.Users;

public record EditUserAddressCommandViewModel(long AddressId, string FullName, string PhoneNumber,
    string Province, string City, string FullAddress, string PostalCode);