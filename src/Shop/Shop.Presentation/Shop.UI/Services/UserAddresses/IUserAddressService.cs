using Common.Api;
using Shop.Query.Users._DTOs;
using Shop.UI.Models.UserAddresses;

namespace Shop.UI.Services.UserAddresses;

public interface IUserAddressService
{
    Task<ApiResult?> Create(CreateUserAddressViewModel model);
    Task<ApiResult?> Edit(EditUserAddressViewModel model);
    Task<ApiResult?> Activate(long addressId);
    Task<ApiResult?> Remove(long addressId);

    Task<UserAddressDto?> GetById(long addressId);
    Task<List<UserAddressDto>?> GetAll(long userId);
}