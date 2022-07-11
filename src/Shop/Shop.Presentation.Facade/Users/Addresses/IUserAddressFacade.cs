using Common.Application;
using Shop.Application.Users.Addresses.ActivateAddress;
using Shop.Application.Users.Addresses.CreateAddress;
using Shop.Application.Users.Addresses.EditAddress;
using Shop.Application.Users.Addresses.RemoveAddress;
using Shop.Query.Users._DTOs;

namespace Shop.Presentation.Facade.Users.Addresses;

public interface IUserAddressFacade
{
    Task<OperationResult> Create(CreateUserAddressCommand command);
    Task<OperationResult> Edit(EditUserAddressCommand command);
    Task<OperationResult> Activate(ActivateUserAddressCommand command);
    Task<OperationResult> Remove(RemoveUserAddressCommand command);

    Task<UserAddressDto?> GetById(long addressId);
    Task<List<UserAddressDto>> GetAll(long userId);
}