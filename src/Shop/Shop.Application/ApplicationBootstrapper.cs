using Common.Application.FileUtility;
using Common.Application.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories._Services;
using Shop.Application.Categories.Create;
using Shop.Application.Products._Services;
using Shop.Domain.CategoryAggregate.Services;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application;

public class ApplicationBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<IProductDomainService, ProductDomainService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

        services.AddMediatR(typeof(CreateCategoryCommand).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateCategoryCommand).Assembly);
    }
}