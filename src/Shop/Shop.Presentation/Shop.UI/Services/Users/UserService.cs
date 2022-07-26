using Common.Api;
using Shop.Query.Users._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Users;
using Shop.API.ViewModels.Users.Auth;
using Shop.API.ViewModels.Users.Roles;

namespace Shop.UI.Services.Users;

public class UserService : BaseService, IUserService
{
    protected override string ApiEndpointName { get; set; } = "User";

    public UserService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions)
    {
    }

    public async Task<ApiResult> Create(CreateUserViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> Edit(EditUserViewModel model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ApiResult> ResetPassword(ResetUserPasswordViewModel model)
    {
        return await PutAsJsonAsync("ResetPassword", model);
    }

    public async Task<ApiResult<bool>> SetNewsletterSubscription(long userId)
    {
        return await PutAsync<bool>($"SetNewsletterSubscription/{userId}");
    }

    public async Task<ApiResult> SetAvatar(long avatarId)
    {
        return await PutAsync($"SetAvatar/{avatarId}");
    }

    public async Task<ApiResult> AddFavoriteItem(long productId)
    {
        return await PutAsync($"AddFavoriteItem/{productId}");
    }

    public async Task<ApiResult> AddRole(AddUserRoleViewModel model)
    {
        return await PutAsJsonAsync("AddRole", model);
    }

    public async Task<ApiResult> RemoveFavoriteItem(long favoriteItemId)
    {
        return await DeleteAsync($"RemoveFavoriteItem/{favoriteItemId}");
    }

    public async Task<ApiResult> RemoveRole(long roleId)
    {
        return await DeleteAsync($"RemoveRole/{roleId}");
    }

    public async Task<ApiResult> Remove(long userId)
    {
        return await DeleteAsync($"Remove/{userId}");
    }

    public async Task<UserDto?> GetById(long userId)
    {
        var result = await GetFromJsonAsync<UserDto>($"GetById/{userId}");
        return result.Data;
    }

    public async Task<UserDto?> GetByEmailOrPhone(string emailOrPhone)
    {
        var result = await GetFromJsonAsync<UserDto>($"GetByEmailOrPhone/{emailOrPhone}");
        return result.Data;
    }

    public async Task<ApiResult<LoginNextStep>> SearchByEmailOrPhone(string emailOrPhone)
    {
        return await GetFromJsonAsync<LoginNextStep>($"SearchByEmailOrPhone/{emailOrPhone}");
    }

    public async Task<UserFilterResult> GetByFilter(UserFilterParams filterParams)
    {
        var url = $"GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&Name={filterParams.Name}&PhoneNumber={filterParams.PhoneNumber}&Email={filterParams.Email}";
        var result = await GetFromJsonAsync<UserFilterResult>(url);
        return result.Data;
    }
}