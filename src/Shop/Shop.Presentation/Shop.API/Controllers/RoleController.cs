using System.Net;
using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Roles.AddPermission;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Remove;
using Shop.Application.Roles.RemovePermission;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Roles;
using Shop.Query.Roles._DTOs;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.RoleManager)]
public class RoleController : BaseApiController
{
    private readonly IRoleFacade _roleFacade;

    public RoleController(IRoleFacade roleFacade)
    {
        _roleFacade = roleFacade;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateRoleCommand command)
    {
        var result = await _roleFacade.Create(command);
        var resultUrl = Url.Action("Create", "Role", new { Id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("AddPermission")]
    public async Task<ApiResult> AddPermission(AddRolePermissionCommand command)
    {
        var result = await _roleFacade.AddPermission(command);
        return CommandResult(result);
    }

    [HttpPut("RemovePermission")]
    public async Task<ApiResult> RemovePermission(RemoveRolePermissionCommand command)
    {
        var result = await _roleFacade.RemovePermission(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove")]
    public async Task<ApiResult> Remove(RemoveRoleCommand command)
    {
        var result = await _roleFacade.Remove(command);
        return CommandResult(result);
    }
    
    [HttpGet("GetById/{roleId}")]
    public async Task<ApiResult<RoleDto?>> GetById(long roleId)
    {
        var result = await _roleFacade.GetById(roleId);
        return QueryResult(result);
    }

    [HttpGet("GetAll")]
    public async Task<ApiResult<List<RoleDto>>> GetByFilter()
    {
        var result = await _roleFacade.GetAll();
        return QueryResult(result);
    }
}