using System.Text.Json;
using System.Text.Json.Serialization;
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

namespace Shop.UI.SetupClasses;

public static class UiBootstrapper
{
    public static void RegisterUiDependencies(this IServiceCollection services)
    {
        const string baseAddress = "https://localhost:7087";

        services.AddHttpClient<IAuthService, AuthService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<ICategoryService, CategoryService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<ICategoryService, CategoryService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<ICommentService, CommentService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IInventoryService, InventoryService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IOrderService, OrderService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IProductService, ProductService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IQuestionService, QuestionService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IRoleService, RoleService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IShippingService, ShippingService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IUserAddressService, UserAddressService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));
        services.AddHttpClient<IUserService, UserService>(httpClient => httpClient.BaseAddress = new Uri(baseAddress));

        services.AddSingleton(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        });

        services.AddHttpContextAccessor();
    }
}