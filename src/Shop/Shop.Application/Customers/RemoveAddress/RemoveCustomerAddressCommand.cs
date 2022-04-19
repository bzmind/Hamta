using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.RemoveAddress;

public record RemoveCustomerAddressCommand(long CustomerId, long AddressId) : IBaseCommand;

public class RemoveCustomerAddressCommandHandler : IBaseCommandHandler<RemoveCustomerAddressCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public RemoveCustomerAddressCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(RemoveCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.RemoveAddress(request.AddressId);
        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}