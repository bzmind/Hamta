using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserFilterDto : BaseDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public long AvatarId { get; set; }
    public bool IsSubscribedToNewsletter { get; set; }
    public List<UserRoleDto> Roles { get; set; }
}