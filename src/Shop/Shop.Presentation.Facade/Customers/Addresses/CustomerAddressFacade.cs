using Common.Application;
using MediatR;
using Shop.Application.Customers.ActivateAddress;
using Shop.Application.Customers.AddAddress;
using Shop.Application.Customers.EditAddress;
using Shop.Application.Customers.RemoveAddress;

namespace Shop.Presentation.Facade.Customers.Addresses;

internal class CustomerAddressFacade : ICustomerAddressFacade
{
    private readonly IMediator _mediator;

    public CustomerAddressFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> ActivateAddress(ActivateCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddAddress(AddCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditAddress(EditCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveAddress(RemoveCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }
}