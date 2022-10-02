using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using static System.Double;

namespace Shop.API.ViewModels.Orders;

public class AddOrderItemViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseSellerInventory)]
    public long InventoryId { get; set; }

    [DisplayName("تعداد سفارش")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    [Range(1, PositiveInfinity, ErrorMessage = "{0} باید بیشتر از 0 باشد")]
    public int Quantity { get; set; }
}