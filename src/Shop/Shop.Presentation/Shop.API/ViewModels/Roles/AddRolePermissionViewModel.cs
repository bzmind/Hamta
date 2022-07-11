using Shop.Domain.RoleAggregate;

namespace Shop.API.ViewModels.Roles;

public class AddRolePermissionViewModel
{
    public long RoleId { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; }
}