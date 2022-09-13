using Microsoft.EntityFrameworkCore;
using Shop.Domain.AvatarAggregate;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.ColorAggregate;
using Shop.Domain.CommentAggregate;
using Shop.Domain.Entities;
using Shop.Domain.UserAggregate;
using Shop.Domain.OrderAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.QuestionAggregate;
using Shop.Domain.RoleAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Domain.ShippingAggregate;

namespace Shop.Infrastructure.Persistence.EF;

public class ShopContext : DbContext
{
    public DbSet<Avatar> Avatars { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Banner> Banners { get; set; }

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

        modelBuilder.HasSequence<long>("ProductHiLoSequence")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.HasSequence<long>("OrderHiLoSequence")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.HasSequence<long>("CategoryHiLoSequence")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.HasSequence<long>("CommentHiLoSequence")
            .StartsAt(1)
            .IncrementsBy(1);

        base.OnModelCreating(modelBuilder);
    }
}