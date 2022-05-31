using Common.Domain.BaseClasses;

namespace Shop.Domain.UserAggregate;

public class UserFavoriteItem : BaseEntity
{
    public long UserId { get; private set; }
    public long ProductId { get; private set; }

    public UserFavoriteItem(long userId, long productId)
    {
        UserId = userId;
        ProductId = productId;
    }
}