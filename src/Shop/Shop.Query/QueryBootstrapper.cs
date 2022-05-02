using Microsoft.Extensions.DependencyInjection;

namespace Shop.Query;

public class QueryBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services, string connectionString)
    {
        services.AddTransient(_ => new DapperContext(connectionString));
    }
}