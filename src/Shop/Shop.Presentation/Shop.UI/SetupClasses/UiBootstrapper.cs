using Shop.UI.Services.Auth;
using Shop.UI.Services.Categories;
using Shop.UI.Services.Comments;
using Shop.UI.Services.Inventories;
using Shop.UI.Services.Orders;
using Shop.UI.Services.Products;
using Shop.UI.Services.Questions;
using Shop.UI.Services.Roles;
using Shop.UI.Services.Shippings;
using Shop.UI.Services.UserAddresses;
using Shop.UI.Services.Users;
using Shop.UI.SetupClasses.RazorUtility;
using System.Text.Json;
using System.Text.Json.Serialization;
using Shop.Infrastructure.EmailService;
using Shop.UI.Services.Avatars;
using Shop.UI.SetupClasses.HttpClient;

namespace Shop.UI.SetupClasses;

public static class UiBootstrapper
{
    private const string BaseAddress = "https://localhost:7087/api/";

    public static void RegisterUiDependencies(this IServiceCollection services)
    {
        services.AddHttpClientService<IAvatarService, AvatarService>();
        services.AddHttpClientService<IAvatarService, AvatarService>();
        services.AddHttpClientService<IAuthService, AuthService>();
        services.AddHttpClientService<ICategoryService, CategoryService>();
        services.AddHttpClientService<ICategoryService, CategoryService>();
        services.AddHttpClientService<ICommentService, CommentService>();
        services.AddHttpClientService<IInventoryService, InventoryService>();
        services.AddHttpClientService<IOrderService, OrderService>();
        services.AddHttpClientService<IProductService, ProductService>();
        services.AddHttpClientService<IQuestionService, QuestionService>();
        services.AddHttpClientService<IRoleService, RoleService>();
        services.AddHttpClientService<IShippingService, ShippingService>();
        services.AddHttpClientService<IUserAddressService, UserAddressService>();
        services.AddHttpClientService<IUserService, UserService>();

        services.AddSingleton(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        });

        services.AddHttpContextAccessor();
        services.AddScoped<HttpClientAuthorizationDelegateHandler>();
        services.AddScoped<IRazorToStringRenderer, RazorToStringRenderer>();
        services.AddScoped<IEmailSender, EmailSender>();
    }

    private static void AddHttpClientService<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddHttpClient<TService, TImplementation>
                (httpClient => httpClient.BaseAddress = new Uri(BaseAddress))
            .AddHttpMessageHandler<HttpClientAuthorizationDelegateHandler>();
    }
}