using AutoMapper;
using Shop.API.ViewModels.Comments;
using Shop.API.ViewModels.Orders;
using Shop.API.ViewModels.Questions;
using Shop.API.ViewModels.Users;
using Shop.Application.Comments.Create;
using Shop.Application.Orders.AddItem;
using Shop.Application.Orders.Checkout;
using Shop.Application.Orders.IncreaseOrderItemCount;
using Shop.Application.Questions.AddReply;
using Shop.Application.Questions.Create;
using Shop.Application.Users.CreateAddress;
using Shop.Application.Users.EditAddress;

namespace Shop.API.SetupClasses;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddOrderItemCommand, AddOrderItemCommandViewModel>().ReverseMap();
        CreateMap<AddReplyCommand, AddReplyCommandViewModel>().ReverseMap();
        CreateMap<CheckoutOrderCommand, CheckoutOrderCommandViewModel>().ReverseMap();
        CreateMap<CreateCommentCommand, CreateCommentCommandViewModel>().ReverseMap();
        CreateMap<CreateQuestionCommand, CreateQuestionCommandViewModel>().ReverseMap();
        CreateMap<CreateUserAddressCommand, CreateUserAddressCommandViewModel>().ReverseMap();
        CreateMap<EditUserAddressCommand, EditUserAddressCommandViewModel>().ReverseMap();
        CreateMap<IncreaseOrderItemCountCommand, IncreaseOrderItemCountCommandViewModel>().ReverseMap();
    }
}