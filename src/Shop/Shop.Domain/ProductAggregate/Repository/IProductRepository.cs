using Common.Domain.Repository;

namespace Shop.Domain.ProductAggregate.Repository;

public interface IProductRepository : IBaseRepository<Product>
{
    Product? GetProductBySlug(string slug);
    Task<bool> RemoveProduct(long productId);
}