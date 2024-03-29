﻿using AutoMapper;
using Common.Api;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Users.Addresses;
using Shop.Application.Users.Addresses.ActivateAddress;
using Shop.Application.Users.Addresses.CreateAddress;
using Shop.Application.Users.Addresses.EditAddress;
using Shop.Application.Users.Addresses.RemoveAddress;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Query.Users._DTOs;

namespace Shop.API.Controllers;

[Authorize]
public class UserAddressController : BaseApiController
{
    private readonly IUserAddressFacade _userAddressFacade;
    private readonly IMapper _mapper;

    public UserAddressController(IUserAddressFacade userAddressFacade, IMapper mapper)
    {
        _userAddressFacade = userAddressFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult> Create(CreateUserAddressViewModel model)
    {
        var command = _mapper.Map<CreateUserAddressCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _userAddressFacade.Create(command);
        return CommandResult(result);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditUserAddressViewModel model)
    {
        var command = _mapper.Map<EditUserAddressCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _userAddressFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("Activate/{addressId}")]
    public async Task<ApiResult> Activate(long addressId)
    {
        var command = new ActivateUserAddressCommand(User.GetUserId(), addressId);
        var result = await _userAddressFacade.Activate(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{addressId}")]
    public async Task<ApiResult> Remove(long addressId)
    {
        var command = new RemoveUserAddressCommand(User.GetUserId(), addressId);
        var result = await _userAddressFacade.Remove(command);
        return CommandResult(result);
    }

    [HttpGet("GetById/{addressId}")]
    public async Task<ApiResult<UserAddressDto?>> GetById(long addressId)
    {
        var result = await _userAddressFacade.GetById(addressId);
        return QueryResult(result);
    }

    [HttpGet("GetAll/{userId}")]
    public async Task<ApiResult<List<UserAddressDto>>> GetAll(long userId)
    {
        var result = await _userAddressFacade.GetAll(userId);
        return QueryResult(result);
    }
}