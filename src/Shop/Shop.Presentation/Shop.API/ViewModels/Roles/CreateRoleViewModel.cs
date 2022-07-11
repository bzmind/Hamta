using Shop.Domain.RoleAggregate;

namespace Shop.API.ViewModels.Roles;

public class CreateRoleViewModel
{
    public string Title { get; set; }
    public List<RolePermission.Permissions> Permissions { get; set; }
}