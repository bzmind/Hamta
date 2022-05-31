using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application.Users._Services;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _userRepository;

    public UserDomainService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsPhoneNumberDuplicate(string phoneNumber)
    {
        return _userRepository.Exists(c => c.PhoneNumber.Value == phoneNumber);
    }

    public bool IsEmailDuplicate(string email)
    {
        return _userRepository.Exists(c => c.Email == email);
    }
}