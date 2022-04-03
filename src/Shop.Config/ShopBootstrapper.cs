using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Category_Use_Cases._Services;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        }
    }
}