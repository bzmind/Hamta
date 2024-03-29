﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.AvatarAggregate.Repository;
using Shop.Domain.CategoryAggregate.Repository;
using Shop.Domain.ColorAggregate.Repository;
using Shop.Domain.CommentAggregate.Repository;
using Shop.Domain.Entities.Repositories;
using Shop.Domain.OrderAggregate.Repository;
using Shop.Domain.ProductAggregate.Repository;
using Shop.Domain.QuestionAggregate.Repository;
using Shop.Domain.RoleAggregate.Repository;
using Shop.Domain.SellerAggregate.Repository;
using Shop.Domain.ShippingAggregate.Repository;
using Shop.Domain.UserAggregate.Repository;
using Shop.Infrastructure.Persistence.EF;
using Shop.Infrastructure.Persistence.EF.Avatars;
using Shop.Infrastructure.Persistence.EF.Categories;
using Shop.Infrastructure.Persistence.EF.Colors;
using Shop.Infrastructure.Persistence.EF.Comments;
using Shop.Infrastructure.Persistence.EF.Entities.Repositories;
using Shop.Infrastructure.Persistence.EF.Orders;
using Shop.Infrastructure.Persistence.EF.Products;
using Shop.Infrastructure.Persistence.EF.Questions;
using Shop.Infrastructure.Persistence.EF.Roles;
using Shop.Infrastructure.Persistence.EF.Sellers;
using Shop.Infrastructure.Persistence.EF.Shippings;
using Shop.Infrastructure.Persistence.EF.Users;
using Shop.Infrastructure.Utility.MediatR;

namespace Shop.Infrastructure;

public class InfrastructureBootstrapper
{
    public static void RegisterDependencies(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IAvatarRepository, AvatarRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IColorRepository, ColorRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISellerRepository, SellerRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IQuestionRepository, QuestionRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IShippingRepository, ShippingRepository>();
        services.AddTransient<IBannerRepository, BannerRepository>();
        services.AddTransient<ISliderRepository, SliderRepository>();
        services.AddTransient(_ => new DapperContext(connectionString));
        services.AddSingleton<ICustomEventPublisher, CustomEventPublisher>();
        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}