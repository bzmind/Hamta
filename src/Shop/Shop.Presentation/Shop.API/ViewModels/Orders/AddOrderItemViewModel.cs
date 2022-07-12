using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Orders;

public class AddOrderItemViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long InventoryId { get; init; }

    [DisplayName("تعداد سفارش")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    [MinLength(1, ErrorMessage = "{0} باید بیشتر از 0 باشد")]
    public int Quantity { get; init; }
}