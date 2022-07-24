using Common.Application;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Roles._DTOs;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult<long>> Create(CreateRoleCommand command);
    Task<OperationResult> Edit(EditRoleCommand command);
    Task<OperationResult> Remove(long roleId);

    Task<RoleDto?> GetById(long roleId);
    Task<List<RoleDto>> GetAll();
}