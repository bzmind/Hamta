using System.ComponentModel.DataAnnotations;
using Shop.Domain.UserAggregate;

namespace Shop.API.ViewModels.Users;

public class EditUserViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public User.UserGender Gender { get; set; }
}