using System.Net;
using AutoMapper;
using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Entities.Slider;
using Shop.Application.Entities.Sliders.Create;
using Shop.Application.Entities.Sliders.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Entities.Slider;
using Shop.Query.Entities._DTOs;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.SliderManager)]
public class SliderController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly ISliderFacade _sliderFacade;

    public SliderController(IMapper mapper, ISliderFacade sliderFacade)
    {
        _mapper = mapper;
        _sliderFacade = sliderFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create([FromForm] CreateSliderViewModel model)
    {
        var command = _mapper.Map<CreateSliderCommand>(model);
        var result = await _sliderFacade.Create(command);
        var resultUrl = Url.Action("Create", "Slider", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit([FromForm] EditSliderViewModel model)
    {
        var command = _mapper.Map<EditSliderCommand>(model);
        var result = await _sliderFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{sliderId}")]
    public async Task<ApiResult> Remove(long sliderId)
    {
        var result = await _sliderFacade.Remove(sliderId);
        return CommandResult(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResult<SliderDto?>> GetById(long id)
    {
        var result = await _sliderFacade.GetById(id);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ApiResult<List<SliderDto>>> GetAll()
    {
        var result = await _sliderFacade.GetAll();
        return QueryResult(result);
    }
}