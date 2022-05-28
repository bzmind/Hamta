using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.ActivateAddress;

public record ActivateCustomerAddressCommand(long CustomerId, long AddressId) : IBaseCommand;

public class ActivateCustomerAddressCommandHandler : IBaseCommandHandler<ActivateCustomerAddressCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public ActivateCustomerAddressCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(ActivateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.Addresses.ToList().ForEach(address =>
        {
            customer.SetAddressActivation(address.Id, false);
        });

        customer.SetAddressActivation(request.AddressId, true);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}