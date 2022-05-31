using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Colors;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Presentation.Facade.Inventories;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Questions;
using Shop.Presentation.Facade.Shippings;

namespace Shop.Presentation.Facade;

public static class FacadeBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<IColorFacade, ColorFacade>();
        services.AddScoped<ICommentFacade, CommentFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IUserAddressFacade, UserAddressFacade>();
        services.AddScoped<IInventoryFacade, InventoryFacade>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<IQuestionFacade, QuestionFacade>();
        services.AddScoped<IShippingFacade, ShippingFacade>();
    }
}