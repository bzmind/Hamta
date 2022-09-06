using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Sellers.Inventories;

public class DecreaseSellerInventoryQuantityViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseSellerInventory)]
    public long InventoryId { get; set; }

    [DisplayName("تعداد")]
    [Required(ErrorMessage = ValidationMessages.QuantityRequired)]
    public int Quantity { get; set; }
}