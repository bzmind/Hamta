using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Users;
using Shop.Application.Users.AddFavoriteItem;
using Shop.Application.Users.AddRole;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.RemoveFavoriteItem;
using Shop.Application.Users.RemoveRole;
using Shop.Application.Users.SetAvatar;
using Shop.Application.Users.SetNewsletterSubscription;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users._DTOs;
using System.Net;
using Shop.API.CommandViewModels.Users;
using Shop.Application.Users.ResetPassword;

namespace Shop.API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateUserCommand command)
    {
        var result = await _userFacade.Create(command);
        var resultUrl = Url.Action("Create", "User", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditUserCommandViewModel model)
    {
        var command = new EditUserCommand(User.GetUserId(), model.FullName, model.Email, model.PhoneNumber);
        var result = await _userFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("SetAvatar")]
    public async Task<ApiResult> SetAvatar([FromForm] SetUserAvatarCommandViewModel model)
    {
        var command = new SetUserAvatarCommand(User.GetUserId(), model.Avatar);
        var result = await _userFacade.SetAvatar(command);
        return CommandResult(result);
    }

    [HttpPut("ResetPassword")]
    public async Task<ApiResult> ResetPassword(ResetUserPasswordViewModel model)
    {
        var command = new ResetUserPasswordCommand(User.GetUserId(), model.CurrentPassword, model.NewPassword);
        var result = await _userFacade.ResetPassword(command);
        return CommandResult(result);
    }

    [HttpPut("SetNewsletterSubscription/{userId}")]
    public async Task<ApiResult> SetNewsletterSubscription(long userId)
    {
        var result = await _userFacade.SetNewsletterSubscription(userId);
        return CommandResult(result);
    }

    [HttpPut("AddFavoriteItem/{productId}")]
    public async Task<ApiResult> AddFavoriteItem(long productId)
    {
        var command = new AddUserFavoriteItemCommand(User.GetUserId(), productId);
        var result = await _userFacade.AddFavoriteItem(command);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpPut("AddRole")]
    public async Task<ApiResult> AddRole(AddUserRoleCommand command)
    {
        var result = await _userFacade.AddRole(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveFavoriteItem/{favoriteItemId}")]
    public async Task<ApiResult> RemoveFavoriteItem(long favoriteItemId)
    {
        var command = new RemoveUserFavoriteItemCommand(User.GetUserId(), favoriteItemId);
        var result = await _userFacade.RemoveFavoriteItem(command);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpDelete("RemoveRole/{roleId}")]
    public async Task<ApiResult> RemoveRole(RemoveUserRoleCommand command)
    {
        var result = await _userFacade.RemoveRole(command);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpDelete("Remove/{userId}")]
    public async Task<ApiResult> Remove(long userId)
    {
        var result = await _userFacade.Remove(userId);
        return CommandResult(result);
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpGet("GetById/{userId}")]
    public async Task<ApiResult<UserDto?>> GetById(long userId)
    {
        var result = await _userFacade.GetById(userId);
        return QueryResult(result);
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpGet("GetByEmailOrPhone/{emailOrPhone}")]
    public async Task<ApiResult<UserDto?>> GetByEmailOrPhone(string emailOrPhone)
    {
        var result = await _userFacade.GetByEmailOrPhone(emailOrPhone);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpGet("SearchByEmailOrPhone/{emailOrPhone}")]
    public async Task<ApiResult<LoginNextStep>> SearchByEmailOrPhone(string emailOrPhone)
    {
        var result = await _userFacade.SearchByEmailOrPhone(emailOrPhone);
        return QueryResult(result);
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<UserFilterResult>> GetByFilter([FromQuery] UserFilterParams filterParams)
    {
        var result = await _userFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}