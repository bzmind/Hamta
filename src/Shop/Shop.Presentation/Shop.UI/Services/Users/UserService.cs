﻿using Common.Api;
using Shop.API.ViewModels.Users;
using Shop.Query.Users._DTOs;
using Shop.UI.Models.Users;
using System.Text.Json;

namespace Shop.UI.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public UserService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateUserViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/user/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Edit(EditUserViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/user/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> SetAvatar(SetUserAvatarViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/user/setavatar", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> ResetPassword(ResetUserPasswordViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/user/resetpassword", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> SetSubscriptionToNews(bool subscription)
    {
        var result = await _client.PutAsync($"api/user/setsubscriptiontonews/{subscription}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> AddFavoriteItem(long productId)
    {
        var result = await _client.PutAsync($"api/user/addfavoriteitem/{productId}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> AddRole(AddUserRoleViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/user/addrole", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> RemoveFavoriteItem(long favoriteItemId)
    {
        var result = await _client.DeleteAsync($"api/user/removefavoriteitem/{favoriteItemId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> RemoveRole(long roleId)
    {
        var result = await _client.DeleteAsync($"api/user/removerole/{roleId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Remove(long userId)
    {
        var result = await _client.DeleteAsync($"api/user/remove/{userId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<UserDto?> GetById(long userId)
    {
        var result = await _client.GetFromJsonAsync<ApiResult<UserDto>>($"api/user/getbyid/{userId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<UserDto?> GetByEmailOrPhone(string emailOrPhone)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<UserDto>>($"api/user/getbyemailorphone/{emailOrPhone}", _jsonOptions);
        return result?.Data;
    }

    public async Task<ApiResult<LoginNextStep>> SearchByEmailOrPhone(string emailOrPhone)
    {
        var result = await _client.GetAsync($"api/user/searchbyemailorphone/{emailOrPhone}");
        var jsonResult = await result.Content.ReadFromJsonAsync<ApiResult<LoginNextStep>>(_jsonOptions);
        return jsonResult;
    }

    public async Task<UserFilterResult?> GetByFilter(UserFilterParams filterParams)
    {
        var url = $"api/user/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&Name={filterParams.Name}&PhoneNumber={filterParams.PhoneNumber}&Email={filterParams.Email}";

        var result = await _client.GetFromJsonAsync<ApiResult<UserFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}