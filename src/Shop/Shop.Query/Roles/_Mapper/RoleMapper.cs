using Shop.Domain.RoleAggregate;
using Shop.Query.Roles._DTOs;

namespace Shop.Query.Roles._Mapper;

public static class RoleMapper
{
    public static RoleDto MapToRoleDto(this Role? role)
    {
        if (role == null)
            return null;

        return new RoleDto
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Title = role.Title,
            Permissions = role.Permissions.Select(p => p.Permission).ToList()
        };
    }

    public static List<RoleDto> MapToRoleDto(this List<Role> roles)
    {
        var dtoRoles = new List<RoleDto>();

        roles.ForEach(r =>
        {
            dtoRoles.Add(new RoleDto
            {
                Id = r.Id,
                CreationDate = r.CreationDate,
                Title = r.Title,
                Permissions = r.Permissions.Select(p => p.Permission).ToList()
            });
        });

        return dtoRoles;
    }
}