using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Api.Attributes;
using Common.Application.Utility.Validation;
using Shop.Domain.OrderAggregate;

namespace Shop.API.ViewModels.Orders;

public class SetOrderStatusViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseUser)]
    public long UserId { get; set; }

    [DisplayName("وضعیت سفارش")]
    [Required(ErrorMessage = ValidationMessages.OrderStatusRequired)]
    public Order.OrderStatus Status { get; set; }
}