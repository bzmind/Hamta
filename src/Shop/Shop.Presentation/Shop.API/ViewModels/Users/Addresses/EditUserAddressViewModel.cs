namespace Shop.API.ViewModels.Users.Addresses;

public class EditUserAddressViewModel
{
    public long AddressId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string FullAddress { get; set; }
    public string PostalCode { get; set; }
}