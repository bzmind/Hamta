using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.ActivateAddress;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.RemoveAddress;
using Shop.Presentation.Facade.Users.Addresses;

namespace Shop.API.Controllers;

public class UserAddressController : BaseApiController
{
    private readonly IUserAddressFacade _userAddressFacade;

    public UserAddressController(IUserAddressFacade userAddressFacade)
    {
        _userAddressFacade = userAddressFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult> Create(AddUserAddressCommand command)
    {
        var result = await _userAddressFacade.Create(command);
        return CommandResult(result);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditUserAddressCommand command)
    {
        var result = await _userAddressFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("Activate")]
    public async Task<ApiResult> Activate(ActivateUserAddressCommand command)
    {
        var result = await _userAddressFacade.Activate(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove")]
    public async Task<ApiResult> Remove(RemoveUserAddressCommand command)
    {
        var result = await _userAddressFacade.Remove(command);
        return CommandResult(result);
    }
}