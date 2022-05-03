namespace Shop.Domain.CustomerAggregate.Services;

public interface ICustomerDomainService
{
    bool IsPhoneNumberDuplicate(string phoneNumber);
    bool IsEmailDuplicate(string phoneNumber);
}