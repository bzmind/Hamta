using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using Shop.Query;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependencies(this IServiceCollection services, string connectionString)
        {
            ApplicationBootstrapper.RegisterDependencies(services);
            InfrastructureBootstrapper.RegisterDependencies(services, connectionString);
            QueryBootstrapper.RegisterDependencies(services, connectionString);
            FacadeBootstrapper.RegisterDependencies(services);
        }
    }
}