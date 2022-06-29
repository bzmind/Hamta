using Common.Api;
using Shop.API.ViewModels.Users;
using Shop.Query.Users._DTOs;
using Shop.UI.Models.Users;

namespace Shop.UI.Services.Users;

public interface IUserService
{
    Task<ApiResult?> Create(CreateUserViewModel model);
    Task<ApiResult?> Edit(EditUserViewModel model);
    Task<ApiResult?> SetAvatar(SetUserAvatarViewModel model);
    Task<ApiResult?> ResetPassword(ResetUserPasswordViewModel model);
    Task<ApiResult?> SetSubscriptionToNews(bool subscription);
    Task<ApiResult?> AddFavoriteItem(long productId);
    Task<ApiResult?> AddRole(AddUserRoleViewModel model);
    Task<ApiResult?> RemoveFavoriteItem(long favoriteItemId);
    Task<ApiResult?> RemoveRole(long roleId);
    Task<ApiResult?> Remove(long userId);

    Task<UserDto?> GetById(long userId);
    Task<UserDto?> GetByEmailOrPhone(string emailOrPhone);
    Task<ApiResult<LoginNextStep>> SearchByEmailOrPhone(string emailOrPhone);
    Task<UserFilterResult?> GetByFilter(UserFilterParams filterParams);
}