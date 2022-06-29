using AutoMapper;
using Shop.API.ViewModels.Comments;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Users.Addresses;
using Shop.Application.Comments.Create;
using Shop.Application.Orders.IncreaseOrderItemCount;
using Shop.Application.Users.CreateAddress;
using Shop.Application.Users.EditAddress;

namespace Shop.API.SetupClasses;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCommentCommand, CreateCommentViewModel>().ReverseMap();
        CreateMap<CreateUserAddressCommand, CreateUserAddressViewModel>().ReverseMap();
        CreateMap<EditUserAddressCommand, EditUserAddressViewModel>().ReverseMap();
        CreateMap<IncreaseOrderItemCountCommand, IncreaseOrderItemCountViewModel>().ReverseMap();
    }
}