using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Roles.Create;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Roles;
using Shop.Application.Roles.Edit;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.RoleManager)]
public class RoleController : BaseApiController
{
    private readonly IRoleFacade _roleFacade;
    private readonly IMapper _mapper;

    public RoleController(IRoleFacade roleFacade, IMapper mapper)
    {
        _roleFacade = roleFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateRoleViewModel model)
    {
        var command = _mapper.Map<CreateRoleCommand>(model);
        var result = await _roleFacade.Create(command);
        var resultUrl = Url.Action("Create", "Role", new { Id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditRoleViewModel model)
    {
        var command = _mapper.Map<EditRoleCommand>(model);
        var result = await _roleFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{roleId}")]
    public async Task<ApiResult> Remove(long roleId)
    {
        var result = await _roleFacade.Remove(roleId);
        return CommandResult(result);
    }

    [HttpGet("GetById/{roleId}")]
    public async Task<ApiResult<RoleDto?>> GetById(long roleId)
    {
        var result = await _roleFacade.GetById(roleId);
        return QueryResult(result);
    }

    [HttpGet("GetAll")]
    public async Task<ApiResult<List<RoleDto>>> GetAll()
    {
        var result = await _roleFacade.GetAll();
        return QueryResult(result);
    }
}