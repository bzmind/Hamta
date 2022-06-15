using Shop.Domain.RoleAggregate;

namespace Shop.UI.Models.Roles;

public class SetPermissionCommandViewModel
{
    public long RoleId { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; }
}