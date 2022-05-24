﻿using Common.Application.FileUtility;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Categories._Services;
using Shop.Application.Customers._Services;
using Shop.Application.Products._Services;
using Shop.Domain.CategoryAggregate.Services;
using Shop.Domain.CustomerAggregate.Services;
using Shop.Domain.ProductAggregate.Services;

namespace Shop.Application;

public class ApplicationBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<IProductDomainService, ProductDomainService>();
        services.AddTransient<ICustomerDomainService, CustomerDomainService>();
        services.AddTransient<IFileService, FileService>();
    }
}