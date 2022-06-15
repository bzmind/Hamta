using Common.Api;
using Shop.Query.Roles._DTOs;
using Shop.UI.Models.Roles;

namespace Shop.UI.Services.Roles;

public interface IRoleService
{
    Task<ApiResult?> Create(CreateRoleCommandViewModel model);
    Task<ApiResult?> AddPermissions(SetPermissionCommandViewModel model);
    Task<ApiResult?> RemovePermissions(SetPermissionCommandViewModel model);
    Task<ApiResult?> Remove(long roleId);

    Task<RoleDto?> GetById(long roleId);
    Task<List<RoleDto>?> GetAll();
}