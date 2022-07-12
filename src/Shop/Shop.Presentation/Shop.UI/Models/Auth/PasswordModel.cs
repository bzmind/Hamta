using Common.Application.Utility.Validation;
using System.ComponentModel.DataAnnotations;

namespace Shop.UI.Models.Auth;

public class PasswordModel
{
    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [MinLength(8, ErrorMessage = "{0} باید حداقل 8 کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}