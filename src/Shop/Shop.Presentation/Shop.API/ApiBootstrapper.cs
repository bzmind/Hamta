namespace Shop.API;

public static class ApiBootstrapper
{
    public static void RegisterApiDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
    }
}