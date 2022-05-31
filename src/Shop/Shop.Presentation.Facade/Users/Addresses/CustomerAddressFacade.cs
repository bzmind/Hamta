using Common.Application;
using MediatR;
using Shop.Application.Users.ActivateAddress;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.RemoveAddress;

namespace Shop.Presentation.Facade.Users.Addresses;

internal class UserAddressFacade : IUserAddressFacade
{
    private readonly IMediator _mediator;

    public UserAddressFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Activate(ActivateUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Create(AddUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(RemoveUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }
}