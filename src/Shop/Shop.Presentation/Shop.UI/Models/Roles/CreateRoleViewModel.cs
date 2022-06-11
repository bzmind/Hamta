using Shop.Domain.RoleAggregate;

namespace Shop.UI.Models.Roles;

public class CreateRoleViewModel
{
    public string Title { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; }
}