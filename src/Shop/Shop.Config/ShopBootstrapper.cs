using Common.Application.Utility.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using Shop.Application.Categories.Create;
using Shop.Infrastructure;
using Shop.Presentation.Facade;
using Shop.Query;
using Shop.Query.Categories.GetList;

namespace Shop.Config;

public static class ShopBootstrapper
{
    public static void RegisterShopDependencies(this IServiceCollection services, string connectionString)
    {
        ApplicationBootstrapper.RegisterDependencies(services);
        InfrastructureBootstrapper.RegisterDependencies(services, connectionString);
        FacadeBootstrapper.RegisterDependencies(services);

        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommand).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
        services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
        services.AddMediatR(typeof(GetCategoryListQuery).Assembly);
    }
}