using System.Text.Json;
using Common.Api;
using Shop.Query.Users._DTOs;
using Shop.UI.Models.UserAddresses;

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

    public async Task<ApiResult?> Create(CreateUserAddressViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/useraddress/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Edit(EditUserAddressViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/useraddress/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Activate(long addressId)
    {
        var result = await _client.PutAsync($"api/useraddress/activate/{addressId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Remove(long addressId)
    {
        var result = await _client.DeleteAsync($"api/useraddress/remove/{addressId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<UserAddressDto?> GetById(long addressId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<UserAddressDto>>($"api/useraddress/getbyid/{addressId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<UserAddressDto>?> GetAll(long userId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<UserAddressDto>>>($"api/useraddress/getall/{userId}", _jsonOptions);
        return result?.Data;
    }
}