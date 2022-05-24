using Common.Domain.Repository;

namespace Shop.Domain.ProductAggregate.Repository;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<bool> RemoveProduct(long productId);
}