﻿using Common.Api;
using Shop.API.CommandViewModels.Users;
using Shop.Application.Users.AddRole;
using Shop.Application.Users.Create;
using Shop.Application.Users.ResetPassword;
using Shop.Query.Users._DTOs;

namespace Shop.UI.Services.Users;

public interface IUserService
{
    Task<ApiResult?> Create(CreateUserCommand model);
    Task<ApiResult?> Edit(EditUserCommandViewModel model);
    Task<ApiResult?> SetAvatar(SetUserAvatarCommandViewModel model);
    Task<ApiResult?> ResetPassword(ResetUserPasswordCommand model);
    Task<ApiResult?> SetSubscriptionToNews(bool subscription);
    Task<ApiResult?> AddFavoriteItem(long productId);
    Task<ApiResult?> AddRole(AddUserRoleCommand model);
    Task<ApiResult?> RemoveFavoriteItem(long favoriteItemId);
    Task<ApiResult?> RemoveRole(long roleId);
    Task<ApiResult?> Remove(long userId);

    Task<UserDto?> GetById(long userId);
    Task<UserDto?> GetByEmailOrPhone(string emailOrPhone);
    Task<ApiResult<LoginNextStep>> SearchByEmailOrPhone(string emailOrPhone);
    Task<UserFilterResult?> GetByFilter(UserFilterParams filterParams);
}