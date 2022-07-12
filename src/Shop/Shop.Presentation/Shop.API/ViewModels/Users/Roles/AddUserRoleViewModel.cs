using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.Roles;

public class AddUserRoleViewModel
{
    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long UserId { get; set; }

    [Required(ErrorMessage = ValidationMessages.IdRequired)]
    public long RoleId { get; set; }
}