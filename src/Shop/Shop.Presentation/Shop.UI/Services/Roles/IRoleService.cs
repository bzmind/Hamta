using Common.Api;
using Shop.API.ViewModels.Roles;
using Shop.Query.Roles._DTOs;

namespace Shop.UI.Services.Roles;

public interface IRoleService
{
    Task<ApiResult> Create(CreateRoleViewModel model);
    Task<ApiResult> Edit(EditRoleViewModel model);
    Task<ApiResult> Remove(long roleId);

    Task<RoleDto?> GetById(long roleId);
    Task<List<RoleDto>> GetAll();
}