using AutoMapper;
using Shop.API.ViewModels.Comments;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Users;
using Shop.Application.Comments.Create;
using Shop.Application.Orders.IncreaseOrderItemCount;
using Shop.Application.Users.CreateAddress;
using Shop.Application.Users.EditAddress;

namespace Shop.API.SetupClasses;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCommentCommand, CreateCommentCommandViewModel>().ReverseMap();
        CreateMap<CreateUserAddressCommand, CreateUserAddressCommandViewModel>().ReverseMap();
        CreateMap<EditUserAddressCommand, EditUserAddressCommandViewModel>().ReverseMap();
        CreateMap<IncreaseOrderItemCountCommand, IncreaseOrderItemCountCommandViewModel>().ReverseMap();
    }
}