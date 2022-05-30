using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Validation;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.Remove;

public record RemoveCustomerCommand(long CustomerId) : IBaseCommand;

public class RemoveCustomerCommandHandler : IBaseCommandHandler<RemoveCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public RemoveCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("کاربر"));

        _customerRepository.Delete(customer);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}