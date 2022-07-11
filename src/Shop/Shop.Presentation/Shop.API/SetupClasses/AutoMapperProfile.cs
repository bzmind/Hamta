using AutoMapper;
using Shop.API.ViewModels.Comments;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Users.Addresses;
using Shop.Application.Comments.Create;
using Shop.Application.Orders.IncreaseItemCount;
using Shop.Application.Users.Addresses.CreateAddress;
using Shop.Application.Users.Addresses.EditAddress;

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