using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Customers.ActivateAddress;
using Shop.Application.Customers.AddAddress;
using Shop.Application.Customers.EditAddress;
using Shop.Application.Customers.RemoveAddress;
using Shop.Presentation.Facade.Customers.Addresses;

namespace Shop.API.Controllers
{
    public class CustomerAddressController : BaseApiController
    {
        private readonly ICustomerAddressFacade _customerAddressFacade;

        public CustomerAddressController(ICustomerAddressFacade customerAddressFacade)
        {
            _customerAddressFacade = customerAddressFacade;
        }

        [HttpPost]
        public async Task<ApiResult> AddAddress(AddCustomerAddressCommand command)
        {
            var result = await _customerAddressFacade.AddAddress(command);
            return CommandResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> EditAddress(EditCustomerAddressCommand command)
        {
            var result = await _customerAddressFacade.EditAddress(command);
            return CommandResult(result);
        }

        [HttpPut("ActivateAddress")]
        public async Task<ApiResult> ActivateAddress(ActivateCustomerAddressCommand command)
        {
            var result = await _customerAddressFacade.ActivateAddress(command);
            return CommandResult(result);
        }

        [HttpDelete]
        public async Task<ApiResult> RemoveAddress(RemoveCustomerAddressCommand command)
        {
            var result = await _customerAddressFacade.RemoveAddress(command);
            return CommandResult(result);
        }
    }
}
