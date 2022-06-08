using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserRoleDto : BaseDto
{
    public long RoleId { get; set; }
    public string RoleTitle { get; set; }
}