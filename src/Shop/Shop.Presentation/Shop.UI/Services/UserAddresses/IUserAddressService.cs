using Common.Api;
using Shop.API.CommandViewModels.Users.Addresses;
using Shop.Query.Users._DTOs;

namespace Shop.UI.Services.UserAddresses;

public interface IUserAddressService
{
    Task<ApiResult> Create(CreateUserAddressCommandViewModel model);
    Task<ApiResult> Edit(EditUserAddressCommandViewModel model);
    Task<ApiResult> Activate(long addressId);
    Task<ApiResult> Remove(long addressId);

    Task<UserAddressDto> GetById(long addressId);
    Task<List<UserAddressDto>> GetAll(long userId);
}