using Shop.Domain.CustomerAggregate;
using Shop.Domain.CustomerAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Customers;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ShopContext context) : base(context)
    {
    }
}