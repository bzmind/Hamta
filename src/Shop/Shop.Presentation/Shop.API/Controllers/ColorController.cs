using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Application.Colors.Remove;
using Shop.Presentation.Facade.Colors;
using Shop.Query.Colors._DTOs;

namespace Shop.API.Controllers;

public class ColorController : BaseApiController
{
    private readonly IColorFacade _colorFacade;

    public ColorController(IColorFacade colorFacade)
    {
        _colorFacade = colorFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateColorCommand command)
    {
        var result = await _colorFacade.Create(command);
        var resultUrl = Url.Action("Create", "Color", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditColorCommand command)
    {
        var result = await _colorFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete]
    public async Task<ApiResult> Remove(RemoveColorCommand command)
    {
        var result = await _colorFacade.Remove(command);
        return CommandResult(result);
    }

    [HttpGet("{colorId}")]
    public async Task<ApiResult<ColorDto?>> GetColorById(long colorId)
    {
        var result = await _colorFacade.GetColorById(colorId);
        return QueryResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<ColorFilterResult>> GetColorByFilter([FromQuery] ColorFilterParam filterParams)
    {
        var result = await _colorFacade.GetColorByFilter(filterParams);
        return QueryResult(result);
    }
}