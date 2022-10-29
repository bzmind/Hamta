using AspNetCoreRateLimit;
using Common.Api.Jwt;
using System.Text.Json.Serialization;
using System.Text.Json;
using Shop.API.Setup.Gateways.Zibal;

namespace Shop.API.Setup;

public static class ApiBootstrapper
{
    public static void RegisterApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddTransient<CustomJwtValidation>();
        services.AddSingleton(new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        });
        services.AddHttpClient<IZibalService, ZibalService>();

        // AspNetCoreRateLimit dependencies
        services.AddOptions();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}