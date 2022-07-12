using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Inventories;

public class RemoveInventoryViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long InventoryId { get; set; }
}