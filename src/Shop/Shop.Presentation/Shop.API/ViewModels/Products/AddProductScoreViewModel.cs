using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Products;

public class AddProductScoreViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long ProductId { get; set; }

    [DisplayName("امتیاز")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(1, ErrorMessage = "{0} باید بیشتر از 0 باشد")]
    [MaxLength(5, ErrorMessage = "{0} نمی‌تواند بیشتر از 5 باشد")]
    public int Score { get; set; }
}