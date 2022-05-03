using Common.Application;
using Shop.Application.Customers.ActivateAddress;
using Shop.Application.Customers.AddAddress;
using Shop.Application.Customers.EditAddress;
using Shop.Application.Customers.RemoveAddress;

namespace Shop.Presentation.Facade.Customers.Addresses;

public interface IAddressFacade
{
    Task<OperationResult> ActivateAddress(ActivateCustomerAddressCommand command);
    Task<OperationResult> AddAddress(AddCustomerAddressCommand command);
    Task<OperationResult> EditAddress(EditCustomerAddressCommand command);
    Task<OperationResult> RemoveAddress(RemoveCustomerAddressCommand command);
}