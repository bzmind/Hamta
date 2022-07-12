using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Colors;
using Shop.Query.Colors._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Colors;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.ColorManager)]
public class ColorController : BaseApiController
{
    private readonly IColorFacade _colorFacade;
    private readonly IMapper _mapper;

    public ColorController(IColorFacade colorFacade, IMapper mapper)
    {
        _colorFacade = colorFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateColorViewModel model)
    {
        var command = _mapper.Map<CreateColorCommand>(model);
        var result = await _colorFacade.Create(command);
        var resultUrl = Url.Action("Create", "Color", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditColorViewModel model)
    {
        var command = _mapper.Map<EditColorCommand>(model);
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
    public async Task<ApiResult<ColorFilterResult>> GetByFilter([FromQuery] ColorFilterParams filterParams)
    {
        var result = await _colorFacade.GetByFilter(filterParams);
        return QueryResult(result);
    }
}