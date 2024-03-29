﻿using Common.Application;
using MediatR;
using Shop.Application.Users.Addresses.ActivateAddress;
using Shop.Application.Users.Addresses.CreateAddress;
using Shop.Application.Users.Addresses.EditAddress;
using Shop.Application.Users.Addresses.RemoveAddress;
using Shop.Query.Users._DTOs;
using Shop.Query.Users.Addresses;

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

    public async Task<OperationResult> Create(CreateUserAddressCommand command)
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

    public async Task<UserAddressDto?> GetById(long addressId)
    {
        return await _mediator.Send(new GetUserAddressByIdQuery(addressId));
    }

    public async Task<List<UserAddressDto>> GetAll(long userId)
    {
        return await _mediator.Send(new GetUserAddressesListQuery(userId));
    }
}