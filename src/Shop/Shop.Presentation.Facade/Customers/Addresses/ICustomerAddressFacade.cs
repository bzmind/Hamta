using Common.Application;
using Shop.Application.Customers.ActivateAddress;
using Shop.Application.Customers.AddAddress;
using Shop.Application.Customers.EditAddress;
using Shop.Application.Customers.RemoveAddress;

namespace Shop.Presentation.Facade.Customers.Addresses;

public interface ICustomerAddressFacade
{
    Task<OperationResult> Create(AddCustomerAddressCommand command);
    Task<OperationResult> Edit(EditCustomerAddressCommand command);
    Task<OperationResult> Activate(ActivateCustomerAddressCommand command);
    Task<OperationResult> Remove(RemoveCustomerAddressCommand command);
}