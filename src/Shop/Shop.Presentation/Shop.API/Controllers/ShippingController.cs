using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Shippings;
using Shop.Query.Shippings._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Shippings;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.ShippingManager)]
public class ShippingController : BaseApiController
{
    private readonly IShippingFacade _shippingFacade;
    private readonly IMapper _mapper;

    public ShippingController(IShippingFacade shippingFacade, IMapper mapper)
    {
        _shippingFacade = shippingFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateShippingViewModel model)
    {
        var command = _mapper.Map<CreateShippingCommand>(model);
        var result = await _shippingFacade.Create(command);
        var resultUrl = Url.Action("Create", "Shipping", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditShippingViewModel model)
    {
        var command = _mapper.Map<EditShippingCommand>(model);
        var result = await _shippingFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{shippingId}")]
    public async Task<ApiResult> Remove(long shippingId)
    {
        var result = await _shippingFacade.Remove(shippingId);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{shippingId}")]
    public async Task<ApiResult<ShippingDto?>> GetById(long shippingId)
    {
        var result = await _shippingFacade.GetById(shippingId);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ApiResult<List<ShippingDto>>> GetAll()
    {
        var result = await _shippingFacade.GetAll();
        return QueryResult(result);
    }
}