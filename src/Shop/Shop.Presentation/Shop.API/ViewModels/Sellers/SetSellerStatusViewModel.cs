using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Shop.Domain.SellerAggregate;

namespace Shop.API.ViewModels.Sellers;

public class SetSellerStatusViewModel
{
    [DisplayName("وضعیت")]
    [Required(ErrorMessage = ValidationMessages.ChooseStatusRequired)]
    public Seller.SellerStatus Status { get; set; }
}