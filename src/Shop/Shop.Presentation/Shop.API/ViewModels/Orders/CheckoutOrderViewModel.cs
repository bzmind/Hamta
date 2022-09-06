using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Orders;

public class CheckoutOrderViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseUserAddress)]
    public long UserAddressId { get; set; }

    [Required(ErrorMessage = ValidationMessages.ChooseShipping)]
    public long ShippingMethodId { get; set; }
}