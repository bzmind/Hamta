using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Orders;

public class CheckoutOrderViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long UserAddressId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ShippingMethodId { get; set; }
}