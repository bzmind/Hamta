using Common.Api;
using Shop.Query.Roles._DTOs;
using Shop.UI.Models.Roles;

namespace Shop.UI.Services.Roles;

public interface IRoleService
{
    Task<ApiResult?> Create(CreateRoleViewModel model);
    Task<ApiResult?> AddPermissions(SetPermissionViewModel model);
    Task<ApiResult?> RemovePermissions(SetPermissionViewModel model);
    Task<ApiResult?> Remove(long roleId);

    Task<RoleDto?> GetById(long roleId);
    Task<List<RoleDto>?> GetAll();
}