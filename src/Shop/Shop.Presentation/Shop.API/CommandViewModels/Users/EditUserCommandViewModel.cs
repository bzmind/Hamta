using Shop.Domain.UserAggregate;

namespace Shop.API.CommandViewModels.Users;

public class EditUserCommandViewModel
{
    public string FullName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public User.UserGender Gender { get; init; }
    public long AvatarId { get; set; }
}