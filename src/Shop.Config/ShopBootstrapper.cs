using Common.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories.Services;
using Shop.Application.Categories.UseCases.Create;
using Shop.Domain.CategoryAggregate.Services;

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