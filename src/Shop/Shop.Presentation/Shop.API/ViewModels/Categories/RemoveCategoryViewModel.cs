using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Categories;

public class RemoveCategoryViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long CategoryId { get; set; }
}