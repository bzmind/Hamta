using Shop.Domain.CustomerAggregate.Repository;
using Shop.Domain.CustomerAggregate.Services;

namespace Shop.Application.Customers._Services;

public class CustomerDomainService : ICustomerDomainService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerDomainService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public bool IsPhoneNumberDuplicate(string phoneNumber)
    {
        return _customerRepository.Exists(c => c.PhoneNumber.Value == phoneNumber);
    }

    public bool IsEmailDuplicate(string email)
    {
        return _customerRepository.Exists(c => c.Email == email);
    }
}