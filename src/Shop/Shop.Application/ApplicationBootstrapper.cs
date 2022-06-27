using Common.Application.Utility.FileUtility;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories._Services;
using Shop.Application.Products._Services;
using Shop.Application.Users._Services;
using Shop.Domain.CategoryAggregate.Services;
using Shop.Domain.ProductAggregate.Services;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Application;

public class ApplicationBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<IProductDomainService, ProductDomainService>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<IFileService, FileService>();
    }
}