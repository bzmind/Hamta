using Common.Application;
using MediatR;
using Shop.Application.Roles.AddPermission;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Remove;
using Shop.Application.Roles.RemovePermission;
using Shop.Query.Roles._DTOs;
using Shop.Query.Roles.GetById;
using Shop.Query.Roles.GetList;

namespace Shop.Presentation.Facade.Roles;

public class RoleFacade : IRoleFacade
{
    private readonly IMediator _mediator;

    public RoleFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddPermission(AddRolePermissionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemovePermission(RemoveRolePermissionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(RemoveRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<RoleDto?> GetById(long roleId)
    {
        return await _mediator.Send(new GetRoleByIdQuery(roleId));
    }

    public async Task<List<RoleDto>> GetAll()
    {
        return await _mediator.Send(new GetRoleListQuery());
    }
}