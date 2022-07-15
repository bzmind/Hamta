﻿using AspNetCoreRateLimit;
using Common.Api.Jwt;

namespace Shop.API.Setup;

public static class ApiBootstrapper
{
    public static void RegisterApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddTransient<CustomJwtValidation>();
        // AspNetCoreRateLimit dependencies
        services.AddOptions();
        services.AddMemoryCache();
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}