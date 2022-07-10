using Common.Api;
using Shop.Application.Roles.AddPermission;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.RemovePermission;
using Shop.Query.Roles._DTOs;

namespace Shop.UI.Services.Roles;

public interface IRoleService
{
    Task<ApiResult> Create(CreateRoleCommand model);
    Task<ApiResult> AddPermissions(AddRolePermissionCommand model);
    Task<ApiResult> RemovePermissions(RemoveRolePermissionCommand model);
    Task<ApiResult> Remove(long roleId);

    Task<RoleDto> GetById(long roleId);
    Task<List<RoleDto>> GetAll();
}