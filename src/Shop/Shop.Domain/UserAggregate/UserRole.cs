using Common.Domain.BaseClasses;

namespace Shop.Domain.UserAggregate;

public class UserRole : BaseEntity
{
    public long UserId { get; private set; }
    public long RoleId { get; private set; }

    public UserRole(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}