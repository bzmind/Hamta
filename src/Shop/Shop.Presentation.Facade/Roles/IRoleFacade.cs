using Common.Application;
using Shop.Application.Roles.AddPermission;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.RemovePermission;
using Shop.Query.Roles._DTOs;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult<long>> Create(CreateRoleCommand command);
    Task<OperationResult> AddPermission(AddRolePermissionCommand command);
    Task<OperationResult> RemovePermission(RemoveRolePermissionCommand command);
    Task<OperationResult> Remove(long roleId);

    Task<RoleDto?> GetById(long roleId);
    Task<List<RoleDto>> GetAll();
}