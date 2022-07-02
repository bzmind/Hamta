using Common.Api;
using Shop.Query.Users._DTOs;
using System.Text.Json;
using Shop.API.CommandViewModels.Users.Addresses;

namespace Shop.UI.Services.UserAddresses;

public class UserAddressService : IUserAddressService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public UserAddressService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateUserAddressCommandViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/UserAddress/Create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditUserAddressCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/UserAddress/Edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Activate(long addressId)
    {
        var result = await _client.PutAsync($"api/UserAddress/Activate/{addressId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long addressId)
    {
        var result = await _client.DeleteAsync($"api/UserAddress/Remove/{addressId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<UserAddressDto?> GetById(long addressId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<UserAddressDto>>($"api/UserAddress/GetById/{addressId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<UserAddressDto>?> GetAll(long userId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<UserAddressDto>>>($"api/UserAddress/GetAll/{userId}", _jsonOptions);
        return result?.Data;
    }
}