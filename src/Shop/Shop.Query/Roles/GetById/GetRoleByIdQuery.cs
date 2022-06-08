using Common.Query.BaseClasses;
using Shop.Domain.RoleAggregate.Repository;
using Shop.Query.Roles._DTOs;
using Shop.Query.Roles._Mapper;

namespace Shop.Query.Roles.GetById;

public record GetRoleByIdQuery(long RoleId) : IBaseQuery<RoleDto?>;

public class GetRoleByIdQueryHandler : IBaseQueryHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(request.RoleId);
        return role.MapToRoleDto();
    }
}