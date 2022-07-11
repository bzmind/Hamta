using Shop.Domain.UserAggregate;

namespace Shop.API.ViewModels.Users;

public class CreateUserViewModel
{
    public string FullName { get; set; }
    public User.UserGender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}