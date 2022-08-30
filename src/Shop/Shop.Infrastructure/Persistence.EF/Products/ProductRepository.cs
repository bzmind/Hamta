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

    public Product? GetProductBySlug(string slug)
    {
        return Context.Products.FirstOrDefault(product => product.Slug == slug);
    }

    public async Task<bool> RemoveProduct(long productId)
    {
        var result = await Context.Products
            .Where(p => p.Id == productId)
            .GroupJoin(
                Context.Sellers.SelectMany(s => s.Inventories),
                product => product.Id,
                inventory => inventory.ProductId,
                (product, inventory) => new
                {
                    product,
                    inventory
                })
            .SelectMany(
                tables => tables.inventory.DefaultIfEmpty(),
                (tables, inventory) => new { tables.product, inventory })
            .ToListAsync();

        if (result.Count == 0)
            return false;

        var product = result.First().product;
        var inventories = result.Select(tables => tables.inventory).ToList();

        if (inventories.FirstOrDefault() != null)
        {
            var inventoriesToRemove = Context.Sellers.SelectMany(seller =>
                seller.Inventories.Where(inventory => inventories.Contains(inventory)));
            Context.RemoveRange(inventoriesToRemove);
        }

        Context.Products.Remove(product);

        return true;
    }
}