using Common.Api;
using Common.Api.Attributes;
using Common.Api.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Users;
using Shop.Application.Users.Auth.ResetPassword;
using Shop.Application.Users.FavoriteItems.AddFavoriteItem;
using Shop.Application.Users.FavoriteItems.RemoveFavoriteItem;
using Shop.Application.Users.Roles.AddRole;
using Shop.Application.Users.Roles.RemoveRole;
using Shop.API.ViewModels.Users.Auth;
using Shop.API.ViewModels.Users.Roles;

namespace Shop.API.Controllers;

[Authorize]
public class UserController : BaseApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IMapper _mapper;

    public UserController(IUserFacade userFacade, IMapper mapper)
    {
        _userFacade = userFacade;
        _mapper = mapper;
    }

    [CheckPermission(RolePermission.Permissions.UserManager)]
    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateUserViewModel model)
    {
        var command = _mapper.Map<CreateUserCommand>(model);
        var result = await _userFacade.Create(command);
        var resultUrl = Url.Action("Create", "User", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditUserViewModel model)
    {
        var command = _mapper.Map<EditUserCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _userFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("ResetPassword")]
    public async Task<ApiResult> ResetPassword(ResetUserPasswordViewModel model)
    {
        var command = _mapper.Map<ResetUserPasswordCommand>(model);
        command.UserId = User.GetUserId();
        var result = await _userFacade.ResetPassword(command);
        return CommandResult(result);
    }

    [HttpPut("SetNewsletterSubscription/{userId}")]
    public async Task<ApiResult> SetNewsletterSubscription(long userId)
    {
        var result = await _userFacade.SetNewsletterSubscription(userId);
        return CommandResult(result);
    }

    [HttpPut("SetAvatar/{avatarId}")]
    public async Task<ApiResult> SetAvatar(long avatarId)
    {
        var result = await _userFacade.SetAvatar(User.GetUserId(), avatarId);
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
    public async Task<ApiResult> AddRole(AddUserRoleViewModel model)
    {
        var command = _mapper.Map<AddUserRoleCommand>(model);
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
    public async Task<ApiResult> RemoveRole(RemoveUserRoleViewModel model)
    {
        var command = _mapper.Map<RemoveUserRoleCommand>(model);
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