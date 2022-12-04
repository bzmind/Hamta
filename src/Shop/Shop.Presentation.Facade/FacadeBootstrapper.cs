using Microsoft.Extensions.DependencyInjection;
using Shop.Presentation.Facade.Avatars;
using Shop.Presentation.Facade.Categories;
using Shop.Presentation.Facade.Colors;
using Shop.Presentation.Facade.Comments;
using Shop.Presentation.Facade.Entities.Banner;
using Shop.Presentation.Facade.Entities.Slider;
using Shop.Presentation.Facade.Orders;
using Shop.Presentation.Facade.Products;
using Shop.Presentation.Facade.Questions;
using Shop.Presentation.Facade.Roles;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Shippings;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Addresses;
using Shop.Presentation.Facade.Users.Tokens;

namespace Shop.Presentation.Facade;

public static class FacadeBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<IAvatarFacade, AvatarFacade>();
        services.AddScoped<ICategoryFacade, CategoryFacade>();
        services.AddScoped<IColorFacade, ColorFacade>();
        services.AddScoped<ICommentFacade, CommentFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IUserAddressFacade, UserAddressFacade>();
        services.AddScoped<IUserTokenFacade, UserTokenFacade>();
        services.AddScoped<ISellerFacade, SellerFacade>();
        services.AddScoped<IOrderFacade, OrderFacade>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddScoped<IQuestionFacade, QuestionFacade>();
        services.AddScoped<IRoleFacade, RoleFacade>();
        services.AddScoped<IShippingFacade, ShippingFacade>();
        services.AddScoped<IBannerFacade, BannerFacade>();
        services.AddScoped<ISliderFacade, SliderFacade>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
        });
    }
}