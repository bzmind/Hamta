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

    public async Task<OperationResult> Activate(ActivateCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(AddCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(RemoveCustomerAddressCommand command)
    {
        return await _mediator.Send(command);
    }
}