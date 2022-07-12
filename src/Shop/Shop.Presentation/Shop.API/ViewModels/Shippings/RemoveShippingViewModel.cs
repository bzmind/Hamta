using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Shippings;

public class RemoveShippingViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ShippingId { get; set; }
}