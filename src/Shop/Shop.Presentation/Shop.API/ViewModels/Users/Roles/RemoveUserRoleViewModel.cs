using System.ComponentModel.DataAnnotations;
using Common.Application.Utility.Validation;

namespace Shop.API.ViewModels.Users.Roles;

public class RemoveUserRoleViewModel
{
    [Required(ErrorMessage = ValidationMessages.ChooseUser)]
    public long UserId { get; set; }

    [Required(ErrorMessage = ValidationMessages.ChooseRole)]
    public long RoleId { get; set; }
}