﻿using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Avatars.Create;
using Shop.Domain.AvatarAggregate;
using Shop.Presentation.Facade.Avatars;
using Shop.Query.Avatars._DTOs;

namespace Shop.API.Controllers;

public class AvatarController : BaseApiController
{
    private readonly IAvatarFacade _avatarFacade;

    public AvatarController(IAvatarFacade avatarFacade)
    {
        _avatarFacade = avatarFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create([FromForm] CreateAvatarCommand command)
    {
        var result = await _avatarFacade.Create(command);
        var resultUrl = Url.Action("Create", "Avatar", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpDelete("Remove/{avatarId}")]
    public async Task<ApiResult> Remove(long avatarId)
    {
        var result = await _avatarFacade.Remove(avatarId);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{avatarId}")]
    public async Task<ApiResult<AvatarDto?>> GetById(long avatarId)
    {
        var result = await _avatarFacade.GetById(avatarId);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetByGender/{gender}")]
    public async Task<ApiResult<AvatarDto?>> GetByGender(Avatar.AvatarGender gender)
    {
        var result = await _avatarFacade.GetByGender(gender);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ApiResult<List<AvatarDto>>> GetAll()
    {
        var result = await _avatarFacade.GetAll();
        return QueryResult(result);
    }
}