namespace Shop.Presentation.Facade.Caching;

public class CacheKeys
{
    public static string User(long id) => $"user-{id}";
    public static string User(string emailOrPhone) => $"user-{emailOrPhone}";
    public static string UserToken(string hashToken) => $"token-{hashToken}";
    public static string UserOrders(long userId) => $"user-orders-{userId}";
    public static string Order(long id) => $"order-{id}";
    public static string Product(long id) => $"product-{id}";
    public static string Product(string slug) => $"product-{slug}";
    public static string Categories => "categories";
    public static string MenuCategories => "menu-categories";
    public static string Banners => "banners";
    public static string Sliders => "sliders";
}