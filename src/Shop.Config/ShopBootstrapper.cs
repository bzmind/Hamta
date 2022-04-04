using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Category_Use_Cases._Services;
using Shop.Application.Category_Use_Cases.Use_Cases.Create;
using Shop.Domain.Category_Aggregate.Services;

namespace Shop.Config
{
    public static class ShopBootstrapper
    {
        public static void RegisterShopDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateCategoryCommand).Assembly);
        }
    }
}