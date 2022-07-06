using Common.Query.BaseClasses;
using Shop.Domain.RoleAggregate;

namespace Shop.Query.Roles._DTOs;

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; } = new();
}