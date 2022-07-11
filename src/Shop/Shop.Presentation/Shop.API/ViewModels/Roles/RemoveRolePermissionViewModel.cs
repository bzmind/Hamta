using Shop.Domain.RoleAggregate;

namespace Shop.API.ViewModels.Roles;

public class RemoveRolePermissionViewModel
{
    public long RoleId { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; }
}