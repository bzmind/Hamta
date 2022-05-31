namespace Shop.Domain.UserAggregate.Services;

public interface IUserDomainService
{
    bool IsPhoneNumberDuplicate(string phoneNumber);
    bool IsEmailDuplicate(string phoneNumber);
}