using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.AddFavoriteItem;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.RemoveFavoriteItem;
using Shop.Application.Users.SetAvatar;
using Shop.Application.Users.SetSubscriptionToNews;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users._DTOs;

namespace Shop.API.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserFacade _userFacade;

    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateUserCommand command)
    {
        var result = await _userFacade.Create(command);
        var resultUrl = Url.Action("Create", "User", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditUserCommand command)
    {
        var result = await _userFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPut("SetAvatar")]
    public async Task<ApiResult> SetAvatar([FromForm] SetUserAvatarCommand command)
    {
        var result = await _userFacade.SetAvatar(command);
        return CommandResult(result);
    }

    [HttpPut("SetSubscriptionToNews")]
    public async Task<ApiResult> SetSubscriptionToNews(SetUserSubscriptionToNewsCommand command)
    {
        var result = await _userFacade.SetSubscriptionToNews(command);
        return CommandResult(result);
    }

    [HttpPut("AddFavoriteItem")]
    public async Task<ApiResult> AddFavoriteItem(AddUserFavoriteItemCommand command)
    {
        var result = await _userFacade.AddFavoriteItem(command);
        return CommandResult(result);
    }

    [HttpDelete("RemoveFavoriteItem")]
    public async Task<ApiResult> RemoveFavoriteItem(RemoveUserFavoriteItemCommand command)
    {
        var result = await _userFacade.RemoveFavoriteItem(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{userId}")]
    public async Task<ApiResult> Remove(long userId)
    {
        var result = await _userFacade.Remove(userId);
        return CommandResult(result);
    }

    [HttpGet("GetById/{userId}")]
    public async Task<ApiResult<UserDto?>> GetById(long userId)
    {
        var result = await _userFacade.GetById(userId);
        return QueryResult(result);
    }

    [HttpGet("GetByPhoneNumber/{phoneNumber}")]
    public async Task<ApiResult<UserDto?>> GetByPhoneNumber(string phoneNumber)
    {
        var result = await _userFacade.GetByPhoneNumber(phoneNumber);
        return QueryResult(result);
    }

    [HttpGet("GetByFilter")]
    public async Task<ApiResult<UserFilterResult>> GetByFilter([FromQuery] UserFilterParam filterParam)
    {
        var result = await _userFacade.GetByFilter(filterParam);
        return QueryResult(result);
    }
}