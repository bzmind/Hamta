using System.Net;
using AutoMapper;
using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.ViewModels.Entities.Banner;
using Shop.Application.Entities.Banners.Create;
using Shop.Application.Entities.Banners.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Entities.Banner;
using Shop.Query.Entities._DTOs;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.BannerManager)]
public class BannerController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IBannerFacade _bannerFacade;

    public BannerController(IMapper mapper, IBannerFacade bannerFacade)
    {
        _mapper = mapper;
        _bannerFacade = bannerFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create([FromForm] CreateBannerViewModel model)
    {
        var command = _mapper.Map<CreateBannerCommand>(model);
        var result = await _bannerFacade.Create(command);
        var resultUrl = Url.Action("Create", "Banner", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }
    
    [HttpPut("Edit")]
    public async Task<ApiResult> Edit([FromForm] EditBannerViewModel model)
    {
        var command = _mapper.Map<EditBannerCommand>(model);
        var result = await _bannerFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{bannerId}")]
    public async Task<ApiResult> Remove(long bannerId)
    {
        var result = await _bannerFacade.Remove(bannerId);
        return CommandResult(result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ApiResult<BannerDto?>> GetById(long id)
    {
        var result = await _bannerFacade.GetById(id);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ApiResult<List<BannerDto>>> GetAll()
    {
        var result = await _bannerFacade.GetAll();
        return QueryResult(result);
    }
}