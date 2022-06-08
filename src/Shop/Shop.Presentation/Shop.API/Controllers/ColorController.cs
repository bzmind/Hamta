using System.Net;
using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Colors;
using Shop.Query.Colors._DTOs;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.ColorManager)]
public class ColorController : BaseApiController
{
    private readonly IColorFacade _colorFacade;

    public ColorController(IColorFacade colorFacade)
    {
        _colorFacade = colorFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateColorCommand command)
    {
        var result = await _colorFacade.Create(command);
        var resultUrl = Url.Action("Create", "Color", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditColorCommand command)
    {
        var result = await _colorFacade.Edit(command);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{colorId}")]
    public async Task<ApiResult<ColorDto?>> GetById(long colorId)
    {
        var result = await _colorFacade.GetById(colorId);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetByFilter")]
    public async Task<ApiResult<ColorFilterResult>> GetByFilter([FromQuery] ColorFilterParam filterParams)
    {
        var result = await _colorFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}