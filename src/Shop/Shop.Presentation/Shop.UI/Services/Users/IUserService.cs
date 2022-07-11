using Common.Api;
using Shop.API.ViewModels.Users;
using Shop.API.ViewModels.Users.Auth;
using Shop.Application.Users.Create;
using Shop.Application.Users.Roles.AddRole;
using Shop.Query.Users._DTOs;

namespace Shop.UI.Services.Users;

public interface IUserService
{
    Task<ApiResult> Create(CreateUserCommand model);
    Task<ApiResult> Edit(EditUserViewModel model);
    Task<ApiResult> ResetPassword(ResetUserPasswordViewModel model);
    Task<ApiResult<bool>> SetNewsletterSubscription(long userId);
    Task<ApiResult> SetAvatar(long avatarId);
    Task<ApiResult> AddFavoriteItem(long productId);
    Task<ApiResult> AddRole(AddUserRoleCommand model);
    Task<ApiResult> RemoveFavoriteItem(long favoriteItemId);
    Task<ApiResult> RemoveRole(long roleId);
    Task<ApiResult> Remove(long userId);

    Task<UserDto> GetById(long userId);
    Task<UserDto> GetByEmailOrPhone(string emailOrPhone);
    Task<ApiResult<LoginNextStep>> SearchByEmailOrPhone(string emailOrPhone);
    Task<UserFilterResult> GetByFilter(UserFilterParams filterParams);
}