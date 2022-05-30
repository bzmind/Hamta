using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Customers.ActivateAddress;
using Shop.Application.Customers.AddAddress;
using Shop.Application.Customers.EditAddress;
using Shop.Application.Customers.RemoveAddress;
using Shop.Presentation.Facade.Customers.Addresses;

namespace Shop.API.Controllers;

public class CustomerAddressController : BaseApiController
{
    private readonly ICustomerAddressFacade _customerAddressFacade;

    public CustomerAddressController(ICustomerAddressFacade customerAddressFacade)
    {
        _customerAddressFacade = customerAddressFacade;
    }

    [HttpPost]
    public async Task<ApiResult> Create(AddCustomerAddressCommand command)
    {
        var result = await _customerAddressFacade.Create(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditCustomerAddressCommand command)
    {
        var result = await _customerAddressFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("Activate")]
    public async Task<ApiResult> Activate(ActivateCustomerAddressCommand command)
    {
        var result = await _customerAddressFacade.Activate(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> Remove(RemoveCustomerAddressCommand command)
    {
        var result = await _customerAddressFacade.Remove(command);
        return CommandResult(result);
    }
}