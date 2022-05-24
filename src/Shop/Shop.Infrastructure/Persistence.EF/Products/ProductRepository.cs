using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAggregate;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Products;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ShopContext context) : base(context)
    {
    }

    public async Task<bool> RemoveProduct(long productId)
    {
        var result = await Context.Products
            .Where(p => p.Id == productId)
            .AsTracking()
            .GroupJoin(
                Context.Inventories,
                p => p.Id,
                i => i.ProductId,
                (product, inventory) => new
                {
                    product,
                    inventory
                })
            .SelectMany(
                t => t.inventory.DefaultIfEmpty(),
                (t, i) => new { t.product, inventory = i })
            .ToListAsync();

        if (result.Count == 0)
            return false;

        var product = result.First().product;
        var inventories = result.Select(t => t.inventory).ToList();

        if (inventories.FirstOrDefault() != null)
            Context.Inventories.RemoveRange(inventories);

        Context.Products.Remove(product);

        return true;
    }
}