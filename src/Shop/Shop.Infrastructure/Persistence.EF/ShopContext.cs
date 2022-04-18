using Microsoft.EntityFrameworkCore;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.ColorAggregate;
using Shop.Domain.CommentAggregate;
using Shop.Domain.CustomerAggregate;
using Shop.Domain.InventoryAggregate;
using Shop.Domain.OrderAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.QuestionAggregate;

namespace Shop.Infrastructure.Persistence.EF;

public class ShopContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Question> Questions { get; set; }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}