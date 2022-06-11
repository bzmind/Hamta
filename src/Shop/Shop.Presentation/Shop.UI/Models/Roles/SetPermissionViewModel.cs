using Shop.Domain.RoleAggregate;

namespace Shop.UI.Models.Roles;

public class SetPermissionViewModel
{
    public long RoleId { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; }
}