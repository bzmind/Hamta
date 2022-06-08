using Common.Query.BaseClasses;

namespace Shop.Query.Roles._DTOs;

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<string> Permissions { get; set; } = new();
}