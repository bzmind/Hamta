using Common.Application;
using Shop.Application.Users.ActivateAddress;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.EditAddress;
using Shop.Application.Users.RemoveAddress;

namespace Shop.Presentation.Facade.Users.Addresses;

public interface IUserAddressFacade
{
    Task<OperationResult> Create(AddUserAddressCommand command);
    Task<OperationResult> Edit(EditUserAddressCommand command);
    Task<OperationResult> Activate(ActivateUserAddressCommand command);
    Task<OperationResult> Remove(RemoveUserAddressCommand command);
}