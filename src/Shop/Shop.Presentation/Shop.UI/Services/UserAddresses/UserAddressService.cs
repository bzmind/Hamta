using Common.Api;
using Shop.Query.Users._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Users.Addresses;

namespace Shop.UI.Services.UserAddresses;

public class UserAddressService : BaseService, IUserAddressService
{
    protected override string ApiEndpointName { get; set; } = "UserAddress";

    public UserAddressService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateUserAddressViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> Edit(EditUserAddressViewModel model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ApiResult> Activate(long addressId)
    {
        return await PutAsync($"Activate/{addressId}");
    }

    public async Task<ApiResult> Remove(long addressId)
    {
        return await DeleteAsync($"Remove/{addressId}");
    }

    public async Task<UserAddressDto> GetById(long addressId)
    {
        var result = await GetFromJsonAsync<UserAddressDto>($"GetById/{addressId}");
        return result.Data;
    }

    public async Task<List<UserAddressDto>> GetAll(long userId)
    {
        var result = await GetFromJsonAsync<List<UserAddressDto>>($"GetAll/{userId}");
        return result.Data;
    }
}