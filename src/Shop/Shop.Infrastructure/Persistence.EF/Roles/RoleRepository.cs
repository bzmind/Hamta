using Shop.Domain.RoleAggregate;
using Shop.Domain.RoleAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Roles;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ShopContext context) : base(context)
    {
    }
}