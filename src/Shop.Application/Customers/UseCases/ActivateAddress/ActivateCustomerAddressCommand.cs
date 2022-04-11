using Common.Application;
using Common.Application.Base_Classes;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.Use_Cases.ActivateAddress;

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

        customer.ActivateAddress(request.AddressId);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}