using Common.Api.Jwt;

namespace Shop.API.SetupClasses;

public static class ApiBootstrapper
{
    public static void RegisterApiDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddTransient<CustomJwtValidation>();
    }
}