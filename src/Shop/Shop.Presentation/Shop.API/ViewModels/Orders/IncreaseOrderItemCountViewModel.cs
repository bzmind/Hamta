using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Orders;

public class IncreaseOrderItemCountViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long InventoryId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long OrderItemId { get; set; }
}