﻿using AutoMapper;
using Shop.API.ViewModels.Auth;
using Shop.API.ViewModels.Avatars;
using Shop.API.ViewModels.Categories;
using Shop.API.ViewModels.Colors;
using Shop.API.ViewModels.Comments;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Products;
using Shop.API.ViewModels.Questions;
using Shop.API.ViewModels.Roles;
using Shop.API.ViewModels.Sellers;
using Shop.API.ViewModels.Sellers.Inventories;
using Shop.API.ViewModels.Shippings;
using Shop.API.ViewModels.Users;
using Shop.API.ViewModels.Users.Addresses;
using Shop.API.ViewModels.Users.Roles;
using Shop.Application.Avatars.Create;
using Shop.Application.Categories._DTOs;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.SetStatus;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.SetStatus;
using Shop.Application.Products._DTOs;
using Shop.Application.Products.AddScore;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Questions.AddReply;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.Inventories.Add;
using Shop.Application.Sellers.Inventories.DecreaseQuantity;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Application.Sellers.Inventories.IncreaseQuantity;
using Shop.Application.Sellers.SetStatus;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;
using Shop.Application.Users.Addresses.CreateAddress;
using Shop.Application.Users.Addresses.EditAddress;
using Shop.Application.Users.Auth.Register;
using Shop.Application.Users.Auth.ResetPassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Roles.AddRole;
using Shop.Application.Users.Roles.RemoveRole;
using Shop.Query.Categories._DTOs;
using Shop.Query.Products._DTOs;
using Shop.Query.Roles._DTOs;
using Shop.Query.Users._DTOs;

namespace Shop.API.Setup;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterUserCommand, RegisterUserViewModel>().ReverseMap();
        CreateMap<CreateAvatarCommand, CreateAvatarViewModel>().ReverseMap();
        CreateMap<AddSubCategoryCommand, AddSubCategoryViewModel>().ReverseMap();
        CreateMap<CreateCategoryCommand, CreateCategoryViewModel>().ReverseMap();
        CreateMap<EditCategoryCommand, EditCategoryViewModel>().ReverseMap();
        CreateMap<CreateColorCommand, CreateColorViewModel>().ReverseMap();
        CreateMap<EditColorCommand, EditColorViewModel>().ReverseMap();
        CreateMap<CreateCommentCommand, CreateCommentViewModel>().ReverseMap();
        CreateMap<SetCommentStatusCommand, SetCommentStatusViewModel>().ReverseMap();
        CreateMap<CreateSellerCommand, CreateSellerViewModel>().ReverseMap();
        CreateMap<EditSellerCommand, EditSellerViewModel>().ReverseMap();
        CreateMap<SetSellerStatusCommand, SetSellerStatusViewModel>().ReverseMap();
        CreateMap<AddSellerInventoryCommand, AddSellerInventoryViewModel>().ReverseMap();
        CreateMap<EditSellerInventoryCommand, EditSellerInventoryViewModel>().ReverseMap();
        CreateMap<IncreaseSellerInventoryQuantityCommand, IncreaseSellerInventoryQuantityViewModel>().ReverseMap();
        CreateMap<DecreaseSellerInventoryQuantityCommand, DecreaseSellerInventoryQuantityViewModel>().ReverseMap();
        CreateMap<AddOrderItemCommand, AddOrderItemViewModel>().ReverseMap();
        CreateMap<CheckoutOrderCommand, CheckoutOrderViewModel>().ReverseMap();
        CreateMap<SetOrderStatusCommand, SetOrderStatusViewModel>().ReverseMap();
        CreateMap<AddProductScoreCommand, AddProductScoreViewModel>().ReverseMap();
        CreateMap<CreateProductCommand, CreateProductViewModel>().ReverseMap();
        CreateMap<CreateProductCommand, ControllerCreateProductViewModel>().ReverseMap();
        CreateMap<EditProductCommand, EditProductViewModel>().ReverseMap();
        CreateMap<EditProductCommand, ControllerEditProductViewModel>().ReverseMap();
        CreateMap<AddReplyCommand, AddReplyViewModel>().ReverseMap();
        CreateMap<CreateQuestionCommand, CreateQuestionViewModel>().ReverseMap();
        CreateMap<RemoveReplyCommand, RemoveReplyViewModel>().ReverseMap();
        CreateMap<SetQuestionStatusCommand, SetQuestionStatusViewModel>().ReverseMap();
        CreateMap<EditRoleCommand, EditRoleViewModel>().ReverseMap();
        CreateMap<CreateRoleCommand, CreateRoleViewModel>().ReverseMap();
        CreateMap<CreateShippingCommand, CreateShippingViewModel>().ReverseMap();
        CreateMap<EditShippingCommand, EditShippingViewModel>().ReverseMap();
        CreateMap<CreateUserAddressCommand, CreateUserAddressViewModel>().ReverseMap();
        CreateMap<EditUserAddressCommand, EditUserAddressViewModel>().ReverseMap();
        CreateMap<ResetUserPasswordCommand, ResetUserPasswordViewModel>().ReverseMap();
        CreateMap<AddUserRoleCommand, AddUserRoleViewModel>().ReverseMap();
        CreateMap<RemoveUserRoleCommand, RemoveUserRoleViewModel>().ReverseMap();
        CreateMap<CreateUserCommand, CreateUserViewModel>().ReverseMap();
        CreateMap<EditUserCommand, EditUserViewModel>().ReverseMap();
        CreateMap<CategorySpecificationDto, CategorySpecificationViewModel>().ReverseMap();
        CreateMap<QueryCategorySpecificationDto, CategorySpecificationViewModel>().ReverseMap();
        CreateMap<ProductSpecificationDto, ProductSpecificationViewModel>().ReverseMap();
        CreateMap<ProductCategorySpecificationDto, ProductCategorySpecificationViewModel>().ReverseMap();
        CreateMap<QueryProductSpecificationDto, ProductSpecificationViewModel>().ReverseMap();
        CreateMap<UserAddressDto, EditUserAddressViewModel>().ReverseMap();
        CreateMap<RoleDto, EditRoleViewModel>().ReverseMap();
        CreateMap<CategoryDto, EditCategoryViewModel>().ReverseMap()
            .ForMember(dto => dto.SubCategories, options => options.Ignore());
    }
}