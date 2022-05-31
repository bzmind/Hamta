using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.ColorAggregate.Repository;
using Shop.Domain.CommentAggregate.Repository;
using Shop.Domain.UserAggregate.Repository;
using Shop.Domain.InventoryAggregate.Repository;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.QuestionAggregate.Repository;
using Shop.Domain.ShippingAggregate.Repository;
using Shop.Infrastructure.Persistence.EF;
using Shop.Infrastructure.Persistence.EF.Categories;
using Shop.Infrastructure.Persistence.EF.Colors;
using Shop.Infrastructure.Persistence.EF.Comments;
using Shop.Infrastructure.Persistence.EF.Users;
using Shop.Infrastructure.Persistence.EF.Inventories;
using Shop.Infrastructure.Persistence.EF.Orders;
using Shop.Infrastructure.Persistence.EF.Products;
using Shop.Infrastructure.Persistence.EF.Questions;
using Shop.Infrastructure.Persistence.EF.Shippings;

namespace Shop.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IColorRepository, ColorRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IInventoryRepository, InventoryRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IQuestionRepository, QuestionRepository>();
        services.AddTransient<IShippingRepository, ShippingRepository>();

        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}