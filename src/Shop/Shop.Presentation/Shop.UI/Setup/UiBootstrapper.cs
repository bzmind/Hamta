using System.Text.Json;
using System.Text.Json.Serialization;
using Shop.API.Setup;
using Shop.Infrastructure.EmailService;
using Shop.UI.Services.Auth;
using Shop.UI.Services.Avatars;
using Shop.UI.Services.Categories;
using Shop.UI.Services.Colors;
using Shop.UI.Services.Comments;
using Shop.UI.Services.Entities.Banners;
using Shop.UI.Services.Entities.Sliders;
using Shop.UI.Services.MainPage;
using Shop.UI.Services.Orders;
using Shop.UI.Services.Products;
using Shop.UI.Services.Questions;
using Shop.UI.Services.Roles;
using Shop.UI.Services.Sellers;
using Shop.UI.Services.Shippings;
using Shop.UI.Services.UserAddresses;
using Shop.UI.Services.Users;
using Shop.UI.Setup.CookieUtility;
using Shop.UI.Setup.HttpClient;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Setup;

public static class UiBootstrapper
{
    public static void RegisterUiDependencies(this IServiceCollection services)
    {
        const string baseAddress = "https://localhost:7087/api/";

        services.AddHttpClient<IAvatarService, AvatarService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IAuthService, AuthService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<ICategoryService, CategoryService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IColorService, ColorService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IUserService, UserService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<ICommentService, CommentService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<ISellerService, SellerService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IOrderService, OrderService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IProductService, ProductService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IQuestionService, QuestionService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IRoleService, RoleService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IShippingService, ShippingService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IUserAddressService, UserAddressService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<IBannerService, BannerService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddHttpClient<ISliderService, SliderService>
            (httpClient => httpClient.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();

        services.AddCookieManager();

        services.AddScoped<CartCookieManager>();
        
        services.AddSingleton(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        });

        services.AddHttpContextAccessor();
        services.AddScoped<HttpClientAuthorizationDelegateHandler>();
        services.AddScoped<IRazorToStringRenderer, RazorToStringRenderer>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IMainPageService, MainPageService>();
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
    }
}