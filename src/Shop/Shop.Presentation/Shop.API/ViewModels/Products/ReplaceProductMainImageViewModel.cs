using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;
using Common.Application.Utility.Validation.CustomAttributes;

namespace Shop.API.ViewModels.Products;

public class ReplaceProductMainImageViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; set; }

    [DisplayName("عکس اصلی محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد")]
    [ImageFile(ErrorMessage = "{0} نامعتبر است")]
    public IFormFile MainImage { get; set; }
}