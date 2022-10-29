using Shop.Domain.UserAggregate;
using Shop.Domain.UserAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ShopContext shopContext) : base(shopContext)
    {
    }
}