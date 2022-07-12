using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Products;

public class RemoveProductViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; init; }
}